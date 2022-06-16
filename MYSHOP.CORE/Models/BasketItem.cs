namespace MYSHOP.CORE.Models
{
    public class BasketItem : BaseEntity
    {
        public string BasketId { get; set; }
        public string ProductId { get; set; }
        public int Quantiy { get; set; }

    }
}
