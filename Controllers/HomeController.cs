using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using dr.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace dr.Controllers;

public class HomeController : Controller
{
    private readonly IMapper map;
    private readonly Context db;

    private readonly IWebHostEnvironment   _env;
    public HomeController(IMapper _map,Context _db,IWebHostEnvironment env)
    {
        map=_map;
        db=_db;
        _env=env;
    }

    public IActionResult Index()
    {
         ViewBag.userme=db.tbl_Users.OrderByDescending(a=>a.Id).ToList();
         ViewBag.doctor=db.tbl_Doctors.Count();
         ViewBag.user=db.tbl_Users.Count();
         ViewBag.question=db.tbl_Questions.Count();
        return View();
    }

    public IActionResult login()
    {
        // TODO: Your code here
        return View();
    }

     public IActionResult adddoctor()
    {
       
        
        return View();
    }
    

    [HttpPost]
    public async Task<IActionResult> adddoctorAsync(MV_Doctor dr)
    {

               string FileExtension1 = Path.GetExtension(dr.FileUploadMe.FileName);
                string  NewFileName = String.Concat(Guid.NewGuid().ToString(), FileExtension1);
                var path = $"{_env.WebRootPath}\\fileupload\\{NewFileName}";
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await dr.FileUploadMe.CopyToAsync(stream);
                }

       Tbl_Doctor d=new Tbl_Doctor()
       {
           Name=dr.Name,
           Email=dr.Email,
           password=dr.password,
           Registration_date=DateTime.Now,
           Descreption=dr.Descreption,
           FileUpload=NewFileName,
           City=dr.City,
           Gender=dr.Gender

       };




    
        db.tbl_Doctors.Add(d);
        db.SaveChanges();
        
        return View();
    }

    public IActionResult listdoctor()
    {
        var q=db.tbl_Doctors.OrderByDescending(a=>a.Id).ToList();
        var select=map.Map<List<MV_Doctor>>(q);
       
        return View(select);
    }
    
    public IActionResult addques()
    {
        // TODO: Your code here
        return View();
    }

[HttpPost]
     public IActionResult addques(string title)
    {
       Tbl_question q=new Tbl_question()
       {
           title=title,
           create_date=DateTime.Now
       };
       db.tbl_Questions.Add(q);
       db.SaveChanges();
       var finalid=db.tbl_Questions.OrderByDescending(a=>a.Id).Select(a=>a.Id).FirstOrDefault();
       HttpContext.Session.SetInt32("idquestion",finalid);
        return RedirectToAction("addlistques");
    }


[HttpPost]
    public IActionResult addlistques(string ques)
    {
        var idques=  HttpContext.Session.GetInt32("idquestion");
      
            Tbl_listquestion listques=new Tbl_listquestion()
            {
               title_question=ques,
               idquestion=idques.ToString(),
               
            };
            db.tbl_Listquestions.Add(listques);
            db.SaveChanges();
      
        return RedirectToAction("addlistques");
    }
    
    public IActionResult addlistques()
    {
        var idques=  HttpContext.Session.GetInt32("idquestion");
         var qlist=db.tbl_Listquestions.Where(a=>a.idquestion==idques.ToString()).ToList();
          var result=map.Map<List<MV_listquestion>>(qlist);
          ViewBag.t=db.tbl_Questions.Where(a=>a.Id==idques).Select(a=>a.title).FirstOrDefault();
            
      
        return View(result);
    }
    
    public IActionResult listquestions()
    {
       var q=db.tbl_Questions.OrderByDescending(a=>a.Id).ToList();
       var result=map.Map<List<MV_question>>(q);
        return View(result);
    }
    
    public IActionResult join(int id)
    {
       var qlist=db.tbl_Doctors.OrderByDescending(a=>a.Id).ToList();
       ViewBag.q=new SelectList(qlist, "Id", "Name");
       HttpContext.Session.SetInt32("idq",id);
       ViewBag.q2=db.tbl_Questions.Where(a=>a.Id==id).Select(a=>a.title).SingleOrDefault();
      
       //list
      
      var qjoin=db.tbl_Joins.ToList();

      List<MV_ViewJoin> j=new List<MV_ViewJoin>();

      foreach (var item in qjoin)
      {
           var qd=db.tbl_Questions.Where(a=>a.Id==item.idquestion).OrderByDescending(q=>q.Id).Select(a=>a.title).FirstOrDefault();
           var qq=db.tbl_Doctors.Where(a=>a.Id==item.iddoctor).OrderByDescending(q=>q.Id).Select(a=>a.Name).FirstOrDefault();

           MV_ViewJoin mj=new MV_ViewJoin()
           {
               Name=qd,
               Title=qq

           };
           j.Add(mj);
          
      }
      

       
        return View(j);
    }

    public IActionResult addjoin(int iddoctor)
    {
        int id=Convert.ToInt32(HttpContext.Session.GetInt32("idq"));
        if (db.tbl_Joins.Any(a=>a.iddoctor==iddoctor && a.idquestion==id))
        {
          TempData["err"]="The item is a duplicate" ;
        }else
        {
            Tbl_join j=new Tbl_join()
        {
            iddoctor=iddoctor,
            idquestion=id
             
        };
         db.tbl_Joins.Add(j);
         db.SaveChanges();
        }
        
        
        return RedirectToAction("join",new{id=id});
    }
    

    public IActionResult users()
    {
       ViewBag.userme=db.tbl_Users.OrderByDescending(a=>a.Id).ToList();
        return View();
    }
    
    public IActionResult proccess()
    {
         var qjoin=db.tbl_Joins.ToList();

      List<MV_ViewJoin> j=new List<MV_ViewJoin>();

      foreach (var item in qjoin)
      {
           var qd=db.tbl_Questions.Where(a=>a.Id==item.idquestion).OrderByDescending(q=>q.Id).Select(a=>a.title).FirstOrDefault();
           var qq=db.tbl_Doctors.Where(a=>a.Id==item.iddoctor).OrderByDescending(q=>q.Id).Select(a=>a.Name).FirstOrDefault();

           MV_ViewJoin mj=new MV_ViewJoin()
           {
               Name=qd,
               Title=qq,
               state=item.status,
               Id=item.Id

           };
           j.Add(mj);
          
      }
      

       
        return View(j);
    }
    
    public IActionResult act(int id)
    {
        var qjoin=db.tbl_Joins.Where(a=>a.Id==id).FirstOrDefault();
        qjoin.status=true;
        db.tbl_Joins.Update(qjoin);
        db.SaveChanges();
       
        return RedirectToAction("proccess");
    }
    
      public IActionResult dact(int id)
    {
        var qjoin=db.tbl_Joins.Where(a=>a.Id==id).FirstOrDefault();
        qjoin.status=false;
        db.tbl_Joins.Update(qjoin);
        db.SaveChanges();
       
        return RedirectToAction("proccess");
    }
    
    public IActionResult listactive()
    {
        var qjoin=db.tbl_Joins.Where(a=>a.status==true).ToList();
        
      List<MV_ViewJoin> j=new List<MV_ViewJoin>();

      foreach (var item in qjoin)
      {
           var qd=db.tbl_Questions.Where(a=>a.Id==item.idquestion).OrderByDescending(q=>q.Id).Select(a=>a.title).FirstOrDefault();
           var qq=db.tbl_Doctors.Where(a=>a.Id==item.iddoctor).OrderByDescending(q=>q.Id).Select(a=>a.Name).FirstOrDefault();

           MV_ViewJoin mj=new MV_ViewJoin()
           {
               Name=qd,
               Title=qq,
               state=item.status,
               Id=item.Id

           };
           j.Add(mj);
          
      }
      

       
        return View(j);
    }
    

    public IActionResult answer(int id)
    {
        var qjoin=db.tbl_Joins.Where(a=>a.Id==id).FirstOrDefault();

        HttpContext.Session.SetInt32("idjoin",id);
        

        var t=db.tbl_Questions.Where(a=>a.Id==qjoin.idquestion).FirstOrDefault();
         var d=db.tbl_Doctors.Where(a=>a.Id==qjoin.iddoctor).FirstOrDefault();
        
         var qlist=db.tbl_Listquestions.Where(a=>a.idquestion==t.Id.ToString()).ToList();

         int user=Convert.ToInt32(HttpContext.Session.GetInt32("iduder"));

          var quser=db.tbl_Users.Where(a=>a.Id==user).FirstOrDefault();
          if (quser != null)
          {
               ViewBag.name=quser.Name;
               ViewBag.email=quser.Email;
          }
         
          ViewBag.questiontitle=t.title;
          ViewBag.doctor=d.Name;

          List<MV_listquestions> ans=new List<MV_listquestions>();
           int idjoin=Convert.ToInt32(HttpContext.Session.GetInt32("idjoin")) ;
           int useme=Convert.ToInt32(HttpContext.Session.GetInt32("iduder"));
       
          
          foreach (var item in qlist)
          {
             var qanswer=db.tbl_Answers.Where(a=>a.Idlistquestion==item.Id&& a.idjoin==idjoin && a.IdUser==useme ).FirstOrDefault();
             if (qanswer != null)
             {
                 MV_listquestions a=new MV_listquestions()
                {
                        Idlistquestion=item.Id,
                        title_question=item.title_question,
                        answer=qanswer.answer
                        
                    

                };
                 ans.Add(a);
             }else
             {
                  MV_listquestions a=new MV_listquestions()
                {
                        Idlistquestion=item.Id,
                        title_question=item.title_question,
                        
                        
                    

                };
                  ans.Add(a);
             }

              
             
              
              
          }


        
            
      
        return View(ans);
    }
    
    [HttpPost]
    public IActionResult Adduser(string Name,string Email)
    {
        int idjoin=Convert.ToInt32(HttpContext.Session.GetInt32("idjoin")) ;
         var qjoin=db.tbl_Joins.Where(a=>a.Id==idjoin).FirstOrDefault();
         var t=db.tbl_Questions.Where(a=>a.Id==qjoin.idquestion).FirstOrDefault();

        var q=db.tbl_Users.Where(a=>a.joinid==idjoin && a.Name==Name && a.Email==Email).FirstOrDefault();
        if (q != null)
        {
            q.Name=Name;
            q.Email=Email;
            db.tbl_Users.Update(q);
            db.SaveChanges();
            var finalid=db.tbl_Users.OrderByDescending(a=>a.Id).Take(1).Select(a=>a.Id).FirstOrDefault();
            HttpContext.Session.SetInt32("iduder",finalid);
            return RedirectToAction("answer",new{id=idjoin});

        }else
        {
            Tbl_Users usm=new Tbl_Users()
            {
                Name=Name,
                Email=Email,
                create_date=DateTime.Now,
                joinid=idjoin,
                Idquestion=t.Id


            };

             db.tbl_Users.Add(usm);
             db.SaveChanges();
             var finalid=db.tbl_Users.OrderByDescending(a=>a.Id).Take(1).Select(a=>a.Id).FirstOrDefault();
              HttpContext.Session.SetInt32("iduder",finalid);
            return RedirectToAction("answer",new{id=idjoin});

        }
       
    }


    public IActionResult addanswer(string answer,string Idlistquestion)
    {
        int idjoin=Convert.ToInt32(HttpContext.Session.GetInt32("idjoin")) ;
        int user=Convert.ToInt32(HttpContext.Session.GetInt32("iduder"));
        var qanswer=db.tbl_Answers.Where(a=>a.Idlistquestion==Convert.ToInt32(Idlistquestion)  && a.idjoin==idjoin && a.IdUser==user ).FirstOrDefault();
        if(qanswer != null)
        {
           qanswer.answer=answer;
           qanswer.answer_date=DateTime.Now;
            db.tbl_Answers.Update(qanswer);
             db.SaveChanges();

            return RedirectToAction("answer",new{id=idjoin});


        }else
        {
             Tbl_answer ans=new Tbl_answer()
            {
              Idlistquestion=Convert.ToInt32(Idlistquestion),
              idjoin=idjoin,
              IdUser=user,
              answer=answer,
              answer_date=DateTime.Now

            };
             db.tbl_Answers.Add(ans);
             db.SaveChanges();
            return RedirectToAction("answer",new{id=idjoin});
        }
        return View();
    }
    
    
    public IActionResult finaliz()
    {
        var id=Convert.ToInt32(HttpContext.Session.GetInt32("idjoin"));
        var qjoin=db.tbl_Joins.Where(a=>a.Id==id).FirstOrDefault();
        qjoin.status=false;
        db.tbl_Joins.Update(qjoin);
        db.SaveChanges();

       return RedirectToAction("listactive");

       
        
    }
    

     public IActionResult showanswer(int id,int user)
    {
        HttpContext.Session.SetInt32("iduder",user);
         var qjoin=db.tbl_Joins.Where(a=>a.Id==id).ToList();

      List<MV_ViewJoin> j=new List<MV_ViewJoin>();

      foreach (var item in qjoin)
      {
           var qd=db.tbl_Questions.Where(a=>a.Id==item.idquestion).OrderByDescending(q=>q.Id).Select(a=>a.title).FirstOrDefault();
           var qq=db.tbl_Doctors.Where(a=>a.Id==item.iddoctor).OrderByDescending(q=>q.Id).Select(a=>a.Name).FirstOrDefault();

           MV_ViewJoin mj=new MV_ViewJoin()
           {
               Name=qd,
               Title=qq,
               state=item.status,
               Id=item.Id

           };
           j.Add(mj);
          
      }
      

       
        return View(j);
    }
    
    
}
