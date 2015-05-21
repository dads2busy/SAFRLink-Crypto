using System;
using System.Collections.Generic;

namespace EDU.VT.IPG.VTIntegrate.SchroederPadAndChaff
{
    class Chaffer
    {
        public static List<string> GetPositionHashes(string saltKey, int numberPositions, int hashLength)
        {
            List<string> positionHashes = new List<string>();

            for (int x = 0; x < numberPositions; x++)
            {
                string hash = GetHMACKey.HMAC_SHA1(saltKey, x.ToString()).ToString().Substring(0, hashLength);
                positionHashes.Add(hash);
            }
            return positionHashes;
        }
        
        public static string AddHashedPositions(string cipher, List<string> positionHashes)
        {
            string retString = "";

            for (int x = 0; x < cipher.Length; x++)
            {
                retString += cipher[x].ToString() + positionHashes[x];
            }
            return retString;
        }

        public static string AddChaffedPositions(string cipherWheat, List<string> positionHashes, int newCipherLength, int hashLength)
        {
            string retString = cipherWheat;
            int numCharsToAdd = newCipherLength - cipherWheat.Length / (hashLength + 1);

            for (int x = 0; x < numCharsToAdd; x++)
            {
                retString += Char.ToUpper(RandomLetter.GetLetter()) + positionHashes[x];
            }
            return retString;
        }
    }
}
