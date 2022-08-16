using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gifting_System
{
    public partial class BuyerForm : Form
    {
        
        public BuyerForm(string user, int limit)
        {
            InitializeComponent();
            labelUserName.Text = user;
            labelLimit.Text = limit.ToString(); 
        }

        DataClasses2DataContext db = new DataClasses2DataContext();
        DataClasses3DataContext db3 = new DataClasses3DataContext();

        private void dataGridViewDisplay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxGiftId.Text = dataGridViewDisplay.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBoxGiftName.Text = dataGridViewDisplay.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBoxPrice.Text = dataGridViewDisplay.Rows[e.RowIndex].Cells[5].Value.ToString();
            textBoxStock.Text = dataGridViewDisplay.Rows[e.RowIndex].Cells[4].Value.ToString();
            textBoxCategory.Text = dataGridViewDisplay.Rows[e.RowIndex].Cells[2].Value.ToString();          
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ld = from x in db.Gifts where x.G_category == comboBoxType.Text select x;
            dataGridViewDisplay.DataSource = ld;       
        }

        private void label7_Click(object sender, EventArgs e)
        {
            SignIn s1 = new SignIn();
            s1.Visible = true;
            this.Hide();
        }

        private void Profile_Click(object sender, EventArgs e)
        {
            Profile p = new Profile(labelUserName.Text);
            p.Visible = true;
            this.Hide();
        }

        private void BuyerForm_Load(object sender, EventArgs e)
        {
            loadDataGift();
            clearDataGridView();                     
        }

        void loadDataBill() 
        {
            var loadBill = from x in db3.BillTables select x;
            dataGridViewBill.DataSource = loadBill;
        }

        void loadDataGift()
        {          
            int limit = Convert.ToInt32(labelLimit.Text);
            var loadGift = from x in db.Gifts where x.G_price <= limit select x;
            dataGridViewDisplay.DataSource = loadGift;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try {               
            int quantityByUser = Convert.ToInt32(textBoxQuantity.Text);
            int quantityInStock = Convert.ToInt32(textBoxStock.Text);

                if (quantityInStock != 0)
                {
                    if (quantityByUser <= quantityInStock)
                    {
                        BillTable buyerBill = new BillTable
                        {
                            GB_id = Convert.ToInt32(textBoxGiftId.Text),
                            G_name = textBoxGiftName.Text,
                            G_description = dataGridViewDisplay.CurrentRow.Cells[3].Value.ToString(),
                            G_quantity = Convert.ToInt32(textBoxQuantity.Text),
                            G_price = Convert.ToInt32(textBoxPrice.Text) * Convert.ToInt32(textBoxQuantity.Text),
                            G_category = dataGridViewDisplay.CurrentRow.Cells[2].Value.ToString(),
                        };

                        db3.BillTables.InsertOnSubmit(buyerBill);
                        db3.SubmitChanges();
                        loadDataBill();

                        int id = int.Parse(textBoxGiftId.Text);
                        int updateQ = Convert.ToInt32(textBoxStock.Text) - Convert.ToInt32(textBoxQuantity.Text);

                        Gift g = db.GetTable<Gift>().Single(x => x.G_id == id);
                        g.G_quantity = updateQ;
                        db.SubmitChanges();

                        clearTextbox();
                        calculateTotal();
                    }

                    if (quantityByUser > quantityInStock)
                        MessageBox.Show("Entered quantity more than in stock");
                }
                else
                    MessageBox.Show("Out of stock");
            }

            catch (Exception)
            {
                MessageBox.Show("Maybe you did't enter the quantity OR the item already exist");
            }
        }
            
        void clearTextbox()
        {
            textBoxGiftId.Text = textBoxGiftName.Text = textBoxPrice.Text = textBoxQuantity.Text = comboBoxType.Text = textBoxStock.Text = textBoxCategory.Text = "";
        }

        private void dataGridViewBill_CellClick(object sender, DataGridViewCellEventArgs e)
        {
 
        }


        void clearDataGridView() 
        {
            var gt = (from x in db3.BillTables select x);
            db3.BillTables.DeleteAllOnSubmit(gt);
            db3.SubmitChanges();
        }

        void DeleteItems() 
        {
            var gt = (from x in db.Gifts where x.G_quantity == 0 select x).First();
            db.Gifts.DeleteOnSubmit(gt);
            db.SubmitChanges();
        }

        private void buttonBuy_Click(object sender, EventArgs e) 
        {
            label5.Text = "Total is 0 SAR";

            var bill = from x in db3.BillTables select x;
            foreach (var a in bill) 
            {
                db3.BillTables.DeleteOnSubmit(a);
                db3.SubmitChanges();
            }

            loadDataBill();

            MessageBox.Show("Successfully transiction");
        }

        void calculateTotal() 
        {
            var prices = from x in db3.BillTables select x.G_price;
            int total = 0;
            foreach (var b in prices)
            {
                total += b;
                label5.Text = "Total is " + total + " SAR";
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e) { }

    }
}

