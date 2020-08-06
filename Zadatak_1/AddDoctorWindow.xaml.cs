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
using Zadatak_1.Validation;
using Zadatak_1.ViewModel;

namespace Zadatak_1
{
    /// <summary>
    /// Interaction logic for AddDoctorWindow.xaml
    /// </summary>
    public partial class AddDoctorWindow : Window
    {
        AddUserViewModel auvm = new AddUserViewModel();

        public AddDoctorWindow()
        {
            InitializeComponent();
            DataContext = auvm;
            auvm.Doctor.FirstName = "";
            auvm.Doctor.LastName = "";
            auvm.Doctor.JMBG = "";
            auvm.Doctor.Username = "";
            auvm.Doctor.Password = "";
            auvm.Doctor.Account = "";
        }

        private void Btn_Confirm(object sender, RoutedEventArgs e)
        {
            if (AddDoctorValidation.Validate(auvm.Doctor))
            {
                auvm.AddDoctor();
                LoginScreen window = new LoginScreen();
                window.Show();
                Close(); 
            }
        }

        private void Btn_Cancel(object sender, RoutedEventArgs e)
        {
            LoginScreen window = new LoginScreen();
            window.Show();
            Close();
        }
    }
}
