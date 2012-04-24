<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Logged In User View</h2>
    
    This view is for non-admin, non-webmaster users. This uses the UserHomeController, which is contextually injected with an instance of IAccountService. You should see different messages based on if you logged in with TrialUser or PaidUser. <br /><br />
    
    <b><%= ViewData["Message"] %></b>

</asp:Content>
