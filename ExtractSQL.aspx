<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" CodeFile="ExtractSQL.aspx.cs" Inherits="ExtractSQL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        body {
            font-family: Arial, Helvetica, sans-serif;
            font-size: smaller;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />

    <div style="text-align:left;">
    
        <table>
            <tr>
                <td>Server:</td>
                <td>
                    <asp:DropDownList ID="ddserver" runat="server">
                        <asp:ListItem Value="141.197.198.201">AMEREAST</asp:ListItem>
                        <asp:ListItem Value="141.197.198.200">AMERWEST</asp:ListItem>
                        <asp:ListItem Value="10.51.16.23" Selected="True">ASIAPAC</asp:ListItem>
                        <asp:ListItem Value="141.197.198.200">EMEA</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Port:</td>
                <td>
                    <asp:TextBox ID="txtPort" runat="server" Text="1521"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Username:</td>
                <td>
                    <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Password:</td>
                <td>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnConnect" runat="server" Text="Connect" OnClick="btnConnect_Click" />
                </td>
            </tr>
            <tr>
                <td>Schema:</td>
                <td>
                     <asp:DropDownList ID="ddSchema" runat="server" Enabled="false"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Path:</td>
                <td>
                    <asp:TextBox ID="txtPath" runat="server">\\mlptsmt001\tmpExtract\</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="btnExtract" runat="server" Text="Extract Triggers and Views" OnClick="btnExtract_Click" Enabled="False" />
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="Extract DA_Type" Enabled="False" OnClick="Button1_Click" />
                    <asp:Button ID="Button2" runat="server" Text="Extract DA_Init" Enabled="False" OnClick="Button2_Click" />
                    <asp:Button ID="Button3" runat="server" Text="Extract SQL_Script" Enabled="False" OnClick="Button3_Click" />
                    <asp:Button ID="Button4" runat="server" Text="Extract REP_Report" Enabled="False" OnClick="Button4_Click" />
                    <asp:Button ID="Button5" runat="server" Text="Extract LS_ListSetup" Enabled="False" OnClick="Button5_Click" />
                </td>
            </tr>
        </table>  
        <asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label> 
            
    </div>
</asp:Content>

