using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);


var appConfigConnectionString = builder.Configuration["Azure:AppConfig:ConnectionString"];
var keyValutManagedIdentity = builder.Configuration["Azure:AppConfig:ManagedIdentityClientId"];

builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(appConfigConnectionString)
           .ConfigureKeyVault(kv =>
           {
               kv.SetCredential(new DefaultAzureCredential(new DefaultAzureCredentialOptions
               { ManagedIdentityClientId = keyValutManagedIdentity }));
           });
});

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"Azure Config Example - API V1");
    c.RoutePrefix = string.Empty;
});
app.Run();
