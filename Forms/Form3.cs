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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        AppDbContext _db = new();
        private void Form3_Load(object sender, EventArgs e)
        {
            var customerList = _db.Customers.Where(s => s.IsInUse).Select(
                s => new
                {
                    id = s.ID,
                    name = s.Name,
                    surname = s.Surname,
                    phone = s.Phone,
                    email = s.Email,
                }).ToList();

            if (customerList != null)
            {
                dataGridView1.DataSource = customerList;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Customer item = new();
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                item.Name = textBox1.Text;
                item.Surname = textBox2.Text;
                item.Phone = textBox3.Text;
                item.Email = textBox5.Text;
                item.IsInUse = true;
                _db.Customers.Add(item);
                _db.SaveChanges();
                MessageBox.Show("Kaydetme İşlemi Başarılı","Kaydettin", MessageBoxButtons.OK, MessageBoxIcon.Information);

                var customerList = _db.Customers.Where(s => s.IsInUse).Select(
                s => new
                {
                    name = s.Name,
                    surname = s.Surname,
                    phone = s.Phone,
                    email = s.Email,
                }).ToList();

                if (customerList != null)
                {
                    dataGridView1.DataSource = customerList;
                }

            }
            else
            {
                MessageBox.Show("Lütfen Ad Giriniz","Ad Gir", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                panel1.Visible = true;
                var selectedRow = dataGridView1.SelectedRows[0];

                textBox8.Text = selectedRow.Cells[1].Value.ToString();
                textBox7.Text = selectedRow.Cells[2].Value.ToString();
                textBox6.Text = selectedRow.Cells[3].Value.ToString();
                textBox4.Text = selectedRow.Cells[4].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                var selectedId = selectedRow.Cells[0].Value.ToString();

                var selectedRecord = _db.Customers.Where(s => s.ID.ToString() == selectedId).FirstOrDefault();

                if (selectedRecord != null)
                {
                    selectedRecord.Name = textBox8.Text;
                    selectedRecord.Surname = textBox7.Text;
                    selectedRecord.Phone = textBox6.Text;
                    selectedRecord.Email = textBox4.Text;
                    _db.Customers.Update(selectedRecord);
                    _db.SaveChanges();
                    MessageBox.Show("Güncelleme İşlemi Başarılı","Güncelledi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    var customerList = _db.Customers.Where(s => s.IsInUse).Select(
                s => new
                {
                    name = s.Name,
                    surname = s.Surname,
                    phone = s.Phone,
                    email = s.Email,
                }).ToList();

                    if (customerList != null)
                    {
                        dataGridView1.DataSource = customerList;
                    }
                }
            }
            else
            {
                MessageBox.Show("lütfen Satırı Seçiniz","Satır Seç", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
