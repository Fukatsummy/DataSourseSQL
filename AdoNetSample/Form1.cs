using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Diagnostics;
using System.Data.Common;
using System.Threading;
using System.Net.Http;
using System.Net;
using Microsoft.IdentityModel.Protocols;

namespace AdoNetSample3
{
    public partial class Form1 : Form
    {
        private SqlConnection conn = null;
        SqlDataAdapter da = null;
        DataSet set = null;
        SqlCommandBuilder cmd = null;
        string cs = "";

        public object InputFileStream { get; private set; }

        public Form1()
        {
            InitializeComponent();
            conn = new SqlConnection();
            cs = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;
            conn.ConnectionString = cs;
        }

        private void show_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(cs);
                set = new DataSet();
                string sql = tbRequest.Text;
                da = new SqlDataAdapter(sql, conn);
                dataGridView1.DataSource = null;

                cmd = new SqlCommandBuilder(da);
                da.Fill(set, "mybook");

                dataGridView1.DataSource = set.Tables["mybook"];
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            da.Update(set, "mybook");
        }
    }
}