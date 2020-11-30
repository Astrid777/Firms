using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;


namespace TestDb
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "test2511DataSet.City". При необходимости она может быть перемещена или удалена.
            this.cityTableAdapter.Fill(this.test2511DataSet.City);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "test2511DataSet.Firm". При необходимости она может быть перемещена или удалена.
            this.firmTableAdapter.Fill(this.test2511DataSet.Firm);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

            string firmFilter="";

            cityBindingSource.Filter = string.Format("Name Like '{0}'", comboBox1.Text);

            firmFilter = string.Format("Name Like '{0}'", comboBox2.Text);


            if (checkBox2.Checked)
                firmFilter = firmFilter + string.Format(" AND Post_city_id = '{0}'", comboBox1.SelectedValue.ToString());


            if (checkBox1.Checked)
                firmFilter = firmFilter + string.Format(" AND Jur_city_id = '{0}'", comboBox1.SelectedValue.ToString());

            firmBindingSource.Filter = firmFilter;

            MessageBox.Show("Выполнено");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["TestDb.Properties.Settings.Test2511ConnectionString"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);

            string sql =
            "select year(DOCUMENT.DOC_DATE) as Год," +
            "sum(IIF(month(DOCUMENT.DOC_DATE) = 1, DOCUMENT.SUM, 0)) as Январь, " +
            "sum(IIF(month(DOCUMENT.DOC_DATE) = 2, DOCUMENT.SUM, 0)) as Февраль, " +
            "sum(IIF(month(DOCUMENT.DOC_DATE) = 3, DOCUMENT.SUM, 0)) as Март, " +
            "sum(IIF(month(DOCUMENT.DOC_DATE) = 4, DOCUMENT.SUM, 0)) as Апрель, " +
            "sum(IIF(month(DOCUMENT.DOC_DATE) = 5, DOCUMENT.SUM, 0)) as Май, " +
            "sum(IIF(month(DOCUMENT.DOC_DATE) = 6, DOCUMENT.SUM, 0)) as Июнь, " +
            "sum(IIF(month(DOCUMENT.DOC_DATE) = 7, DOCUMENT.SUM, 0)) as Июль, " +
            "sum(IIF(month(DOCUMENT.DOC_DATE) = 8, DOCUMENT.SUM, 0)) as Август," +
            "sum(IIF(month(DOCUMENT.DOC_DATE) = 9, DOCUMENT.SUM, 0)) as Сентябрь," +
            "sum(IIF(month(DOCUMENT.DOC_DATE) = 10, DOCUMENT.SUM, 0)) as Октябрь, " +
            "sum(IIF(month(DOCUMENT.DOC_DATE) = 11, DOCUMENT.SUM, 0)) as Ноябрь, " +
            "sum(IIF(month(DOCUMENT.DOC_DATE) = 12, DOCUMENT.SUM, 0)) as Декабрь " +
            "from DOCUMENT, Firm " +
            "where DOCUMENT.firm_id =  Firm.Firm_id and Firm.Firm_id = " + comboBox3.SelectedValue.ToString() + " " +
            "group by year(DOCUMENT.DOC_DATE)";


            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(sql, connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            dataGridView3.DataSource = ds.Tables[0];
        }
    }
}
