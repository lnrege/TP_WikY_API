using DTOs.Article;
using DTOs.Comment;
using IRepositories;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Models;
using Repositories;
using Repositories.Repositories;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(o =>
{ o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; }
);
builder.Services.AddAutoMapper(c =>
{
	c.CreateMap<ArticleAddDTO, Article>();
	c.CreateMap<ArticleUpdateDTO, Article>();
	//.ForMember(dest=>dest.AppUserID, a=>a.MapFrom(src=>src.UserID));
	//c.CreateMap<CommentUpdateDTO, Comment>();
});
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<AppUser>().AddEntityFrameworkStores<WikyDbContext>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
	// Set the comments path for the Swagger JSON and UI.
	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	c.IncludeXmlComments(xmlPath);

	c.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
	{
		In = Microsoft.OpenApi.Models.ParameterLocation.Header,
		Name = "Authorization",
		Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
	});
	c.OperationFilter<SecurityRequirementsOperationFilter>();
}
);
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
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
