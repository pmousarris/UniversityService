using ModelUtil.Entities.UnicBase;
using Newtonsoft.Json;

namespace UnicService.ViewModels.Class
{
    public class Class_CourseVM
    {
        [JsonProperty]
        public int Id { get; private set; }
        [JsonProperty]
        public string Code { get; private set; }
        [JsonProperty]
        public string Title { get; private set; }

        public Class_CourseVM(Course course)
        {
            Id = course.Id;
            Code = course.Code;
            Title = course.Title;
        }
    }
}
