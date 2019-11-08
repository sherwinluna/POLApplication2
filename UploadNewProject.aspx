<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" CodeFile="UploadNewProject.aspx.cs" Inherits="UploadNewProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />

    <div style="text-align:left;">
        <table>
            <tr>
                <td>New Project Request</td>
                <td>
                    <asp:FileUpload ID="fuUpload" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left"><asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" /></td>
            </tr>
        </table>

        <asp:HiddenField ID="hfUserId" runat="server" />
    </div>
</asp:Content>

