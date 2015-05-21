using System;
using System.Reflection;

[assembly: Obfuscation(Feature = "string encryption", Exclude = true)]
[assembly: Obfuscation(Feature = "code control flow obfuscation", Exclude = false)]
