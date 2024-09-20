using System.ComponentModel.DataAnnotations;

namespace PokeApi.Models
{
    public class ErrorLog
    {
        [Key] 
        public int LogId { get; set; }

        public int? Code { get; set; } 

        public string Message { get; set; }

        public DateTime EventDate { get; set; } = DateTime.Now;

        public string? UserName { get; set; }

        public string? Url { get; set; }
    }
}
