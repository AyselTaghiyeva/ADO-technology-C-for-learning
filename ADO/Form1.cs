using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace ADO
{
    public partial class Form1 : Form
    {
        public static string connectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/User/Desktop/Database.mdb;";
        private OleDbConnection myConnection;
        ComboBox comboBox2 = new ComboBox(); //for Qrup column
        ComboBox comboBox3 = new ComboBox(); //for Soyad column
        ComboBox comboBox4 = new ComboBox(); //for ID column
        public string secilmishID;
        public string secilmishqrup;
        public string secilmishsoyad;

        public bool datagetirildi = false;
        public Form1()
        {
            InitializeComponent();
        }

        public void datagetir() // Data gətirən funksiya yaradırıq
        {

            //Qoşuntu yaradırıq, Access-dən Combobox 1,2,3,4ə uyğun olaraq - ad, soyad,qrup,id sütunlarını əlavə edirik.
            myConnection = new OleDbConnection(connectString);
            myConnection.Open();
            OleDbCommand cmd = new OleDbCommand("select * From Table1", myConnection);
            OleDbDataReader odr = null;
            odr = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(odr);
            comboBox1.DataSource = table;
            comboBox1.BindingContext = this.BindingContext;
            comboBox1.DisplayMember = "Table1";
            comboBox1.ValueMember = "AD";
            comboBox1.SelectedIndex = -1;


            comboBox2.DataSource = table;
            comboBox2.BindingContext = this.BindingContext;
            comboBox2.DisplayMember = "Table1";
            comboBox2.ValueMember = "Qrup";


            comboBox3.DataSource = table;
            comboBox3.BindingContext = this.BindingContext;
            comboBox3.DisplayMember = "Table1";
            comboBox3.ValueMember = "Soyad";

            comboBox4.DataSource = table;
            comboBox4.BindingContext = this.BindingContext;
            comboBox4.DisplayMember = "Table1";
            comboBox4.ValueMember = "ID";

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            datagetir(); // Form1 yüklənəndə datamızı gətirmək üçün yaratdığımız funksiyanı çağırırırıq 
            datagetirildi = true; // Datamız gələndən sonra comboBox1_SelectedValueChanged üçün yaratdığımız dəyişəni aktivləşdiririk

        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e) // AD seçildikdə
        {

            if (datagetirildi == true) // Datamız gətirilibsə

            {
                int secilmisadindeksi = comboBox1.SelectedIndex;


                secilmishqrup = comboBox2.GetItemText(comboBox2.Items[secilmisadindeksi]);
                secilmishsoyad = comboBox3.GetItemText(comboBox3.Items[secilmisadindeksi]);
                secilmishID = comboBox4.GetItemText(comboBox4.Items[secilmisadindeksi]);

                textBox3.Text = secilmishqrup;
                textBox2.Text = secilmishsoyad;


               
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:/Users/User/Desktop/Database.mdb;";
            string commandText = "INSERT INTO Table1 (Ad, Soyad, Qrup) VALUES (?, ?, ?)";

            using (OleDbConnection connection = new OleDbConnection(connectString))
            {
                OleDbCommand command = new OleDbCommand(commandText, connection);
                command.Parameters.AddWithValue("@Ad", textBox1.Text);
                command.Parameters.AddWithValue("@Soyad", textBox2.Text);
                command.Parameters.AddWithValue("@Qrup", textBox3.Text);
                MessageBox.Show("Məlumat daxil edildi");
                try
                {
                    command.Connection.Open();
                    int response = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: {0}" + ex.Message);
                }


            }
        }
    }
}
