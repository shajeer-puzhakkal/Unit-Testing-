using CloudCustomers.Config;
using CloudCustomers.Services;

var builder = WebApplication.CreateBuilder(args);
ConfigyreServices(builder.Services);


// Add services to the container.

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


void ConfigyreServices(IServiceCollection services)
{
    services.Configure<UserApiOptions>(builder.Configuration.GetSection("UserApiOptions"));
    services.AddTransient<IUserService, UserService>();
    services.AddHttpClient<IUserService, UserService>();
}