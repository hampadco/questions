using AutoMapper;

public class Mapme:Profile
{
    public Mapme()
    {
        CreateMap<Tbl_Doctor,MV_Doctor>().ReverseMap();
         CreateMap<Tbl_listquestion,MV_listquestion>().ReverseMap();
          CreateMap<Tbl_question,MV_question>().ReverseMap();
    }
}