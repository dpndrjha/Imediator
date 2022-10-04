using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Imediator.Models
{
    public class getProductQuery : IRequest<IEnumerable<Product>>
    {
        public class getProductQueryHandler : IRequestHandler<getProductQuery, IEnumerable<Product>>
        {
            private readonly _DBcontext dBcontext;
            public getProductQueryHandler(_DBcontext dBcontext) => this.dBcontext = dBcontext;

            public async Task<IEnumerable<Product>> Handle(getProductQuery request, CancellationToken cancellationToken)
            {
                var getProduct = await dBcontext.Products.ToListAsync();
                if (getProduct == null) return null;
                return getProduct.AsReadOnly();
            }
        }
    }

    public class AddProductQuery : IRequest<int>
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal Rate { get; set; }
        public class AddProductQueryHandler : IRequestHandler<AddProductQuery, int>
        {
            private readonly _DBcontext dBcontext;
            public AddProductQueryHandler(_DBcontext dBcontext) => this.dBcontext = dBcontext;

            public async Task<int> Handle(AddProductQuery request, CancellationToken cancellationToken)
            {
                var product = new Product()
                {
                    Name = request.Name,
                    Barcode = request.Barcode,
                    Description = request.Description,
                    BuyingPrice = request.BuyingPrice,
                    Rate = request.Rate
                };
                dBcontext.Add(product);
                dBcontext.SaveChanges();
                return product.ID;
            }
        }
        public class getProductDetailById : IRequest<Product>
        {
            public int Id { get; set; }
            public class getProductDetailByIdHandler : IRequestHandler<getProductDetailById, Product>
            {
                private readonly _DBcontext dbContext;
                public getProductDetailByIdHandler(_DBcontext dbContext) => this.dbContext = dbContext;

                public async Task<Product> Handle(getProductDetailById request, CancellationToken cancellationToken)
                {
                    var product = await dbContext.Products.Where(a => a.ID == request.Id).FirstOrDefaultAsync();
                    if (product == null) return null;
                    return product;
                }
            }
        }

        public class deleteProductById : IRequest<int>
        {
            public int Id { get; set; }
            public class deleteProductByIdHandler : IRequestHandler<deleteProductById, int>
            {
                private readonly _DBcontext _dBcontext;
                public deleteProductByIdHandler(_DBcontext dBcontext) => this._dBcontext = dBcontext;
                public async Task<int> Handle(deleteProductById request, CancellationToken cancellationToken)
                {
                    var ms = await _dBcontext.Products.Where(x => x.ID == request.Id).FirstOrDefaultAsync();
                    _dBcontext.SaveChanges();
                    return request.Id;
                }
            }
        }
    }

}
