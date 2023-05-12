namespace Jolib.Models
{
    public class ApiResponse
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public object? Data { get; set; } 
        public ApiResponse() { }
    }
}
