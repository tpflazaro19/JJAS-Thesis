using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Product : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        FillPage();
    }

    private void FillPage()
    {
        //Get selected product's data
        if (!String.IsNullOrWhiteSpace(Request.QueryString["id"]))
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            ProductModel productModel = new ProductModel();
            Product product = productModel.GetProduct(id);
                        
            //Fill page with data
            lblPrice.Text = "Price per unit: <br/>₱ " + product.Price;
            lblTitle.Text = product.Name;
            lblDescription.Text = product.Description;
            lblItemNr.Text = id.ToString();
            imgProduct.ImageUrl = "~/Images/Products/" + product.Image;

            GetStocks(id);

            //Fill Amount dropdownlist with numbers 1-20
            //int[] amount = Enumerable.Range(1, 20).ToArray();
            //ddlAmount.DataSource = amount;
            //ddlAmount.AppendDataBoundItems = true;
            //ddlAmount.DataBind();                        
        }
    }

    private void GetStocks(int productId)
    {
        ProductStocksModel productStockModel = new ProductStocksModel();

        //Get list of branches that has the product ID
        List<ProductStock> stockList = productStockModel.GetProductStocksByProduct(productId);
        CreateStockTable(stockList);
    }

    private void CreateStockTable(List<ProductStock> stockList)
    {
        ProductStocksModel productStockModel = new ProductStocksModel();
        StoreModel storeModel = new StoreModel();

        foreach (ProductStock productStock in stockList)
        {
            Store store = storeModel.GetBranch(productStock.StoreID);

            //Create the 'Quantity' dropdownlist
            //Generate list with numbers from 0 to max stock
            int stock = Convert.ToInt32(productStock.Stock);
            int maxStock = Convert.ToInt32(productStock.MaxStock);
            string availablity = "Not Specified";
            int[] amount = Enumerable.Range(0, stock).ToArray();
            DropDownList ddlAmount = new DropDownList
            {
                DataSource = amount,
                AppendDataBoundItems = true,
                AutoPostBack = true,
                ID = productStock.ID.ToString()
            };

            ddlAmount.DataBind();
            //ddlAmount.SelectedIndexChanged += ddlAmount_SelectedIndexChanged;

            //Determine product availability
            if (stock == 0)
            {
                availablity = "Unavailable";
            }
            else
            {
                if (stock < 0.25 * maxStock)
                {
                    availablity = "Low availability";
                }
                else
                {
                    availablity = "Available";
                }
            }

            //Create the Add to cart link
            LinkButton lnkAdd = new LinkButton
            {
                //PostBackUrl = string.Format("~/Pages/Product.aspx?id={0}", productStock.ProductID),
                Text = "Add to cart",
                ID = "add" + productStock.ID,
                CssClass = "button"
            };

            //Add an OnClick Event
            //lnkAdd.Click += (sender, e) => Add_Item(sender, e, amount);

            //Create checkboxes
            CheckBox cbSelectStore = new CheckBox
            {
                ID = "select" + productStock.ID,
                AutoPostBack = true
            };
            
            cbSelectStore.CheckedChanged += cbSelectStore_CheckedChanged;

            //Create HTML table with 1 row
            Table table = new Table { CssClass = "cartTable" };
            TableRow a = new TableRow();

            //Create 4 cells for row a
            TableCell a1 = new TableCell { Text = "Branch: " + store.Address };
            TableCell a2 = new TableCell { Text = "Location: " + store.Location };
            TableCell a3 = new TableCell { Text =  availablity };
            TableCell a4 = new TableCell { };
            TableCell a5 = new TableCell { };

            //Set 'special' controls
            a4.Controls.Add(ddlAmount);
            //a5.Controls.Add(lnkAdd);
            a5.Controls.Add(cbSelectStore);

            //Add cells to rows
            a.Cells.Add(a1);
            a.Cells.Add(a2);
            a.Cells.Add(a3);
            a.Cells.Add(a4);
            a.Cells.Add(a5);

            //Add rows to table
            table.Rows.Add(a);

            //Add table to pnlStoreList
            pnlStoreList.Controls.Add(table);
        }
    }

    private void cbSelectStore_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox selectedRow = (CheckBox)sender;
        string row = selectedRow.ID.Replace("select", "");
        int stockId = Convert.ToInt32(row);

        ProductStocksModel productStockModel = new ProductStocksModel();
        ProductStock productStock = productStockModel.GetProductStock(stockId);

        //Fill Amount dropdownlist with numbers 1-max stock
        int stock = Convert.ToInt32(productStock.Stock);
        int maxStock = Convert.ToInt32(productStock.MaxStock);
        int[] amount = Enumerable.Range(0, stock).ToArray();
        ddlAmount.DataSource = amount;
        ddlAmount.AppendDataBoundItems = true;
        ddlAmount.DataBind(); 
    }

    private void ddlAmount_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList selectedList = (DropDownList)sender;
        int amount = Convert.ToInt32(selectedList.SelectedValue);
    }

    private void Add_Item(object sender, EventArgs e, int amount)
    {
        LinkButton selectedLink = (LinkButton)sender;
        string link = selectedLink.ID.Replace("add", "");
        int ddlID = Convert.ToInt32(link);
        
        if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
        {
            string clientId = Context.User.Identity.GetUserId();

            if (clientId != null)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                //int amount = Convert.ToInt32(ddlAmount.SelectedValue);

                Cart cart = new Cart
                {
                    Amount = amount,
                    ClientID = clientId,
                    DatePurchased = DateTime.Now,
                    IsInCart = true,
                    ProductID = id
                };

                CartModel model = new CartModel();
                lblResult.Text = model.InsertCart(cart);
            }
            else
            {
                lblResult.Text = "Please login to order items";
            }
        }
        //LinkButton selectedLink = (LinkButton)sender;
        //string link = selectedLink.ID.Replace("add", "");
        //int cartId = Convert.ToInt32(link);

        //CartModel model = new CartModel();
        //model.DeleteCart(cartId);

        //Response.Redirect("~/Pages/ShoppingCart.aspx");
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if(!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
        {
            string clientId = Context.User.Identity.GetUserId();

            if (clientId != null)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                int amount = Convert.ToInt32(ddlAmount.SelectedValue);

                Cart cart = new Cart
                {
                    Amount = amount,
                    ClientID = clientId,
                    DatePurchased = DateTime.Now,
                    IsInCart = true,
                    ProductID = id
                };

                CartModel model = new CartModel();
                lblResult.Text = model.InsertCart(cart);
            }
            else
            {
                lblResult.Text = "Please login to order items";
            }
        }
    }
}