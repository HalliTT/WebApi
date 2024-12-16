using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models.DTO;
using WebApi.Models;
using WebApi.Services.Mappers;
using WebApi.Services.Mappers.AddressMap;
using WebApi.Services.Mappers.SportMap;
using WebApi.Services.Mappers.UserMap;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MembershipContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Mapper injection
builder.Services.AddScoped<ICustomMapper<Address, AddressDTO>, AddressToAddressDTOMapper>();
builder.Services.AddScoped<ICustomMapper<AddressDTO, Address>, AddressDTOtoAddressMapper>();
builder.Services.AddScoped<ICustomMapper<Sport, SportDTO>, SportToSportDTOMapper>();
builder.Services.AddScoped<ICustomMapper<SportDTO, Sport>, SportDTOtoSportMapper>();
builder.Services.AddScoped<ICustomMapper<User, UserDTO>, UserToUserDTOMapper>();
builder.Services.AddScoped<ICustomMapper<UserDTO, User>, UserDTOtoUserMapper>();


var app = builder.Build();

app.UseMiddleware<WebApi.Middleware.ExceptionMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<MembershipContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}


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
