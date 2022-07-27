namespace Product_Management_API.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public bool isDisabled { get; set; }
        public bool isDeleted { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
