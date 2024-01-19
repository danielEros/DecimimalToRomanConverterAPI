using System.Text.RegularExpressions;
using DecimimalToRomanConverterAPI.DTOs;

namespace DecimimalToRomanConverterAPI.Converters
{
    public class RomanNumberConverter : IRomanNumberConverter
    {
        private const string REGEX_PATTERN_ROMAN_LETTERS = @"^[IVXLCDM]+$";
        private const string REGEX_PATTERN_ROMAN_NUMBER = @"^M{0,3}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$";
        
        public const string ERROR_MESSAGE_TOO_LONG_INPUT = "The input string is too long! The maximal length is {0}";
        public const string ERROR_MESSAGE_NON_ROMAN_CHARACTERS = "The input string ('{0}') contains non-roman numer characters! (Only 'I', 'V', 'X', 'L', 'C', 'D' and 'M' characters are allowed).";
        public const string ERROR_MESSAGE_INVALID_ROMAN_NUMBER = "The input string ('{0}') is not a valid roman number!";
        public const int MAX_INPUT_LENGTH_ROMAN_NUMBER = 15;

        private Dictionary<char, int> _RomanNumberDict = new Dictionary<char, int>
        {
            {'I', 1}, {'V', 5}, {'X', 10}, {'L', 50}, {'C', 100}, {'D', 500}, {'M', 1000}
        };

        private string GetValidationErrorMessage ( string input )
        {
            string errorMessage = string.Empty;
                        
            if ( input.Length > MAX_INPUT_LENGTH_ROMAN_NUMBER )
            {
                errorMessage = string.Format ( ERROR_MESSAGE_TOO_LONG_INPUT , MAX_INPUT_LENGTH_ROMAN_NUMBER );
            }
            else if ( !Regex.IsMatch ( input , REGEX_PATTERN_ROMAN_LETTERS ) )
            {
                errorMessage = string.Format( ERROR_MESSAGE_NON_ROMAN_CHARACTERS, input);
            }
            else if( !Regex.IsMatch(input, REGEX_PATTERN_ROMAN_NUMBER ) )
            {
                errorMessage = string.Format ( ERROR_MESSAGE_INVALID_ROMAN_NUMBER , input ); ;
            }

            return errorMessage;
        }

        public RomanToDecimalDTO RomanToDecimal ( string romanNumber )
        {
            RomanToDecimalDTO result = new RomanToDecimalDTO();
            result.ErrorMessage = GetValidationErrorMessage ( romanNumber );

            if ( string.IsNullOrEmpty( result.ErrorMessage ) )
            {
                int convertedNumber = 0;
                for ( int i = 0 ; i < romanNumber.Length ; i++ )
                {
                    // if this is the last number or if the current number is larger or equal to the next one: add
                    if ( i == romanNumber.Length - 1 || _RomanNumberDict [ romanNumber [ i ] ] >= _RomanNumberDict [ romanNumber [ i + 1 ] ] )
                    {
                        convertedNumber += _RomanNumberDict [ romanNumber [ i ] ];
                    }
                    // if the current number is smaller than the next one: subtract
                    else
                    {
                        convertedNumber -= _RomanNumberDict [ romanNumber [ i ] ];
                    }
                }
                result.Result = convertedNumber;
            }
            
            return result;
        }
    }
}
