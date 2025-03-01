using AutoMapper;
using ManageHotel.Config;
using ManageHotel.DAO;
using ManageHotel.Models;
using ManageHotel.Repository;
using ManageHotel.Repository.impl;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<HotelDAO>();
builder.Services.AddTransient<IHotelRepository, HotelRepository>();
builder.Services.AddTransient<AccountDAO>();
builder.Services.AddTransient<IAccountRepository,AccountRepository>();
builder.Services.AddTransient<BlogDAO>();
builder.Services.AddTransient<IBlogRepository,BlogRepository>();
builder.Services.AddTransient<RoomDAO>();
builder.Services.AddTransient<IRoomRepository,RoomRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HotelManageContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("value")));
var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MapperConfig()));
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
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
