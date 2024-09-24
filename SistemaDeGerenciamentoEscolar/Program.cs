using Microsoft.EntityFrameworkCore;

public class BancoDeDados : DbContext {


    protected override void OnConfiguring(
        DbContextOptionsBuilder builder)
    {
        string credencial = "server=localhost;port=3306;database=planner;user=root;password=positivo";

        builder.UseMySQL(credencial);
        //registros da solictação da api
        //.LogTo(Console.WriteLine, LogLevel.Information);
        
    }

    public DbSet<Aluno> Alunos => Set<Aluno>();
    public DbSet<Professor> Professores => Set<Professor>();
    public DbSet<Disciplina> Disciplinas => Set<Disciplina>();
    public DbSet<Turma> Turmas => Set<Turma>();

}