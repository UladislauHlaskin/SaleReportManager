using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using SaleReport.Model;
using System.Globalization;

namespace SaleReportFiller
{
    public partial class ReportGenerator : Form
    {
        public ReportGenerator()
        {
            InitializeComponent();
        }

        void LoadManagers()
        {
            using (var context = new SaleReportModel())
            {
                var managers = context.Managers.OrderBy(m => m.Name).ToList();
                comboBoxManager.DataSource = managers;
                comboBoxManager.DisplayMember = "Name";
            }
        }

        private void ReportGenerator_Load(object sender, EventArgs e)
        {
            try
            {
                folderBrowserDialog.SelectedPath = ConfigurationManager.AppSettings.Get("SaleDirectoryPath");
                textBoxDestinationPath.Text = folderBrowserDialog.SelectedPath;
                LoadManagers();
                ActiveControl = buttonGenerateData;

                buttonGenerateData_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.ToString());
            }
        }

        private void buttonGenerateData_Click(object sender, EventArgs e)
        {
            int managerCount;
            List<Client> clients;
            List<Product> products;

            dataGridView.Rows.Clear();
            Random rng = new Random(DateTime.Now.Millisecond);
            dateTimePicker.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, rng.Next(1, DateTime.Now.Day + 1));
            
            using (var context = new SaleReportModel())
            {
                managerCount = context.Managers.Count();
                clients = context.Clients.OrderBy(c => c.Name).ToList();
                products = context.Products.OrderBy(c => c.Name).ToList();
            }

            comboBoxManager.SelectedIndex = rng.Next(0, managerCount);
            var records = rng.Next(5, 11);
            for (int i = 0; i < records; i++)
            {
                var client = clients[rng.Next(0, clients.Count)];
                var product = products[rng.Next(0, products.Count)];
                var quantity = rng.Next(1, 11);
                var price = quantity * product.Price;
                var date = new DateTime(dateTimePicker.Value.Year, dateTimePicker.Value.Month, rng.Next(1, dateTimePicker.Value.Day + 1));
                dataGridView.Rows.Add(date.ToString("dd.MM.yyyy"), client.Name, product.Name, price.ToString(CultureInfo.InvariantCulture));
            }

            if (checkBoxWrite.Checked)
            {
                buttonAddFile_Click(sender, e);
            }
        }

        private void buttonChangePath_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxDestinationPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void buttonAddFile_Click(object sender, EventArgs e)
        {
            FileReport file = new FileReport(folderBrowserDialog.SelectedPath, comboBoxManager.Text, dateTimePicker.Value);
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                file.AddRecord(new FileRecord(
                    Convert.ToDateTime(row.Cells["date"].Value), 
                    row.Cells["client"].Value.ToString(), 
                    row.Cells["product"].Value.ToString(), 
                    Convert.ToDecimal(row.Cells["sum"].Value)));
            }
            file.WriteResult();
            MessageBox.Show($"{file.FineName} is written.");
        }
    }
}
