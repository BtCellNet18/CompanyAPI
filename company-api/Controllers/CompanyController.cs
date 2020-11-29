using CompanyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CompanyAPI.Controllers
{
  [ApiController]
	[Route("api/[controller]")]
	public class CompanyController : ControllerBase
	{
		private readonly ICompanyRepository companyRepository;

		public CompanyController(ICompanyRepository companyRepository)
		{
			this.companyRepository = companyRepository;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Company>> GetAll()
		{
			var items = companyRepository.GetAll();
			return Ok(items);
		}

		[HttpGet("{id:int}")]
		public ActionResult<Company> GetById(int id)
		{
			var item = companyRepository.GetById(id);

			if (item == null)
			{
				return NotFound();
			}

			return Ok(item);
		}

		[HttpGet("{isin}")]
		public ActionResult<Company> GetByIsin(string isin)
		{
			var item = companyRepository.GetByIsin(isin);

			if (item == null)
			{
				return NotFound();
			}

			return Ok(item);
		}

		[HttpPost]
		public ActionResult Add(Company item)
		{
			if (!this.IsCompanyValid(item))
			{
				return BadRequest();
			}

			companyRepository.Add(item);

			return CreatedAtAction(nameof(GetById), new
			{
				id = item.Id
			}, item);
		}

		[HttpPut("{id}")]
		public ActionResult<Company> Update(Company item)
		{
			if (!this.IsCompanyValid(item))
			{
				return BadRequest();
			}

			var company = companyRepository.Update(item);

			if (company == null)
			{
				return NotFound();
			}

			return Ok(item);
		}

		private bool IsCompanyValid(Company item)
		{
			if (string.IsNullOrWhiteSpace(item.Name) ||
			 string.IsNullOrWhiteSpace(item.Exchange) ||
			 string.IsNullOrWhiteSpace(item.Ticker) ||
			 string.IsNullOrWhiteSpace(item.Isin) ||
			 item.Isin.Length < 2)
			{
				return false;
			}

			string input = item.Isin.Substring(0, 2);

			for (int i = 0; i < input.Length; i++)
			{
				if (!char.IsLetter(input[i]))
				{
					return false;
				}
			}

			var companies = companyRepository.GetAll();

			if (companies?.Count() > 0 &&
			 companies.Any(c => c.Id != item.Id &&
				c.Isin.ToLower() == item.Isin.ToLower()))
			{
				return false;
			}

			return true;
		}
	}
}
