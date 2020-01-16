using IShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IShop.BusinessLogic.Services
{
    public abstract class ServiceBase
    {
        public int GetMaxId(List<IIdentifiable> entities)
        {
            if (entities == null || !entities.Any())
                return 0;

            return entities.Max(e => e.Id);
        }

        protected string GetStoragePath(string fileName)
        {
            return $@"{AppDomain.CurrentDomain.BaseDirectory}\{fileName}";
        }
    }
}
