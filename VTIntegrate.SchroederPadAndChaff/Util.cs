using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace EDU.VT.IPG.VTIntegrate.SchroederPadAndChaff
{
    class Util
    {
        public static string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary);
        }

        public static IEnumerable<string> Splice(string s, int spliceLength)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (spliceLength < 1)
                throw new ArgumentOutOfRangeException("spliceLength");

            if (s.Length == 0)
                yield break;
            var start = 0;
            for (var end = spliceLength; end < s.Length; end += spliceLength)
            {
                yield return s.Substring(start, spliceLength);
                start = end;
            }
            yield return s.Substring(start);
        }

    }
}
