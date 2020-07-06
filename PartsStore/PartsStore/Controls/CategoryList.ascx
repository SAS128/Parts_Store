<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CategoryList.ascx.cs" Inherits="PartsStore.Controls.CategoryList" %>

<%= CreateHomeLinkHtml() %>

<% 
    foreach (string item in GetCategories())
    {
        Response.Write(CreateLinkHtml(item));
    }
%>