using Newtonsoft.Json;

namespace UnicService.ViewModels.Class
{
    public class ClassVM : BaseVM<int>
    {
        [JsonProperty]
        public string ClassNumber { get; set; }
        [JsonProperty]
        public Class_CourseVM Course { get; set; }
        [JsonProperty]
        public Class_AcademicPeriodVM AcademicPeriod { get; set; }
        [JsonProperty]
        public int? NumberOfStudents { get; set; }

        public ClassVM(int sectionId, string sectionNumber, Class_CourseVM course, Class_AcademicPeriodVM academicPeriod, int? numberOfStudents)
        {
            Id = sectionId;
            ClassNumber = sectionNumber;
            Course = course;
            AcademicPeriod = academicPeriod;
            NumberOfStudents = numberOfStudents;
        }
    }
}
