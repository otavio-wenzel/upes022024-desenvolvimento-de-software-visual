using Microsoft.EntityFrameworkCore;

public static class DisciplinaApi
{
  public static void MapDisciplinaApi(this WebApplication app)
  {
    var tabela = app.MapGroup("/disciplinas");

    tabela.MapGet("/discilinas", async (BancoDeDados db) =>
    await db.Disciplinas.ToListAsync());

    tabela.MapGet("/disciplinas/{id}", async (int id, BancoDeDados db) => 
        await db.Disciplinas.FindAsync(id)
        is Disciplina disciplina
            ? Results.Ok(disciplina)
            : Results.NotFound());

    tabela.MapPost("/disciplinas", async (Disciplina disciplina, BancoDeDados db) => {
        db.Disciplinas.Add(disciplina);
        await db.SaveChangesAsync();
        return Results.Created($"/disciplinas/{disciplina.Id}", disciplina);
    });

    tabela.MapPut("disciplina/{id}", async (int id, Disciplina disciplinaAtualizada, BancoDeDados db) =>
    {
        var disciplina = await db.Disciplinas.FindAsync(id);
        if (disciplina is null) return Results.NotFound();

        disciplina.Materia = alunoAtualizado.Materia;

        await db.SaveChangesAsync();

        return Results.NoContent();
    });

    tabela.MapDelete("disciplinass/{id}", async (int id, BancoDeDados db) =>
    {
        if(await db.Disciplinas.FindAsync(id) is Disciplina disciplina){

            db.Disciplinas.Remove(disciplina);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }
        return Results.NotFound();
    
    });
  }
}
