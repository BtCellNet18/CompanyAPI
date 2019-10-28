using CompanyAPI.Controllers;
using CompanyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Xunit;

namespace CompanyAPI_Test
{
	public class CompanyControllerTest
	{
		CompanyController controller;
		readonly ICompanyRepository repository;

		public CompanyControllerTest()
		{
			repository = new MockCompanyRepository();
			controller = new CompanyController(repository);
		}

		[Fact]
		public void GetAll_Returns_Ok()
		{
			// Act
			var response = controller.GetAll();

			// Assert
			Assert.IsType<OkObjectResult>(response.Result);
		}

		[Fact]
		public void GetAll_Returns_AllItems()
		{
			// Act
			var response = controller.GetAll().Result as OkObjectResult;

			// Assert
			var items = Assert.IsType<List<Company>>(response.Value);
			Assert.Equal(3, items.Count);
		}

		[Fact]
		public void GetById_Returns_NotFound()
		{
			// Act
			var response = controller.GetById(4);

			// Assert
			Assert.IsType<NotFoundResult>(response.Result);
		}

		[Fact]
		public void GetById_Returns_Ok()
		{
			// Act
			var response = controller.GetById(1);

			// Assert
			Assert.IsType<OkObjectResult>(response.Result);
		}

		[Fact]
		public void GetById_Returns_Requested_Id()
		{
			// Arrange 
			var company = repository.GetById(2);

			// Act
			var response = controller.GetById(company.Id).Result as OkObjectResult;

			// Assert
			var item = Assert.IsType<Company>(response.Value);
			Assert.Equal(company.Id, item.Id);
		}

		[Fact]
		public void GetByIsin_Returns_NotFound()
		{
			// Act
			var response = controller.GetByIsin("FAKE123");

			// Assert
			Assert.IsType<NotFoundResult>(response.Result);
		}

		[Fact]
		public void GetByIsin_Returns_Ok()
		{
			// Arrange
			var company = repository.GetById(2);

			// Act
			var response = controller.GetByIsin(company.Isin);

			// Assert
			Assert.IsType<OkObjectResult>(response.Result);
		}

		[Fact]
		public void GetByIsin_Returns_Requested_Isin()
		{
			// Arrange
			var company = repository.GetById(3);

			// Act
			var response = controller.GetByIsin(company.Isin).Result as OkObjectResult;

			// Assert
			var item = Assert.IsType<Company>(response.Value);
			Assert.Equal(company.Isin, item.Isin);
		}

		[Fact]
		public void Add_Duplicate_Isin_Returns_BadRequest()
		{
			// Arrange
			var company1 = repository.GetById(1);
			var company = new Company() { Name = "4", Exchange = "4", Ticker = "4", Isin = company1.Isin };

			// Act
			var response = controller.Add(company) as BadRequestResult;

			// Assert
			Assert.IsType<BadRequestResult>(response);
		}

		[Fact]
		public void Add_Invalid_Isin_Returns_BadRequest()
		{
			// Arrange
			var company = new Company() { Name = "4", Exchange = "4", Ticker = "4", Isin = "#Tag" };

			// Act
			var response = controller.Add(company) as BadRequestResult;

			// Assert
			Assert.IsType<BadRequestResult>(response);
		}

		[Fact]
		public void Add_Returns_CreatedAtAction()
		{
			// Arrange
			var company = new Company() { Name = "4", Exchange = "4", Ticker = "4", Isin = "FAKE4" };

			// Act
			var response = controller.Add(company) as CreatedAtActionResult;

			// Assert
			Assert.IsType<CreatedAtActionResult>(response);
		}

		[Fact]
		public void Update_Duplicate_Isin_Returns_BadRequest()
		{
			// Arrange
			var company1 = repository.GetById(1);
			var company2 = repository.GetById(2);
			company1.Isin = company2.Isin;

			// Act
			var response = controller.Update(company1);

			// Assert
			Assert.IsType<BadRequestResult>(response.Result);
		}

		[Fact]
		public void Update_Invalid_Isin_Returns_BadRequest()
		{
			// Arrange
			var company = repository.GetById(3);
			company.Isin = "A+";

			// Act
			var response = controller.Update(company);

			// Assert
			Assert.IsType<BadRequestResult>(response.Result);
		}

		[Fact]
		public void Update_Returns_NotFound()
		{
			// Arrange
			var company = new Company() { Id = 5, Name = "5", Exchange = "5", Ticker = "5", Isin = "FAKE" };

			// Act
			var response = controller.Update(company);

			// Assert
			Assert.IsType<NotFoundResult>(response.Result);
		}

		[Fact]
		public void Update_Returns_Ok()
		{
			// Arrange
			var company = repository.GetById(1);
			company.Exchange = "NASDAQ";

			// Act
			var response = controller.Update(company);

			// Assert
			Assert.IsType<OkObjectResult>(response.Result);
		}
	}
}
