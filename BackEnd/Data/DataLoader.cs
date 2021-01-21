using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Data
{
    public abstract class DataLoader<T, Y>
    {
        public abstract Task<ServiceResponse<Y>> DataLoaderAsync(IFormFile file, T storage);
    }
}
