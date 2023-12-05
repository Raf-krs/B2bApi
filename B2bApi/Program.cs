using B2bApi.ExchangeRates;
using B2bApi.Points;
using B2bApi.Shared.Db;
using B2bApi.Shared.Security;
using B2bApi.Shared.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddMiddlewares();
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddDb(builder.Configuration);
builder.Services.AddExchangeRates(builder.Configuration);
builder.Services.AddPolicies();

builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthSwagger();

builder.Host.AddLogger();

var app = builder.Build();
app.UseMiddlewares();
if(app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();