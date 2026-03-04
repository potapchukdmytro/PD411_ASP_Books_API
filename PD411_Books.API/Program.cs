using Microsoft.EntityFrameworkCore;
using PD411_Books.DAL;
using PD411_Books.DAL.Initializer;
using PD411_Books.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add repositories
builder.Services.AddScoped<AuthorRepository>();

// Add services


// Add dbcontext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    string? connectionString = builder.Configuration.GetConnectionString("LocalDb");
    options.UseNpgsql(connectionString);
});

builder.Services.AddControllers();

// CORS - дозволяємо реакту кидати запити на наш бек
string corsName = "allowAll";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(corsName, cfg =>
    {
        cfg.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// CORS - дозволяємо реакту кидати запити на наш бек
app.UseCors(corsName);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.SeedAsync().Wait();

app.Run();
