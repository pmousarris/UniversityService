namespace UnicService.ViewModels.Login
{
    public class UserLoginParams
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public bool AreAllPropertiesNotNull()
        {
            return !(String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(Password));
        }
    }
}
