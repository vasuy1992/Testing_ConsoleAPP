using Testing_ConsoleAPP.DTOs;

namespace Testing_ConsoleAPP
{
    public interface IOfficeRepository
    {
        public bool CreateEmployee(CreateEmployeeDto createEmployeeDto);
        public bool UpdateEmployee(UpdateEmployeeDto updateEmployeeDto);
        public bool DeleteEmployee(int FId);
        public List<OfficeTableModel> GetAllEmployees();
        
    }
   
}
