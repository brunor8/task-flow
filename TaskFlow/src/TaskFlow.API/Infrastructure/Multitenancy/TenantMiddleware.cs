using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;

namespace TaskFlow.API.Infrastructure.Multitenancy;

public class TenantMiddleware
{
    private readonly RequestDelegate _next; // RequestDelegate é um delegate que representa o próximo middleware na pipeline do ASP.NET Core.
                                            // Pense na pipeline como uma fila de middlewares — cada um processa a requisição e passa para o próximo.
                                            // O readonly garante que a referência nunca seja substituída após o construtor.

    public TenantMiddleware(RequestDelegate next) => _next = next; // O ASP.NET Core injeta automaticamente o RequestDelegate quando registra o middleware

    public async Task InvokeAsync(HttpContext context) // Todo middleware no ASP.NET Core precisa ter um método chamado exatamente InvokeAsync ou Invoke.
                                                       // O framework reconhece esse nome e chama automaticamente para cada requisição.
                                                       // HttpContext contém tudo sobre a requisição atual — headers, usuário autenticado, claims, body, response, etc.
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var tenantId = context.User.FindFirst("TenantId")?.Value;

            if (string.IsNullOrEmpty(tenantId))
            {
                await _next(context);
                return;
            }

            var tenantContext = context.RequestServices.GetRequiredService<TenantContext>(); // RequestServices é o container de injeção de dependência scoped para a requisição atual. Cada requisição tem seu próprio scope.
                                                                                             // GetRequiredService<TenantContext>() — busca a instância do TenantContext registrada como Scoped no Program.cs.
                                                                                             // Como é Scoped, cada requisição tem sua própria instância isolada.
                                                                                             //Se usasse app.Services em vez de context.RequestServices, pegaria o container raiz — que não tem serviços Scoped, causando erro.
            tenantContext.TenantId = Guid.Parse(tenantId);
            
        }
        await _next(context);
    }
}
