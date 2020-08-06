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
using Zadatak_1.ViewModel;

namespace Zadatak_1
{
    /// <summary>
    /// Interaction logic for ChoseDoctorWindow.xaml
    /// </summary>
    public partial class ChoseDoctorWindow : Window
    {
        PatientViewModel pvm = new PatientViewModel();

        public ChoseDoctorWindow()
        {
            InitializeComponent();
            DataContext = pvm;
        }

        private void AssignDoctor(object sender, RoutedEventArgs e)
        {
            if (Doctor.Text != "")
            {
                pvm.UpdatePatientDoctor();
                PatientWindow window = new PatientWindow();
                window.Show();
                Close();
            }
            else
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("You must chose doctor before continuing.", "Notification");
            }
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            LoginScreen window = new LoginScreen();
            window.Show();
            Close();
        }
    }
}
