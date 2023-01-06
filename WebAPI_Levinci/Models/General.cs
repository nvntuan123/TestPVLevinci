namespace WebAPI_Levinci.Models
{
    public class General
    {
        public class ResponMessages
        {
            public int? Status { get; set; }
            public string? messages { get; set; }
            public object? Data { get; set; }
            public string? accesstoken { get; set; }
            public string? refreshtoken { get; set; }
        }

        public class UserRefreshToken
        {
            public string? User { get; set; }
            public string? token { get; set; }
        }
    }
}
