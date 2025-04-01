using AutoMapper;
using ManageHotel.Config;
using ManageHotel.DAO;
using Microsoft.AspNetCore.OData;
using ManageHotel.Models;
using ManageHotel.Repository;
using ManageHotel.Repository.impl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});
// Add services to the container.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["jwt:issuer"],
        ValidAudience = builder.Configuration["jwt:audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("User", policy => policy.RequireRole("User"));
});
builder.Services.AddControllers()
    .AddXmlSerializerFormatters()
    .AddOData(opt => opt.Select().Filter().OrderBy().Expand().Count()
    .SetMaxTop(100).AddRouteComponents("odata", GetEdmModel()));

builder.Services.AddTransient<HotelDAO>();
builder.Services.AddTransient<IHotelRepository, HotelRepository>();
builder.Services.AddTransient<AccountDAO>();
builder.Services.AddTransient<IAccountRepository,AccountRepository>();
builder.Services.AddTransient<BlogDAO>();
builder.Services.AddTransient<IBlogRepository,BlogRepository>();
builder.Services.AddTransient<FeedbackDAO>();
builder.Services.AddTransient<IFeedbackRepository,FeedbackRepository>();
builder.Services.AddTransient<RoomDAO>();
builder.Services.AddTransient<IRoomRepository,RoomRepository>();
builder.Services.AddTransient<BookingDAO>();
builder.Services.AddTransient<IBookingRepository,BookingRepository>();
builder.Services.AddTransient<TypeRoomDAO>();
builder.Services.AddTransient<ITypeRoomRepository,TypeRoomRepository>();
builder.Services.AddTransient<BookingDetailsDAO>();
builder.Services.AddTransient<IBookingDetailRepository,BookingDetailRepository>();
builder.Services.AddTransient<RoleDAO>();
builder.Services.AddTransient<IRoleRepository,RoleRepository>();
builder.Services.AddTransient<PaymentDAO>();
builder.Services.AddTransient<IPaymentRepository,PaymentRepository>();
builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<EmailService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<HotelManageContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("value")));
var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MapperConfig()));
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
var app = builder.Build();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder modelBuilder = new ODataConventionModelBuilder();
    modelBuilder.EntitySet<Account>("Account");
    return modelBuilder.GetEdmModel();
}