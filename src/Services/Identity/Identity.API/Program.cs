using Identity.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddMvc();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// Add API services
services.AddInfrastructureServices(builder.Configuration);


// Configure CORS Policy and Cookie
services.AddCors(options => options.AddPolicy("CorsPolicy", policy =>
{
    policy.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
}));
services.ConfigureApplicationCookie(options => {
    options.Cookie.SameSite = SameSiteMode.None;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication().UseAuthorization();
app.MapControllers();

app.Run();
