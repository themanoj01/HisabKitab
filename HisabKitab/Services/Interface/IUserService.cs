﻿using HisabKitab.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HisabKitab.Services.Interface
{
    public interface IUserService
    {
        bool Login(Users user);
    }
}
