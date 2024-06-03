using MarketSatisProjesi.Forms;
using MarketSatisProjesi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;

namespace MarketSatisProjesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        AppDbContext _db = new();
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var item in _db.Customers.Select(s => s.Name))
            {
                comboBox1.Items.Add(item);
                comboBox4.Items.Add(item);
            }

            foreach (var item in _db.Products.Select(s => s.Name))
            {
                comboBox2.Items.Add(item);
                comboBox3.Items.Add(item);
            }

            var invoiceList = _db.Invoices
                .Include(s => s.Customer)
                .Include(s => s.Product)
                .Where(s => s.IsInUse)
                .Select(s => new
                {
                    Id = s.ID,
                    customerName = s.Customer.Name,
                    productName = s.Product.Name,
                    quantity = s.Quantity,
                    price = s.Price,
                    amount = s.Amount
                })
                .ToList();


            if (invoiceList.Count > 0)
            {
                dataGridView1.DataSource = invoiceList;
            }

            panel2.Visible = false;
            panel3.Visible = false;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            var selectedProduct = _db.Products.Where(s => s.Name == comboBox2.SelectedItem.ToString()).FirstOrDefault();

            if (selectedProduct != null)
            {
                textBox2.Text = selectedProduct.Price;
                textBox1.Text = 1.ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                var selectedProduct = _db.Products.Where(s => s.Name == comboBox2.SelectedItem.ToString()).FirstOrDefault();
                if (selectedProduct != null)
                {
                    double price = Convert.ToDouble(selectedProduct.Price);
                    double quantity = Convert.ToDouble(textBox1.Text);
                    textBox2.Text = (price * quantity).ToString();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var selectedCustomer = _db.Customers.Where(s => s.Name == comboBox1.SelectedItem.ToString()).FirstOrDefault();
            var selectedProduct = _db.Products.Where(s => s.Name == comboBox2.SelectedItem.ToString()).FirstOrDefault();
            var selectedPrice = Convert.ToDouble(textBox2.Text);
            var selectedQuantity = Convert.ToInt32(textBox1.Text);

            if (selectedCustomer != null && selectedProduct != null)
            {
                Invoice item = new();
                item.CustomerId = selectedCustomer.ID;
                item.ProductId = selectedProduct.ID;
                item.Quantity = selectedQuantity;
                item.Price = Convert.ToDouble(selectedProduct.Price);
                item.Quantity = selectedQuantity;
                item.Amount = selectedPrice * selectedQuantity;
                item.IsInUse = true;

                _db.Invoices.Add(item);
                _db.SaveChanges();
                MessageBox.Show("Satýþ Ýþlemi Kaydedilmiþtir.","Tamam",  MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox1.Items.Clear();
                comboBox2.Items.Clear();
                textBox1.Text = "";
                textBox2.Text = "";

                var invoiceList = _db.Invoices
                .Include(s => s.Customer)
                .Include(s => s.Product)
                .Where(s => s.IsInUse)
                .Select(s => new
                {
                    Id = s.ID,
                    customerName = s.Customer.Name,
                    productName = s.Product.Name,
                    quantity = s.Quantity,
                    price = s.Price,
                    amount = s.Amount
                })
                .ToList();


                if (invoiceList.Count > 0)
                {
                    dataGridView1.DataSource = invoiceList;
                }

            }
            else
            {
                MessageBox.Show("Lütfen Müþteri Ya Da Ürün Seçimi Yapýnýz.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                panel2.Visible = false;
                panel3.Visible = true;

                var selectedRow = dataGridView1.SelectedRows[0];

                var a = selectedRow.Cells[0].Value.ToString();
                comboBox4.SelectedItem = selectedRow.Cells[1].Value.ToString();
                comboBox3.SelectedItem = selectedRow.Cells[2].Value.ToString();
                textBox4.Text = selectedRow.Cells[3].Value.ToString(); //quantity
                textBox3.Text = selectedRow.Cells[4].Value.ToString(); //price

            }
            else
            {
                MessageBox.Show("Lütfen Düzenlemek Ýstediðiniz Kaydýn Satýrýný Seçin","Seç", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

            var selectedProduct = _db.Products.Where(s => s.Name == comboBox3.SelectedItem.ToString()).FirstOrDefault();

            if (selectedProduct != null)
            {
                textBox3.Text = selectedProduct.Price;
                textBox4.Text = 1.ToString();
            }

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

            var selectedProduct = _db.Products.Where(s => s.Name == comboBox3.SelectedItem.ToString()).FirstOrDefault();
            if (selectedProduct != null)
            {
                double price = Convert.ToDouble(selectedProduct.Price);
                double quantity = Convert.ToDouble(textBox4.Text);
                textBox3.Text = (price * quantity).ToString();
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];
                var selectedID = selectedRow.Cells[0].Value.ToString();

                var selectedRecord = _db.Invoices.Where(s => s.ID.ToString() == selectedID).FirstOrDefault();
                if (selectedRecord != null)
                {
                    var selectedCustomer = _db.Customers.Where(s => s.Name == comboBox4.SelectedItem.ToString()).FirstOrDefault();
                    var selectedProduct = _db.Products.Where(s => s.Name == comboBox3.SelectedItem.ToString()).FirstOrDefault();
                    var selectedPrice = Convert.ToDouble(textBox3.Text);
                    var selectedQuantity = Convert.ToInt32(textBox4.Text);
                    if (selectedCustomer != null && selectedProduct != null)
                    {
                        selectedRecord.CustomerId = selectedCustomer.ID;
                        selectedRecord.ProductId = selectedProduct.ID;
                        selectedRecord.Description = "";
                        selectedRecord.Price = selectedPrice;
                        selectedRecord.Quantity = selectedQuantity;
                        selectedRecord.Amount = selectedPrice * selectedQuantity;
                    }
                    _db.Invoices.Update(selectedRecord);
                    _db.SaveChanges();

                    MessageBox.Show("Güncelleme Ýþlemi Baþarýlý!","Güncellendi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    var invoiceList = _db.Invoices
                .Include(s => s.Customer)
                .Include(s => s.Product)
                .Where(s => s.IsInUse)
                .Select(s => new
                {
                    Id = s.ID,
                    customerName = s.Customer.Name,
                    productName = s.Product.Name,
                    quantity = s.Quantity,
                    price = s.Price,
                    amount = s.Amount
                })
                .ToList();


                    if (invoiceList.Count > 0)
                    {
                        dataGridView1.DataSource = invoiceList;
                    }
                }
                else
                {
                    MessageBox.Show("lütfen Güncelleme Ýþlemini Tekrar Deneyiniz.","Tekrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Kaydetme Ýþlemi Sýrasýnda Kayýt Satýrý Seçili Olmalýdýr.","Satýr Seç", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridView1.SelectedRows[0];

                var selectedId = selectedRow.Cells[0].Value.ToString();

                var deleteRecord = _db.Invoices.Where(s => s.ID.ToString() == selectedId).FirstOrDefault();
                deleteRecord.IsInUse = false;

                _db.Invoices.Update(deleteRecord);
                _db.SaveChanges();
                MessageBox.Show("Silme Ýþlemi Baþarýlý!","Silindi",MessageBoxButtons.OK, MessageBoxIcon.Information);

                var invoiceList = _db.Invoices
                .Include(s => s.Customer)
                .Include(s => s.Product)
                .Where(s => s.IsInUse)
                .Select(s => new
                {
                    Id = s.ID,
                    customerName = s.Customer.Name,
                    productName = s.Product.Name,
                    quantity = s.Quantity,
                    price = s.Price,
                    amount = s.Amount
                })
                .ToList();


                if (invoiceList.Count > 0)
                {
                    dataGridView1.DataSource = invoiceList;
                }
            }
            else
            {
                MessageBox.Show("Silmek Ýstediðiniz Satýrý Seçiniz","Seç", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new();
            form2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new();
            form3.ShowDialog();
        }
    }
}
