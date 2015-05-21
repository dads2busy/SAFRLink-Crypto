using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;
using Microsoft.SqlServer.Server;
using System.Security.Cryptography;

namespace EDU.VT.IPG.VTIntegrate.SchroederPadAndChaff
{
    public partial class UserDefinedFunctions
    {
        [Microsoft.SqlServer.Server.SqlFunction]
        public static string VT_Cypher_Generate_Key()
        {
            string alphanumKey = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            int maxSize = alphanumKey.Length;
            char[] chars = new char[62];
            string a;
            a = alphanumKey;
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

        [Microsoft.SqlServer.Server.SqlFunction]
        public static string VT_Cypher_Person_Name(string key, string personName)
        {

            if (personName == null || string.IsNullOrEmpty(personName.Trim()))
                return "";

            personName = StringNormalization.PersonNameNormalize(personName);

            return VT_Cypher_String(key, personName);
        }

        [Microsoft.SqlServer.Server.SqlFunction]
        public static string VT_Cypher_String(string key, string message)
        {
            string alphanumKey = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            if (message == null || string.IsNullOrEmpty(message.Trim()))
                return "";

            // trunc message to size of key
            if (message.Length > alphanumKey.Length)
                message = message.Substring(0, alphanumKey.Length);

            // onetimepad it
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < message.Length; i++)
            {
                int messageLetterPos = alphanumKey.IndexOf(message[i]);
                if (messageLetterPos < 0)
                {
                    throw new ArgumentException("message contained a character not found in the alphanum key");
                }
                int secureLetterPos = alphanumKey.IndexOf(key[i]);
                int oneTimePadPos = (messageLetterPos + secureLetterPos) % alphanumKey.Length;
                char oneTimePadLetter = alphanumKey[oneTimePadPos];
                builder.Append(oneTimePadLetter);
            }

            return builder.ToString();
        }
    };

}