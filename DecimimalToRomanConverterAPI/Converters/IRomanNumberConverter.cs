using DecimimalToRomanConverterAPI.DTOs;

namespace DecimimalToRomanConverterAPI.Converters
{
    public interface IRomanNumberConverter
    {
        /// <summary>
        /// Converts a roman number to decimal
        /// </summary>
        /// <param name="romanNumber">The roman number to be converted</param>
        /// <returns>RomanToDecimalDTO with the converted number or the error message</returns>
        RomanToDecimalDTO RomanToDecimal ( string romanNumber );
    }
}
