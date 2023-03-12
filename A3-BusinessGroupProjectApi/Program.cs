using A3_BusinessGroupProjectApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string not found.")));


// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

/*app.Use((context, next) =>
{
    if (context.Request.Headers.ContainsKey("Accept"))
    {
        var mediaType = context.Request.Headers["Accept"].ToString().ToLowerInvariant();

        if (mediaType == "application/xml")
        {
            context.Request.Headers["Accept"] = "application/xml";
            context.Response.ContentType = "application/xml";
        }
        else if (mediaType == "application/json")
        {
            context.Request.Headers["Accept"] = "application/json";
            context.Response.ContentType = "application/json";
        }
        else
        {
            context.Response.StatusCode = 406;
            return Task.CompletedTask;
        }
    }
    else
    {
        context.Request.Headers["Accept"] = "application/json";
        context.Response.ContentType = "application/json";
    }

    return next();
});*/
app.Run(async (context) =>
{
    context.Response.StatusCode = StatusCodes.Status200OK;
    await context.Response.WriteAsync("Hello World");
});




app.Run();




