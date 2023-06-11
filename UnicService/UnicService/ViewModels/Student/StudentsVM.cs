using Newtonsoft.Json;

namespace UnicService.ViewModels.Student
{
    public class StudentsVM
    {
        [JsonProperty]
        public List<StudentVM> Students { get; set; }
        public int TotalRecords { get; set; }

        public StudentsVM()
        {
            Students = new List<StudentVM>();
        }

        public StudentsVM(List<StudentVM> students, int totalRecords)
            : base()
        {
            Students = students;
            TotalRecords = totalRecords;
        }
    }
}
