using InvictusAcademyApp.AppExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddAppServices(builder.Configuration);
services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
        policy => { policy.WithOrigins("http://localhost:8080/").AllowAnyHeader().AllowAnyMethod();});
});
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyAllowSpecificOrigins");
app.UseAuthentication().UseAuthorization();

app.MapControllers();

app.Run();