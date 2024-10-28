using Microsoft.OpenApi.Models;
using review_api.Business;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IAzureAITextAnalytics, AzureAITextAnalytics>();
builder.Services.AddSingleton<IReviewFileReader, ReviewFileReader>();
builder.Services.AddSingleton<IMarkovChainTrainer, MarkovChainTrainer>();
builder.Services.AddSingleton<IRatingGenerator, RatingGenerator>();
builder.Services.AddSingleton<IReviewGenerator, ReviewGenerator>();

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

var markovChainTrainer = app.Services.GetService<IMarkovChainTrainer>();
markovChainTrainer?.TrainMarkovChain();

app.MapGet("/API/generate", (IReviewGenerator reviewGenerator) =>
{
    try
    {
        var review = reviewGenerator.GenerateRandomReview();
        return Results.Ok(review);
    }
    catch
    {
        return Results.NotFound();
    }
})
.WithName("GetRandomReview");

app.Run();
