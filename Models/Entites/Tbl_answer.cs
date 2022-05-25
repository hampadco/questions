using System;
using System.ComponentModel.DataAnnotations;

public class Tbl_answer
{
    [Key]
    public int Id { get; set; }
     public int idjoin { get; set; }
    public int Idlistquestion { get; set; }
    public string answer { get; set; }
    public int IdUser { get; set; }
    public DateTime answer_date { get; set; }
    
   
    
    
    
    
    
    
    
    
    
    
   
    
    
   
   
    
    
    
}