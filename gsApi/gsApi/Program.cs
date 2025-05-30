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
    earlyManualLog("INICIANDO SEQUÊNCIA DE STARTUP DA APLICAÇÃO GSAPI...");
    var builder = WebApplication.CreateBuilder(args);
    earlyManualLog($"WebApplication.CreateBuilder concluído.");

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
    earlyStageLogger.LogInformation("Logger de estágio inicial criado.");

    earlyStageLogger.LogInformation("Iniciando configuração de serviços...");
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // Use o nome da sua string de conexão
    earlyStageLogger.LogInformation("String de conexão 'DefaultConnection' lida: '{ConnString}'", string.IsNullOrEmpty(connectionString) ? "NÃO ENCONTRADA" : "***OCULTADA***");

    if (string.IsNullOrEmpty(connectionString))
    {
        var errorMsg = "FATAL: String de conexão 'DefaultConnection' não encontrada ou vazia.";
        earlyStageLogger.LogCritical(errorMsg); earlyManualLog(errorMsg);
        throw new InvalidOperationException(errorMsg);
    }

    // === CONFIGURAÇÃO DO DBCONTEXT COM O NOME CORRETO ===
    builder.Services.AddDbContext<AppDbContext>(options => // <<<----- USANDO AppDbContext
    {
        earlyStageLogger.LogInformation("Configurando AppDbContext com provedor Oracle...");
        options.UseOracle(connectionString, oracleOptions =>
        {
            // Ex: oracleOptions.UseOracleSQLCompatibility("12"); // Ajuste para sua versão do Oracle
        })
        .LogTo(logMessage => earlyStageLogger.LogInformation(logMessage),
               new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
        .EnableSensitiveDataLogging() // CUIDADO EM PRODUÇÃO
        .EnableDetailedErrors();
        earlyStageLogger.LogInformation("AppDbContext configurado.");
    });

    builder.Services.AddSwaggerGen(c =>
    {
        var description = "API RESTful desenvolvida ... (sua descrição completa aqui) ..." +
                          "\n\n**Equipe MetaMind:**" +
                          "\n- Paulo André Carminati (RM: 557881) - GitHub: [carmipa](https://github.com/carmipa)" +
                          "\n- Arthur Bispo de Lima (RM: 557568) - GitHub: [ArthurBispo00](https://github.com/ArthurBispo00)" +
                          "\n- João Paulo Moreira (RM: 557808) - GitHub: [joao1015](https://github.com/joao1015)";
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "GS Alerta Desastres - API (.NET) - Desafio Eventos Extremos",
            Version = "v1.0.0",
            Description = description,
            Contact = new OpenApiContact { Name = "Equipe MetaMind", Email = "equipe.metamind.fiap@example.com", Url = new Uri("https://github.com/carmipa/GS_FIAP_2025_1SM") },
            License = new OpenApiLicense { Name = "MIT License", Url = new Uri("https://github.com/carmipa/GS_FIAP_2025_1SM/blob/main/LICENSE") },
            // === CORREÇÃO AQUI: ExternalDocs DENTRO de OpenApiInfo ===
            ExternalDocs = new OpenApiExternalDocs
            {
                Description = "Saiba mais sobre a Global Solution FIAP",
                Url = new Uri("https://www.fiap.com.br/graduacao/global-solution/")
            }
        });
        // Removido c.AddServer(...) que era para o link da FIAP, pois ExternalDocs é o correto para isso.

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath)) { c.IncludeXmlComments(xmlPath); }
        else { earlyStageLogger.LogWarning($"Arquivo XML de comentários Swagger não encontrado: {xmlPath}"); }
    });
    earlyStageLogger.LogInformation("Configuração de serviços concluída.");

    var app = builder.Build();
    appLogger = app.Services.GetRequiredService<ILogger<Program>>();
    appLogger.LogInformation("Aplicação construída. Configurando pipeline HTTP...");

    app.UseMiddleware<TratadorGlobalExcecoesMiddleware>(); // Use o namespace correto
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(o => o.SwaggerEndpoint("/swagger/v1/swagger.json", "GS Alerta API V1"));
    }
    // else { app.UseHsts(); } // Considere HSTS em produção

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    appLogger.LogInformation("Pipeline HTTP configurado. Iniciando aplicação...");
    earlyManualLog("TENTANDO EXECUTAR APP.RUN()...");
    app.Run();
}
catch (Exception ex)
{
    var fatalMsg = $"EXCEÇÃO FATAL NA INICIALIZAÇÃO: {ex}";
    Console.ForegroundColor = ConsoleColor.Red; Console.Error.WriteLine(fatalMsg); Console.ResetColor();
    earlyManualLog(fatalMsg);
    if (appLogger != null) { appLogger.LogCritical(ex, "Falha CRÍTICA na inicialização."); }
    else { LoggerFactory.Create(lb => lb.AddConsole()).CreateLogger("StartupCrash").LogCritical(ex, "Falha CRÍTICA na inicialização (appLogger nulo)."); }
    throw;
}
finally
{
    earlyManualLog("SEQUÊNCIA DE STARTUP FINALIZADA.");
}