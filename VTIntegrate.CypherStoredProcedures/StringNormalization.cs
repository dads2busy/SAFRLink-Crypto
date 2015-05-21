using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace EDU.VT.IPG.VTIntegrate.SchroederPadAndChaff
{
    class StringNormalization
    {

        public static string PlaceNameNormalize(string s)
        {
            string normalized = s.Normalize(NormalizationForm.FormD);
            normalized = normalized.ToUpper();

            StringBuilder resultBuilder = new StringBuilder();
            foreach (var character in normalized)
            {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(character);
                if (category == UnicodeCategory.UppercaseLetter
                    || category == UnicodeCategory.SpaceSeparator)
                    resultBuilder.Append(character);
            }
            string retString = resultBuilder.ToString().TrimStart();
            retString = Regex.Replace(retString, "^THE ", "");
            retString = Regex.Replace(retString, "^A ", "");
            retString = Regex.Replace(retString, @"\s", "");
            return retString;
        }

        public static string PlaceStreetNameNormalize(string s)
        {
            string normalized = s.Normalize(NormalizationForm.FormD);
            normalized = normalized.ToUpper();

            StringBuilder resultBuilder = new StringBuilder();
            foreach (var character in normalized)
            {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(character);
                if (category == UnicodeCategory.UppercaseLetter
                    || category == UnicodeCategory.DecimalDigitNumber
                    || category == UnicodeCategory.SpaceSeparator)
                    resultBuilder.Append(character);
            }
            string retString = resultBuilder.ToString().Trim();
            retString = Regex.Replace(retString, "^EAST ", "E");
            retString = Regex.Replace(retString, "^WEST ", "W");
            retString = Regex.Replace(retString, "^NORTH ", "N");
            retString = Regex.Replace(retString, "^SOUTH ", "S");
            retString = Regex.Replace(retString, " CIRCLE$", "");
            retString = Regex.Replace(retString, " PLACE$", "");
            retString = Regex.Replace(retString, " BLVD$", "");
            retString = Regex.Replace(retString, " HIGHWAY$", "");
            retString = Regex.Replace(retString, " HWY$", "");
            retString = Regex.Replace(retString, " AVE$", "");
            retString = Regex.Replace(retString, " AVE ", "");
            retString = Regex.Replace(retString, " AVENUE$", "");
            retString = Regex.Replace(retString, " ROAD$", "");
            retString = Regex.Replace(retString, " RAOD$", "");
            retString = Regex.Replace(retString, " RD$", "");
            retString = Regex.Replace(retString, " LANE$", "");
            retString = Regex.Replace(retString, " LN$", "");
            retString = Regex.Replace(retString, " SQUARE$", "");
            retString = Regex.Replace(retString, " SQR$", "");
            retString = Regex.Replace(retString, " COURT$", "");
            retString = Regex.Replace(retString, " CRT$", "");
            retString = Regex.Replace(retString, " STREET$", "");
            retString = Regex.Replace(retString, " STREET ", "");
            retString = Regex.Replace(retString, " STR$", "");
            retString = Regex.Replace(retString, " STR ", "");
            retString = Regex.Replace(retString, @"\s", "");
            return retString;
        }

        public static string PersonNameNormalize(string s)
        {
            string normalized = s.Normalize(NormalizationForm.FormD);
            normalized = normalized.ToUpper();

            StringBuilder resultBuilder = new StringBuilder();
            foreach (var character in normalized)
            {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(character);
                if (category == UnicodeCategory.UppercaseLetter || category == UnicodeCategory.SpaceSeparator)
                    resultBuilder.Append(character);
            }
            string retString = resultBuilder.ToString().Trim();
            retString = Regex.Replace(retString, "^MRS ", "");
            retString = Regex.Replace(retString, "^MS ", "");
            retString = Regex.Replace(retString, "^MR ", "");
            retString = Regex.Replace(retString, "^DR ", "");
            retString = Regex.Replace(retString, " II$", "");
            retString = Regex.Replace(retString, " III$", "");
            retString = Regex.Replace(retString, " IV$", "");
            retString = Regex.Replace(retString, " V$", "");
            retString = Regex.Replace(retString, " JR$", "");
            retString = Regex.Replace(retString, " SR$", "");
            retString = Regex.Replace(retString, @"\s", "");
            return retString;
        }
    }
}
