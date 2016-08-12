using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_ShoppingCart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Get ID of current logged in user and display items in Cart
        string userId = User.Identity.GetUserId();
        GetPurchasesInCart(userId);
    }

    private void GetPurchasesInCart(string userId)
    {
        CartModel model = new CartModel();
        double subTotal = 0;

        //Generate HTML for each element in purchaseList
        List<Cart> purchaseList = model.GetOrdersInCart(userId);
        CreateShopTable(purchaseList, out subTotal);

        //Add totals to webpage
        double vat = subTotal * 0.12;
        double totalAmount = subTotal + vat;

        //Display values on the page
        litTotal.Text = "₱ " + subTotal;
        litVat.Text = "₱ " + vat;
        litTotalAmount.Text = "₱ " + totalAmount;
     }

    private void CreateShopTable(List<Cart> purchaseList, out double subTotal)
    {
        subTotal = new Double();
        ProductModel model = new ProductModel();

        foreach (Cart cart in purchaseList)
        {
            Product product = model.GetProduct(cart.ProductID);

            //Create the image button
            ImageButton btnImage = new ImageButton
            {
                ImageUrl = string.Format("~/Images/Products/{0}", product.Image),
                PostBackUrl = string.Format("~/Images/Product.aspx?id={0}", product.ID)
            };

            //Create the delete link
            LinkButton lnkDelete = new LinkButton
            {
                PostBackUrl = string.Format("~/Pages/ShoppingCart.aspx?productId={0}", cart.ID),
                Text = "Delete Item",
                ID = "del" + cart.ID
            };

            //Add an OnClick Event
            lnkDelete.Click += Delete_Item;

            //Create the 'Quantity' dropdownlist
            //Generate list with numbers from 1 to 20
            int[] amount = Enumerable.Range(1, 20).ToArray();
            DropDownList ddlAmount = new DropDownList
            {
                DataSource = amount,
                AppendDataBoundItems = true,
                AutoPostBack = true,
                ID = cart.ID.ToString()
            };

            ddlAmount.DataBind();
            ddlAmount.SelectedValue = cart.Amount.ToString();
            ddlAmount.SelectedIndexChanged += ddlAmount_SelectedIndexChanged;

            //Create delivery option ddl
            string[] option = new string[3] { "-- Choose Option --", "Store pick - up", "Ship to Home" };
            DropDownList ddlDelivery = new DropDownList
            {
                DataSource = option,
                AppendDataBoundItems = true,
                Width = 120,
                ID = "list" + cart.ID.ToString()
            };

            ddlDelivery.DataBind();
            ddlDelivery.SelectedIndexChanged += ddlDelivery_SelectedIndexChanged;


            //Create HTML table with 2 rows
            Table table = new Table { CssClass = "cartTable" };
            TableRow a = new TableRow();
            TableRow b = new TableRow();

            //Create 6 cells for row a
            TableCell a1 = new TableCell { RowSpan = 2, Width = 50 };
            TableCell a2 = new TableCell { Text = string.Format("<h4>{0}</h4><br/>{1}<br/>In Stock",
                product.Name, "Item No: " + product.ID),
                HorizontalAlign = HorizontalAlign.Left, Width=150};
            TableCell a3 = new TableCell { };
            TableCell a4 = new TableCell { Text = "Unit Price<hr/>" };
            TableCell a5 = new TableCell { Text = "Quantity<hr/>" };
            TableCell a6 = new TableCell { Text = "Item Total<hr/>" };
            TableCell a7 = new TableCell { };

            //Create 6 cells for row b
            TableCell b1 = new TableCell { };
            TableCell b2 = new TableCell { };
            TableCell b3 = new TableCell { Text = "₱ " + product.Price };
            TableCell b4 = new TableCell { Text = "₱ " + Math.Round((cart.Amount * (double)product.Price), 2) };
            TableCell b5 = new TableCell { };
            TableCell b6 = new TableCell { };
            TableCell b7 = new TableCell { };

            //Set 'special' controls
            a1.Controls.Add(btnImage);
            a3.Controls.Add(ddlDelivery);
            a7.Controls.Add(lnkDelete);
            b5.Controls.Add(ddlAmount);

            //Add cells to rows
            a.Cells.Add(a1);
            a.Cells.Add(a2);
            a.Cells.Add(a3);
            a.Cells.Add(a4);
            a.Cells.Add(a5);
            a.Cells.Add(a6);
            a.Cells.Add(a7);

            b.Cells.Add(b1);
            b.Cells.Add(b2);
            b.Cells.Add(b3);
            b.Cells.Add(b4);
            b.Cells.Add(b5);
            b.Cells.Add(b6);
            b.Cells.Add(b7);

            //Add rows to table
            table.Rows.Add(a);
            table.Rows.Add(b);

            //Add table to pnlShoppingCart
            pnlShoppingCart.Controls.Add(table);

            //Add total amount of item in cart to subtotal
            subTotal += (cart.Amount * (double)product.Price);
        }

        //Add current user's shopping cart to user specific session value
        Session[User.Identity.GetUserId()] = purchaseList;
    }

    private void ddlDelivery_SelectedIndexChanged(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void ddlAmount_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList selectedList = (DropDownList)sender;
        int quantity = Convert.ToInt32(selectedList.SelectedValue);
        int cartId = Convert.ToInt32(selectedList.ID);

        CartModel model = new CartModel();
        model.UpdateQuantity(cartId, quantity);

        Response.Redirect("~/Pages/ShoppingCart.aspx");
    }

    private void Delete_Item(object sender, EventArgs e)
    {
        LinkButton selectedLink = (LinkButton)sender;
        string link = selectedLink.ID.Replace("del", "");
        int cartId = Convert.ToInt32(link);

        CartModel model = new CartModel();
        model.DeleteCart(cartId);

        Response.Redirect("~/Pages/ShoppingCart.aspx");
    }
}