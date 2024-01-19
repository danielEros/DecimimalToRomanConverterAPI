using System.ComponentModel.DataAnnotations;

namespace DecimimalToRomanConverterAPI.DTOs
{
    public class RequestDTO
    {
        [Required]
        public string RomanNumber { get; set; }
    }
}
