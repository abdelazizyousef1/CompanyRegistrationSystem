﻿using Company.Services.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.Interfaces
{
    public interface IHomeService
    {
        Task<HomeResponseDto> GetHomeDataAsync();
    }
}
