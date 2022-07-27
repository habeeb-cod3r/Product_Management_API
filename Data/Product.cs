namespace Product_Management_API.Data
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public bool isDisabled { get; set; }
        public bool isDeleted { get; set; }
        public DateTimeOffset CreatedDate {get; set;}
    }
}
