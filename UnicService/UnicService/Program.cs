using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ModelUtil.Logger;
using ModelUtil.Repositories.UnicBase;
using System.Text;
using UnicService.Filters;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
#region Context
builder.Services.AddDbContext<ModelUtil.Contexts.UnicBaseContext>(options =>
{
    options.UseSqlServer(@"Server=DESKTOP-9H6FERQ\SQLEXPRESS;Database=UnicBase;User Id=unicUser;Password=myPa5$word1!;TrustServerCertificate=True;");
});
#endregion
#region Repositories
builder.Services.AddTransient<IUnicBaseRepository, UnicBaseRepository>();
#endregion
#region Services
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IUniLogger, UniLoggerConsole>();
#endregion
builder.Services.AddControllers(options =>
{
    options.Filters.Add<AccessActionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
});
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
