using AutoMapper;
using Cybervision.Dapr.DataModels;
using Domain.DataTransferObjects;

namespace Cybervision.Dapr.Profiles
{
    public class JournalEntryProfile : Profile
    {
        public JournalEntryProfile()
        {
            CreateMap<JournalEntryDocument, JournalEntryDto>();
            CreateMap<JournalEntryDto, JournalEntryDocument>();
        }
    }
}
