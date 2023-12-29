using System.Globalization;
using System.Runtime.Intrinsics.Arm;

namespace Bussiness.Helper
{
    public class StringUtility
    {
        public static string GenerateRandomString(int length)
        {
            int asciiStart = 33;
            int asciiEnd = 126;
            char[] randomArray = new char[length];
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                int rd;
                do
                {
                    rd = random.Next(asciiStart, asciiEnd + 1);
                    randomArray[i] = (char)rd;
                } while (rd == 39);
            }
            string randomString = new string(randomArray);
            return randomString;
        }
        public static string CapitalizeFirstLetter(string input)
        {
            // Check for null or empty input
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            // Capitalize the first letter using CultureInfo
            string firstLetter = input.Substring(0, 1);
            string restOfString = input.Substring(1);

            string capitalizedString = firstLetter.ToUpper(CultureInfo.CurrentCulture) + restOfString;

            return capitalizedString;
        }
    }
}