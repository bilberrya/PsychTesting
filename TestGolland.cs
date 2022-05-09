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
    public partial class TestGolland : Form
    {
        string ConnStr = @"Data Source=desktop-jfut083;Initial Catalog=PsychTesting;Integrated Security=True";
        int t = 0;
        float real = 0, intel = 0, soc = 0, konv = 0, pred = 0, artist = 0;
        string [,] varian = new string[3,43];
        public TestGolland()
        {
            InitializeComponent();
            mas(varian);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (t == 0)
            {
                btnStart.Text = "Следущий вопрос";
                label1.Visible = false;
                t++;
                label2.Visible = true;
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                label6.Text = "Вопрос " + t + ":";
                label6.Visible = true;
                radioButton1.Text = varian[1, 0];
                radioButton2.Text = varian[2, 0];
            }
            else if ((t >= 1) && (t < 42))
                Testing();
            else if (t == 42)
            {
                btnStart.Text = "Завершить тест";
                Testing();
            }
            else if (t == 43)
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
            else
            {
                Process.Start(new ProcessStartInfo("Описание результатов теста Голланда.docx") { UseShellExecute = true });
            }
        }

        private void Counting()
        {
            switch (t)
            {
                case 1 or 16 or 31:
                    if (radioButton1.Checked) real++;
                    else if (radioButton2.Checked) intel++;
                    break;
                case 2 or 17 or 32:
                    if (radioButton1.Checked) real++;
                    else if (radioButton2.Checked) soc++;
                    break;
                case 3 or 18 or 33:
                    if (radioButton1.Checked) real++;
                    else if (radioButton2.Checked) konv++;
                    break;
                case 4 or 19 or 34:
                    if (radioButton1.Checked) real++;
                    else if (radioButton2.Checked) pred++;
                    break;
                case 5 or 20 or 35:
                    if (radioButton1.Checked) real++;
                    else if (radioButton2.Checked) artist++;
                    break;
                case 6 or 21 or 36:
                    if (radioButton1.Checked) intel++;
                    else if (radioButton2.Checked) soc++;
                    break;
                case 7 or 22 or 37:
                    if (radioButton1.Checked) intel++;
                    else if (radioButton2.Checked) konv++;
                    break;
                case 8 or 23 or 38:
                    if (radioButton1.Checked) intel++;
                    else if (radioButton2.Checked) pred++;
                    break;
                case 9 or 24 or 39:
                    if (radioButton1.Checked) intel++;
                    else if (radioButton2.Checked) artist++;
                    break;
                case 10 or 25 or 40:
                    if (radioButton1.Checked) soc++;
                    else if (radioButton2.Checked) konv++;
                    break;
                case 11 or 26 or 41:
                    if (radioButton1.Checked) soc++;
                    else if (radioButton2.Checked) pred++;
                    break;
                case 12 or 27 or 42:
                    if (radioButton1.Checked) soc++;
                    else if (radioButton2.Checked) artist++;
                    break;
                case 13 or 28 or 43:
                    if (radioButton1.Checked) konv++;
                    else if (radioButton2.Checked) pred++;
                    break;
                case 14 or 29:
                    if (radioButton1.Checked) konv++;
                    else if (radioButton2.Checked) artist++;
                    break;
                case 15 or 30:
                    if (radioButton1.Checked) pred++;
                    else if (radioButton2.Checked) artist++;
                    break;
                default:
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
            radioButton1.Text = varian[1, t - 1];
            radioButton2.Text = varian[2, t - 1];
            label6.Text = "Вопрос " + t + ":";
        }

        private void mas (string[,] varian)
        {
            SqlCommand cmd;
            SqlConnection cn = new SqlConnection(ConnStr);
            cn.Open();
            cmd = cn.CreateCommand();
            for (int i = 0; i < 3; i++)
            {
                if (i == 0) cmd.CommandText = "select id_q from GollandTest where id_q = \'";
                else if (i == 1) cmd.CommandText = "select VariantA from GollandTest where id_q = \'";
                else if (i == 2) cmd.CommandText = "select VariantB from GollandTest where id_q = \'";
                for (int j = 0; j < 43; j++)
                {
                    string str = cmd.CommandText;
                    int count = 101 + j;
                    cmd.CommandText = cmd.CommandText + count + "\'";
                    varian[i,j] = Convert.ToString(cmd.ExecuteScalar());
                    cmd.CommandText = str;
                }
            }
        }

        private void Finish()
        {
            label1.Visible = false;
            label2.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            label6.Visible = false;
            btnStart.Text = "Подробное описание";
            t++;
            label5.Text = "Реалистический тип: " + real + " (" + Math.Round(real / 15 * 100, 2) + " %)\nИнтеллектуальный тип: " + intel + " (" + Math.Round(intel / 15 * 100, 2) +
                " %)\nСоциальный тип: " + soc + " (" + Math.Round(soc / 15 * 100, 2) + " %)\nКонвенциальный тип: " + konv + " (" + Math.Round(konv / 14 * 100, 2) +
                " %)\nПредприимчивый тип: " + pred + " (" + Math.Round(pred / 14 * 100, 2) + " %)\nАртистичный тип: " + artist + " (" + Math.Round(artist / 13 * 100, 2) + " %)";
            label4.Visible = true;
            label5.Visible = true;
            SqlCommand cmd;
            SqlConnection cn = new SqlConnection(ConnStr);
            cn.Open();
            cmd = cn.CreateCommand();
            cmd.CommandText = "select count(*) from Results where id_w = \'" + Program.id + "\' and test = \'Тест Голланда\'";
            int c = Convert.ToInt32(cmd.ExecuteScalar());
            if (c == 0)
                cmd.CommandText = "insert into Results (id_w,test,result,date) values (\'" + Program.id + "\', \'Тест Голланда\', \'" + label5.Text + "\', GetDate())";
            else
            {
                cmd.CommandText = "select id_r from Results where id_w = \'" + Program.id + "\' and test = \'Тест Голланда\'";
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.CommandText = "update Results set result = \'" + label5.Text + "\', date = GetDate() where id_r = " + i;
            }
            cmd.ExecuteNonQuery();
            cn.Close();
        }
    }
}