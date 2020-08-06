using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1.Model
{
    public class Patient : User
    {

        public string CardNumber { get; set; }
        public int DoctorId { get; set; }

        public Patient() : base()
        {
        }
    }
}
