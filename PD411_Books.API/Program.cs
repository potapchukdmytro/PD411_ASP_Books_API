using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using PD411_Books.API.Settings;
using PD411_Books.BLL.Services;
using PD411_Books.DAL;
using PD411_Books.DAL.Initializer;
using PD411_Books.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add repositories
builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<BookRepository>();

// Add services
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<ImageService>();

// Add dbcontext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    string? localDb = builder.Configuration.GetConnectionString("LocalDb");
    string? aivenDb = builder.Configuration.GetConnectionString("AivenDb");
    options.UseNpgsql(aivenDb);
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

// Static files
string root = app.Environment.ContentRootPath;
string storagePath = Path.Combine(root, StaticFilesSettings.StorageDir);

// Books
string booksPath = Path.Combine(storagePath, StaticFilesSettings.BooksDir);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(booksPath),
    RequestPath = StaticFilesSettings.BookUrl
});

//Authors
string authorsPath = Path.Combine(storagePath, StaticFilesSettings.AuthorsDir);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(authorsPath),
    RequestPath = StaticFilesSettings.AuthorUrl
});

// Share
string sharePath = Path.Combine(storagePath, StaticFilesSettings.ShareDir);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(sharePath),
    RequestPath = StaticFilesSettings.ShareUrl
});

app.UseAuthorization();

app.MapControllers();

app.SeedAsync().Wait();

app.Run();
