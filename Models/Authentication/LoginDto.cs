namespace DesignIntentDesktop.Models
{
    public class LoginDto
    {
        public string UserNameOrEmailAddress { get; set; }

        public string Password { get; set; }

        public bool RememberClient { get; set; } = true;
    }
}