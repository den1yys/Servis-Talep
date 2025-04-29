using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Servis_Talep
{
    public partial class Form1 : Form
    {
        List<TalepC> talepList = new List<TalepC>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new FrmEkle();
            frm.ShowDialog();
            VeriAl();
        }

        private void TalepEkle()
        {

        }

        private void VeriAl()
        {
            talepList = DbHelper.GetUrunList();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = talepList;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false;
            VeriAl();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TalepGuncelle();
        }

        private void TalepGuncelle()
        {
            if (dataGridView1.CurrentRow is null) return;
            var talep = dataGridView1.CurrentRow.DataBoundItem as TalepC;

            var frm = new FrmGuncelle(talep);
            frm.ShowDialog();
            VeriAl();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            TalepGuncelle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TalepSil();
        }

        private void TalepSil()
        {
            if (dataGridView1.CurrentRow is null) return;
            var talep = dataGridView1.CurrentRow.DataBoundItem as TalepC;
            var mesaj = $"Ad:{talep.Ad}, SoyAd:{talep.SoyAd},Marka:{talep.Marka},Model:{talep.Model}\nÜrün silinsin mi?";
            var cevap = MessageBox.Show(mesaj, "Silme Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (cevap == DialogResult.Yes)
            {
                DbHelper.TalepSil(talep);
                VeriAl();
            }
        }
    }
}
