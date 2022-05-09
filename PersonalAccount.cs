using System;
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
    public partial class PersonalAccount : Form
    {
        string ConnStr = @"Data Source=desktop-jfut083;Initial Catalog=PsychTesting;Integrated Security=True";
        public PersonalAccount()
        {
            InitializeComponent();
            ShowRes();
        }

        private void ShowRes()
        {
            string[] name = new string[4];
            string[] res = new string[4];
            string[] date = new string[4];

            SqlCommand cmd;
            SqlConnection cn = new SqlConnection(ConnStr);
            cn.Open();
            cmd = cn.CreateCommand();
            cmd.CommandText = "select count(*) from Results where id_w = \'" + Program.id + "\'";
            int c = Convert.ToInt32(cmd.ExecuteScalar());

            cmd.CommandText = "select test from Results where id_w = \'" + Program.id + "\'";
            using (IDataReader reader = cmd.ExecuteReader())
            {
                int i = 0;
                while (reader.Read())
                {
                    name[i] = reader.GetString(0);
                    i++;
                }
            }

            cmd.CommandText = "select result from Results where id_w = \'" + Program.id + "\'";
            using (IDataReader reader = cmd.ExecuteReader())
            {
                int i = 0;
                while (reader.Read())
                {
                    res[i] = reader.GetString(0);
                    i++;
                }
            }

            cmd.CommandText = "select date from Results where id_w = \'" + Program.id + "\'";
            using (IDataReader reader = cmd.ExecuteReader())
            {
                int i = 0;
                while (reader.Read())
                {
                    date[i] = Convert.ToString(reader["date"]);
                    i++;
                }
            }

            switch (c)
            {
                case 0:
                    label13.Visible = true;
                    panel1.Visible = false;
                    panel2.Visible = false;
                    panel3.Visible = false;
                    panel4.Visible = false;
                    break;
                case 1:
                    label13.Visible = false;
                    panel1.Visible = true;
                    panel2.Visible = false;
                    panel3.Visible = false;
                    panel4.Visible = false;

                    label1.Text = name[0];
                    label2.Text = res[0];
                    label3.Text = date[0];
                    break;
                case 2:
                    label13.Visible = false;
                    panel1.Visible = true;
                    panel2.Visible = true;
                    panel3.Visible = false;
                    panel4.Visible = false;

                    label1.Text = name[0];
                    label2.Text = res[0];
                    label3.Text = date[0];
                    label6.Text = name[1];
                    label5.Text = res[1];
                    label4.Text = date[1];
                    break;
                case 3:
                    label13.Visible = false;
                    panel1.Visible = true;
                    panel2.Visible = true;
                    panel3.Visible = true;
                    panel4.Visible = false;

                    label1.Text = name[0];
                    label2.Text = res[0];
                    label3.Text = date[0];
                    label6.Text = name[1];
                    label5.Text = res[1];
                    label4.Text = date[1];
                    label9.Text = name[2];
                    label8.Text = res[2];
                    label7.Text = date[2];
                    break;
                case 4:
                    label13.Visible = false;
                    panel1.Visible = true;
                    panel2.Visible = true;
                    panel3.Visible = true;
                    panel4.Visible = true;

                    label1.Text = name[0];
                    label2.Text = res[0];
                    label3.Text = date[0];
                    label6.Text = name[1];
                    label5.Text = res[1];
                    label4.Text = date[1];
                    label9.Text = name[2];
                    label8.Text = res[2];
                    label7.Text = date[2];
                    label12.Text = name[3];
                    label11.Text = res[3];
                    label10.Text = date[3];
                    break;
            }
        }
    }
}
