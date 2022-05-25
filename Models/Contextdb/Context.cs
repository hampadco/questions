using Microsoft.EntityFrameworkCore;

public class Context:DbContext
{
    
    public DbSet<Tbl_Doctor> tbl_Doctors { get; set; }
    public DbSet<Tbl_answer> tbl_Answers { get; set; }
     public DbSet<Tbl_listquestion> tbl_Listquestions { get; set; }
      public DbSet<Tbl_question> tbl_Questions { get; set; }
      public DbSet<Tbl_join> tbl_Joins { get; set; }
      public DbSet<Tbl_Users> tbl_Users { get; set; }
      
      
    
    
    
    
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder db)
    {
        db.UseSqlServer("data source=.;initial catalog=dr;integrated security=true");
    }
    
    
    
    
    
}