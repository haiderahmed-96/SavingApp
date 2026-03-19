using AutoMapper;
using SavingsApp.Models.Entities;

namespace SavingsApp.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // SavingGoal Mappings
            CreateMap<CreateSavingGoalDto, SavingGoal>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.Now));

            CreateMap<UpdateSavingGoalDto, SavingGoal>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<SavingGoal, SavingGoalDetailsDto>();
            CreateMap<SavingGoal, SavingGoalListItemDto>();

            // TravelSaving Mappings
            CreateMap<CreateTravelSavingDto, TravelSaving>();

            // EventSaving Mappings
            CreateMap<CreateEventSavingDto, EventSaving>();

            // GroupSaving Mappings
            CreateMap<CreateGroupSavingDto, GroupSaving>();

            // SavingTransaction Mappings
            CreateMap<AddSavingTransactionDto, SavingTransaction>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.Now));

            // GroupMember Mappings
            CreateMap<AddGroupMemberDto, GroupMember>();
        }
    }
}
