using System;
using System.ComponentModel.DataAnnotations;

public class MV_Users
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
     public DateTime create_date { get; set; }
    public int joinid { get; set; }
    public int Idquestion { get; set; }
    
    
    
    
    
}