using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProductByCategory
{

    public record GetProuctByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

    public record GetProductByCategoryResult(IEnumerable<Product> Products);


    internal class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> logger) : IQueryHandler<GetProuctByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProuctByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCategoryQueryHandler.Handle called with {@Query}", query);


            var products = await session.Query<Product>()
                .Where(p=> p.Category.Contains(query.Category))
                .ToListAsync();

            if (products is null)
            {
                throw new  ProductNotFoundException();
            }


            return new GetProductByCategoryResult(products);

        }
    }
}
