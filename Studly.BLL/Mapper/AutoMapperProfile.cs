using AutoMapper;
using Studly.BLL.DTO.Challenge;
using Studly.BLL.DTO.Customer;
using Studly.DAL.Entities;
using Studly.Entities;

namespace Studly.BLL.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {   
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();

            CreateMap<Customer, CustomerListDto>();
            CreateMap<CustomerListDto, Customer>();

            CreateMap<Customer, CustomerRegistrationDTO>();
            CreateMap<CustomerRegistrationDTO, Customer>();

            CreateMap<Challenge, ChallengeDto>();
            CreateMap<ChallengeDto, Challenge>();

            CreateMap<Challenge, ChallengeRegistrationDto>();
            CreateMap<ChallengeRegistrationDto, Challenge>();
        }
    }
}
