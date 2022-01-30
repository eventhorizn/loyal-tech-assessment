using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Rating Generator API",
        Description = "An ASP.NET Core Web API for generating random reviews"
    });
});

var app = builder.Build();

// Kill this if you want old swagger light mode
// Just playing around!
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => {
        options.InjectStylesheet("/swagger-ui/swagger-dark.css");
    });
}

app.MapGet("/API/generate", () =>
{
    return new RandomReview {
        ReviewText = "Test review",
        Rating = 3
    };
})
.WithName("GetRandomReview");

app.Run();
