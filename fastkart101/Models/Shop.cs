using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace fastkart101.Models
{
    public class Shop 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<ShopProduct> ShopProducts { get; set; }
        
    }
}
