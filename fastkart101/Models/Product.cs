namespace fastkart101.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ShopProduct> ShopProducts { get; set; }

    }
}
