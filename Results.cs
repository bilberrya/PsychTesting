using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PsychTesting
{
    public partial class Results : Form
    {
        string ConnStr = @"Data Source=desktop-jfut083;Initial Catalog=PsychTesting;Integrated Security=True";
        int c = 0, n = 0;
        public Results()
        {
            InitializeComponent();
            SqlCommand cmd;
            SqlConnection cn = new SqlConnection(ConnStr);
            cn.Open();
            cmd = cn.CreateCommand();
            cmd.CommandText = "select count(*) from Results";
            n = Convert.ToInt32(cmd.ExecuteScalar());
            dataGridView1.RowCount = n + 1;
            for (int i = 0; i < n; i++)
            {
                cmd.CommandText = "select id_r from Results where id_r = \'" + (i + 501) + "\'";
                dataGridView1["Column1", i].Value = Convert.ToString(cmd.ExecuteScalar());

                cmd.CommandText = "select id_w from Results where id_r = \'" + (i + 501) + "\'";
                c = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.CommandText = "select fio from Workers where id_w = \'" + c + "\'";
                dataGridView1["Column2", i].Value = Convert.ToString(cmd.ExecuteScalar());

                cmd.CommandText = "select test from Results where id_r = \'" + (i + 501) + "\'";
                dataGridView1["Column3", i].Value = Convert.ToString(cmd.ExecuteScalar());

                cmd.CommandText = "select result from Results where id_r = \'" + (i + 501) + "\'";
                dataGridView1["Column4", i].Value = Convert.ToString(cmd.ExecuteScalar());

                cmd.CommandText = "select date from Results where id_r = \'" + (i + 501) + "\'";
                dataGridView1["Column5", i].Value = Convert.ToString(cmd.ExecuteScalar());
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.Equals(""))
                {
                    for (int i = 0; i < n; i++)
                        dataGridView1.Rows[i].Visible = true;
                }
                else
                {
                    for (int i = 0; i < n; i++)
                        for (int j = 0; j < 5; j++)
                        {
                            if (dataGridView1.Rows[i].Cells[j].Value != null)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value.ToString().Contains(textBox1.Text))
                                {
                                    dataGridView1.Rows[i].Visible = true;
                                    break;
                                }
                                else dataGridView1.Rows[i].Visible = false;
                            }
                        }
                }
            }
        }
    }
}