using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PhoneBook.API.Auth;
using PhoneBook.BL.Services;
using PhoneBook.BL.Services.Abstraction;
using PhoneBook.DAL.DB;
using PhoneBook.DAL.Repositories;
using PhoneBook.DAL.Repositories.Abstraction;
using PhoneBook.Models.DTOs.Contacts;
using PhoneBook.Models.Entities;
using PhoneBook.Models.Services;
using PhoneBook.Models.Services.Abstraction;
using System.Text;

namespace PhoneBook.API;

public class MapperProfile : Profile
{
  public MapperProfile()
  {
    CreateMap<Contact, ContactGetDto>().ReverseMap();
    CreateMap<Contact, ContactPostDto>().ReverseMap();
  }
}

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddDbContext<PhoneBookContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("PhoneBookConnectionString")));

        builder.Services.AddIdentity<SystemUser, Role>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
        })
        .AddEntityFrameworkStores<PhoneBookContext>();

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options => 
        {
            var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["JWT:Issuer"],
                ValidAudience = builder.Configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
            options.Events = new JwtBearerEvents();
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("", policy =>
            {
                policy.RequireAssertion(ctx =>
                {
                    return ctx.User.IsInRole("SystemUser") && ctx.User.HasClaim(c => c.Type == "") || ctx.User.IsInRole("Admin");
                });
            });
        });

        builder.Services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));
        builder.Services.AddSingleton<IJWTManager, JWTManager>();
        builder.Services.AddTransient<IUserRepository, UserRepository>();
        builder.Services.AddTransient<ICurrentUserService, CurrentUserService>();
        builder.Services.AddTransient<IContactService, ContactService>();

    builder.Services.AddAutoMapper(config => config.AddProfile<MapperProfile>());

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "PhoneBook APIs", Version = "v1" });
            
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[]{ }
                }
            });
        });

        var app = builder.Build();

        app.UseCors(policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        });

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}
