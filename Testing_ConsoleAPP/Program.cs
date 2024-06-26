using System.Data.SqlClient;
using Testing_ConsoleAPP.DTOs;

namespace Testing_ConsoleAPP
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            Runner runner=new Runner();
            runner.Execute();
        }

    }

    public class Runner
    {
        public IOfficeRepository officeRepository;
        public Runner()
        {
            officeRepository =new OfficeRepository();
        }
        public void Execute()
        {
            //CreateEmployeeDto createEmployeeDto= SetEmployee();
            UpdateEmployeeDto updateEmployeeDto = SetUpdateEmployeeDto();
            IOfficeRepository officeRepository = new OfficeRepository();

            officeRepository.GetAllEmployees();

            bool ans = officeRepository.UpdateEmployee(updateEmployeeDto);
            Console.WriteLine(ans);
            //officeRepository.CreateEmployee(createEmployeeDto);

            officeRepository.GetAllEmployees();
        }

        public CreateEmployeeDto SetEmployee()
        {
            return new CreateEmployeeDto()
            {
                FId = 107,
                Name = "raghav",
                Department="IT"
                
            };
        }
        public UpdateEmployeeDto SetUpdateEmployeeDto()
        {
            return new UpdateEmployeeDto()
            {
                FId = 106,
                Name =null,
                Department = "New Department"
            };
        }

    }
}