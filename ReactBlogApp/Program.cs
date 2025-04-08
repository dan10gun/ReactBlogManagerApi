using MySql.Data.MySqlClient;
using System.Data;
using ReactBlogApp.RepoLogic;
using ReactBlogApp.ServiceLogic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Dependency injection
builder.Services.AddScoped<IBlogAppSL, BlogAppSL>();
builder.Services.AddScoped<IBlogAppRL, BlogAppRL>();
builder.Services.AddScoped<IDbConnection>(sp =>
    new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

#endregion

#region swagger
builder.Services.AddSwaggerGen();
#endregion

// Enable HTTPS Redirection
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 5001; // Ensure this is the correct port for your environment
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
