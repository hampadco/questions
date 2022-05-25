using System;
using System.ComponentModel.DataAnnotations;

public class Tbl_join
{
    [Key]
    public int Id { get; set; }
    public int iddoctor { get; set; }
    
    public int idquestion { get; set; }
    public bool status { get; set; }
    
    
    
  
    
    
    
}