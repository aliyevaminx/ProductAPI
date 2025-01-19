using Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Product.Read;

public interface IProductReadRepository : IBaseReadRepository<Core.Entities.Product>
{
	Task<Core.Entities.Product> GetByNameAsync(string name);
}
