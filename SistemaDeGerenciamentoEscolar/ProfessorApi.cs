using Microsoft.EntityFrameworkCore;

public static class ProfessorApi
{
  public static void MapProfessorApi(this WebApplication app)
  {
    var tabela = app.MapGroup("/professores");

    tabela.MapGet("/professores", async (BancoDeDados db) =>
    await db.Professores.ToListAsync());

    tabela.MapGet("/professores/{id}", async (int id, BancoDeDados db) => 
        await db.Professores.FindAsync(id)
        is Professor professor
            ? Results.Ok(professor)
            : Results.NotFound());

    tabela.MapPost("/professores", async (Professor professor, BancoDeDados db) => {
        db.Professores.Add(professor);
        await db.SaveChangesAsync();
        return Results.Created($"/professores/{professor.Id}", professor);
    });

    tabela.MapPut("professores/{id}", async (int id, Professor professorAtualizado, BancoDeDados db) =>
    {
        var professor = await db.Professores.FindAsync(id);
        if (professor is null) return Results.NotFound();

        professor.Nome = professorAtualizado.Nome;
        professor.Curso = professorAtualizado.Curso;
        professor.Matricula = professorAtualizado.Matricula;

        await db.SaveChangesAsync();

        return Results.NoContent();
    });

    tabela.MapDelete("professores/{id}", async (int id, BancoDeDados db) =>
    {
        if(await db.Professores.FindAsync(id) is Professor professor){

            db.Professores.Remove(professor);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }
        return Results.NotFound();
    
    });
  }
}
