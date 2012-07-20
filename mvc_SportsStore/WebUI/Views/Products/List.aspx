<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DomainModel.Entities.Product>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Product List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% foreach (var product in Model)
	{
		 %>
         <h3><%= product.Name %></h3>
         <%= product.Description%>
         <h4><%= product.Price.ToString("c")%></h4>
<%	} %>
<h2></h2>

</asp:Content>
