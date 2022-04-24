using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ServerAppTwo.DTO;
using ServerAppTwo.Models;

namespace ServerAppTwo.Helpers
{
    public class MapperProfiles:Profile
    {
        public MapperProfiles()
        {
            CreateMap<User,UserForListDTO>()
            .ForMember(dest=>dest.Image,opt=>opt.MapFrom(src=>src.Images.FirstOrDefault(x=>x.IsProfile)))
            .ForMember(dest=>dest.ProfileImage,opt=>opt.MapFrom(x=>x.Images.FirstOrDefault(x=>x.IsProfile).Name))
            .ForMember(dest=>dest.Age,opt=>opt.MapFrom(x=>x.DateOfBirth.CalculateAge()));
           CreateMap<Image,ImagesForDetailDTO>();
        }
    }
}