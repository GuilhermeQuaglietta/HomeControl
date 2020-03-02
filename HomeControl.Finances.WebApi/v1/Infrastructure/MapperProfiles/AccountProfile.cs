using AutoMapper;
using HomeControl.Finances.Domain.Entity.AccountAggregate;
using HomeControl.Finances.Infrastructure.Persistence.AccountData.Entity;

namespace HomeControl.Finances.WebApi.Infrastructure.MapperProfiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Account, AccountEntity>().ReverseMap();
            CreateMap<AccountTransaction, AccountTransactionEntity>().ReverseMap();
            CreateMap<AccountTransfer, AccountTransferEntity>().ReverseMap();
        }
    }
}
