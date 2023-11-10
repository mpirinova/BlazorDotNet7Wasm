using System.ComponentModel.DataAnnotations.Schema;

namespace ShopOnline.Data.Entities
{
    public class Cart
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
