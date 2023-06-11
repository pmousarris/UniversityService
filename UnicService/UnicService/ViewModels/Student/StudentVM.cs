using Newtonsoft.Json;

namespace UnicService.ViewModels.Student
{
    public class StudentVM : BaseVM<Guid>
    {
        [JsonProperty]
        public string FirstName { get; private set; }
        [JsonProperty]
        public string LastName { get; private set; }
        [JsonProperty]
        public string Phone1 { get; private set; }
        [JsonProperty]
        public string Email { get; private set; }

        public StudentVM(Guid userId, string firstName, string lastName, string phone1 = null, string email = null)
            : base(id: userId)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone1 = phone1;
            Email = email;
        }
    }
}
