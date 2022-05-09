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
    public partial class TestProfBurnout : Form
    {
        string ConnStr = @"Data Source=desktop-jfut083;Initial Catalog=PsychTesting;Integrated Security=True";
        int t = 0, emist = 0, deper = 0, redli = 0, sum = 0;
        string [] ques = new string[22];
        public TestProfBurnout()
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
                radioButton3.Visible = true;
                radioButton4.Visible = true;
                radioButton5.Visible = true;
                radioButton6.Visible = true;
                radioButton7.Visible = true;
                label6.Text = "Вопрос " + t + ":";
                label6.Visible = true;
            }
            else if ((t >= 1) && (t < 21))
                Testing();
            else if (t == 21)
            {
                btnStart.Text = "Завершить тест";
                Testing();
            }
            else if (t == 22)
            {
                if ((radioButton1.Checked) || (radioButton2.Checked) || (radioButton3.Checked) || (radioButton4.Checked) || (radioButton5.Checked) || (radioButton6.Checked) || (radioButton7.Checked))
                {
                    if (radioButton1.Checked) sum = 0;
                    else if (radioButton2.Checked) sum = 1;
                    else if (radioButton3.Checked) sum = 2;
                    else if (radioButton4.Checked) sum = 3;
                    else if (radioButton5.Checked) sum = 4;
                    else if (radioButton6.Checked) sum = 5;
                    else if (radioButton7.Checked) sum = 6;
                    label3.Visible = false;
                    Counting();
                    t++;
                    Finish();
                }
                else label3.Visible = true;
            }
            else Process.Start(new ProcessStartInfo("Описание результатов диагностики профессионального выгорания.docx") { UseShellExecute = true });
        }

        private void Counting()
        {
            switch(t)
            {
                case 1 or 2 or 3 or 6 or 8 or 13 or 14 or 16 or 20:
                    emist += sum;
                    break;
                case 5 or 10 or 11 or 15 or 22:
                    deper += sum;
                    break;
                case 4 or 7 or 9 or 12 or 17 or 18 or 19 or 21:
                    redli += sum;
                    break;
            }
            
        }

        private void Testing()
        {
            if ((radioButton1.Checked) || (radioButton2.Checked) || (radioButton3.Checked) || (radioButton4.Checked) || (radioButton5.Checked) || (radioButton6.Checked) || (radioButton7.Checked))
            {
                if (radioButton1.Checked) sum = 0;
                else if (radioButton2.Checked) sum = 1;
                else if (radioButton3.Checked) sum = 2;
                else if (radioButton4.Checked) sum = 3;
                else if (radioButton5.Checked) sum = 4;
                else if (radioButton6.Checked) sum = 5;
                else if (radioButton7.Checked) sum = 6;
                label3.Visible = false;
                Counting();
                t++;
            }
            else label3.Visible = true;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            radioButton6.Checked = false;
            radioButton7.Checked = false;
            label2.Text = ques[t - 1];
            label6.Text = "Вопрос " + t + ":";
        }

        private void mas (string[] ques)
        {
            SqlCommand cmd;
            SqlConnection cn = new SqlConnection(ConnStr);
            cn.Open();
            cmd = cn.CreateCommand();
            for (int i = 0; i < 22; i++)
            {
                cmd.CommandText = "select Question from ProfBurnoutTest where id_q = \'" + (i + 201) + "\'";
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
            radioButton3.Visible = false;
            radioButton4.Visible = false;
            radioButton5.Visible = false;
            radioButton6.Visible = false;
            radioButton7.Visible = false;
            label6.Visible = false;
            btnStart.Text = "Подробное описание";
            t++;
            string str = null;
            if (emist < 16) str = "Низкий";
            else if (emist < 25) str = "Средний";
            else str = "Высокий";
            label5.Text = "Эмоциональное истощение: " + str + " уровень (" + emist + " баллов)\n";
            if (deper < 6) str = "Низкий";
            else if (deper < 11) str = "Средний";
            else str = "Высокий";
            label5.Text += "Деперсонализация: " + str + " уровень(" + deper + " баллов)\n";
            if (redli < 31) str = "Высокий";
            else if (redli < 37) str = "Средний";
            else str = "Низкий";
            label5.Text += "Редукция личных достижений: " + str + " уровень(" + redli + " баллов)";
            label4.Visible = true;
            label5.Visible = true;
            SqlCommand cmd;
            SqlConnection cn = new SqlConnection(ConnStr);
            cn.Open();
            cmd = cn.CreateCommand();
            cmd.CommandText = "select count(*) from Results where id_w = \'" + Program.id + "\' and test = \'Диагностика профессионального выгорания\'";
            int c = Convert.ToInt32(cmd.ExecuteScalar());
            if (c == 0)
                cmd.CommandText = "insert into Results (id_w,test,result,date) values (\'" + Program.id + "\', \'Диагностика профессионального выгорания\', \'" + label5.Text + "\', GetDate())";
            else
            {
                cmd.CommandText = "select id_r from Results where id_w = \'" + Program.id + "\' and test = \'Диагностика профессионального выгорания\'";
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.CommandText = "update Results set result = \'" + label5.Text + "\', date = GetDate() where id_r = " + i;
            }
            cmd.ExecuteNonQuery();
            cn.Close();
        }
    }
}