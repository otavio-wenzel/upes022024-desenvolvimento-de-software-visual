using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Configurações Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BancoDeDados>();

var app = builder.Build();

//Configurações Swagger
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Sistema de Gerenciamento Escolar");
app.MapAlunoApi();
app.MapDisciplinaApi();
app.MapProfessorApi();
app.MapTurmaApi();

app.Run();