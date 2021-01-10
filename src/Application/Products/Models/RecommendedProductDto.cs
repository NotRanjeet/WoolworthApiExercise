namespace Woolworth.Application.Products.Models
{
    public class RecommendedProductDto: ProductDto
    {
        public decimal Rank { get; private set;}
        public RecommendedProductDto(ProductDto product)
        {
            Price = product.Price;
            Quantity = product.Quantity;
            Name = product.Name;
            Rank = 0;
        }

        public RecommendedProductDto(ProductDto product, decimal rank) : this(product)
        {
            Rank = rank;
        }


        public void IncrementRank()
        {
            Rank += 1;
        }

        public void SetRank(uint rank)
        {
            Rank = rank;
        }
    }
}
