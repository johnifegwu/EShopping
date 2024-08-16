//=====================================================
Jay.EFCore.UnitOfWork

A dynamic repository and UnitOfWork implementation for Entity Framework Core, 
incorporating IDisposable to enhance the scalability and efficiency of your CRUD operations. 
Jay.EFCore.UnitOfWork works with EF Core for SQL Server, 
Azure SQL Database, SQLite, Azure Cosmos DB, MySQL, PostgreSQL and MongoDB.

How to use:

In program.cs add the below code snipet.

//Nuget installation
Install Jay.EFCore.UnitOfWork

//EFCore configurations
```
var conString = builder.Configuration["ConnectionStrings:ProductDbConnection"];
builder.Services.AddDbContextPool<ProductDbContext>(options => options.UseSqlServer(conString));
builder.Services.AddTransient<IJayDbContext, ProductDbContext>();
```

//Jay.EFCore.UnitOfWork configuaration
```
builder.Services.AddEFCoreUnitOfWork();
```

//Make sure your DbContext class implemetenst IJayDbContext
```
internal class ProductDbContext : DbContext, IJayDbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; init; }
    public DbSet<ProductBrand> ProductBrands { get; init; }
    public DbSet<ProductType> ProductTypes { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }

}
```

Do the following to use Jay.EFCore.UnitOfWork
```
 public class CreateProductHandler : IRequestHandler<CreateProductCommand, Product>
 {
     private readonly IUnitOfWorkCore _unitOfWork;

     public CreateProductHandler(IUnitOfWorkCore unitOfWork)
     {
         _unitOfWork = unitOfWork;
     }

     public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
     {
         //Create new Product
         retur await _unitOfWork.Repository<Product>().AddAsync(request.Product, cancellationToken);
     }
 }
 ```
//=======================================================