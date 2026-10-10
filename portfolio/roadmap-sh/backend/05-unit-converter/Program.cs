var builder = WebApplication.CreateBuilder(args);

string localHostPort = "http://127.0.0.1:5500";
string LocalCORS = "_LocalSpecifOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
   options.AddPolicy(name: LocalCORS, 
        policy =>
        {
           policy.WithOrigins(localHostPort);
        }) ;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
       options.SwaggerEndpoint("/openapi/v1.json", "v1"); 
    });
}

app.UseHttpsRedirection();

app.UseCors(LocalCORS);

app.UseAuthorization();

app.MapControllers();

app.Run();
