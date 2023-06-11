using ModelUtil.Entities.UnicBase;
using Newtonsoft.Json;

namespace UnicService.ViewModels.Class
{
    public class Class_LecturersVM : BaseVM<int>
    {
        [JsonProperty]
        public string ClassNumber { get; private set; }
        [JsonProperty]
        public Class_CourseVM Course { get; private set; }
        [JsonProperty]
        public Class_AcademicPeriodVM AcademicPeriod { get; private set; }
        [JsonProperty]
        public List<Class_SectionUserVM> Lecturers { get; private set; }

        public Class_LecturersVM(int sectionId, string sectionNumber, Class_CourseVM course, Class_AcademicPeriodVM academicPeriod, List<Class_SectionUserVM> lecturers)
        {
            Id = sectionId;
            ClassNumber = sectionNumber;
            Course = course;
            AcademicPeriod = academicPeriod;
            Lecturers = lecturers;
        }
    }
}
