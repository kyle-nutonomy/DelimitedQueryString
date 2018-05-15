using System;

namespace DelimitedQueryString
{
    [AttributeUsage(AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
    public class CommaDelimitedAttribute : Attribute
    {
    }
}