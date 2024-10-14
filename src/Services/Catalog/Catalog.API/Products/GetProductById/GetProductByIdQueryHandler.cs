
namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product Product);

    public class GetProductByIdQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await session.LoadAsync<Product>(query.Id, cancellationToken);

            if (result is null)
            {
                throw new ProductNotFoundException(query.Id);
            }

            return new GetProductByIdResult(result);
        }
    }
}
