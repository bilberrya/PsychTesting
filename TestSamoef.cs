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
    public partial class TestSamoef : Form
    {
        string ConnStr = @"Data Source=desktop-jfut083;Initial Catalog=PsychTesting;Integrated Security=True";
        int t = 0, samef = 0;
        string[] ques = new string[10];
        public TestSamoef()
        {
            InitializeComponent();
            mas(ques);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (t == 0)
            {
                btnStart.Text = "Следущий вопрос";
                label1.Visible = false;
                t++;
                label2.Text = ques[0];
                label2.Visible = true;
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                radioButton3.Visible = true;
                radioButton4.Visible = true;
                label6.Text = "Вопрос " + t + ":";
                label6.Visible = true;
            }
            else if ((t >= 1) && (t < 9))
                Testing();
            else if (t == 9)
            {
                btnStart.Text = "Завершить тест";
                Testing();
            }
            else if (t == 10)
            {
                if ((radioButton1.Checked) || (radioButton2.Checked) || (radioButton3.Checked) || (radioButton4.Checked))
                {
                    label3.Visible = false;
                    Counting();
                    t++;
                    Finish();
                }
                else label3.Visible = true;
            }
        }

        private void Counting()
        {
            if (radioButton1.Checked) samef += 1;
            else if (radioButton2.Checked) samef += 2;
            else if (radioButton3.Checked) samef += 3;
            else if (radioButton4.Checked) samef += 4;
        }

        private void Testing()
        {
            if ((radioButton1.Checked) || (radioButton2.Checked) || (radioButton3.Checked) || (radioButton4.Checked))
            {
                label3.Visible = false;
                Counting();
                t++;
            }
            else label3.Visible = true;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            label2.Text = ques[t - 1];
            label6.Text = "Вопрос " + t + ":";
        }

        private void mas (string[] ques)
        {
            SqlCommand cmd;
            SqlConnection cn = new SqlConnection(ConnStr);
            cn.Open();
            cmd = cn.CreateCommand();
            for (int i = 0; i < 10; i++)
            {
                cmd.CommandText = "select Question from SamoefTest where id_q = \'" + (i + 301) + "\'";
                ques[i] = Convert.ToString(cmd.ExecuteScalar());
            }
            cn.Close();
        }

        private void Finish()
        {
            label1.Visible = false;
            label2.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;
            radioButton4.Visible = false;
            label6.Visible = false;
            btnStart.Visible = false;
            t++;
            string str = "Ваш показатель равен " + samef + " (";
            if (samef < 27) str += "низкий уровень).";
            else if (samef < 36) str += "средний уровень).";
            else str += "высокий уровень).";
            label5.Text = "Показатели до 27 баллов свидетельствуют о низкой самоэффективности; 27-35 – показатели средней самоэффективности, более 35 высокой.\n\n\n" + str;
            label4.Visible = true;
            label5.Visible = true;
            SqlCommand cmd;
            SqlConnection cn = new SqlConnection(ConnStr);
            cn.Open();
            cmd = cn.CreateCommand();
            cmd.CommandText = "select count(*) from Results where id_w = \'" + Program.id + "\' and test = \'Диагностика самоэффективности\'";
            int c = Convert.ToInt32(cmd.ExecuteScalar());
            if (c == 0)
                cmd.CommandText = "insert into Results (id_w,test,result,date) values (\'" + Program.id + "\', \'Диагностика самоэффективности\', \'" + str + "\', GetDate())";
            else
            {
                cmd.CommandText = "select id_r from Results where id_w = \'" + Program.id + "\' and test = \'Диагностика самоэффективности\'";
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.CommandText = "update Results set result = \'" + str + "\', date = GetDate() where id_r = " + i;
            }
            cmd.ExecuteNonQuery();
            cn.Close();
        }
    }
}