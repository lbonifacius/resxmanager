using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResourceManager.Core;
using System.Globalization;

namespace ResourceManager.Storage
{
    public class TranslationStorageManager
    {
        public IEnumerable<TranslationText> Search(ResourceDataBase source, CultureInfo targetCulture)
        {
            using (TranslationStorage context = new TranslationStorage())
            {
                return context.SearchTranslation(source.Value, source.ResxFile.Culture.Name, targetCulture.Name)
                    .ToList();
            }
        }

        public void Store(VSSolution solution)
        {
            using (TranslationStorage store = new TranslationStorage())
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
