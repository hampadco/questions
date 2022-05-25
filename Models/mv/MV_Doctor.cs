using System;
public class MV_Doctor
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string password { get; set; }
    
    public string Gender { get; set; }
    public string FileUpload { get; set; }

    public string City { get; set; }
    public string Descreption { get; set; }
    
    
 public IFormFile FileUploadMe { get; set; }
   public DateTime Registration_date { get; set; } 
    
    
    
}