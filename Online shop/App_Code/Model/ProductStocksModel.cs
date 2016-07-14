using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProductStocks
/// </summary>
public class ProductStocksModel
{
    public List<ProductStock> GetProductStocksByProduct(int productId)
    {
        try
        {
            using (JJASDBEntities db = new JJASDBEntities())
            {
                List<ProductStock> productStocks = (from x in db.ProductStocks
                                          where x.ProductID == productId
                                          select x).ToList();
                return productStocks;
            }
        }
        catch (Exception)
        {
            return null;
        }
    }

    public ProductStock GetProductStock(int id)
    {
        try
        {
            using (JJASDBEntities db = new JJASDBEntities())
            {
                ProductStock stock = db.ProductStocks.Find(id);
                return stock;
            }
        }
        catch (Exception)
        {
            return null;
        }
    }
}