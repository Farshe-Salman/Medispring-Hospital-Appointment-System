using AutoMapper;
using BLL.DTOs;
using BLL.Services;
using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MapperConfig
    {
        static MapperConfiguration cfg = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Branch, BranchDTO>().ReverseMap();

        });

        public static Mapper GetMapper()
        {
            return new Mapper(cfg);
        }
    }
}
