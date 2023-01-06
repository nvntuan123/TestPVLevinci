namespace WebAPI_Levinci.Models
{
    public class ServiceResponse<T>
    {
        public T? Data { get; set; }
        public bool bSuccess { get; set; } = true;
        public string strMessage { get; set; } = string.Empty;
    }
}
