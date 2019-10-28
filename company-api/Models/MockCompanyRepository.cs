using System.Collections.Generic;
using System.Linq;

namespace CompanyAPI.Models
{
	public class MockCompanyRepository : ICompanyRepository
	{
		private readonly List<Company> companies;

		public MockCompanyRepository()
		{
			companies = new List<Company>()
			{
				new Company() { Id = 1, Name = "1", Exchange = "1", Ticker="1", Isin="FAKE1" },
				new Company() { Id = 2, Name = "2", Exchange = "2", Ticker="2", Isin="FAKE2" },
				new Company() { Id = 3, Name = "3", Exchange = "3", Ticker="3", Isin="FAKE3" }
			};
		}
		Company ICompanyRepository.Add(Company item)
		{
			companies.Add(item);
			return item;
		}

		IEnumerable<Company> ICompanyRepository.GetAll()
		{
			return companies;
		}

		Company ICompanyRepository.GetById(int id)
		{
			return companies.FirstOrDefault(c => c.Id == id);
		}

		Company ICompanyRepository.GetByIsin(string isin)
		{
			return companies.FirstOrDefault(c => c.Isin == isin);
		}

		Company ICompanyRepository.Update(Company item)
		{
			var company = companies.FirstOrDefault(c => c.Id == item.Id);

			if (company != null)
			{
				company.Name = item.Name;
				company.Exchange = item.Exchange;
				company.Ticker = item.Ticker;
				company.Isin = item.Isin;
				company.Website = item.Website;
			}

			return company;
		}
	}
}
