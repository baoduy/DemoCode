using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace TestApi
{
    public class SwaggerSecurityRequirementsDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
            => swaggerDoc.Security = new List<IDictionary<string, IEnumerable<string>>>
            { new Dictionary<string, IEnumerable<string>>{{ "Bearer", new string[0] }}};
    }
}
