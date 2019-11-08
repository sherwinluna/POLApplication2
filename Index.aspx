<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <style>
        body{
            font-family: Arial, Helvetica, sans-serif;
            font-size: smaller;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td colspan="2"><font size="-2"><i><b>Note:</b> Login using your FDNet username and password.</i></font></td>
            </tr>
            <tr>
                <td>Username</td>
                <td><asp:TextBox ID="txtUsername" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Password</td>
                <td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2" align="right"><asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" /></td>
            </tr>
            <tr>
                <td colspan="2"><asp:Label ID="lblLoginRemark" runat="server" ForeColor="Red"></asp:Label></td>
            </tr>
        </table>        
    </div>
    </form>
</body>
</html>
