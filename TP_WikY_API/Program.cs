using Microsoft.EntityFrameworkCore;
using Models;
using Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<AppUser>().AddEntityFrameworkStores<WikyDbContext>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<WikyDbContext>(o =>
{
	o.UseSqlServer(builder.Configuration.GetConnectionString("TP_WikY_API"));
});
var app = builder.Build();
app.MapIdentityApi<AppUser>();

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
