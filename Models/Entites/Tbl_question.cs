using System;
using System.ComponentModel.DataAnnotations;

public class Tbl_question
{
    [Key]
    public int Id { get; set; }
    public string title { get; set; }
    
    public DateTime create_date { get; set; }
   
   
    
    
    
}