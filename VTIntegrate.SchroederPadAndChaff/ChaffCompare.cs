using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using SimMetricsMetricUtilities;
using SimMetricsUtilities;

namespace EDU.VT.IPG.VTIntegrate.SchroederPadAndChaff
{
    public class ChaffCompare
    {
        public enum ComparisonMethod
        {
            JaroWinkler,
            Jaro,
            JaccardSimilarity,
            DiceSimilarity
        };

        private HMACSHA1 hash;
        private int hashLength;
        private ComparisonMethod comparisonMethod;

        public ChaffCompare(string wheatKey, int hashLength, ComparisonMethod comparisonMethod)
        {
            if (string.IsNullOrWhiteSpace(wheatKey))
                throw new ArgumentNullException("hashSalt");

            this.hash = new HMACSHA1(Encoding.ASCII.GetBytes(wheatKey));
            this.hashLength = hashLength;
            this.comparisonMethod = comparisonMethod;
        }

        public double CompareChaffed(string chaff1, string chaff2, int hashLength, string comparisonMethod)
        {
            return CompareChaffed(chaff1, chaff2, this.hashLength, this.comparisonMethod.ToString());
        }

        public string Winnow(string chaffedStr)
        {
            return Winnow(chaffedStr, this.hash);
        }

        public static double CompareChaffed(string chaff1, string chaff2, string wheatKey, int hashLength, string comparisonMethod)
        {
            if (chaff1 == "" || chaff2 == "")
            {
                return 0D;
            }

            double returnValue = 0D;
            int chunck = hashLength + 1;
            int numChars = chaff1.Length / chunck;

            Dictionary<int, string> positionHashes = new Dictionary<int, string>();
            HMACSHA1 hash = new HMACSHA1(Encoding.ASCII.GetBytes(wheatKey));
            for (int x = 0; x < numChars + 1; x++)
            {
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(x.ToString());
                string hashValue = Util.ByteToString(hash.ComputeHash(bytes)).Substring(0, hashLength);
                positionHashes.Add(x, hashValue);
            }

            List<string> chaff1Chuncks = Util.Splice(chaff1, chunck).ToList();
            List<string> chaff2Chuncks = Util.Splice(chaff2, chunck).ToList();
            SortedList<int, string> winnow1Chuncks = new SortedList<int, string>();
            SortedList<int, string> winnow2Chuncks = new SortedList<int, string>();

            foreach (string s in chaff1Chuncks)
            {
                string sValue = s.Substring(1);
                foreach (KeyValuePair<int, string> pH in positionHashes)
                {
                    if (pH.Value == sValue)
                    {
                        winnow1Chuncks.Add(pH.Key, s.Substring(0,1));
                    }
                }
            }

            foreach (string s in chaff2Chuncks)
            {
                string sValue = s.Substring(1);
                foreach (KeyValuePair<int, string> pH in positionHashes)
                {
                    if (pH.Value == sValue)
                    {
                        winnow2Chuncks.Add(pH.Key, s.Substring(0,1));
                    }
                }
            }

            System.Text.StringBuilder sb1 = new StringBuilder();
            foreach (KeyValuePair<int, string> key in winnow1Chuncks)
            {
                sb1.Append(key.Value);
            }
            string winnow1 = sb1.ToString();

            System.Text.StringBuilder sb2 = new StringBuilder();
            foreach (KeyValuePair<int, string> key in winnow2Chuncks)
            {
                sb2.Append(key.Value);
            }
            string winnow2 = sb2.ToString();

            switch (comparisonMethod)
            {
                case "JaroWinkler":
                    JaroWinkler jW = new JaroWinkler();
                    returnValue = jW.GetSimilarity(winnow1, winnow2);
                    break;
                case "Jaro":
                    Jaro j = new Jaro();
                    returnValue = j.GetSimilarity(winnow1, winnow2);
                    break;
                case "JaccardSimilarity":
                    JaccardSimilarity jS = new JaccardSimilarity(new TokeniserQGram2());
                    returnValue = jS.GetSimilarity(winnow1, winnow2);
                    break;
                case "DiceSimilarity":
                    DiceSimilarity dS = new DiceSimilarity(new TokeniserQGram2());
                    returnValue = dS.GetSimilarity(winnow1, winnow2);
                    break;
                    
                default:
                    jW = new JaroWinkler();
                    returnValue = jW.GetSimilarity(winnow1, winnow2);
                    break;
            }

            return returnValue;
        }

        public static string Winnow(string chaffedStr, HMACSHA1 hash)
        {
            // TODO: Move this out of here and into VTIntegrate.
            if (string.IsNullOrWhiteSpace(chaffedStr))
                return chaffedStr;

            int hashLength = 3;

            int chunck = hashLength + 1;
            int numChars = chaffedStr.Length / chunck;

            Dictionary<int, string> positionHashes = new Dictionary<int, string>();
            for (int x = 0; x < numChars + 1; x++)
            {
                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(x.ToString());
                string hashValue = Util.ByteToString(hash.ComputeHash(bytes)).Substring(0, hashLength);
                positionHashes.Add(x, hashValue);
            }

            List<string> chaffedStrChuncks = Util.Splice(chaffedStr, chunck).ToList();
            SortedList<int, string> winnowChuncks = new SortedList<int, string>();

            foreach (string s in chaffedStrChuncks)
            {
                string sValue = s.Substring(1);
                foreach (KeyValuePair<int, string> pH in positionHashes)
                {
                    if (pH.Value == sValue)
                    {
                        winnowChuncks.Add(pH.Key, s.Substring(0, 1));
                    }
                }
            }

            System.Text.StringBuilder sb1 = new StringBuilder();
            foreach (KeyValuePair<int, string> key in winnowChuncks)
            {
                sb1.Append(key.Value);
            }

            return sb1.ToString();
        }
    }
}
