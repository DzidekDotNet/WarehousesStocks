namespace WarehousesStocks.Products
{
    public class ProductWarehouse
    {
        public string Name { get; }
        public int Amount { get; }

        internal ProductWarehouse(string name, int amount)
        {
            Name = name;
            Amount = amount;
        }
    }
}