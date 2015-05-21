using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace EDU.VT.IPG.VTIntegrate.SchroederPadAndChaff
{
    public sealed class GetHMACKey
    {
        // Get Random Hashing Key That Will Produce Unique Hashes of Length 'HashLength' For All Capital Letters and Numerals
        public static string GetWheatHMACKey(string Characters, int HashLength)
        {
            // Put your code here
            bool unique = true;

            while (unique == true)
            {
                Guid g = Guid.NewGuid();
                string key = g.ToString();
                string chars = Characters;
                string[] charArray = chars.Split(',');

                HashSet<string> hashSet = new HashSet<string>();

                HMACSHA1 hash = new HMACSHA1(Encoding.ASCII.GetBytes(key));

                for (int x = 0; x < charArray.Length; x++)
                {
                    byte[] bytes = System.Text.Encoding.ASCII.GetBytes(charArray[x].ToString());

                    unique = hashSet.Add(Util.ByteToString(hash.ComputeHash(bytes)).Substring(0, HashLength));
                    if (unique == false)
                        break;
                }

                if (unique == true)
                {
                    //foreach (string s in hashSet)
                    //{
                    //    Console.Write(s + " ");
                    //}
                    return key;
                }

                unique = true;
            }
            return "No Key Found";
        }

        public static string GetChaffHMACKey(string Characters, int HashLength, string hashingKey)
        {
            // Put your code here
            bool unique = true;
            Guid g1 = new Guid(hashingKey);
            string key = g1.ToString();
            string chars = Characters;
            string[] charArray = chars.Split(',');

            HashSet<string> hashSet = new HashSet<string>();
            HMACSHA1 hash1 = new HMACSHA1(Encoding.ASCII.GetBytes(key));

            for (int x = 0; x < charArray.Length; x++)
            {
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(charArray[x].ToString());
                hashSet.Add(Util.ByteToString(hash1.ComputeHash(bytes)).Substring(0, HashLength));
            }

            while (unique == true)
            {
                Guid g2 = Guid.NewGuid();
                string key2 = g2.ToString();
                string chars2 = Characters;
                string[] charArray2 = chars2.Split(',');

                HMACSHA1 hash2 = new HMACSHA1(Encoding.ASCII.GetBytes(key2));

                for (int x = 0; x < charArray2.Length; x++)
                {
                    byte[] bytes = System.Text.Encoding.ASCII.GetBytes(charArray2[x].ToString());

                    unique = hashSet.Add(Util.ByteToString(hash2.ComputeHash(bytes)).Substring(0, HashLength));
                    if (unique == false)
                        break;
                }

                if (unique == true)
                {
                    //List<string> list = hashSet.ToList<string>();
                    //Console.WriteLine("CHAFF");
                    //for (int x = 0; x < hashSet.Count; x++)
                    //{
                    //    Console.Write(list[x] + " ");
                    //}
                    //Console.WriteLine("CHAFF");
                    return key2;
                }

                unique = true;
            }
            return "No Key Found";
        }

        public static string HMAC_SHA1(string key, string message)
        {
            // Put your code here
            HMACSHA1 hash = new HMACSHA1(Encoding.ASCII.GetBytes(key));
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(message);
            string result = Util.ByteToString(hash.ComputeHash(bytes));
            return result;
        }

        public static string HMAC_SHA512(string key, string message)
        {
            // Put your code here
            HMACSHA512 hash = new HMACSHA512(Encoding.ASCII.GetBytes(key));
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(message);
            string result = Util.ByteToString(hash.ComputeHash(bytes));
            return result;
        }
    }
}
