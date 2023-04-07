using AutoMapper;
using MyDoo.Entities;
using MyDoo.Views;

namespace MyDoo;

public class MappingProfile : Profile
{
   public MappingProfile()
   {
      CreateMap<UserView, User>();
      CreateMap<GetTasksView, UserTask>();
   }
}