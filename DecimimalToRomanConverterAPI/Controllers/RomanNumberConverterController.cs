using DecimimalToRomanConverterAPI.Converters;
using DecimimalToRomanConverterAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace DecimimalToRomanConverterAPI.Controllers
{
    [ApiController]
    [Route ( "[controller]" )]
    public class RomanNumberConverterController : ControllerBase
    {
        private readonly IRomanNumberConverter _RomanNumberConverter;
        private readonly ILogger<RomanNumberConverterController> _Logger;

        public RomanNumberConverterController ( IRomanNumberConverter romanNumberConverter, ILogger<RomanNumberConverterController> logger )
        {
            _RomanNumberConverter = romanNumberConverter;
            _Logger = logger;
        }

        /// <summary>
        /// Converts the provided roman number to decimal number
        /// </summary>
        /// <param name="input">The roman number to be converted</param>
        /// <remarks>
        /// Sample request:
        ///     GET romantodecimal/romantodecimal?RomanNumber=VII
        ///     
        /// Sample result:
        ///     {
        ///         "Result": 7,
        ///         "ErrorMeassage": ""
        ///     }
        ///  
        /// Limitations:
        ///     The input roman number shuold be between 1 and 3999.
        ///     Consequelently the maximum length of the input string is 15 (the longest roman number in the above range is 3888
        ///     (MMMDCCCLXXXVIII) with character length of 15.
        ///     The input should only contain the following letters: I, V, X, L, C, D, M (case sensitive).
        ///     The input should be a valid roman numnber (i.e. IIV is invalid).
        /// </remarks> 
        /// <returns></returns>
        /// <response code="200">Returns the converted decimal number</response>
        /// <response code="400">If the input srting is not a valid roman number</response>
        /// <response code="500">In case of an unexpected server error</response>
        [HttpGet ( "romantodecimal" )]
        [Produces( "application/json" )]
        [ProducesResponseType ( 200 )]
        [ProducesResponseType ( 400 )]
        [ProducesResponseType ( 500 )]
        public IActionResult ConvertRomanToDecimal ( [FromQuery] RequestDTO input )
        {
            _Logger.LogTrace ($"{nameof( ConvertRomanToDecimal )} was called with the input of '{input}'.");
            
            RomanToDecimalDTO result = new RomanToDecimalDTO();
            try
            {
                result = _RomanNumberConverter.RomanToDecimal ( input.RomanNumber );
                if ( !string.IsNullOrEmpty( result.ErrorMessage ) )
                {
                    _Logger.LogError ( result.ErrorMessage );
                    return BadRequest ( result );
                }
                
                _Logger.LogTrace ( $"{nameof ( ConvertRomanToDecimal )} returns '{result.Result}'." );
                return Ok ( result );
            }
            catch (Exception ex)
            {
                result.ErrorMessage = "An unexpected error occured during processing the request, please consult the system administrator!";
                _Logger.LogError ( ex.Message );
                return StatusCode ( 500 , result );
            }   
        }
    }
}