using RMDesktop.UI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMDesktop.UI.Library.Api
{
    public interface IProductEndPoint
    {
        Task<List<ProductModel>> GetAll();
    }
}