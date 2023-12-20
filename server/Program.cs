using Microsoft.EntityFrameworkCore;
using server;
using server.Repository.IRepository;
using server.Repository;
using server.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using server.Authentication;
using server.Services;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("HealthMonitoringDB")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddScoped<IRoomRecordService, RoomRecordService>();
builder.Services.AddScoped<IPersonRecordService, PersonRecordService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.SaveToken = true;
//    options.RequireHttpsMetadata = false;
//    options.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidAudience = configuration["JWT:ValidAudience"],
//        ValidIssuer = configuration["JWT:ValidIssuer"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
//    };
//});

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://securetoken.google.com/YOUR_PROJECT_ID";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "https://securetoken.google.com/YOUR_PROJECT_ID",
            ValidateAudience = true,
            ValidAudience = "healthmonitoring-676cb",
            ValidateLifetime = true
        };
    });

//Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", @"<PATH_TO_CREDENTIALS_FILE");
//builder.Services.AddSingleton(FirebaseApp.Create());
//builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.MapControllers();

app.UseRouting();
app.UseAuthentication();    // аутентификация
app.UseAuthorization();     // авторизация
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

//app.UseSession();
//app.Use(async (context, next) =>
//{
//    var token = context.Session.GetString("token");
//    if (!string.IsNullOrEmpty(token))
//    {
//        context.Request.Headers.Add("Authorization", "Bearer " + token);
//    }
//    await next();
//});

FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile("Authentication\\firebaseAuth\\firebase-credentials.json")
});

app.Run();
