using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private SqlDataReader reader;
        private DataTable table;
        private SqlConnection conn;
        private SqlCommand comm;

        string cs = "";

        public Form1()
        {
            InitializeComponent();
            conn = new SqlConnection();
            cs = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
            conn.ConnectionString = cs;
        }

        private void show_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                try
                {
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = "SELECT * FROM Authors"; //tbRequest.Text;
                    comm.Connection = conn;

                    dataGridView1.DataSource = null;

                    conn.Open();//открываем соединение

                    table = new DataTable();//базовый конструктор
                    reader = comm.ExecuteReader();
                    int line = 0;//количество строк

                    do
                    {
                        while (reader.Read())
                        {
                            if (line == 0)//нулевая строка, без данных
                            {
                                for (int i = 0; i < reader.FieldCount; i++)//создаем заголовки в таблице
                                {
                                    table.Columns.Add(reader.GetName(i));//в коллекции столбцов таблице добавляем колличество столбцов
                                }
                                line++;
                            }
                            DataRow row = table.NewRow();//обрабатываем строки(добавляем строки)
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[i] = reader[i];//поячеечно строку заполняем
                            }
                            table.Rows.Add(row);
                        }
                    } while (reader.NextResult());
                    dataGridView1.DataSource = table;
                }
                catch (Exception ex)//выводится при ошибке
                {
                    MessageBox.Show("Probably wrong request syntax");
                }
                finally
                {
                    // Close the connection
                    if (conn != null)
                    {
                        conn.Close();//закрываем
                    }
                    if (reader != null)
                    {
                        reader.Close();//закрываем ридер
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}