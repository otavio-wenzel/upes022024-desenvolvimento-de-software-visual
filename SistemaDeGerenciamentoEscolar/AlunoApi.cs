using Microsoft.EntityFrameworkCore;

public static class AlunoApi
{
  public static void MapAlunoApi(this WebApplication app)
  {
    var tabela = app.MapGroup("/alunos");

    tabela.MapGet("/alunos", async (BancoDeDados db) =>
    await db.Alunos.ToListAsync());

    tabela.MapGet("/alunos/{id}", async (int id, BancoDeDados db) => 
        await db.Alunos.FindAsync(id)
        is Aluno aluno
            ? Results.Ok(aluno)
            : Results.NotFound());

    tabela.MapPost("/alunos", async (Aluno aluno, BancoDeDados db) => {
        db.Alunos.Add(aluno);
        await db.SaveChangesAsync();
        return Results.Created($"/alunos/{aluno.Id}", aluno);
    });

    tabela.MapPut("alunos/{id}", async (int id, Aluno alunoAtualizado, BancoDeDados db) =>
    {
        var aluno = await db.Alunos.FindAsync(id);
        if (aluno is null) return Results.NotFound();

        aluno.Nome = alunoAtualizado.Nome;
        aluno.Matricula = alunoAtualizado.Matricula;
        aluno.Idade = alunoAtualizado.Idade;

        await db.SaveChangesAsync();

        return Results.NoContent();
    });

    tabela.MapDelete("alunos/{id}", async (int id, BancoDeDados db) =>
    {
        if(await db.Alunos.FindAsync(id) is Aluno aluno){

            db.Alunos.Remove(aluno);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }
        return Results.NotFound();
    
    });
  }
}
