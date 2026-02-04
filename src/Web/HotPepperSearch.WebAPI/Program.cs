using FluentValidation;
using FluentValidation.AspNetCore;
using HotPepperSearch.Application.Interfaces;
using HotPepperSearch.Application.Services;
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
