using System.Collections.Generic;

namespace EDU.VT.IPG.VTIntegrate.SchroederPadAndChaff
{
    public class WheatChaffer
    {
        public string stringToEncode { get; set; }
        public string stringToEncodeNormalized { get; set; }
        public int    maxStringLength { get; set; }
        public string positions { get; set; }
        public string alphaNumericKey { get; set; }
        public int    hashLength { get; set; }
        public string wheatKey { get; set; }
        public string chaffKey { get; set; }
        public string alphaNumericKeyShuffle { get; set; }
        public string oTPCipher { get; set; }
        public string cipherWithWheatPositions { get; set; }
        public string cipherWithChaffPositions { get; set; }
        public string chaffedValue { get; set; }
        public string secureRandomAlphaNumericKey { get; set; }

        public WheatChaffer(int maxStrLen = 15, int hashLen = 3)
        {
            // Defaults
            maxStringLength = maxStrLen;
            alphaNumericKey = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            hashLength = hashLen;
            setSecureRandomAlphaNumericKey();
            setPositions();
            setWheatKey();
            setChaffKey();
            
        }

        public string getChaffedValue(string strToEncode)
        {
            if (strToEncode == "")
                return "";
            stringToEncode = strToEncode;
            stringToEncodeNormalized = StringNormalization.PersonNameNormalize(stringToEncode);        
            setOTPCipher();
            setCipherWithWheatPositions();
            setCipherWithChaffPositions();
            setChaffedValue();
            return chaffedValue;
        }

        public string getHMACSHA512(string strToEncode)
        {
            if (strToEncode == "")
                return "";
            stringToEncode = strToEncode;
            stringToEncodeNormalized = StringNormalization.PersonNameNormalize(stringToEncode);        
            
            return GetHMACKey.HMAC_SHA1(wheatKey, strToEncode);
        }

        public void setPositions()
        {
            positions = "0";
            for (int x = 1; x < maxStringLength + 1; x++)
            {
                positions += "," + x;
            }
        }
        public void setWheatKey()
        {
            wheatKey = GetHMACKey.GetWheatHMACKey(positions, hashLength);
        }
        public void setChaffKey()
        {
            chaffKey = GetHMACKey.GetChaffHMACKey(positions, hashLength, wheatKey);
        }
        public void setSecureRandomAlphaNumericKey()
        {
            secureRandomAlphaNumericKey = OneTimePadCipher.GetSecureRandomKey(alphaNumericKey);
        }
        public void setOTPCipher()
        {
            oTPCipher = OneTimePadCipher.TupleList2String(OneTimePadCipher.OTP(alphaNumericKey, secureRandomAlphaNumericKey, stringToEncodeNormalized));
        }
        public void setCipherWithWheatPositions()
        {
            List<string> wheatHashes = Chaffer.GetPositionHashes(wheatKey, maxStringLength, hashLength);
            cipherWithWheatPositions = Chaffer.AddHashedPositions(oTPCipher, wheatHashes);
        }
        public void setCipherWithChaffPositions()
        {
            List<string> chaffHashes = Chaffer.GetPositionHashes(chaffKey, maxStringLength, hashLength);
            cipherWithChaffPositions = Chaffer.AddChaffedPositions(cipherWithWheatPositions, chaffHashes, maxStringLength, hashLength);
        }
        public void setChaffedValue()
        {
            chaffedValue = Fisher_Yates_Shuffle.FY_Shuffle(chaffKey, cipherWithChaffPositions, hashLength + 1);
        }
        

    }
}
