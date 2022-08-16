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
    public partial class SignUp : Form
    {

        DataClasses1DataContext db = new DataClasses1DataContext();

        public SignUp()
        {
            InitializeComponent();
        }

        private void buttonSignUp_Click(object sender, EventArgs e)
        {

            switch (comboBoxType.Text)
            {
                case "Buyer":
                    {
                        String username = textBoxUserName.Text;
                        String password = textBoxPassword.Text;
                        int phone = Convert.ToInt32(textBoxPhone.Text);
                        int spendinglimit = Convert.ToInt32(textBoxSpendingLimit.Text);

                        if (username == null)
                            MessageBox.Show("Username must be filled.");

                        else if (password == null)
                            MessageBox.Show("Password must be filled.");

                        else if (phone == 0)
                            MessageBox.Show("Phone must be filled.");

                        else if (spendinglimit == 0)
                            MessageBox.Show("Spending limit must be filled.");

                        else
                        {
                            var checkUsername = (from x in db.Buyers where x.B_username == username select x);

                            if (checkUsername.Count() != 0)
                            {
                                MessageBox.Show("Username already exist!");
                            }

                            else
                            {
                                Buyer newBuyer = new Buyer();
                                newBuyer.B_username = username;
                                newBuyer.B_password = password;
                                newBuyer.B_phone = phone;
                                newBuyer.B_spendinglimit = spendinglimit;

                                db.Buyers.InsertOnSubmit(newBuyer);

                                try
                                {
                                    db.SubmitChanges();
                                    MessageBox.Show("Successfully register new account");

                                    Profile p = new Profile(username);                                  
                                    Close();
                                    
                                    BuyerForm B = new BuyerForm(username, spendinglimit);
                                    B.Visible = true;
                                    this.Hide();
                                }

                                catch (Exception)
                                {
                                    MessageBox.Show("Woops! Something went wrong!");
                                }
                            }
                        }

                        break;}

                case "Seller":
                    {
                        String username = textBoxUserName.Text;
                        String password = textBoxPassword.Text;
                        int phone = Convert.ToInt32(textBoxPhone.Text); ;

                        if (username == null)
                            MessageBox.Show("Username must be filled.");

                        else if (password == null)
                            MessageBox.Show("Password must be filled.");

                        else if (phone == 0)
                            MessageBox.Show("Phone must be filled.");

                        else
                        {
                            var checkUsername = (from x in db.Sellers where x.S_username == username select x);

                            if (checkUsername.Count() != 0)
                            {
                                MessageBox.Show("Username already exist!");
                            }
                            
                            else
                            {
                                Seller newSeller = new Seller();
                                newSeller.S_username = username;
                                newSeller.S_password = password;
                                newSeller.S_phone = phone;
                                db.Sellers.InsertOnSubmit(newSeller);

                                try
                                {
                                    db.SubmitChanges();
                                    MessageBox.Show("Successfully register new account");
                                    Close();

                                    SellerForm s = new SellerForm(username);
                                    s.Visible = true;
                                    this.Hide();
                                }

                                catch (Exception)
                                {
                                    MessageBox.Show("Woops! Something went wrong!");
                                }
                            }
                        }

                        break;}
            }
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( comboBoxType.Text == "Buyer")
            {
                labelSpendingLimit.Visible = true;
                textBoxSpendingLimit.Visible = true;
            }

            else
            {
                labelSpendingLimit.Visible = false;
                textBoxSpendingLimit.Visible = false;
            }
        }
    }
}
