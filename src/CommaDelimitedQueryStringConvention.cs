using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace DelimitedQueryString
{
    public class CommaDelimitedQueryStringConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            DelimitedQueryStringAttribute attribute = null;
            foreach (var parameter in action.Parameters)
            {
                if (!parameter.Attributes.OfType<CommaDelimitedAttribute>().Any())
                {
                    continue;
                }

                if (attribute == null)
                {
                    attribute = new DelimitedQueryStringAttribute(",");
                    parameter.Action.Filters.Add(attribute);
                }

                attribute.AddKey(parameter.ParameterName);
            }
        }
    }
}