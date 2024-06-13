using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HareketProje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=berk;Initial Catalog=Hareket;Integrated Security=True;");
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'hareketDataSet1.ProjeHareket' table. You can move, or remove it, as needed.
            this.projeHareketTableAdapter1.Fill(this.hareketDataSet1.ProjeHareket);
            // TODO: This line of code loads data into the 'hareketDataSet.ProjeHareket' table. You can move, or remove it, as needed.
            this.projeHareketTableAdapter.Fill(this.hareketDataSet.ProjeHareket);

            // Ürünleri Çekme
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from URUNLER ", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbUrun.ValueMember = "ID";
            CmbUrun.DisplayMember = "AD";
            CmbUrun.DataSource = dt;
            komut.ExecuteNonQuery();
            baglanti.Close();

            //Müşterileri Çekme
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select * from MUSTERILER",baglanti);
            SqlDataAdapter da1 = new SqlDataAdapter(komut1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            CmbMusteri.ValueMember = "ID";
            CmbMusteri.DisplayMember = "ADSOYAD";
            CmbMusteri.DataSource = dt1;
            komut1.ExecuteNonQuery();
            baglanti.Close();

            // Perspnelleri Çekme
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select * from PERSONELLER", baglanti);
            SqlDataAdapter da2 = new SqlDataAdapter(komut2);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);
            CmbPersonel.ValueMember = "Id";
            CmbPersonel.DisplayMember = "Ad";
            CmbPersonel.DataSource = dt2;
            komut2.ExecuteNonQuery();
            baglanti.Close();

            
        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into HAREKETLER(URUN,MUSTERI,PERSONEL,FIYAT)values(@p1,@p2,@p3,@p4)", baglanti);
            komut.Parameters.AddWithValue("@p1", CmbUrun.Text);
            komut.Parameters.AddWithValue("@p2", CmbMusteri.Text);
            komut.Parameters.AddWithValue("@p3", CmbPersonel.Text);
            komut.Parameters.AddWithValue("@p4", TxtFiyat.Text);

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("İşleminiz Eklendi", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
