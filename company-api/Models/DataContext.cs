using Microsoft.EntityFrameworkCore;

namespace CompanyAPI.Models
{
  public partial class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Company { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Company>().HasData(
        new Company
        {
          Id = 1,
          Name = "Apple Inc",
          Exchange = "NASDAQ",
          Ticker = "AAPL",
          Isin = "US0378331005",
          Website = "http://www.apple.com"
        },
        new Company
        {
          Id = 2,
          Name = "British Airways Plc",
          Exchange = "Pink Sheets",
          Ticker = "BAIRY",
          Isin = "US1104193065",
          Website = null
        },
        new Company
        {
          Id = 3,
          Name = "Heineken NV",
          Exchange = "Euronext Amsterdam",
          Ticker = "HEIA",
          Isin = "NL0000009165",
          Website = null
        },
        new Company
        {
          Id = 4,
          Name = "Panasonic Corp",
          Exchange = "Tokyo Stock Exchange",
          Ticker = "6752",
          Isin = "JP3866800000",
          Website = "http://panasonic.co.jp"
        },
        new Company
        {
          Id = 5,
          Name = "Porsche Automobil",
          Exchange = "Deutsche Borse",
          Ticker = "PAH3",
          Isin = "DE000PAH0038",
          Website = "https://www.porsche.com"
        }
      );

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}
