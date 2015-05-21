using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EDU.VT.IPG.VTIntegrate.SchroederPadAndChaff.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set two strings for comparison
            Console.Write("String 1 to Chaff: ");
            string strToChaff1 = Console.ReadLine();
            Console.Write("String 2 to Chaff: ");
            string strToChaff2 = Console.ReadLine();

            // Initialize WheatChaffer object
            WheatChaffer wC = new WheatChaffer();
            Console.WriteLine("Wheat key:                     " + wC.wheatKey);
            Console.WriteLine("Chaff key:                     " + wC.chaffKey);
            Console.WriteLine("Encryp-safe rand alphanum key: " + wC.secureRandomAlphaNumericKey);
            Console.WriteLine(Environment.NewLine);

            // Get first chaffed value
            string chaff1 = wC.getChaffedValue(strToChaff1);

            Console.WriteLine("String to encode:              " + wC.stringToEncode);
            Console.WriteLine("String normalized:             " + wC.stringToEncodeNormalized);           
            Console.WriteLine("One-time-pad cipher norm str:  " + wC.oTPCipher);
            Console.WriteLine("Add wheat hashed positions:    " + wC.cipherWithWheatPositions);
            Console.WriteLine("Add chaff hashed positions:    " + wC.cipherWithChaffPositions);
            Console.WriteLine("FY-Shuffled final value:       " + wC.chaffedValue);
            Console.WriteLine(Environment.NewLine);

            // Get second chaffed value
            string chaff2 = wC.getChaffedValue(strToChaff2);

            Console.WriteLine("String to encode:              " + wC.stringToEncode);
            Console.WriteLine("String normalized:             " + wC.stringToEncodeNormalized);
            Console.WriteLine("One-time-pad cipher norm str:  " + wC.oTPCipher);
            Console.WriteLine("Add wheat hashed positions:    " + wC.cipherWithWheatPositions);
            Console.WriteLine("Add chaff hashed positions:    " + wC.cipherWithChaffPositions);
            Console.WriteLine("FY-Shuffled final value:       " + wC.chaffedValue);
            Console.WriteLine(Environment.NewLine);

            // Initialize ChaffCompare object and compare chaffed values
            double matchRatio = ChaffCompare.CompareChaffed(chaff1, chaff2, wC.wheatKey, 3, "JaroWinkler");
            Console.WriteLine("JaroWinkler: " + matchRatio.ToString());
            matchRatio = ChaffCompare.CompareChaffed(chaff1, chaff2, wC.wheatKey, 3, "Jaro");
            Console.WriteLine("Jaro: " + matchRatio.ToString());
            matchRatio = ChaffCompare.CompareChaffed(chaff1, chaff2, wC.wheatKey, 3, "JaccardSimilarity");
            Console.WriteLine("JaccardSimilarity: " + matchRatio.ToString());
            matchRatio = ChaffCompare.CompareChaffed(chaff1, chaff2, wC.wheatKey, 3, "DiceSimilarity");
            Console.WriteLine("DiceSimilarity: " + matchRatio.ToString());
            Console.WriteLine(Environment.NewLine);

            // Get timing to run getChaffedValue 10000 times
            //Console.WriteLine("Time to run 10000 Pad and Chaff");
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //string padKey = OneTimePadCipher.GetSecureRandomKey("ABCDEFGHIJKLMNOP");

            //Parallel.For (0, 10000, item =>
            //{
            //    List<Tuple<string, int>> list = OneTimePadCipher.OTP("ABCDEFGHIJKLMNOP", padKey, strToChaff1);
            //});
            //sw.Stop();

            //Console.WriteLine("Elapsed ms: " + sw.ElapsedMilliseconds);

            //Console.WriteLine("Time to run 10000 SHA512");
            //sw.Restart();
            //Parallel.For(0, 10000, item =>
            //{
            //    GetHMACKey.HMAC_SHA512(padKey, strToChaff1);
            //});
            //sw.Stop();
            //Console.WriteLine("Elapsed ms: " + sw.ElapsedMilliseconds);
        }

    }
}
