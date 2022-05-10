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
    public partial class Autorization : Form
    {
        string ConnStr = @"Data Source=desktop-jfut083;Initial Catalog=PsychTesting;Integrated Security=True";
        public Autorization()
        {
            InitializeComponent();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Program.MainForm = null;
            Application.Exit();
        }

        public void btnAutorize_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string password = textBox2.Text;
            SqlCommand cmd;
            SqlConnection cn = new SqlConnection(ConnStr);
            cn.Open();
            cmd = cn.CreateCommand();
            cmd.CommandText = "select id_w from Workers where login = \'" + login + "\'";
            Program.id = Convert.ToInt32(cmd.ExecuteScalar());
            cmd.CommandText = "select fio from Workers where id_w = " + Program.id;
            Program.fio = Convert.ToString(cmd.ExecuteScalar());
            cmd.CommandText = "select password from Workers where id_w = " + Program.id;
            if (password.Equals((string)cmd.ExecuteScalar()))
            {
                Program.MainForm = new MainWindow();
                this.Close();
            }
            else
            {
                Msg m = new Msg();
                m.button1.Visible = false;
                m.btnExit.Text = "OK";
                m.label2.Text = "Такого пользователя не существует.\nПроверьте введённые данные.";
                m.ShowDialog();
                textBox1.Text = "";
                textBox2.Text = "";
            }
            cn.Close();
            textBox1.Text = "";
            textBox2.Text = "";
        }
    }
}
