<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="Pages_Product" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

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
                </td>
        </tr>
        <tr>
            <td>
    <asp:Panel ID="pnlStoreList" runat="server" CssClass="modalPopup" style="display:none" Width="600px">
    </asp:Panel>
                Item no. :
                <asp:Label ID="lblItemNr" runat="server" Text="Label"></asp:Label>
                <br />
                <asp:Label ID="lblPrice" runat="server" CssClass="detailsPrice"></asp:Label>
                <br />
                <br />
                <asp:Label ID="lblDescription" runat="server" CssClass="detailsDescription"></asp:Label>
                <br />
                <br />
            </td>
            <td>
                Quantity:&nbsp;
                <asp:TextBox ID="txtAmount" runat="server" TextMode="Number" Width="30px"></asp:TextBox>
                <br />
                <br />
                <asp:Button ID="btnAdd" runat="server" CssClass="button" OnClick="btnAdd_Click" Text="ADD TO CART" />
                <br />
                <asp:Label ID="lblResult" runat="server" Text=""></asp:Label>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;<asp:Button ID="btnShowStore" runat="server" Text="Check In-Store Availability" OnClick="btnAdd_Click" CssClass="button" />
                <ajaxToolkit:ModalPopupExtender ID="storePopup" runat="server" DropShadow="false" 
                    PopupControlID="pnlStoreList" TargetControlID="btnShowStore" BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
                <br />
                </td>
        </tr>
    </table>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    </asp:Content>

