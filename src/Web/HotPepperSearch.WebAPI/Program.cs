using FluentValidation;
using FluentValidation.AspNetCore;
using HotPepperSearch.Application.Interfaces;
using HotPepperSearch.Application.Services;
using HotPepperSearch.Infrastructure.Scraping.Configuration;
using HotPepperSearch.Infrastructure.Scraping.Interfaces;
using HotPepperSearch.Infrastructure.Scraping.Services;
using HotPepperSearch.WebAPI.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(SearchMappingProfile));

// Configuration
builder.Services.Configure<ScrapingSettings>(
    builder.Configuration.GetSection("ScrapingSettings"));

// Infrastructure Services
builder.Services.AddScoped<IHtmlParserService, AngleSharpHtmlParserService>();
builder.Services.AddScoped<IDelayService, RandomDelayService>();
builder.Services.AddHttpClient<Application.Interfaces.IScrapingService, HotPepperBeautyScrapingService>((sp, client) =>
{
    var settings = builder.Configuration.GetSection("ScrapingSettings").Get<ScrapingSettings>();
    client.DefaultRequestHeaders.UserAgent.ParseAdd(settings?.UserAgent ?? "Mozilla/5.0");
});

// Application Services
builder.Services.AddScoped<ISalonSearchService, SalonSearchService>();
builder.Services.AddScoped<ISalonSortService, SalonSortService>();
builder.Services.AddScoped<ISearchHistoryService, SearchHistoryService>();
builder.Services.AddScoped<IMasterDataService, MasterDataService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
