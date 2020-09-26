using System.Collections.Generic;

namespace MVCEmployeesApp.Classes
{
    public class Employees
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public List<Employees> lstEmployees { get; set; }
    }
}
