using Backend.Database.DatabaseContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Backend.Helper;
using System.Text.Json.Serialization;
using Backend.Interfaces.IUser;
using Backend.Repositories.UserRepository;
using Backend.Services.UserService;
using Backend.Games.Dice.DiceServices;
using Backend.Games.Dice;
using Backend.Games.Yatzy;
using Backend.Games.Yatzy.Service;
using Backend.Games.Keno;
using Backend.Games.Keno.Service;
using Backend.Interfaces.IBalance;
using Backend.Repositories.BalanceRepository;
using Backend.Services.BalanceService;
using Backend.Authentication;
using Backend.Interfaces.IEmail;
using Backend.Services.EmailService;
using Backend.Interfaces.IGames;
using Backend.Services.GamesService;
using Backend.Repositories.GamesRepository;
using Backend.Interfaces.IGamesHistory;
using Backend.Services.GamesHistoryService;
using Backend.Repositories.GamesHistoryRepository;
using Backend.Database.Entities;
using Backend.Interfaces.ITransactions;
using Backend.Services.TransactionsService;
using Backend.Repositories.TransactionsRepository;



namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConString"));
            });

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IDiceGameService, DiceGameService>();
            builder.Services.AddScoped<IYatzyGameService, YatzyGameService>();
            builder.Services.AddScoped<IKenoService, KenoService>();
            builder.Services.AddScoped<IBalanceRepository, BalanceRepository>();
            builder.Services.AddScoped<IBalanceService, BalanceService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IGamesService, GamesService>();
            builder.Services.AddScoped<IGamesRepository, GamesRepository>();
            builder.Services.AddScoped<IGameHistoryService, GameHistoryService>();
            builder.Services.AddScoped<IGameHistoryRepository, GameHistoryRepository>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            
            builder.Services.AddScoped<GameHistoryHelper>();



            builder.Services.AddScoped<IJwtUtils, JwtUtils>();

            var key = Encoding.ASCII.GetBytes(builder.Configuration["AppSettings:Secret"]!);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero 
                };
            });

            builder.Services.AddAuthorization();


            builder.Services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.Configure<AppSettings>(
                builder.Configuration.GetSection("AppSettings"));

            builder.Services.Configure<MailSettings>(
                builder.Configuration.GetSection("MailSettings"));


            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EvenBetterBackend", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Indtast 'Bearer' efterfulgt af dit token."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new string[] { }
                    }
                });
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policyBuilder => policyBuilder
                    .WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithExposedHeaders("Content-Disposition")
                );
            });


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<JwtMiddleware>(); 

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}
