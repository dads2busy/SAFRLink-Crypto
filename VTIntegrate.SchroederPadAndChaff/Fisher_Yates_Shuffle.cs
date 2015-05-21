using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EDU.VT.IPG.VTIntegrate.SchroederPadAndChaff
{
    class Fisher_Yates_Shuffle
    {
        // Get Shuffled AlphaNumericKey
        public static string FY_Shuffle(string seed, string characters, int chunkSize)
        {
            List<string> shuffleArr = Util.Splice(characters, chunkSize).ToList();

            Guid guid = new Guid(seed);
            byte[] gu = guid.ToByteArray();
            int seedInt = BitConverter.ToInt32(gu, 0);

            Random random = new Random(seedInt);

            for (int i = shuffleArr.Count; i > 1; i--)
            {
                // Pick random element to swap.
                int j = random.Next(i); // 0 <= j <= i-1
                // Swap.
                string tmp = shuffleArr[j];
                shuffleArr[j] = shuffleArr[i - 1];
                shuffleArr[i - 1] = tmp;
            }

            StringBuilder builder = new StringBuilder();
            foreach (string value in shuffleArr)
            {
                builder.Append(value);
            }
            return builder.ToString();
        }
    }
}
