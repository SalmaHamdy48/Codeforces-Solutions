namespace ApiUserStory.Models
{
    public class SessionToken
    {
        public string UserId { get; set; } = "";
        public DateTime Expiration { get; set; }
    }
}