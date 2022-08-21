using RMDesktop.UI.Library.Models;
using System.Threading.Tasks;

namespace RMDesktop.UI.Library.Api
{
    public interface ISaleEndPoint
    {
        Task PostSale(SaleModel sale);
    }
}