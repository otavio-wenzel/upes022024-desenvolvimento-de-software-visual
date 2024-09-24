using Microsoft.EntityFrameworkCore;

public static class TurmaApi
{
  public static void MapTurmaApi(this WebApplication app)
  {
    var tabela = app.MapGroup("/turmas");

    tabela.MapGet("/turmas", async (BancoDeDados db) =>
    await db.Turmas.ToListAsync());

    tabela.MapGet("/turmas/{id}", async (int id, BancoDeDados db) => 
        await db.Turmas.FindAsync(id)
        is Turma turma
            ? Results.Ok(turma)
            : Results.NotFound());

    tabela.MapPost("/turmas", async (Turma turma, BancoDeDados db) => {
        db.Turmas.Add(turma);
        await db.SaveChangesAsync();
        return Results.Created($"/turmas/{turma.Id}", turma);
    });

    tabela.MapPut("turmas/{id}", async (int id, Turma turmaAtualizada, BancoDeDados db) =>
    {
        var turma = await db.Turmas.FindAsync(id);
        if (turma is null) return Results.NotFound();

        turma.Serie = turmaAtualizada.Serie;
        turma.Turno = turmaAtualizada.Turno;

        await db.SaveChangesAsync();

        return Results.NoContent();
    });

    tabela.MapDelete("turmas/{id}", async (int id, BancoDeDados db) =>
    {
        if(await db.Turmas.FindAsync(id) is Turma turma){

            db.Turmas.Remove(turma);
            await db.SaveChangesAsync();
            return Results.NoContent();
        }
        return Results.NotFound();
    
    });
  }
}
