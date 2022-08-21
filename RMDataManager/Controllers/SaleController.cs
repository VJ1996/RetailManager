using Microsoft.AspNet.Identity;
using RMDataManager.Library.Models;
using RMDataManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TRMDataManager.Library.DataAccess;

namespace RMDataManager.Controllers
{
    [Authorize]
    public class SaleController : ApiController
    {
        public void Post(SaleModel sale)
        {
            SaleData saleData = new SaleData();
            string userId = RequestContext.Principal.Identity.GetUserId();

            saleData.SaveSale(sale, userId);
        }

       /* public List<ProductModel> Get()
        {
            ProductData data = new ProductData();

            return data.GetProducts();
        }*/
    }
}
