using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StoreModel
/// </summary>
public class StoreModel
{
    public Store GetBranch(int storeId)
    {
        try
        {
            using (JJASDBEntities db = new JJASDBEntities())
            {
                Store store = db.Stores.Find(storeId);
                return store;
            }
        }
        catch (Exception)
        {
            return null;
        }
    }
}