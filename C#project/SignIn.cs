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
    public partial class SignIn : Form
    {
        public SignIn()
        {
            InitializeComponent();
        }

        DataClasses1DataContext db = new DataClasses1DataContext();

        private void buttonSignIn_Click(object sender, EventArgs e)
        {
            switch (comboBoxType.Text)
            {
                case "Buyer":
                    {
                        String Username = textBoxUserName.Text;
                        String Password = textBoxPassowrd.Text;

                        if (Username == null)
                            MessageBox.Show("Username must be filled");

                        else if (Password == null)
                            MessageBox.Show(" Password must be filled");

                        else
                        {
                            var getAccount = (from x in db.Buyers where x.B_username == Username && x.B_password == Password select x);

                            if (getAccount.Count() != 1)
                                MessageBox.Show("Account not found");

                            else
                            {
                                MessageBox.Show("Successfully login");
                                var getAccountF = getAccount.First();

                                Profile p = new Profile(Username);
                                p.Visible = true;
                                this.Hide();                   
                            }
                        }
                        break;}

                case "Seller":
                    {
                        String Username = textBoxUserName.Text;
                        String Password = textBoxPassowrd.Text;

                        if (Username == null)
                            MessageBox.Show("Username must be filled");

                        else if (Password == null)
                            MessageBox.Show("Password must be filled");

                        else
                        {
                            var getAccount = (from x in db.Sellers where x.S_username == Username && x.S_password == Password select x);

                            if (getAccount.Count() != 1)
                                MessageBox.Show("Account not found");

                            else
                            {
                                MessageBox.Show("Successfully login");
                                var getAccountF = getAccount.First();

                                SellerForm s = new SellerForm(Username);
                                s.Visible = true;
                                this.Hide();
                            }
                        }
                        break;}
            }
        }

        private void labelSignUp_Click(object sender, EventArgs e)
        {
            SignUp s1 = new SignUp();
            s1.Visible = true;
            this.Hide();
        }
    }
}
    
