namespace WarehousesStocks.Warehouses
{
    public class WarehouseProduct
    {
        public string ProductId { get; }
        public int Amount { get; }

        internal WarehouseProduct(string productId, int amount)
        {
            ProductId = productId;
            Amount = amount;
        }
    }
}