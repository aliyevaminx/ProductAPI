using Data.Contexts;
using Data.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Product.Write;

public class ProductWriteRepository : BaseWriteRepository<Core.Entities.Product>, IProductWriteRepository
{
	public ProductWriteRepository(AppDbContext context) : base(context)
	{
		
	}
}
