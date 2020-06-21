using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WarehousesStocks
{
    internal interface IWarehousesStocksService
    {
        Task<StringBuilder> Run(Stream input);
    }
}