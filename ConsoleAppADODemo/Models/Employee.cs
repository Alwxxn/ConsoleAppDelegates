using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppADODemo.Models
{
    public  class Employee
    {
        // Fields / Properties

        public int Id { get; set; }             // Auto-generated & digits

        public string Name { get; set; }        // Validates employee name
                                                // (alphabets single space allowed,
                                                //  not starting with space)

        public DateTime DateOfJoin { get; set; } // Should be within last 5 days

        public decimal Salary { get; set; }      // Accepts only digits

        // Object Oriented Class Model
        public Department Dept { get; set; }

        public bool InService { get; set; }
    }
}
