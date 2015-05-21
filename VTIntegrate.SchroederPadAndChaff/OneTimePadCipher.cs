using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace EDU.VT.IPG.VTIntegrate.SchroederPadAndChaff
{
    public class OneTimePadCipher
    /*
     * In cryptography, the one-time pad (OTP) is a type of encryption which has been proven to be impossible to crack if used correctly. 
     * Each bit or character from the plaintext is encrypted by a modular addition with a bit or character from a secret random key (or pad) 
     * of the same length as the plaintext, resulting in a ciphertext. If the key is truly random, as large as or greater than the plaintext, 
     * never reused in whole or part, and kept secret, the ciphertext will be impossible to decrypt or break without knowing the key.
     */
    {
        public static string GetSecureRandomKey(string alphaNumericKey)
        {
            int maxSize = alphaNumericKey.Length;
            char[] chars = new char[62];
            string a;
            a = alphaNumericKey;
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            { result.Append(chars[b % (chars.Length - 1)]); }
            return result.ToString();
        }

        public static List<Tuple<string, int>> OTP(string alphaNumericKey, string secureRandomAlphaNumericKey, string message)
        {
            List<Tuple<string, int>> alphaNumericList = TupleList(alphaNumericKey);
            List<Tuple<string, int>> secureRandomList = TupleList(secureRandomAlphaNumericKey);
            List<Tuple<string, int>> messageList = TupleList(message);
            List<Tuple<string, int>> oneTimePad = new List<Tuple<string, int>>();
            for (int i = 0; i < messageList.Count; i++)
            {
                int messageLetterNumber = alphaNumericList.Find(p => p.Item1 == messageList[i].Item1).Item2;
                int secureLetterNumber = alphaNumericList.Find(p => p.Item1 == secureRandomList[i].Item1).Item2;
                int letterKeyNumber = (messageLetterNumber + secureLetterNumber) % 36;
                string letterKeyLetter = alphaNumericList[letterKeyNumber].Item1;
                Tuple<string, int> tuple = new Tuple<string,int>(letterKeyLetter, letterKeyNumber);
                oneTimePad.Add(tuple);
            }
            return oneTimePad;
        }

        public static string OTP_string(string alphaNumericKey, string secureRandomAlphaNumericKey, string message)
        {
            if (alphaNumericKey == null)
                throw new ArgumentNullException("alphaNumericKey was null");

            if (secureRandomAlphaNumericKey == null)
                throw new ArgumentNullException("secureRandomAlphaNumericKey was null");

            if (message == null || string.IsNullOrEmpty(message.Trim()))
                return "";

            // trunc message to size of key
            if (message.Length > alphaNumericKey.Length)
                message = message.Substring(0, alphaNumericKey.Length);

            // onetimepad it
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < message.Length; i++)
            {
                int messageLetterPos = alphaNumericKey.IndexOf(message[i]);
                if (messageLetterPos < 0)
                {
                    throw new ArgumentException("message contained a character not found in the alphanum key");
                }
                int secureLetterPos = alphaNumericKey.IndexOf(secureRandomAlphaNumericKey[i]);
                int oneTimePadPos = (messageLetterPos + secureLetterPos) % alphaNumericKey.Length;
                char oneTimePadLetter = alphaNumericKey[oneTimePadPos];
                builder.Append(oneTimePadLetter);
            }

            return builder.ToString();
        }

        public static List<Tuple<string, int>> TupleList(string tupleString)
        {
            List<Tuple<string, int>> tupleList = new List<Tuple<string, int>>();
            for (int i = 0; i < tupleString.Length; i++)
            {
                Tuple<string, int> tuple = new Tuple<string, int>(tupleString[i].ToString(), i);
                tupleList.Add(tuple);
            }
            return tupleList;
        }

        public static string TupleList2String(List<Tuple<string, int>> tupleList)
        {
            string returnString = "";
            foreach (Tuple<string, int> tuple in tupleList)
            {
                returnString += tuple.Item1;
            }
            return returnString;
        }
    }
}
