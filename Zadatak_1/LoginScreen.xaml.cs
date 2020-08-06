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

        public static Patient CurrentUserPatient = new Patient();
        public static Doctor CurrentUserDoctor = new Doctor();

        private void BtnLogin(object sender, RoutedEventArgs e)
        {
            CurrentUserPatient = null;

            byte[] data = System.Text.Encoding.ASCII.GetBytes(txtPassword.Password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);

            SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
            //User is extracted from the database matching inserted paramaters Username and Password.
            SqlCommand query = new SqlCommand("SELECT * FROM tblPatient WHERE Username=@Username AND Password=@Password", sqlCon);
            query.CommandType = CommandType.Text;
            query.Parameters.AddWithValue("@Username", txtUsername.Text);
            query.Parameters.AddWithValue("@Password", hash);
            sqlCon.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                CurrentUserPatient = new Patient
                {
                    Id = int.Parse(row[0].ToString()),
                    FirstName = row[1].ToString(),
                    LastName = row[2].ToString(),
                    JMBG = row[3].ToString(),
                    Username = row[4].ToString(),
                    Password = row[5].ToString(),
                    CardNumber = row[6].ToString()
                };
                if (row[7] != null)
                    CurrentUserPatient.DoctorId = int.Parse(row[7].ToString());
            }

            sqlCon.Close();

            if (CurrentUserPatient == null)
            {
                CurrentUserDoctor = null;

                data = System.Text.Encoding.ASCII.GetBytes(txtPassword.Password);
                data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
                hash = System.Text.Encoding.ASCII.GetString(data);

                sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString());
                //User is extracted from the database matching inserted paramaters Username and Password.
                query = new SqlCommand("SELECT * FROM tblDoctor WHERE Username=@Username AND Password=@Password", sqlCon);
                query.CommandType = CommandType.Text;
                query.Parameters.AddWithValue("@Username", txtUsername.Text);
                query.Parameters.AddWithValue("@Password", hash);
                sqlCon.Open();
                sqlDataAdapter = new SqlDataAdapter(query);
                dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    CurrentUserDoctor = new Doctor
                    {
                        Id = int.Parse(row[0].ToString()),
                        FirstName = row[1].ToString(),
                        LastName = row[2].ToString(),
                        JMBG = row[3].ToString(),
                        Username = row[4].ToString(),
                        Password = row[5].ToString(),
                        Account = row[6].ToString()
                    };
                }
            }

            if (CurrentUserPatient != null)
            {
                if (CurrentUserPatient.DoctorId != 0)
                {
                    PatientWindow window = new PatientWindow();
                    window.Show();
                    Close();
                    return;
                }
                else
                {
                    ChoseDoctorWindow window = new ChoseDoctorWindow();
                    window.Show();
                    Close();
                    return;
                }
            }
            else if (CurrentUserDoctor != null)
            {
                DoctorWindow window = new DoctorWindow();
                window.Show();
                Close();
                return;
            }
            else
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Incorrect login credentials, please try again.", "Notification");
            }
        }

        private void BtnRegister(object sender, RoutedEventArgs e)
        {
            RegistrationOption window = new RegistrationOption();
            window.Show();
            Close();
        }
    }
}
