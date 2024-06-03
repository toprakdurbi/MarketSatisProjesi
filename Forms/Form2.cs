using MarketSatisProjesi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarketSatisProjesi.Forms
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        AppDbContext _db = new();
        private void Form2_Load(object sender, EventArgs e)
        {
            var productList = _db.Products.Where(s => s.IsInUse).ToList();

            if (productList != null)
            {
                dataGridView1.DataSource = productList;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Product item = new Product();
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                item.Name = textBox1.Text;
                item.Price = textBox2.Text;
                item.IsInUse = true;

                _db.Products.Add(item);
                _db.SaveChanges();
                MessageBox.Show("Kaydetme İşlemi Başarılı","Başardın", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var productList = _db.Products.Where(s => s.IsInUse).ToList();

                if (productList != null)
                {
                    dataGridView1.DataSource = productList;
                }

            }
            else
            {
                MessageBox.Show("Lütfen Ürün Adı Giriniz","Ad Gir", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                var selectedId = selectedRow.Cells[0].Value.ToString();

                var selectedRecord = _db.Products.Where(s => s.ID.ToString() == selectedId).FirstOrDefault();

                if (selectedRecord != null)
                {
                    selectedRecord.Name = textBox4.Text;
                    selectedRecord.Price = textBox3.Text;
                    _db.Products.Update(selectedRecord);
                    _db.SaveChanges();
                    MessageBox.Show("Güncelleme İşlemi Başarılı","Güncellendi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    var productList = _db.Products.Where(s => s.IsInUse).ToList();

                    if (productList != null)
                    {
                        dataGridView1.DataSource = productList;
                    }
                }
            }
            else
            {
                MessageBox.Show("lütfen Satırı Seçiniz","Satır Seç", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                panel3.Visible = true;
                var selectedRow = dataGridView1.SelectedRows[0];

                textBox4.Text = selectedRow.Cells[1].Value.ToString();
                textBox3.Text = selectedRow.Cells[2].Value.ToString();
            }
        }
    }
}
