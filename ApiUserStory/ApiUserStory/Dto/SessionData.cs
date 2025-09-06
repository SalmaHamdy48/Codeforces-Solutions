namespace ApiUserStory.Dto
{
    
    public class SessionData
    {
        public string Email { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}