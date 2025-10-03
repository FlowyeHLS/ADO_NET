using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainForm
{
    internal class Student
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MidleName { get; set; }
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Group { get; set; }
        public byte[] Photo { get; set; }
        public Student() { }
        public Student(string last_name, string first_name,string middle_name,string birth_date,string email,string phone)
        { 
            LastName = last_name;
            FirstName = first_name;
            MidleName = middle_name;
            BirthDate = birth_date;
            Email = email;
            Phone = phone;

        }



        public override string ToString()
        {
            return $"N'{LastName}',N'{FirstName}',N'{MidleName}',N'{Email}',N'{Phone}',N'{Group}'";
        }
    }
}
