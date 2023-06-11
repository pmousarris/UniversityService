using ModelUtil.Entities.UnicBase;
using Newtonsoft.Json;

namespace UnicService.ViewModels.Class
{
    public class Class_SectionUserVM
    {
        [JsonProperty]
        public Guid Id { get; private set; }
        [JsonProperty]
        public string FirstName { get; private set; }
        [JsonProperty]
        public string LastName { get; private set; }
        [JsonProperty]
        public string Email { get; private set; }
        [JsonProperty]
        public string Phone1 { get; private set; }

        public Class_SectionUserVM(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Phone1 = user.Phone1;
        }
    }
}
