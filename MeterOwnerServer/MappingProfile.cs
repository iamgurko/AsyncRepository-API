﻿using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using System.Linq;

namespace AccountOwnerServer
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Owner, OwnerDto>();

            CreateMap<Meter, MeterDto>();

            CreateMap<Owner, OwnerDto>();

            CreateMap<OwnerForCreationDto, Owner>();

            CreateMap<OwnerForUpdateDto, Owner>();
        }
    }
}
