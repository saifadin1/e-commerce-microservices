using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();
        if (await session.Query<Product>().AnyAsync())
            return;
        
        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync(cancellation);
    }
    
    private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>
    {
        new Product()
        {
            Id = new Guid("5334c996-8457-4cf0-815c-ed2b7bc4ff61"),
            Name = "IPhone X",
            Description = "This phone is the company's biggest change to its flagship design in years.",
            ImageUrl = "product-1.png",
            Price = 950.00M,
            Category = new List<string> { "Smart Phone" }
        },
        new Product()
        {
            Id = new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"),
            Name = "Samsung 10",
            Description = "The definitive big-screen phone experience with an ultra-wide triple camera system.",
            ImageUrl = "product-2.png",
            Price = 849.99M,
            Category = new List<string> { "Smart Phone" }
        },
        new Product()
        {
            Id = new Guid("7a4e3210-2b1a-45c3-bd98-76543210abcd"),
            Name = "Google Pixel 8",
            Description = "The all-powered Google phone featuring the latest AI camera enhancements.",
            ImageUrl = "product-3.png",
            Price = 699.00M,
            Category = new List<string> { "Smart Phone" }
        },
        new Product()
        {
            Id = new Guid("11223344-5566-7788-9900-aabbccddeeff"),
            Name = "MacBook Pro 16",
            Description = "Supercharged by the latest silicon chip, built for developers, creators, and pros.",
            ImageUrl = "product-4.png",
            Price = 2499.00M,
            Category = new List<string> { "Laptop", "Computers" }
        },
        new Product()
        {
            Id = new Guid("abcdef01-2345-6789-abcd-ef0123456789"),
            Name = "Dell XPS 15",
            Description = "Stunning OLED display with powerhouse performance for heavy productivity.",
            ImageUrl = "product-5.png",
            Price = 1899.50M,
            Category = new List<string> { "Laptop", "Computers" }
        },
        new Product()
        {
            Id = new Guid("fe654321-4321-4321-4321-0987654321fe"),
            Name = "Sony WH-1000XM5",
            Description = "Industry-leading wireless noise-canceling headphones with premium studio sound.",
            ImageUrl = "product-6.png",
            Price = 398.00M,
            Category = new List<string> { "Audio", "Accessories" }
        },
        new Product()
        {
            Id = new Guid("99887766-5544-3322-1100-abcdef123456"),
            Name = "iPad Pro 12.9",
            Description = "The ultimate tablet experience with a Liquid Retina XDR display and extreme processing power.",
            ImageUrl = "product-7.png",
            Price = 1099.00M,
            Category = new List<string> { "Tablet", "Electronics" }
        }
    };
}