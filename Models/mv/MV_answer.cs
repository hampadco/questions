using System;
using System.ComponentModel.DataAnnotations;

public class MV_answer
{
    [Key]
    public int Id { get; set; }
    public int  IdUser { get; set; }
    public int IdJoin { get; set; }
    public int Idlistquestion { get; set; }
    public string answer { get; set; }
   
    public DateTime answer_date { get; set; }
    public bool state { get; set; }
    
    
    
    
    
    
    
    
   
    
    
   
   
    
    
    
}