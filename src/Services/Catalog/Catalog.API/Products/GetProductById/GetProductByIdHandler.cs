
namespace Catalog.API.Products.GetProductById
{

    public record GetProductsByIdQuery(Guid Id) : IQuery<GetProductsByIdResult>;

    public record GetProductsByIdResult(Product Products);


    internal class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger) 
        : IQueryHandler<GetProductsByIdQuery, GetProductsByIdResult>
    {
        public async Task<GetProductsByIdResult> Handle(GetProductsByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByIdQueryHandler.Handle called with {@Query}", query.Id, cancellationToken);

            var product = await session.Query<Product>().FirstOrDefaultAsync(p => p.Id == query.Id, cancellationToken);

            if (product is null)
            {
                throw new ProductNotFoundException();
            }


            return new GetProductsByIdResult(product);

        }
    }
}
