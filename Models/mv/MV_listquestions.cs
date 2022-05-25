using System;
using System.ComponentModel.DataAnnotations;

public class MV_listquestions
{
    [Key]
    public int Id { get; set; }
    public string idquestion { get; set; }
    
    public string title_question { get; set; }

    public int Idanswer { get; set; }
    public int  IdUser { get; set; }
    public int IdJoin { get; set; }
    public int Idlistquestion { get; set; }
    public string answer { get; set; }
   
    public DateTime answer_date { get; set; }
    public bool state { get; set; }
    public int Iduser { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
     public DateTime create_date { get; set; }
    
    
   
   
    
    
    
}