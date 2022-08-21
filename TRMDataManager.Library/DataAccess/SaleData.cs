using RMDataManager.Library.Models;
using RMDesktop.UI.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRMDataManager.Library.Internal.DataAccess;
using TRMDataManager.Library.Models;

namespace TRMDataManager.Library.DataAccess
{
    public class SaleData
    {
        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            var taxRate = ConfigHelper.GetTaxRate()/100;

            ProductData products = new ProductData();

            //Start filling in the sale detail models we will save to the database
            // Fill in the available info
            foreach ( var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                var productInfo = products.GetProductById(detail.ProductId);
                if(productInfo == null)
                {
                    throw new Exception($"The product Id of {detail.ProductId} could not be found in the database.");
                }

                detail.PurchasePrice = (productInfo.RetailPrice * detail.Quantity);

                if(productInfo.IsTaxable)
                {
                    detail.Tax = (detail.PurchasePrice * taxRate);
                }

                details.Add(detail);
            }

            SaleDBModel sale = new SaleDBModel
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CashierId = cashierId
            };

            sale.Total = sale.SubTotal + sale.Tax;


            //TODO - Make this SOLID/DRY/Better
            // 
            // create Sale Model
            // Save sale model

            SqlDataAccess sql = new SqlDataAccess();

            sql.SaveData<SaleDBModel>("dbo.spSale_Insert", sale, "RMData");

            // Get ID from sale model
            sale.Id =  sql.LoadData<int, dynamic>("spSale_Lookup", new {sale.CashierId, sale.SaleDate}, "RMData").FirstOrDefault();

            // Finish filling in the sale detail models
            foreach(var item in details)
            {
                item.SaleId = sale.Id;

                sql.SaveData("dbo.spSaleDetail_Insert", item, "RMData");
            }
            // save the sale detail models
        }

        /*public List<ProductModel> GetProducts()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<ProductModel, dynamic>("dbo.spProduct_GetAll", new { }, "RMData");

            return output;
        }*/
    }
}
