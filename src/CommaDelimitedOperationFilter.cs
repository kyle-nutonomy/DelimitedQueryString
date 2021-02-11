using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Services;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DelimitedQueryString
{
    public class CommaDelimitedOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var parameters = operation.Parameters?
                .OfType<OpenApiParameter>()
                .Where(p => p.In == ParameterLocation.Query);

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
                    continue;
                }

                parameter.Style = ParameterStyle.Form;
            }
        }

        private static bool HasCommaSeparated(ApiParameterDescription apiParamDesc)
        {
            return ((ControllerParameterDescriptor)apiParamDesc.ParameterDescriptor)
                .ParameterInfo
                .CustomAttributes
                .Any(a => a.AttributeType == typeof(CommaDelimitedAttribute));
        }
    }
}