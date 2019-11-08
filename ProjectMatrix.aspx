<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" CodeFile="ProjectMatrix.aspx.cs" Inherits="ProjectMatrix" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />

    <div style="text-align:left;">
        <table>
            <tr>
                <td>Select a Year</td>
                <td>
                    <asp:DropDownList ID="ddYear" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left"><asp:Button ID="btnGenerate" runat="server" Text="Generate Report" OnClick="btnGenerate_Click" /></td>
            </tr>
        </table>
    </div>
</asp:Content>

