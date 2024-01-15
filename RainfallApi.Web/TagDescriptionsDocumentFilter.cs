using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class TagDescriptionsDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        if (swaggerDoc.Tags == null)
            swaggerDoc.Tags = new List<OpenApiTag>();

        swaggerDoc.Tags.Add(new OpenApiTag
        {
            Name = "Rainfall",
            Description = "Operations relating to rainfall"
        });
    }
}