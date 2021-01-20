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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool flagAdd = true;
        public MainWindow()
        {
            InitializeComponent();
            butSua.IsEnabled = butThem.IsEnabled = butXoa.IsEnabled = false;
            txtSachID.IsReadOnly = true;
            using (var db = new MyEntity())
            {
                dataGirdDSSach.ItemsSource = db.DSSach.ToList();
                cboTheLoai.ItemsSource = db.DSTheLoai.ToList();
                cboTheLoai.SelectedValuePath = "TheLoaiID";
                cboTheLoai.DisplayMemberPath = "TenTheLoai";
                cboTheLoai.SelectedIndex = 0;
                var c = db.DSSach.Count();
                var s = "";
                if (c > 0)
                {
                    var sTemp = db.DSSach.ToList().ElementAt(c - 1).SachID;
                    s = TienIch.TaoMaTuDong(sTemp, sTemp.Substring(2, 2));
                }
                else
                {
                    s = "SA01";
                }
                txtSachID.Text = s;
            }
        }

        private void ButThem_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MyEntity())
            {
                var SachSearch = db.DSSach.Find(txtSachID.Text);
                if (SachSearch != null)
                {
                    MessageBox.Show("Mã trùng! Mời nhập lại...");
                    txtSachID.Focus();
                    return;
                }
                var sach = new Sach { SachID = txtSachID.Text, TenSach = txtTenSach.Text, TacGia = txtTacGia.Text, SoLuong = txtSoLuong.Text,NamXB=txtNamXB.Text,NXB=txtNXB.Text, TheLoaiID = cboTheLoai.SelectedValue.ToString() };
                db.DSSach.Add(sach);
                db.SaveChanges();
                dataGirdDSSach.ItemsSource = db.DSSach.ToList();
                butThem.IsEnabled = false ;
            }
        }

        private void ButThoat_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButXoa_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Có muốn sửa?", "Sửa", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                using (var db = new MyEntity())
                {
                    var sach = db.DSSach.Find(txtSachID.Text);
                    if (sach != null)
                    {
                        db.DSSach.Remove(sach);
                        db.SaveChanges();
                        dataGirdDSSach.ItemsSource = db.DSSach.ToList();
                        butSua.IsEnabled = butXoa.IsEnabled = false;
                        flagAdd = true;
                    }
                    else
                        MessageBox.Show("Bạn chưa chọn sách");
                }
            }
            this.Close();
        }

        private void ButSua_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MyEntity())
            {
                var sach = db.DSSach.Find(txtSachID.Text);
                sach.SoLuong = txtSoLuong.Text;
                sach.TenSach = txtTenSach.Text;
                sach.TacGia = txtTacGia.Text;
                sach.NXB = txtNXB.Text;
                sach.NamXB = txtNamXB.Text;
                db.SaveChanges();
                dataGirdDSSach.ItemsSource = db.DSSach.ToList();

                butSua.IsEnabled = butXoa.IsEnabled = false;
                flagAdd = true;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Search objS = new Search();
            objS.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Có muốn thoát?", "Thoát", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                e.Cancel = true;
        }

        private void ButThemMoi_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new MyEntity())
            {
                dataGirdDSSach.ItemsSource = db.DSSach.ToList();
                cboTheLoai.ItemsSource = db.DSTheLoai.ToList();
                cboTheLoai.SelectedValuePath = "TheLoaiID";
                cboTheLoai.DisplayMemberPath = "TenTheLoai";
                var c = db.DSSach.Count();
                var s = "";
                if (c > 0)
                {
                    var sTemp = db.DSSach.ToList().ElementAt(c - 1).SachID;
                    s = TienIch.TaoMaTuDong(sTemp, sTemp.Substring(2, 2));
                }
                else
                {
                    s = "SA01";
                }
                txtSachID.Text = s;
            }
            txtTenSach.Text = txtTacGia.Text =txtSoLuong.Text= "";
            txtNamXB.Text = txtNXB.Text = "";
            txtTenSach.Focus();
            flagAdd = true;
            butSua.IsEnabled = butXoa.IsEnabled = false;
        }

        private void DataGirdDSSach_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (dataGirdDSSach.Items.Count > 0 && dataGirdDSSach.SelectedIndex < dataGirdDSSach.Items.Count - 1)
            {
                var sach = (Sach)dataGirdDSSach.SelectedItem;
                if (sach != null)
                {
                    flagAdd = false;

                    txtSachID.Text = sach.SachID;
                    txtTenSach.Text = sach.TenSach;
                    txtTacGia.Text = sach.TacGia;
                    txtSoLuong.Text = sach.SoLuong;
                    txtNXB.Text = sach.NXB;
                    txtNamXB.Text = sach.NamXB;
                    butSua.IsEnabled = butXoa.IsEnabled = true;
                    butThem.IsEnabled = false;

                }
            }
        }

        private void TxtSachID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSachID.Text != "" && txtTenSach.Text != "" && txtTacGia.Text != "" && txtSoLuong.Text != "" && txtNXB.Text!="" && txtNamXB.Text!="" && flagAdd == true)
            { butThem.IsEnabled = true; }
        }

        private void ButThemTheLoai_Click(object sender, RoutedEventArgs e)
        {
            TheLoaiView objTLView = new TheLoaiView();
            objTLView.ShowDialog();
            using (var db = new MyEntity())
            {
                cboTheLoai.SelectedValuePath = "TheLoaiID";
            }
        }

        private void ThemTheLoai_Click(object sender, RoutedEventArgs e)
        {
            TheLoaiView objThemTL = new TheLoaiView();
            objThemTL.ShowDialog();
        }

        private void MnuTimDSSTheoTenSach_Click(object sender, RoutedEventArgs e)
        {
            TimKiemTheoTenSach objtheoTenSach = new TimKiemTheoTenSach();
            objtheoTenSach.ShowDialog();
        }

        private void MnuTimDSSTheoTenTacGia_Click(object sender, RoutedEventArgs e)
        {
            TimKiemTheoTenTacGia objtheoTenTacGia = new TimKiemTheoTenTacGia();
            objtheoTenTacGia.ShowDialog();
        }
    }
}