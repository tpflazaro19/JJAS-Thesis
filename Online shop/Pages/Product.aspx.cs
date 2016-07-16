using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        Table table = new Table { CssClass = "cartTable" };

        //Create Table header
        TableRow h = new TableRow();

        //Create 4 cells for row a
        TableCell h1 = new TableCell { Text = "Branch<hr/>" };
        TableCell h2 = new TableCell { Text = "Location<hr/>" };
        TableCell h3 = new TableCell { Text = "Availability<hr/>" };
        TableCell h4 = new TableCell { Text = "Remarks<hr/>" };
        h1.Font.Bold = true;
        h2.Font.Bold = true;
        h3.Font.Bold = true;
        h4.Font.Bold = true;

        //Add cells to rows
        h.Cells.Add(h1);
        h.Cells.Add(h2);
        h.Cells.Add(h3);
        h.Cells.Add(h4);

        //Add rows to table
        table.Rows.Add(h);

        //Add table to pnlStoreList
        pnlStoreList.Controls.Add(table);
        
        foreach (ProductStock productStock in stockList)
        {
            Store store = storeModel.GetBranch(productStock.StoreID);

            //Create the 'Quantity' dropdownlist
            //Generate list with numbers from 0 to max stock
            int stock = Convert.ToInt32(productStock.Stock);
            int maxStock = Convert.ToInt32(productStock.MaxStock);

            //Create HTML table with 1 row
            
            TableRow a = new TableRow();

            //Create 4 cells for row a
            TableCell a1 = new TableCell { Text = store.Address };
            TableCell a2 = new TableCell { Text = store.Location };
            TableCell a3 = new TableCell { };
            TableCell a4 = new TableCell { };

            //Determine product availability
            if (stock == 0)
            {
                a3.Text = "Unavailable";
                a3.Font.Bold = true;
                a3.ForeColor = Color.Red;
                a4.Text = "Ready in 3 days";
                a4.ForeColor = Color.Red;
            }
            else
            {
                if (stock < 0.25 * maxStock)
                {
                    a3.Text = "low";
                    a3.Font.Bold = true;
                    a3.ForeColor = Color.Yellow;
                    
                }
                else
                {
                    a3.Text = "high";
                    a3.Font.Bold = true;
                    a3.ForeColor = Color.Green;
                }
                a4.Text = "Ready in an hour";
                a4.ForeColor = Color.Green;
            }
            
            //Set 'special' controls
            //a4.Controls.Add(ddlAmount);

            //Add cells to rows
            a.Cells.Add(a1);
            a.Cells.Add(a2);
            a.Cells.Add(a3);
            a.Cells.Add(a4);

            //Add rows to table
            table.Rows.Add(a);

            //Add table to pnlStoreList
            //pnlStoreList.Controls.Add(table);
        }

        Button btnCancel = new Button
        {
            Text = "Close",
            ID = "btnCancel",
            CssClass = "button",
            OnClientClick = "return Hidepopup()"
        };
        pnlStoreList.Controls.Add(btnCancel);
    }

    private void Add_Item(object sender, EventArgs e)
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
                int amount = Convert.ToInt32(txtAmount.Text);
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
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if(!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
        {
            string clientId = Context.User.Identity.GetUserId();

            if (clientId != null)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                int amount = Convert.ToInt32(txtAmount.Text);

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

    protected void btnShowStore_Click(object sender, EventArgs e)
    {
        storePopup.Show();
    }
}