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
    public partial class SellerForm : Form
    {
        public SellerForm(string user)
        {
            InitializeComponent();
            textBoxUsername.Text = user;
        }

        DataClasses2DataContext db = new DataClasses2DataContext();

        private void buttonAdd_Click(object sender, EventArgs e)
        {
          try
          {
            string username = textBoxUsername.Text;
            int giftid = Convert.ToInt32(textBoxGiftId.Text);
            string giftname = textBoxGiftName.Text;
            string catgory = comboBoxCategory.Text;
            string description = textBoxDescription.Text;
            int price = Convert.ToInt32(textBoxPrice.Text);
            int quantity = Convert.ToInt32(textBoxQuantity.Text);

            
                var Selleradd = new Gift
                {
                    G_id = giftid,
                    G_name = giftname,
                    G_category = catgory,
                    G_description = description,
                    G_quantity = quantity,
                    G_price = price,
                    S_username = username
                };

                db.Gifts.InsertOnSubmit(Selleradd);
                db.SubmitChanges();

                MessageBox.Show("Succesfully Inserted");
                loadData();
            }

           catch (Exception) 
            {
                MessageBox.Show("Gift id already exist, enter another id");
                
            }
            clear();
        }

        void loadData()
        {
            var ld = from x in db.Gifts where x.S_username == textBoxUsername.Text select x;
            dataGridView1.DataSource = ld;
        }

        void clear() 
        {
            textBoxDescription.Text = textBoxGiftId.Text = textBoxGiftName.Text = textBoxPrice.Text = textBoxQuantity.Text = comboBoxCategory.Text = "";
        }

        private void SellerForm_Load(object sender, EventArgs e)
        {
            loadData();
          
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                var ut = (from x in db.Gifts where x.G_id == int.Parse(textBoxGiftId.Text) select x).First();

                ut.G_id = Convert.ToInt32(textBoxGiftId.Text);
                ut.G_price = Convert.ToInt32(textBoxPrice.Text);
                ut.G_quantity = Convert.ToInt32(textBoxQuantity.Text);
                ut.G_name = textBoxGiftName.Text;
                ut.G_category = comboBoxCategory.Text;
                ut.G_description = textBoxDescription.Text;
                ut.S_username = textBoxUsername.Text;

                db.SubmitChanges();
                MessageBox.Show("Succesfully Updated");
            }

            catch (Exception) 
            {
                MessageBox.Show("Wrong");
                clear();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                textBoxGiftId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                textBoxGiftName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                comboBoxCategory.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBoxDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                textBoxQuantity.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                textBoxPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                textBoxUsername.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure to delete?", "Delete Record", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var gt = (from x in db.Gifts where x.G_id == int.Parse(textBoxGiftId.Text) select x).First();
                    db.Gifts.DeleteOnSubmit(gt);
                    db.SubmitChanges();
                    MessageBox.Show("Succesfully deleted");
                    loadData();
                    clear();
                }
            }
            catch (Exception) 
            {
                MessageBox.Show("Select row please");
            }          
        }

        private void label8_Click(object sender, EventArgs e)
        {
            SignIn s1 = new SignIn();
            s1.Visible = true;
            this.Hide();
        }

        private void Seller1_Load(object sender, EventArgs e) { }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

    }
}

