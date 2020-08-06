using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zadatak_1.Model;

namespace Zadatak_1.Validation
{
    static class AddPatientValidation
    {
        //Static variables made to store usefull data after validation.
        public static string dateOfBirth = "";
        public static string expirationDate = "";
        public static string registrationNumber = "";
        public static string dateOfIssue = "";

        public static bool Validate(Patient e)
        {
            bool cancel = false;
            while (true)
            {
                //name validation is realied below.
                while (true)
                {
                    if (e.FirstName.Length > 0 && e.FirstName.All(Char.IsLetter) && e.FirstName != null && e.FirstName != "")
                    {
                        break;
                    }
                    else
                    {
                        MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("incorrect first name, try again.", "Notification");
                        cancel = true;
                        break;
                    }
                }
                if (cancel) return false;
                //last name validation is realised here.
                while (true)
                {
                    if (e.LastName.Length > 0 && e.LastName.All(Char.IsLetter) && e.LastName != null && e.LastName != "")
                    {
                        break;
                    }
                    else
                    {
                        MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("incorrect last name, try again.", "Notification");
                        cancel = true;
                        break;
                    }
                }
                if (cancel) return false;
                //JMBG validation starts here.
                string day = "";
                string month = "";
                string year = "";

                while (true)
                {
                    DateTime correct = new DateTime();

                    if (e.JMBG.Length == 13 && e.JMBG.All(Char.IsDigit) && e.JMBG != null && e.JMBG != "")
                    {
                        //Validation for checking duplicate JMBG in database.
                        using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
                        {
                            var cmd = new SqlCommand(@"select JMBG from tblEmploye where JMBG = @JMBG", conn);
                            cmd.Parameters.AddWithValue("@JMBG", e.JMBG);
                            conn.Open();
                            SqlDataReader reader1 = cmd.ExecuteReader();
                            while (reader1.Read())
                            {
                                if (reader1[0].ToString() == e.JMBG)
                                {
                                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("JMBG already exists in database, try again.", "Notification");
                                    cancel = true;
                                    break;
                                }
                            }
                            reader1.Close();
                            conn.Close();
                        }
                        using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
                        {
                            var cmd = new SqlCommand(@"select JMBG from tblManager where JMBG = @JMBG", conn);
                            cmd.Parameters.AddWithValue("@JMBG", e.JMBG);
                            conn.Open();
                            SqlDataReader reader1 = cmd.ExecuteReader();
                            while (reader1.Read())
                            {
                                if (reader1[0].ToString() == e.JMBG)
                                {
                                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("JMBG already exists in database, try again.", "Notification");
                                    cancel = true;
                                    break;
                                }
                            }
                            reader1.Close();
                            conn.Close();
                        }
                        //if there is a duplicate, method stops further execution.
                        if (cancel) { break; }
                        //creating date of birth of user is realised below.
                        day = e.JMBG[0].ToString() + e.JMBG[1].ToString();
                        month = e.JMBG[2].ToString() + e.JMBG[3].ToString();
                        year = e.JMBG[4].ToString() + e.JMBG[5].ToString() + e.JMBG[6].ToString();

                        if (int.Parse(year) <= 99)
                        {
                            year = "2" + year;
                        }
                        else
                        {
                            year = "1" + year;
                        }

                        dateOfBirth = year + "-" + month + "-" + day;
                        dateOfIssue = Convert.ToString(DateTime.Now.ToString("yyyy-MM-dd"));
                        //validation if date of birth is in correct format and value.
                        if (!DateTime.TryParse(dateOfBirth, out correct))
                        {
                            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("JMBG is not correct, due to incorrect date of birth, please try again.", "Notification");
                            cancel = true;
                            break;
                        }
                        if (cancel) break;
                    }
                    else
                    {
                        MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("JMBG input is not correct, try again.", "Notification");
                        cancel = true;
                        break;
                    }
                    if (cancel) { break; }

                    //Validations consernig date of birth are realised below.
                    if (int.Parse(year) > int.Parse(DateTime.Now.ToString("yyyy")))
                    {
                        MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Date of birth suggests that you are born in the future, please try again.", "Notification");
                        cancel = true;
                        break;
                    }
                    else if (int.Parse(DateTime.Now.ToString("yyyy")) - int.Parse(year) > 100)
                    {
                        MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Your date of birth suggests that you lived longer than 100 years, please try again.", "Notification");
                        cancel = true;
                        break;
                    }
                    else if (int.Parse(DateTime.Now.ToString("yyyy")) - int.Parse(year) < 18)
                    {
                        MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Your date of birth suggests are under aged (less than 18 y/o), please try again.", "Notification");
                        cancel = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                if (cancel) { break; }

                if (e.Username == null || e.Username == "")
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Username is incorrect, please try again.", "Notification");
                    cancel = true;
                    break;
                }
                else if (e.Password == null || e.Password == "")
                {
                    MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Password is incorrect, please try again.", "Notification");
                    cancel = true;
                    break;
                }
                break;
            }
            if (!cancel)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
