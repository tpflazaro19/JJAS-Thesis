<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="Pages_Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
        <tr>
            <td rowspan="4">
                <asp:Image ID="imgProduct" runat="server" CssClass="detailsImage" Height="300px" Width="300px"/></td>
            <td><h2/>
                <asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label>
                <hr/>
                <asp:Label ID="lblPrice" runat="server" CssClass="detailsPrice"></asp:Label></td>
        </tr>
        <tr>
            <td>
    <asp:Panel ID="pnlStoreList" runat="server">
    </asp:Panel>
            </td>
            <td>
                <br/>
                <br/>
                <br/>
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td>Product No:&nbsp;&nbsp;
                <asp:Label ID="lblItemNr" runat="server" Text="Label"></asp:Label>
                <br />
                </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblDescription" runat="server" CssClass="detailsDescription"></asp:Label>
                <br />
                Quantity:
                <asp:DropDownList ID="ddlAmount" runat="server">
                </asp:DropDownList>
                <br />
                <asp:Button ID="btnAdd" runat="server" CssClass="button" OnClick="btnAdd_Click" Text="Add Product" />
                <br />
                <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                </td>
        </tr>
    </table>
    <br />
    </asp:Content>

