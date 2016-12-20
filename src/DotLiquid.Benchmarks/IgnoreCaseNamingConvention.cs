using System;
using DotLiquid.NamingConventions;

namespace DotLiquid.Benchmarks
{
    internal class IgnoreCaseNamingConvention : INamingConvention
    {
        public string GetMemberName(string name)
        {
            return name.ToLower();
        }

        public StringComparer StringComparer => StringComparer.OrdinalIgnoreCase;
    }
}
