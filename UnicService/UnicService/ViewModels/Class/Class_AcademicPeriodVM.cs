using ModelUtil.Entities.UnicBase;
using Newtonsoft.Json;

namespace UnicService.ViewModels.Class
{
    public class Class_AcademicPeriodVM
    {
        [JsonProperty]
        public int Id { get; private set; }
        [JsonProperty]
        public string Name { get; private set; }
        [JsonProperty]
        public DateTime StartDate { get; private set; }
        [JsonProperty]
        public DateTime EndDate { get; private set; }

        public Class_AcademicPeriodVM(AcademicPeriod academicPeriod)
        {
            Id = academicPeriod.Id;
            Name = academicPeriod.Name;
            StartDate = academicPeriod.StartDate;
            EndDate = academicPeriod.EndDate;
        }
    }
}
