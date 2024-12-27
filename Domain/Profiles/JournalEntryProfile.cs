using AutoMapper;
using Domain.DataTransferObjects;
using Domain.Models;

namespace Domain.DataModels
{
    public class JournalEntryProfile : Profile
    {
        public JournalEntryProfile()
        {
            CreateMap<JournalEntry, JournalEntryDto>();
            CreateMap<JournalEntryDto, JournalEntry>();
            // Use CreateMap... Etc.. here (Profile methods are the same as configuration methods)
        }
    }
}
