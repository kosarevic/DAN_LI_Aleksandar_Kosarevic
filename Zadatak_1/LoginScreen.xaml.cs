using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Shapes;
using Zadatak_1.Model;

namespace Zadatak_1
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        public static User CurrentUser = new User();

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser = new User();
            SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
            try
            {
                //User is extracted from the database matching inserted paramaters Username and Password.
                SqlCommand query = new SqlCommand("SELECT * FROM tblUser WHERE Username=@Username AND Password=@Password", sqlCon);
                query.CommandType = CommandType.Text;
                query.Parameters.AddWithValue("@Username", txtUsername.Text);
                query.Parameters.AddWithValue("@Password", txtPassword.Password);
                sqlCon.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                CurrentUser = new User();

                foreach (DataRow row in dataTable.Rows)
                {
                    CurrentUser = new User
                    {
                        Id = int.Parse(row[0].ToString()),
                        Username = row[1].ToString(),
                        Password = row[2].ToString()
                    };
                }

                if (CurrentUser != null)
                {
                    UserWindow window = new UserWindow(CurrentUser);
                    window.Show();
                    Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }
        }
    }
}
