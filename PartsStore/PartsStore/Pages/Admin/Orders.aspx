﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="PartsStore.Pages.Admin.Orders" MasterPageFile="~/Pages/Admin/Admin.Master"%>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="outerContainer">
        <table id="ordersTable">
            <thead>
                <tr>
                    <th>Имя заказа</th>
                    <th>Город</th>
                    <th>Заказ</th>
                    <th>Стоимость</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="Repeater1" runat="server" SelectMethod="GetOrders" ItemType="PartsStore.Models.Order" >
                    <ItemTemplate>
                        <tr>
                            <td><%#: Item.Name %></td>
                            <td><%#: Item.City %></td>
                            <td><%#  Item.OrderLines.Sum(ol => ol.Quantity) %></td>
                            <td><%#  Total(Item.OrderLines).ToString("C") %></td>
                            <td>
                                <asp:PlaceHolder ID ="PlaceHolder1" Visible="<%# !Item.Dispatched %>" runat="server">
                                    <button type="submit" name="dispatch" value="<%# Item.OrderId %>">
                                        Обработать
                                    </button>
                                </asp:PlaceHolder>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>

    <div id="ordersCheck">
        <asp:CheckBox ID="showDispatched" runat="server" Checked="true" AutoPostBack="true" />
        Показать обработанные
    </div>
</asp:Content>