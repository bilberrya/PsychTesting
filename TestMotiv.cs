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
    public partial class TestMotiv : Form
    {
        string ConnStr = @"Data Source=desktop-jfut083;Initial Catalog=PsychTesting;Integrated Security=True";
        int t = 0, sum = 0;
        string [] ques = new string[41];
        public TestMotiv()
        {
            InitializeComponent();
            mas(ques);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (t == 0)
            {
                btnStart.Text = "Следущий вопрос";
                label7.Visible = false;
                t++; 
                label2.Text = ques[0];
                label2.Visible = true;
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                label6.Text = "Вопрос " + t + ":";
                label6.Visible = true;
            }
            else if ((t >= 1) && (t < 40))
                Testing();
            else if (t == 40)
            {
                btnStart.Text = "Завершить тест";
                Testing();
            }
            else if (t == 41)
            {
                if ((radioButton1.Checked) || (radioButton2.Checked))
                {
                    label3.Visible = false;
                    Counting();
                    t++;
                    Finish();
                }
                else label3.Visible = true;
            }
            else Process.Start(new ProcessStartInfo("Описание результатов теста на мотивацию.docx") { UseShellExecute = true });
        }

        private void Counting()
        {
            switch(t)
            {
                case 2 or 3 or 4 or 5 or 7 or 8 or 9 or 10 or 14 or 15 or 16 or 17 or 21 or 22 or 25 or 26 or 27 or 28 or 29 or 30 or 32 or 37 or 41:
                    if (radioButton1.Checked) sum++;
                    break;
                case 6 or 13 or 18 or 20 or 24 or 31 or 36 or 38 or 39:
                    if (radioButton2.Checked) sum++;
                    break;
            }
            
        }

        private void Testing()
        {
            if ((radioButton1.Checked) || (radioButton2.Checked))
            {
                label3.Visible = false;
                Counting();
                t++;
            }
            else label3.Visible = true;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            label2.Text = ques[t - 1];
            label6.Text = "Вопрос " + t + ":";
        }

        private void mas (string[] ques)
        {
            SqlCommand cmd;
            SqlConnection cn = new SqlConnection(ConnStr);
            cn.Open();
            cmd = cn.CreateCommand();
            for (int i = 0; i < 41; i++)
            {
                cmd.CommandText = "select Question from MotiveTest where id_q = \'" + (i + 401) + "\'";
                ques[i] = Convert.ToString(cmd.ExecuteScalar());
            }
            cn.Close();
        }

        private void Finish()
        {
            label7.Visible = false;
            label2.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            label6.Visible = false;
            btnStart.Text = "Подробное описание";
            t++;
            string str = "Ваш показатель равен " + sum;
            if (sum < 11) str += " (низкая мотивация).";
            else if (sum < 17) str += " (средняя мотивация).";
            else if (sum < 21) str += " (умеренно высокая мотивация).";
            else str = " (слишком высокая мотивация).";
            label5.Text = "От 1 до 10 баллов — низкая мотивация к успеху; \nот 11 до 16 баллов — средний уровень мотивации; \nот 17 до 20 баллов — умеренно высокий уровень мотивации; \nболее 21 балла — слишком высокий уровень мотивации к успеху.\n\n\n" + str;
            label4.Visible = true;
            label5.Visible = true;
            SqlCommand cmd;
            SqlConnection cn = new SqlConnection(ConnStr);
            cn.Open();
            cmd = cn.CreateCommand();
            cmd.CommandText = "select count(*) from Results where id_w = \'" + Program.id + "\' and test = \'Определение мотивации\'";
            int c = Convert.ToInt32(cmd.ExecuteScalar());
            if (c == 0)
                cmd.CommandText = "insert into Results (id_w,test,result,date) values (\'" + Program.id + "\', \'Определение мотивации\', \'" + str + "\', GetDate())";
            else
            {
                cmd.CommandText = "select id_r from Results where id_w = \'" + Program.id + "\' and test = \'Определение мотивации\'";
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.CommandText = "update Results set result = \'" + str + "\', date = GetDate() where id_r = " + i;
            }
            cmd.ExecuteNonQuery();
            cn.Close();
        }
    }
}