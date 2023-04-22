using AutoMapper;
using MyDoo.Entities;
using MyDoo.Views;

namespace MyDoo;

public class MappingProfile : Profile
{
   public MappingProfile()
   {
      CreateMap<UserLoginView, User>();
      CreateMap<UserRegisterView, User>();
      CreateMap<GetTasksView, UserTask>();
   }
}