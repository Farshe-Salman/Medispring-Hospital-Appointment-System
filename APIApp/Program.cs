using BLL.Jwt;
using BLL.Services;
using DAL;
using DAL.EF;
using DAL.Repos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddScoped(typeof(Repository<>));

builder.Services.AddScoped<DataAccessFactory>();

builder.Services.AddScoped<BranchService>();
builder.Services.AddScoped<DoctorService>();
builder.Services.AddScoped<DoctorBranchService>();
builder.Services.AddScoped<DoctorScheduleService>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<AuthService>();  
builder.Services.AddSingleton<JwtService>();  

builder.Services.AddDbContext<HDMSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConn")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("THIS_IS_MY_SUPER_SECRET_KEY_12345")
        )
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
