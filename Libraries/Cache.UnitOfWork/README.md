
//=============================================================================
Cache.UnitOfWork.AspNetCore

How to use:

In program.cs add the below code snipet.

//Cache.UnitOfWork.AspNetCore configuaration
builder.Services.AddCacheUnitOfWork();

//REDIS configurations
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = configuration["configs:redisurl"];
    options.InstanceName = configuration["configs:environment"] + "01";
});

Do the following to use CacheUnitOfWork

 public class GetShoppingCartHandler : IRequestHandler<GetShoppingCartQuery, ShoppingCart>
 {
     private readonly ICacheUnitOfWork _cacheUnitOfWork;

     public GetShoppingCartHandler(ICacheUnitOfWork cacheUnitOfWork)
     {
         this._cacheUnitOfWork = cacheUnitOfWork;
     }
	 
     public async Task<ShoppingCart> Handle(AddUpdateDeleteShoppingCartItemCommand request, CancellationToken cancellationToken)
     {
         var cart = await _cacheUnitOfWork.Repository<ShoppingCart>().GetAsync(request.UserName, cancellationToken);

         return cart;
     }
 }
//=============================================================================

