// File: gsApi/Program.cs

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using gsApi.data; // <<<< AJUSTE PARA O NAMESPACE DO SEU AppDbContext (ex: gsApi.data)
using gsApi.middleware; // Ajuste para o namespace do seu middleware (ex: gsApi.middleware)
using System;
using System.IO;
using System.Reflection;
// using AppDbContext; // Remova esta linha se AppDbContext estiver em SeuProjetoNET.Data

var startupLogPath = Path.Combine(AppContext.BaseDirectory, "gsApi_startup_trace.log");
Action<string> earlyManualLog = (message) =>
{
    try { File.AppendAllText(startupLogPath, $"[{DateTime.UtcNow:o}] {message}{Environment.NewLine}"); }
    catch (Exception ex) { Console.WriteLine($"Falha ao escrever no log manual inicial: {ex.Message}"); }
};
ILogger? appLogger = null;

try
{
    earlyManualLog("INICIANDO SEQU�NCIA DE STARTUP DA APLICA��O GSAPI...");
    var builder = WebApplication.CreateBuilder(args);
    earlyManualLog($"WebApplication.CreateBuilder conclu�do.");

    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
    builder.Logging.AddDebug();
    earlyManualLog("Provedores de logging configurados.");

    using var tempLoggerFactory = LoggerFactory.Create(logBuilder =>
    {
        logBuilder.AddConfiguration(builder.Configuration.GetSection("Logging"));
        logBuilder.AddConsole();
        logBuilder.AddDebug();
    });
    var earlyStageLogger = tempLoggerFactory.CreateLogger("StartupConfig");
    earlyStageLogger.LogInformation("Logger de est�gio inicial criado.");

    earlyStageLogger.LogInformation("Iniciando configura��o de servi�os...");
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // Use o nome da sua string de conex�o
    earlyStageLogger.LogInformation("String de conex�o 'DefaultConnection' lida: '{ConnString}'", string.IsNullOrEmpty(connectionString) ? "N�O ENCONTRADA" : "***OCULTADA***");

    if (string.IsNullOrEmpty(connectionString))
    {
        var errorMsg = "FATAL: String de conex�o 'DefaultConnection' n�o encontrada ou vazia.";
        earlyStageLogger.LogCritical(errorMsg); earlyManualLog(errorMsg);
        throw new InvalidOperationException(errorMsg);
    }

    // === CONFIGURA��O DO DBCONTEXT COM O NOME CORRETO ===
    builder.Services.AddDbContext<AppDbContext>(options => // <<<----- USANDO AppDbContext
    {
        earlyStageLogger.LogInformation("Configurando AppDbContext com provedor Oracle...");
        options.UseOracle(connectionString, oracleOptions =>
        {
            // Ex: oracleOptions.UseOracleSQLCompatibility("12"); // Ajuste para sua vers�o do Oracle
        })
        .LogTo(logMessage => earlyStageLogger.LogInformation(logMessage),
               new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
        .EnableSensitiveDataLogging() // CUIDADO EM PRODU��O
        .EnableDetailedErrors();
        earlyStageLogger.LogInformation("AppDbContext configurado.");
    });

    builder.Services.AddSwaggerGen(c =>
    {
        var description = "API RESTful desenvolvida ... (sua descri��o completa aqui) ..." +
                          "\n\n**Equipe MetaMind:**" +
                          "\n- Paulo Andr� Carminati (RM: 557881) - GitHub: [carmipa](https://github.com/carmipa)" +
                          "\n- Arthur Bispo de Lima (RM: 557568) - GitHub: [ArthurBispo00](https://github.com/ArthurBispo00)" +
                          "\n- Jo�o Paulo Moreira (RM: 557808) - GitHub: [joao1015](https://github.com/joao1015)";
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "GS Alerta Desastres - API (.NET) - Desafio Eventos Extremos",
            Version = "v1.0.0",
            Description = description,
            Contact = new OpenApiContact { Name = "Equipe MetaMind", Email = "equipe.metamind.fiap@example.com", Url = new Uri("https://github.com/carmipa/GS_FIAP_2025_1SM") },
            License = new OpenApiLicense { Name = "MIT License", Url = new Uri("https://github.com/carmipa/GS_FIAP_2025_1SM/blob/main/LICENSE") },
            // === CORRE��O AQUI: ExternalDocs DENTRO de OpenApiInfo ===
            ExternalDocs = new OpenApiExternalDocs
            {
                Description = "Saiba mais sobre a Global Solution FIAP",
                Url = new Uri("https://www.fiap.com.br/graduacao/global-solution/")
            }
        });
        // Removido c.AddServer(...) que era para o link da FIAP, pois ExternalDocs � o correto para isso.

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath)) { c.IncludeXmlComments(xmlPath); }
        else { earlyStageLogger.LogWarning($"Arquivo XML de coment�rios Swagger n�o encontrado: {xmlPath}"); }
    });
    earlyStageLogger.LogInformation("Configura��o de servi�os conclu�da.");

    var app = builder.Build();
    appLogger = app.Services.GetRequiredService<ILogger<Program>>();
    appLogger.LogInformation("Aplica��o constru�da. Configurando pipeline HTTP...");

    app.UseMiddleware<TratadorGlobalExcecoesMiddleware>(); // Use o namespace correto
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(o => o.SwaggerEndpoint("/swagger/v1/swagger.json", "GS Alerta API V1"));
    }
    // else { app.UseHsts(); } // Considere HSTS em produ��o

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    appLogger.LogInformation("Pipeline HTTP configurado. Iniciando aplica��o...");
    earlyManualLog("TENTANDO EXECUTAR APP.RUN()...");
    app.Run();
}
catch (Exception ex)
{
    var fatalMsg = $"EXCE��O FATAL NA INICIALIZA��O: {ex}";
    Console.ForegroundColor = ConsoleColor.Red; Console.Error.WriteLine(fatalMsg); Console.ResetColor();
    earlyManualLog(fatalMsg);
    if (appLogger != null) { appLogger.LogCritical(ex, "Falha CR�TICA na inicializa��o."); }
    else { LoggerFactory.Create(lb => lb.AddConsole()).CreateLogger("StartupCrash").LogCritical(ex, "Falha CR�TICA na inicializa��o (appLogger nulo)."); }
    throw;
}
finally
{
    earlyManualLog("SEQU�NCIA DE STARTUP FINALIZADA.");
}