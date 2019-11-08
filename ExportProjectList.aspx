<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" CodeFile="ExportProjectList.aspx.cs" Inherits="ExportProjectList" %>

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

    <table>
        <tr>
            <td align="center">
                <asp:Button ID="btnExportCSV" runat="server" Text="Export Project List" OnClick="btnExportCSV_Click" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnExportCSV2" runat="server" Text="Migrate to CWX 6.7.1" OnClick="btnExportCSV2_Click" />
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnExportCSV3" runat="server" Text="Plan to Close in POL 6.2.2" OnClick="btnExportCSV3_Click" />
            </td>
        </tr>
    </table>
        
</asp:Content>

