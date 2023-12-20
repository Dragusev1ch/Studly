using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Studly.BLL.DTO.Challenge;
using Studly.BLL.DTO.Customer;
using Studly.BLL.DTO.SubTask;
using Studly.DAL.Entities;
using Studly.Entities;

namespace Studly.BLL.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {   
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerDTO, Customer>();

            CreateMap<Customer, CustomerRegistrationDTO>();
            CreateMap<CustomerRegistrationDTO, Customer>();

            CreateMap<Challenge, ChallengeDto>();
            CreateMap<ChallengeDto, Challenge>();

            CreateMap<Challenge, ChallengeRegistrationDto>();
            CreateMap<ChallengeRegistrationDto, Challenge>();

            CreateMap<SubTask, SubTaskDto>();
            CreateMap<SubTaskDto, SubTask>();

            CreateMap<SubTask, SubTaskRegistrationDto>();
            CreateMap<SubTaskRegistrationDto, SubTask>();
        }
    }
}
