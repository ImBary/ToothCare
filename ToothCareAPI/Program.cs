using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToothCareAPI;
using ToothCareAPI.Data;
using ToothCareAPI.Model;
using ToothCareAPI.Repository;
using ToothCareAPI.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options => //sql connection
    options.UseSqlServer(builder.Configuration.GetConnectionString("DeafultConnection")));

// Add services to the container.

builder.Services.AddIdentity<ApplicationUser, IdentityRole>() //identity setup
    .AddEntityFrameworkStores<ApplicationDbContext>();

//MY REPOSITORIES

//CLIENTS
builder.Services.AddScoped<IClientsRepository, ClientsRepository>();

//DOCTORS
builder.Services.AddScoped<IDoctorsRepository, DoctorsRepository>();

//APPOINTMENTS
builder.Services.AddScoped<IAppointmentsRepository, AppoitmentsRepository>();

//PROCEDURES
builder.Services.AddScoped<IProceduresRepository, ProceduresRepository>();

//USERS
builder.Services.AddScoped<IUserRepository, UserRepository>();

//MAPPER
builder.Services.AddAutoMapper(typeof(MappingConfig));




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
