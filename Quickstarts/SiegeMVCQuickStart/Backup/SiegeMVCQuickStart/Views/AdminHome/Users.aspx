<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Admin.Site.Master" Inherits="System.Web.Mvc.ViewPage<List<User>>" %>
<%@ Import Namespace="SiegeMVCQuickStart.SampleClasses"%>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Admin Portal - View Users
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>System Users</h2>
    <table border="0" cellpadding="0" cellspacing="0">
    <tr>
    <td>User Name</td>
    <td>User Role</td>
    </tr>
    <% foreach (User user in Model)
       {%>
       <tr>
    <td><%= user.UserName %></td>
    <td><%= user.Role %></td>
    </tr>
    <%} %>
    </table>    
    <br />
    <br />
    If you are logged in as Webmaster, you will see a list of users including admins and webmasters. If you are logged in as the admin user, you will see standard users and admins, but no webmasters.
    <br /><br />
    This is a demonstration as not only how a controller is contextually resolved, but its dependencies as well. Siege has the ability to resolve contextually on a (theoretically) infinite level of dependencies, each dependency compared against context before an implementation is selected.
</asp:Content>
