using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.Models
{
    internal class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public DateTime Birthday { get; set; }

        public string Sex { get; set; }

        public bool HasChild { get; set; }
    }
}
