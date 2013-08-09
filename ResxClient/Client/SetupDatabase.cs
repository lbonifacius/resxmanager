using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ResourceManager.Storage;

namespace ResourceManager.Client
{
    public partial class SetupDatabase : Form
    {
        public SetupDatabase()
        {
            InitializeComponent();
        }

        private void SetupDatabase_Load(object sender, EventArgs e)
        {
            var connection = TranslationStorageManager.GetConnectionSetting();
            var sqlconnection = new SqlConnectionStringBuilder(connection.ProviderConnectionString);
            txtServer.Text = sqlconnection.DataSource;
            txtDatabase.Text = sqlconnection.InitialCatalog;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            this.btnTest.Enabled = false;

            try
            {
                var store = new TranslationStorageManager();
                
                if(store.DatabaseExists())
                    MessageBox.Show(Properties.Resources.TestConnectionSuccess);
                else
                    MessageBox.Show(Properties.Resources.DatabaseNotExists);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.btnTest.Enabled = true;
        }

        private void btnCreateDatabase_Click(object sender, EventArgs e)
        {
            this.btnCreateDatabase.Enabled = false;

            try
            {
                var store = new TranslationStorageManager();
                store.CreateDatabase();

                MessageBox.Show(Properties.Resources.CreatedDatabaseSuccess);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.btnCreateDatabase.Enabled = true;
        }
    }
}
