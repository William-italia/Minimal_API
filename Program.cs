using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
// CONFIGURAÇÕES
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

// //ADICIONAR SERVIÇOS
app.MapGet("/", () => DateTime.Now);

app.MapGet("/teste1", () =>
{

  var agora = DateTime.Now;
  return $"Agora é {agora}.";
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


app.Run();

public record area(double x, double y);