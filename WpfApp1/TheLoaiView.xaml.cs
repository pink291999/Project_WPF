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
    /// Interaction logic for TheLoaiView.xaml
    /// </summary>
    public partial class TheLoaiView : Window
    {
        public TheLoaiView()
        {
            InitializeComponent();
            txtTheLoaiID.IsReadOnly = true;
            butSuaTL.IsEnabled = butThemTL.IsEnabled = butXoaTL.IsEnabled = false;
            using (var db = new MyEntity())
            {
                dataGirdDSTL.ItemsSource = db.DSTheLoai.ToList();
                var c = db.DSTheLoai.Count();
                var s = "";
                if (c > 0)
                {
                    var sTemp = db.DSTheLoai.ToList().ElementAt(c - 1).TheLoaiID;
                    s = TienIch.TaoMaTuDong(sTemp, sTemp.Substring(2, 2));
                }
                else
                {
                    s = "TL01";
                }
                txtTheLoaiID.Text = s;
                txtTenTheLoai.Focus();

            }
        }

        private void ButThemMoiTL_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MyEntity())
            {
                dataGirdDSTL.ItemsSource = db.DSTheLoai.ToList();
                var c = db.DSTheLoai.Count();
                var s = "";
                if (c > 0)
                {
                    var sTemp = db.DSTheLoai.ToList().ElementAt(c - 1).TheLoaiID;
                    s = TienIch.TaoMaTuDong(sTemp, sTemp.Substring(2, 2));
                }
                else
                {
                    s = "TL01";
                }
                txtTheLoaiID.Text = s;
                txtTenTheLoai.Text = "";
                txtTenTheLoai.Focus();

            }
        }

        private void ButXoaTL_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Có muốn xóa?", "Xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                using (var db = new MyEntity())
                {
                    var query = db.DSSach.Where(m => m.TheLoaiID == txtTheLoaiID.Text);
                    if (query.ToList().Count() > 0)
                    {
                        MessageBox.Show("Lỗi ràng buộc khóa ngoại...");
                        return;
                    }
                    var theLoai = (TheLoai)db.DSTheLoai.Find(txtTheLoaiID.Text);
                    db.DSTheLoai.Remove(theLoai);
                    db.SaveChanges();
                    dataGirdDSTL.ItemsSource = db.DSTheLoai.ToList();
                    butSuaTL.IsEnabled = butXoaTL.IsEnabled = false;
                }
            }
            this.Close();
        }

        private void ButSuaTL_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MyEntity())
            {
                var theLoai = (TheLoai)db.DSTheLoai.Find(txtTheLoaiID.Text);
                theLoai.TenTheLoai = txtTenTheLoai.Text;
                db.SaveChanges();
                dataGirdDSTL.ItemsSource = db.DSTheLoai.ToList();
                butSuaTL.IsEnabled = butXoaTL.IsEnabled = false;

            }
        }

        private void ButThemTL_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MyEntity())
            {
                var theLoai = new TheLoai { TheLoaiID = txtTheLoaiID.Text, TenTheLoai = txtTenTheLoai.Text };
                db.DSTheLoai.Add(theLoai);
                db.SaveChanges();
                dataGirdDSTL.ItemsSource = db.DSTheLoai.ToList();
                butThemTL.IsEnabled = false;
            }
        }

        private void TxtTenTheLoai_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtTenTheLoai.Text != "")
            {
                butThemTL.IsEnabled = true;
            }
        }

        private void DataGirdDSTL_MouseUp(object sender, MouseButtonEventArgs e)
        {
            using (var db = new MyEntity())
            {
                if (db.DSTheLoai.Count() > 0 && dataGirdDSTL.SelectedIndex < db.DSTheLoai.Count())
                {
                    var theLoai = (TheLoai)dataGirdDSTL.SelectedItem;
                    txtTheLoaiID.Text = theLoai.TheLoaiID;
                    txtTenTheLoai.Text = theLoai.TenTheLoai;
                    butSuaTL.IsEnabled = butXoaTL.IsEnabled = true;
                    butThemTL.IsEnabled = false;
                }
            }
        }

        private void ButThoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
