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
using WpfApp1;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        public Search()
        {
            InitializeComponent();
            using (var db = new MyEntity())
            {
                dataGirdDSSach.ItemsSource = db.DSSach.ToList();
                cboSearch.ItemsSource = db.DSTheLoai.ToList();
                cboSearch.SelectedValuePath = "TheLoaiID";
                cboSearch.DisplayMemberPath = "TenSach";

            }
        }
        private void CboSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var db = new MyEntity())
            {
                var query = from p in db.DSSach
                            where p.TheLoaiID == cboSearch.SelectedValue.ToString()
                            select p;
                dataGirdDSSach.ItemsSource = query.ToList();
            }
        }
    } 
}

