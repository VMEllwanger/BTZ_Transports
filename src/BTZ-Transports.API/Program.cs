using BTZ_Transports.Dados.Context;
using BTZ_Transports.Dados.Repository;
using BTZ_Transports.Negocio.Interfaces;
using BTZ_Transports.Negocio.Models;
using BTZ_Transports.Negocio.Notificacoes;
using BTZ_Transports.Negocio.Servicos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); 
builder.Services.AddMvc();
builder.Services.AddControllersWithViews();
// Add services to the container.
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddDbContext<BTZContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IMotoristaRepository, MotoristaRepository>();
builder.Services.AddScoped<IRepository<RegistroAbastecimento>, Repository<RegistroAbastecimento>>();
builder.Services.AddScoped<IRepository<Veiculo>, Repository<Veiculo>>();
builder.Services.AddScoped<ICombustivelRepository, CombustivelRepository>();

builder.Services.AddScoped<INotificador, Notificador>();
builder.Services.AddScoped<IVeiculoService, VeiculoService>();
builder.Services.AddScoped<IRegistroAbastecimentoService, RegistroAbastecimentoService>();
builder.Services.AddScoped<IMotoristaService, MotoristaService>();


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}"
    );
app.MapRazorPages();
app.Run();
