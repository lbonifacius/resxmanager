using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceManager.Core;
using System.Globalization;
using System.Configuration;
using System.Data.EntityClient;
using System.Data.Common;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace ResourceManager.Storage
{
    public class TranslationStorageManager
    {
        public static string getConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["TranslationStorage"].ConnectionString;
        }
        public static EntityConnectionStringBuilder GetConnectionSetting()
        {
            return new EntityConnectionStringBuilder(getConnectionString());
        }

        private static bool databaseChecked = false;
        public static bool CheckDatabase()
        {
            if (databaseChecked == true)
                return true;
            else
            {
                try
                {
                    using (var connection = new SqlConnection(GetConnectionSetting().ProviderConnectionString))
                    {
                        connection.Open();
                    }
                    databaseChecked = true;
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
        public bool DatabaseExists()
        {
            using (TranslationStorage context = new TranslationStorage(getConnectionString()))
            {
                return context.DatabaseExists();
            }
        }
        public void CreateDatabase()
        {
            using (TranslationStorage context = new TranslationStorage(getConnectionString()))
            {
                if (!context.DatabaseExists())
                    context.CreateDatabase();                               
            }
            using (var connection = new SqlConnection(GetConnectionSetting().ProviderConnectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                ExecuteSQLFile(transaction, "ResourceManager.Storage.Sql.StoredProcedures1.sql");

                transaction.Commit();
            } 
        }

        private static void ExecuteSQLFile(SqlTransaction transaction, string manifestName)
        {
            string sql;

            using (Stream strm = Assembly.GetExecutingAssembly().GetManifestResourceStream(manifestName))
            {
                var reader = new StreamReader(strm);
                sql = reader.ReadToEnd();
            }

            var regex = new Regex("^GO", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            string[] lines = regex.Split(sql);

            foreach (string line in lines)
            {
                if (!String.IsNullOrEmpty(line))
                {
                    var command = transaction.Connection.CreateCommand();
                    command.CommandText = line;
                    command.Transaction = transaction;
                    command.ExecuteNonQuery();
                }
            }
        }
        public IEnumerable<TranslationText> Search(ResourceDataBase source, CultureInfo targetCulture)
        {
            using (TranslationStorage context = new TranslationStorage(getConnectionString()))
            {
                return context.SearchTranslation(source.Value, source.ResxFile.Culture.Name, targetCulture.Name)
                    .ToList();
            }
        }

        public void Store(VSSolution solution)
        {
            using (TranslationStorage store = new TranslationStorage(getConnectionString()))
            {
                foreach (var project in solution.Projects.Values)
                {
                    foreach (var fileGroup in project.ResxGroups.Values)
                    {
                        foreach (var dataGroup in fileGroup.AllData.Values)
                        {
                            var existingTranslationTexts = new List<TranslationText>();
                            foreach (var data in dataGroup.ResxData.Values)
                            {
                                existingTranslationTexts.AddRange(store.FindTranslations(data.Value, data.ResxFile.Culture.Name));
                            }

                            if (existingTranslationTexts.Count == 0)
                            {
                                var t = new Translation();

                                foreach (var data in dataGroup.ResxData.Values)
                                {
                                    var text = new TranslationText();
                                    text.Text = data.Value;
                                    text.Culture = GetCulture(data.ResxFile.Culture, store);
                                    text.CreatedDate = DateTime.Now;
                                    text.ModifiedDate = DateTime.Now;
                                    t.Translations.Add(text);
                                }

                                store.Translations.AddObject(t);
                                store.SaveChanges();
                            }
                            else
                            {
                                var list = from transText in existingTranslationTexts
                                           join data in dataGroup.ResxData.Values
                                           on transText.Text.ToLowerInvariant() equals data.Value.ToLowerInvariant()
                                           group transText by transText.Translation into translation
                                           select new { Translation = translation.Key, Count = translation.Count() };

                                var topCandidate = list.OrderByDescending(t => t.Count)
                                    .FirstOrDefault();

                                if (topCandidate == null)
                                    throw new Exception("TODO: Error");

                                var notExistingCultures = (from c in dataGroup.ResxData.Keys select c)
                                    .Except(from cn in topCandidate.Translation.Translations select CultureInfo.GetCultureInfo(cn.Culture.CultureName));

                                foreach (var culture in notExistingCultures)
                                {
                                    var text = new TranslationText();
                                    text.Text = dataGroup.ResxData[culture].Value;
                                    text.Culture = GetCulture(culture, store);
                                    text.CreatedDate = DateTime.Now;
                                    text.ModifiedDate = DateTime.Now;

                                    topCandidate.Translation.Translations.Add(text);
                                }
                                store.SaveChanges();
                            }
                        }
                    }
                }
            }
        }
        private Culture GetCulture(CultureInfo culture, TranslationStorage storage)
        {
            return storage.GetCulture(culture.Name).First();
        }
    }
}
