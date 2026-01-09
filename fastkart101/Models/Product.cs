namespace fastkart101.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public double? DiscounedPrice { get; set; }
        public int Stock { get; set; }   
        public string ImagePath { get; set; }
        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
