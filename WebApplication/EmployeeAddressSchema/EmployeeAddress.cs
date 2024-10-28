using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.EmployeeAddressSchema
{
    public class EmployeeAddress
    {
        public int EmpAddressId { get; set; }
        public int Id { get; set; } 
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string City { get; set; }
        public string PinCode { get; set; }
    }
}