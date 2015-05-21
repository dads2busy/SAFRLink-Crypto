using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDU.VT.IPG.VTIntegrate.SchroederPadAndChaff
{
    class SubstitutionCipher
    {
        // Substituion Cipher Using AlphaNumericKeyShuffle
        public static string SubCipher(string text, string subKey, int mode) //string saltKey, )
        {
            string result = "";
            if (mode == 1)
            {
                result = Encrypt(text.ToLower(), subKey); //, saltKey);
            }
            if (mode == 2)
            {
                result = Decrypt(text, subKey);
            }
            return result;
        }

        static string Encrypt(string plainText, string subKey) //, string saltKey)
        {
            char[] chars = new char[plainText.Length];
            for (int i = 0; i < plainText.Length; i++)
            {
                if (plainText[i] == ' ')
                {
                    chars[i] = ' ';
                }
                else
                {
                    int j = plainText[i] - 97;
                    chars[i] = subKey[j];
                }
            }

            return new string(chars); //retString;
        }

        static string Decrypt(string cipherText, string key)
        {
            char[] chars = new char[cipherText.Length];
            for (int i = 0; i < cipherText.Length; i++)
            {
                if (cipherText[i] == ' ')
                {
                    chars[i] = ' ';
                }
                else
                {
                    int j = key.IndexOf(cipherText[i]) - 97;
                    chars[i] = (char)j;
                }
            }
            return new string(chars);
        }
    }
}
