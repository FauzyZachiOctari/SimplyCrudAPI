using SimplyCrudAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
//using SimplyCrudAPI.ExampleData.RegionData;
using SimplyCrudAPI.ExampleData.UserData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
//================================
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//ini untuk user example
builder.Services.AddSwaggerExamplesFromAssemblyOf<ExampleAddUser>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<ExampleCheckLogin>();

//untuk JWT Bearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
}
);

//untuk swagger document
builder.Services.AddSwaggerGen(options =>
{
    //ini untuk authorization
    options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization : `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        //Type = SecuritySchemeType.ApiKey,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
             new OpenApiSecurityScheme
             {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    //Id=JwtBearerDefaults.AuthenticationScheme
                    Id="Bearer"
                }
             }, new string[]{ }
        }
    });

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Simply Crud API Fauzy",
        Description = "Testing Web API Server"
        //,
        //TermsOfService = new Uri("https://example.com/terms"),
        //Contact = new OpenApiContact
        //{
        //    Name = "Example Contact",
        //    Url = new Uri("https://example.com/contact")
        //},
        //License = new OpenApiLicense
        //{
        //    Name = "Example License",
        //    Url = new Uri("https://example.com/license")
        //}
    });

    //using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    options.ExampleFilters();
});

//builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserAPIDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("UserApiConnection")));
builder.Services.AddDbContext<BookAPIDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BookApiConnection")));
builder.Services.AddDbContext<ImagesAPIDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ImagesApiConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
