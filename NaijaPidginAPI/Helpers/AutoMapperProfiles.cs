using AutoMapper;
using NaijaPidginAPI.DTOs;
using NaijaPidginAPI.Entities;

namespace NaijaPidginAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Word, AddWordDTO>().ReverseMap();

            CreateMap<Word, ListWordDTO>()
                .ForMember(w => w.WordClassname, opt => opt.MapFrom(src => src.WordClass.WordClassname));
        }
        
    }
}
