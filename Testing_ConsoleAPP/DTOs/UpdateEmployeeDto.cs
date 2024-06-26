using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing_ConsoleAPP.DTOs
{
    public class UpdateEmployeeDto
    {
        // int FId, string Name,string Department
        public int FId { get; set; }
        public string? Name { get; set; }
        public string? Department { get; set; }

    }
}
