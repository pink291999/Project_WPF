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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            butDangNhap.IsEnabled = false;
        }

        private void ButDangKy_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.ShowDialog();
            if (register.UserG != null)
            {
                txtUser.Text = register.UserG.Username;
                pass.Password = register.UserG.Password;
            }
        }



        private void ButDangNhap_Click(object sender, RoutedEventArgs e)
        {
            string s = TienIch.MaHoaMD5(pass.Password);
            using (var db = new MyEntity())
            {
                var user = db.DSUser.Where(m => (m.Password == s) && (m.Username == txtUser.Text));
                if (user.ToList().Count > 0)
                {


                    MainWindow mainWindow = new MainWindow();
                    this.Close();
                    mainWindow.Show();

                }

                else
                    MessageBox.Show("Đăng nhập không thành công");
            }
        }

        private void txtUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtUser.Text != "" && pass.Password != "")
                butDangNhap.IsEnabled = true;

        }

        private void pass_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtUser.Text != "" && pass.Password != "")

            {
                butDangNhap.IsEnabled = true;
                if (e.Key == Key.Enter)
                    ButDangNhap_Click(sender, e);
            }
            else if (e.Key == Key.Enter)
                butDangNhap.Focus();


        }

        private void butHuy_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Có muốn thoát?", "Thoát", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
                this.Close();
        }





        private void TxtUser_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtUser.Text != "" && pass.Password != "")
            {
                butDangNhap.IsEnabled = true;
                if (e.Key == Key.Enter)
                    ButDangNhap_Click(sender, e);
            }
            else if (e.Key == Key.Enter)
                pass.Focus();
        }

       
        

       
    }
}

