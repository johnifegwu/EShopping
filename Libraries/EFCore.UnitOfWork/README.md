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
services.AddEFCoreUnitOfWork();
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