using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApp {
    public static class Helper {
        public static string GetHash (string input) {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider ();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes (input);

            byte[] byteHash = hashAlgorithm.ComputeHash (byteValue);

            return Convert.ToBase64String (byteHash);
        }

        public static string JsonSerializeObject (this object obj) {
            if (obj == null)
                return string.Empty;
            return JsonConvert.SerializeObject (obj);
        }

        public static T JsonDeserializeObject<T> (this string strObj) {
            strObj = strObj ?? "";
            return JsonConvert.DeserializeObject<T> (strObj);
        }

        public static T JsonDeserializeObject<T> (this JObject jObj) {
            return JsonConvert.DeserializeObject<T> (jObj.ToString ());
        }

        public static DateTime ToDateTime (this string strDate) {
            try {
                return DateTime.Parse (strDate);
            } catch (FormatException) {
                throw new Exception ($"{strDate} is not in the correct format.");
            }
        }

        public static DateTime ToDateTime (this object strDate) {
            return strDate.ToString ().ToDateTime ();
        }

        public static DateTime ToDateTime (this string strDate, string formart) {
            try {
                CultureInfo provider = CultureInfo.InvariantCulture;
                return DateTime.ParseExact (strDate, formart, provider);
            } catch (FormatException) {
                throw new Exception ($"{strDate} is not in the correct format.");
            }
        }

        public static string ToCodeDisplay (this Enum e) {
            var type = e.GetType ();
            return $"{type.Name}_{e}".ToUpper ();
        }

        public static int ToInt (this string str) {
            return int.Parse (str);
        }

        public static int ToInt (this object obj) {
            return obj.ToString ().ToInt ();
        }

        public static bool IsNumber (this string str) {
            str = str.Trim ();
            return decimal.TryParse (str, out decimal outPut);
        }

        public static string RemoveUnicodeAndSpace (this string str) {
            return Regex.Replace (str, @"\s+", "").convertToUnSign3 ().Trim ();
        }

        public static string convertToUnSign3 (this string s) {
            Regex regex = new Regex ("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize (NormalizationForm.FormD);
            return regex.Replace (temp, String.Empty).Replace ('\u0111', 'd').Replace ('\u0110', 'D');
        }

    }
}