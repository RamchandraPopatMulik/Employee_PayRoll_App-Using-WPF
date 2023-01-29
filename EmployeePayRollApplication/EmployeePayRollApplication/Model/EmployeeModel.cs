using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePayRollApplication.Model
{
    public class EmployeeModel
    {
        public int empId { get; set; }
        public string? name { get; set; }
        public string? profile { get; set; }
        public String? Gender { get; set; }
        public string? Department { get; set; }
        public string? Salary { get; set; }
        public string? Start_Date { get; set; }    
        public string? Notes { get; set; }
    }
}
