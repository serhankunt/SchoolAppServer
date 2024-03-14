using DefaultCorsPolicyNugetPackage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using NTierArchitecture.Business.Mapping;
using NTierArchitecture.Business.Services;
using NTierArchitecture.DataAccess.Context;
using NTierArchitecture.DataAccess.Repositories;
using NTierArchitecture.Entities.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultCors();

builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = "H�seyin Serhan Kunt",
        ValidAudience = "H�seyin Serhan Kunt",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("benim �ifre anahtar�m benim �ifre anahtar�m benim �ifre anahtar�m benim �ifre anahtar�m benim �ifre anahtar�m"))
    };
});
//builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
{
    options.Password.RequiredLength = 1;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<ApplicationDbContext>();

//Dependency Injection

builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IClassRoomRepository, ClassRoomRepository>();

builder.Services.AddScoped<IStudentService,StudentManager>();
builder.Services.AddScoped<IClassRoomService,ClassRoomManager>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.Use(async (context, next) =>
{
    try
    {
        await next(context);
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(JsonConvert.SerializeObject(new { Message = ex.Message }));
    }
});

using(var scoped = app.Services.CreateScope())
{
    var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    if (!userManager.Users.Any())
    {
        AppUser appUser = new()
        {
            FirstName = "H.Serhan",
            LastName = "Kunt",
            Email = "hserhankunt@gmail.com",
            UserName = "hskunt"
        };
        userManager.CreateAsync(appUser,"1").Wait();
    }

    var context = scoped.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (!context.Students.Any())
    {
        
        Student student = new()
        {
            Id = Guid.NewGuid(),
            FirstName = "Serhan",
            LastName = "Kunt",
            StudentNumber = 111,
            IdentityNumber ="13420057394",
            ClassRoomId = new Guid("89A47B97-60D6-461E-AC96-0ACE48E51ADB"),
            CreatedDate = DateTime.Now,
            CreatedBy = "Admin"
        };

        context.Students.Add(student);
        context.SaveChanges();
    }
    
}

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();
