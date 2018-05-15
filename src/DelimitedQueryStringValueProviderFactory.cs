using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DelimitedQueryString
{
    public class DelimitedQueryStringValueProviderFactory : IValueProviderFactory
    {
        private readonly string _separator;
        private HashSet<string> _keys;

        public DelimitedQueryStringValueProviderFactory(string separator) : this((IEnumerable<string>) null, separator)
        {
        }

        public DelimitedQueryStringValueProviderFactory(string key, string separator) : this(new List<string> {key},
            separator)
        {
        }

        public DelimitedQueryStringValueProviderFactory(IEnumerable<string> keys, string separator)
        {
            _keys = keys != null ? new HashSet<string>(keys) : null;
            _separator = separator;
        }

        public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            context.ValueProviders.Insert(0,
                new DelimitedQueryStringValueProvider(_keys, context.ActionContext.HttpContext.Request.Query,
                    _separator));
            return Task.CompletedTask;
        }

        public void AddKey(string key)
        {
            if (_keys == null)
            {
                _keys = new HashSet<string>();
            }

            _keys.Add(key);
        }
    }
}