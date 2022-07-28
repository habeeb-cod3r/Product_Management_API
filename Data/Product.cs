namespace Product_Management_API.Data
{
    public record Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public bool IsDisabled { get; set; }
        public bool IsDeleted { get; set; }
        public DateTimeOffset CreatedDate {get; set;}
    }
}
