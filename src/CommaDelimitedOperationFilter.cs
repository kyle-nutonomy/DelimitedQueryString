using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DelimitedQueryString
{
    public class CommaDelimitedOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            var parameters = operation.Parameters?
                .OfType<NonBodyParameter>()
                .Where(p => p.In.Equals("query", StringComparison.OrdinalIgnoreCase));

            if (parameters == null)
            {
                return;
            }

            foreach (var parameter in parameters)
            {
                var parameterDescription = context.ApiDescription.ParameterDescriptions
                    .FirstOrDefault(p =>
                        string.Equals(p.Name, parameter.Name, StringComparison.OrdinalIgnoreCase));

                if (parameterDescription == null || !parameterDescription.ModelMetadata.IsEnumerableType ||
                    !HasCommaSeparated(parameterDescription)
                )
                {
                    return;
                }

                parameter.CollectionFormat = "csv";
            }
        }

        private static bool HasCommaSeparated(ApiParameterDescription apiParamDesc)
        {
            return ((ControllerParameterDescriptor) apiParamDesc.ParameterDescriptor)
                .ParameterInfo
                .CustomAttributes
                .Any(a => a.AttributeType == typeof(CommaDelimitedAttribute));
        }
    }
}