using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
// CONFIGURAÇÕES
var app = builder.Build();

//middleware

app.UseHttpsRedirection();


app.UseSwagger();
app.UseSwaggerUI();


app.UseDefaultFiles();
app.UseStaticFiles();

// //ADICIONAR SERVIÇOS
// app.MapGet("/", () => DateTime.Now);

app.MapGet("/api", () => "Resposta ao método GET");
app.MapPost("/api", () => "Resposta ao método POST");
app.MapPut("/api", () => "Resposta ao método PUT");
app.MapDelete("/api", () => "Resposta ao método DELETE");
app.MapMethods("/api", new[] { "PATCH" }, () => "Resposta ao método PATCH");
// app.MapGet("/api", (int x, int y) => $"Recebido: x={x} e y+{y}"); Query string
// app.MapGet("/api/{x}/{y}", (int x, int y) => $"Recebido: x={x} e y={y}");
// app.MapGet("/api/{x}", ([FromRoute] int x, [FromQuery] int y) => $"Recebido: x={x} e y={y}");

app.MapPost("/apixy", ([FromBody] DadosEntrada entrada) => $"Recebido: x = {entrada.x} e y ={entrada.y}");

// app.MapGet("/teste1", () =>
// {

//   var agora = DateTime.Now;
//   return $"Agora é {agora}.";
// });

app.MapGet("/hellow-word", () =>
{

  return Results.Ok(new { mensagem = "Hellow, word!" });
});

app.MapGet("/soma", (int n1, int n2) =>
{

  app.Logger.LogInformation($"O usuário informou n1 = {n1} e n2 = {n2}");

  int soma = n1 + n2;
  return soma;
});

app.MapGet("/divião", (double numerador, double denominador) =>
{
  app.Logger.LogInformation($"O usuário informou n1={numerador} e n2 ={denominador}");

  if (denominador == 0)
  {
    return Results.BadRequest("Denominador não pode ser zero");
  }

  double divisao = numerador / denominador;

  return Results.Ok($"{divisao:N2}");
});

app.MapPost("/triangulo", ([FromBody] area area) =>
{

  return Results.Ok($"{(area.x * area.y):N3}");
});

app.Logger.LogInformation("Aplicação iniciada.");

app.Run();

public record area(double x, double y);

public class DadosEntrada
{
  public int x { get; set; }

  public int y { get; set; }
}