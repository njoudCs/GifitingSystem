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
    public partial class Profile : Form
    {

        DataClasses1DataContext db = new DataClasses1DataContext();
  
        public Profile(string user)
        {
            InitializeComponent();
            textBoxUserName.Text = user;
            
            var input = (from x in db.Buyers where x.B_username == textBoxUserName.Text select x).First();
            BuyerForm bf = new BuyerForm(input.B_username, input.B_spendinglimit);
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            string user = textBoxUserName.Text;
            var profileload = (from x in db.Buyers where x.B_username == user select x).First();

            textBoxUserName.Text = profileload.B_username;
            textBoxPassword.Text = profileload.B_password;
            textBoxPhone.Text = Convert.ToString(profileload.B_phone);
            textBoxSpendingLimit.Text = Convert.ToString(profileload.B_spendinglimit);
        }
        
        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            String username = textBoxUserName.Text;
            String password = textBoxPassword.Text;
            int phone = Convert.ToInt32(textBoxPhone.Text);
            int spendinglimit = Convert.ToInt32(textBoxSpendingLimit.Text);

            if (password == null)
                MessageBox.Show("Password must be filled.");

            else if (phone == 0)
                MessageBox.Show("Phone must be filled.");

            else if (spendinglimit == 0)
                MessageBox.Show("Spending limit must be filled.");

            else
            {
                var checkUsername = (from x in db.Buyers where x.B_username == textBoxUserName.Text select x).First();
                checkUsername.B_password = password;
                checkUsername.B_phone = phone;
                checkUsername.B_spendinglimit = spendinglimit;

                db.SubmitChanges();
                MessageBox.Show("Successfully updated!");
            }
        }

        private void labelLogOut_Click(object sender, EventArgs e)
        {
            SignIn s1 = new SignIn();
            s1.Visible = true;
            this.Hide();
        }

        private void labelShop_Click(object sender, EventArgs e)
        {
            BuyerForm b1 = new BuyerForm(textBoxUserName.Text , Convert.ToInt32(textBoxSpendingLimit.Text));
            b1.Visible = true;
            this.Hide();         
        }

        private void label1_Click(object sender, EventArgs e) { }

    }
}
