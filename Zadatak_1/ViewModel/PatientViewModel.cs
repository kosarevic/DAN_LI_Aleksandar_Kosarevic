using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadatak_1.Model;

namespace Zadatak_1.ViewModel
{
    class PatientViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Doctor> Doctors { get; set; }

        public PatientViewModel()
        {
            FillList();
        }

        private Doctor doctor;

        public Doctor Doctor
        {
            get { return doctor; }
            set
            {
                if (doctor != value)
                {
                    doctor = value;
                    OnPropertyChanged("Doctor");
                }
            }
        }

        public void FillList()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                SqlCommand query = new SqlCommand("select * from tblDoctor", conn);
                conn.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                if (Doctors == null)
                    Doctors = new ObservableCollection<Doctor>();

                foreach (DataRow row in dataTable.Rows)
                {
                    Doctor d = new Doctor
                    {
                        Id = int.Parse(row[0].ToString()),
                        FirstName = row[1].ToString(),
                        LastName = row[2].ToString(),
                        JMBG = row[3].ToString(),
                        Username = row[4].ToString(),
                        Password = row[5].ToString(),
                        Account = row[6].ToString()
                    };
                    Doctors.Add(d);
                }
            }
        }

        public void UpdatePatientDoctor()
        {
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                var cmd = new SqlCommand(@"update tblPatient set DoctorID=@DoctorID where PatientID=@PatientID;", conn);
                cmd.Parameters.AddWithValue("@DoctorID", doctor.Id);
                cmd.Parameters.AddWithValue("@PatientID", LoginScreen.CurrentUserPatient.Id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Doctor succesfully assigned.", "Notification");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
