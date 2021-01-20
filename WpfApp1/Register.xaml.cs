using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public User UserN { get; set; }
        public User UserG { get; set; }

        public Register()
        {
            InitializeComponent();
            butDangKy.IsEnabled = false;
            txtUser.Focus();
        }
      

        private void txtUser_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtUser.Text != "")
            {
                

                if (e.Key == Key.Enter)



                    pass.Focus();

            }
        }

       

        private void Pass_KeyUp(object sender, KeyEventArgs e)
        {
            


            if (e.Key == Key.Enter)

                repass.Focus();
        }

        private void Repass_KeyUp(object sender, KeyEventArgs e)
        {
            if (repass.Password != "")

                butDangKy.IsEnabled = true;


            if (e.Key == Key.Enter)



                butDangKy.Focus();
        }

        private void ButDangKy_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MyEntity())
            {
                if (pass.Password == repass.Password) //Lưu ý 8->20
                {
                    string s = TienIch.MaHoaMD5(pass.Password);
                    var user = db.DSUser.Where(m => (m.Password == s) && (m.Username == txtUser.Text));
                    if (user.ToList().Count > 0)
                    {
                        MessageBox.Show("Trùng User ");
                        txtUser.Focus();
                        return;
                    }
                    var id = 0;
                    var c = db.DSUser.Count();
                    if (c == 0)
                        id = 1;
                    else
                    {
                        var temp = db.DSUser.ToList().Max().UserID;
                        id = ++temp;
                    }
                    UserG = new User
                    {
                        UserID = id,
                        Username = txtUser.Text,
                        Password = pass.Password
                    };
                    UserN = new User
                    {
                        UserID = id,
                        Username = txtUser.Text,
                        Password = TienIch.MaHoaMD5(pass.Password)
                    };
                    db.DSUser.Add(UserN);
                    db.SaveChanges();
                    MessageBox.Show("Bạn đăng kí thành công!");
                    this.Close();

                }
                else
                    MessageBox.Show("Không đúng.....");
            }
        }

        private void ButHuy_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Có muốn thoát?", "Thoát", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                this.Close();
        }
    }
}

