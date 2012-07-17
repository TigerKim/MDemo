<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    홈 페이지
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: ViewData["Message"] %></h2>
    <p>
        ASP.NET MVC에 대한 자세한 내용을 보려면 <a href="http://asp.net/mvc" title="ASP.NET MVC 웹 사이트">http://asp.net/mvc</a>를 방문하십시오.
    </p>
</asp:Content>
