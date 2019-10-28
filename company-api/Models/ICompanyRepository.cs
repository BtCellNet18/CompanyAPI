using System.Collections.Generic;

namespace CompanyAPI.Models
{
	public interface ICompanyRepository
	{
		Company GetById(int id);
		Company GetByIsin(string isin);
		IEnumerable<Company> GetAll();
		Company Add(Company item);
		Company Update(Company item);
	}
}
