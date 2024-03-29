﻿using RMDesktop.UI.Library.Models;
using RMDesktopUI.Library.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktop.UI.Library.Api
{
    public class SaleEndPoint : ISaleEndPoint
    {
        private IAPIHelper _apiHelper;
        public SaleEndPoint(IAPIHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task PostSale(SaleModel sale)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Sale", sale))
            {
                if (response.IsSuccessStatusCode)
                {
                    //Log successful call?
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
        /*  public async Task<List<ProductModel>> GetAll()
          {
              using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/Product"))
              {
                  if (response.IsSuccessStatusCode)
                  {
                      var result = await response.Content.ReadAsAsync<List<ProductModel>>();
                      return result;
                  }
                  else
                  {
                      throw new Exception(response.ReasonPhrase);
                  }

              }
          }*/
    }
}
