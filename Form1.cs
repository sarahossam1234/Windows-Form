using Core.Framework.DataBase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FirstProject
{
    public partial class Form1 : Form
    {
        private SqlDatabaseManager mgrSQL;

        public Form1()
        {
            InitializeComponent();
            mgrSQL = new SqlDatabaseManager(".", "TESTDB", "sa", "password");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mgrSQL.ExecuteSelectQuery("Select * from testTable", new List<SqlParameter>());
        }
    }
}
