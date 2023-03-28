﻿using System.Collections.Generic;
using DogGo.Models;

namespace DogGo.Repositories
{
    public interface INeighborhoodRepository
    {
        List<Neighborhood> GetAll();
    }
}
