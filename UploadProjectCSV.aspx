<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" CodeFile="UploadProjectCSV.aspx.cs" Inherits="UploadProjectCSV" %>

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
        <asp:Panel ID="Panel1" runat="server" BorderWidth="0" Width="500px">
            Upload Templates:
            <br />
            <asp:FileUpload ID="fuUpload" runat="server" />
            <br />
            <asp:RadioButton ID="rdPOL" runat="server" GroupName="Project" Text="POL 6.2" />
            <asp:RadioButton ID="rdCWX" runat="server" GroupName="Project" Text="CWX 6.7.1" />
            <asp:RadioButton ID="rdCLS" runat="server" GroupName="Project" Text="Close Projects Tables" />
            <asp:RadioButton ID="rdActivity" runat="server" GroupName="Project" Text="DMS Activity Tablespace" />
            <asp:RadioButton ID="rdIncidentsLog" runat="server" GroupName="Project" Text="Incidents Log" />
            <br />
            <br />
            <asp:Button ID="btnUpload" runat="server" Text="Upload CSV" OnClick="btnUpload_Click" />
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
        </asp:Panel> 
    </div>
</asp:Content>

