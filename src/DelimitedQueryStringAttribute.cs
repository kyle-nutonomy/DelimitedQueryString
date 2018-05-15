using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DelimitedQueryString
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class DelimitedQueryStringAttribute : Attribute, IResourceFilter
    {
        private readonly DelimitedQueryStringValueProviderFactory _factory;

        public DelimitedQueryStringAttribute() : this(",")
        {
        }

        public DelimitedQueryStringAttribute(string separator)
        {
            _factory = new DelimitedQueryStringValueProviderFactory(separator);
        }

        public DelimitedQueryStringAttribute(string key, string separator)
        {
            _factory = new DelimitedQueryStringValueProviderFactory(key, separator);
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.ValueProviderFactories.Insert(0, _factory);
        }

        public void AddKey(string key)
        {
            _factory.AddKey(key);
        }
    }
}