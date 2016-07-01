using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserInfoModel
/// </summary>
public class UserInfoModel
{
    public UserInformation GetUserInformation(string guId)
    {
        JJASDBEntities db = new JJASDBEntities();
        UserInformation info = (from x in db.UserInformations
                                where x.GUID == guId
                                select x).FirstOrDefault();
        return info;
    }

    public void InsertUserInformation(UserInformation info)
    {
        JJASDBEntities db = new JJASDBEntities();
        db.UserInformations.Add(info);
        db.SaveChanges();
    }
}