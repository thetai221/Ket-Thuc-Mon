using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace Quanli
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void hienthi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<phongban> DanhSachSinhVien = new List<phongban>();
                using (SqlConnection connection =
                new SqlConnection(@"Server=DESKTOP-27EV1LA\SQLEXPRESS; Database=quanlicanbo;
                                   Integrated Security=SSPI"))
                using (SqlCommand command =
                new SqlCommand("SELECT phongbanID, tenphongban FROM phongban; ", connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var sv = new phongban();
                            sv.phongbanID = reader.GetString(0);
                            sv.tenphongban = reader.GetString(1);
                            DanhSachSinhVien.Add(sv);
                        }
                    }
                }
                MessageBox.Show("Mo va dong co so du lieu thanh cong.");
                dulieu.ItemsSource = DanhSachSinhVien;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi mo ket noi: " + ex.Message);
            }
        }

        private void Them_Click(object sender, RoutedEventArgs e)
        {
            phongban sv = new phongban();
            sv.phongbanID = txtID.Text;
            sv.tenphongban = txtTen.Text;
            if (Them_phongban(sv) > 0)
                MessageBox.Show("Du lieu duoc them thanh cong!");
        }
        private int Them_phongban(phongban Phongban)
        {
            try
            {
                using (SqlConnection connection =
                new SqlConnection(@"Server=DESKTOP-27EV1LA\SQLEXPRESS; Database=quanlicanbo;
                                   Integrated Security=SSPI"))
                using (SqlCommand command = new SqlCommand("INSERT INTO phongban(phongbanID,tenphongban) " + "VALUES(@phongbanID,@tenphongban);", connection))
                {
                    command.Parameters.Add("phongbanID", SqlDbType.NVarChar, 20).Value =
                    Phongban.phongbanID;
                    object dbTenSV = Phongban.tenphongban;
                    if (dbTenSV == null)
                    {
                        dbTenSV = DBNull.Value;
                    }
                    command.Parameters.Add("tenphongban", SqlDbType.NVarChar, 50).Value =
                    dbTenSV;
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi mo ket noi:" + ex.Message);
                return -1;
            }
        }

        private void Xoa_Click(object sender, RoutedEventArgs e)
        {
            phongban sv = new phongban();
            sv.phongbanID = txtID.Text;
            if (Xoa_phongban(sv) > 0)
                MessageBox.Show("Du lieu duoc xoa thanh cong!");
        }
        private int Xoa_phongban(phongban Phongban)
        {
            try
            {
                using (SqlConnection connection =
                new SqlConnection(@"Server=DESKTOP-27EV1LA\SQLEXPRESS; Database=quanlicanbo;
                                   Integrated Security=SSPI"))
                using (SqlCommand command = new SqlCommand("DELETE FROM phongban " + "WHERE phongbanID = @phongbanID", connection))
                {
                    command.Parameters.Add("phongbanID", SqlDbType.NVarChar,
                20).Value = Phongban.phongbanID;
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi mo ket noi:" + ex.Message);
                return -1;
            }
        }

        private void Capnhap_Click(object sender, RoutedEventArgs e)
        {
            phongban sv = new phongban();
            sv.phongbanID = txtID.Text;
            sv.tenphongban = txtTen.Text;
            if (Cap_nhap_phongban(sv) > 0)
                MessageBox.Show("Du lieu duoc cap nhat thanh cong!");
        }
        private int Cap_nhap_phongban(phongban Phongban)
        {
            try
            {
                using (SqlConnection connection =
                new SqlConnection(@"Server=DESKTOP-27EV1LA\SQLEXPRESS; Database=quanlicanbo;
                                   Integrated Security=SSPI"))
                using (SqlCommand command = new SqlCommand("UPDATE phongban " + "SET tenphongban = @tenphongban " 
                                                                + "WHERE phongbanID = @phongbanID", connection))
                {
                    command.Parameters.Add("phongbanID", SqlDbType.NVarChar, 20).Value =
                    Phongban.phongbanID;
                    command.Parameters.Add("tenphongban", SqlDbType.NVarChar, 50).Value =
                    Phongban.tenphongban;
                    connection.Open();
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi khi mo ket noi:" + ex.Message);
                return -1;
            }
        }

        private void Thoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
