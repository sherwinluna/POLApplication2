<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" CodeFile="NewProjectLocation.aspx.cs" Inherits="NewProjectLocation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <br />

    <div style="text-align:left;">
        <table>
        <tr>
            <td>
                <asp:Label ID="lblProjectLocation" runat="server" Text="Enter Project Location Name"></asp:Label>
                <asp:TextBox ID="txtProjectLocation" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    </div>
    
</asp:Content>

