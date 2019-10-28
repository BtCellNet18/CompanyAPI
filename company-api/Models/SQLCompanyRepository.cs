using System.Collections.Generic;
using System.Linq;

namespace CompanyAPI.Models
{
	public class SQLCompanyRepository : ICompanyRepository
	{
		private readonly CompanyDBContext context;

		public SQLCompanyRepository(CompanyDBContext context)
		{
			this.context = context;
		}

		public Company Add(Company item)
		{
			context.Add(item);
			context.SaveChanges();
			return item;
		}

		public IEnumerable<Company> GetAll()
		{
			return context.Company;
		}

		public Company GetById(int id)
		{
			return context.Company.Find(id);
		}

		public Company GetByIsin(string isin)
		{
			return context.Company.FirstOrDefault(c => c.Isin == isin);
		}

		public Company Update(Company item)
		{
			var company = context.Company.Find(item.Id);

			if (company != null)
			{
				company.Name = item.Name;
				company.Exchange = item.Exchange;
				company.Ticker = item.Ticker;
				company.Isin = item.Isin;
				company.Website = item.Website;
				context.SaveChanges();
			}

			return company;
		}
	}
}
