using DAL.EF;
using DAL.Repos;
using BLL.Services;
using Microsoft.EntityFrameworkCore;
using DAL;

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




builder.Services.AddDbContext<HDMSContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConn")));

var app = builder.Build();

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
