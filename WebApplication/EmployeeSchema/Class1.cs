using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.EmployeeSchema
{
    public class Class1
    {
        private string _Name;
        private string _PhoneNumber;
        private int _Age;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        public string PhoneNumber
        {
            get
            {
                return _PhoneNumber;
            }
            set
            {
                _PhoneNumber = value;
            }
        }
        public int Age
        {
            get
            {
                return _Age;
            }
            set
            {
                _Age = value;
            }
        }
    }
}