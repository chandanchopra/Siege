<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Admin.Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Admin Portal - Home
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        This is the home view for the Admin Home Controller. <br /><br />
        
        When you use Siege to select your controllers based on context, you have the option of providing a specific version of your view for that controller by following the conventions for views for controllers based on name. In this example, the AdminHomeController uses the AdminHome/Index view. 
        <br /><br />
        Alternatively, by not specifying an override view, the Siege framework will use the default. By removing this view, the AdminHomeController will use the Home/Index view.
    </p>
</asp:Content>
