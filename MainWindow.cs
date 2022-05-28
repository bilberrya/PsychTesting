using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace PsychTesting
{
    public partial class MainWindow : Form
    {
        string ConnStr = @"Data Source=desktop-jfut083;Initial Catalog=PsychTesting;Integrated Security=True";
        public MainWindow()
        {
            InitializeComponent();
            SqlCommand cmd;
            SqlConnection cn = new SqlConnection(ConnStr);
            cn.Open();
            cmd = cn.CreateCommand();
            cmd.CommandText = "select post from Workers where id_w = " + Program.id;
            Program.post = Convert.ToString(cmd.ExecuteScalar());
            cn.Close();
            if (Program.post.Equals("Сотрудник"))
                resbtn.Visible = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Program.MainForm = null;
            Application.Exit();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Msg m = new Msg();
            m.button1.Visible = true;
            m.btnExit.Text = "Отмена";
            m.label2.Text = "Вы уверены, что хотите выйти из учётной записи?";
            m.ShowDialog();
        }

        public void btnCab_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnCab.Height;
            pnlNav.Top = btnCab.Top;
            pnlNav.Left = btnCab.Left;
            btnCab.BackColor = Color.FromArgb(46, 51, 73);
            label2.Text = "Добро пожаловать, " + Program.fio;

            this.pnlFormLoader.Controls.Clear();
            PersonalAccount personalacc = new PersonalAccount() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            personalacc.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(personalacc);
            personalacc.Show();
        }

        private void btnGolland_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnGolland.Height;
            pnlNav.Top = btnGolland.Top;
            pnlNav.Left = btnGolland.Left;
            btnGolland.BackColor = Color.FromArgb(46, 51, 73);
            label2.Text = "Тест Голланда";

            this.pnlFormLoader.Controls.Clear();
            TestGolland testgoll = new TestGolland() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            testgoll.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(testgoll);
            testgoll.Show();
        }

        private void btnVig_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnVig.Height;
            pnlNav.Top = btnVig.Top;
            pnlNav.Left = btnVig.Left;
            btnVig.BackColor = Color.FromArgb(46, 51, 73);
            label2.Text = "Диагностика профессионального выгорания";

            this.pnlFormLoader.Controls.Clear();
            TestProfBurnout profburnout  = new TestProfBurnout() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            profburnout.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(profburnout);
            profburnout.Show();
        }

        private void btnSamef_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnSamef.Height;
            pnlNav.Top = btnSamef.Top;
            pnlNav.Left = btnSamef.Left;
            btnSamef.BackColor = Color.FromArgb(46, 51, 73);
            label2.Text = "Диагностика самоэффективности";

            this.pnlFormLoader.Controls.Clear();
            TestSamoef samoef = new TestSamoef() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            samoef.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(samoef);
            samoef.Show();
        }

        private void btnMotiv_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnMotiv.Height;
            pnlNav.Top = btnMotiv.Top;
            pnlNav.Left = btnMotiv.Left;
            btnMotiv.BackColor = Color.FromArgb(46, 51, 73);
            label2.Text = "Определение мотивации";

            this.pnlFormLoader.Controls.Clear();
            TestMotiv motiv = new TestMotiv() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            motiv.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(motiv);
            motiv.Show();
        }

        private void btnCab_Leave(object sender, EventArgs e)
        {
            btnCab.BackColor = Color.FromArgb(29, 30, 51);
        }

        private void btnGolland_Leave(object sender, EventArgs e)
        {
            btnGolland.BackColor = Color.FromArgb(29, 30, 51);
        }

        private void btnVig_Leave(object sender, EventArgs e)
        {
            btnVig.BackColor = Color.FromArgb(29, 30, 51);
        }

        private void btnSamef_Leave(object sender, EventArgs e)
        {
            btnSamef.BackColor = Color.FromArgb(29, 30, 51);
        }

        private void btnMotiv_Leave(object sender, EventArgs e)
        {
            btnMotiv.BackColor = Color.FromArgb(29, 30, 51);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            btnCab_Click(sender, e);
        }

        private void resbtn_Click(object sender, EventArgs e)
        {
            pnlNav.Height = resbtn.Height;
            pnlNav.Top = resbtn.Top;
            pnlNav.Left = resbtn.Left;
            resbtn.BackColor = Color.FromArgb(46, 51, 73);
            label2.Text = "Результаты";

            this.pnlFormLoader.Controls.Clear();
            Results res = new Results() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            res.FormBorderStyle = FormBorderStyle.None;
            this.pnlFormLoader.Controls.Add(res);
            res.Show();
        }

        private void resbtn_Leave(object sender, EventArgs e)
        {
            resbtn.BackColor = Color.FromArgb(29, 30, 51);
        }
    }
}
