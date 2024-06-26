namespace Testing_ConsoleAPP
{
    public class OfficeTableModel
    {
        public int FId { get; set; }
        public string Name { get; set; } 
        public string Department { get; set; }

        public void PrintDetails()
        {
            Console.WriteLine($"Id: {FId}, Name: {Name},Department: {Department}");
        }
    }

}
