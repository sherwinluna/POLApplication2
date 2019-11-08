<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" CodeFile="BulkUpdate.aspx.cs" Inherits="BulkUpdate" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type = "text/css">
    input[type=text], select{background-color:#FFFFD2; border:1px solid #ccc}
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <br />
    <br />

    <div style ="width: 900px;height:400px;overflow:auto;">

    <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns="false" DataKeyNames = "Project_Number">
    <Columns>
        <asp:TemplateField>
            <HeaderTemplate>
                <asp:CheckBox ID = "chkAll" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:CheckBox runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Project Number" ItemStyle-Width = "150">
            <ItemTemplate>
                <asp:Label runat="server" Text='<%# Eval("Project_Number") %>'></asp:Label>
                <asp:TextBox ID="txtProject_Number" runat="server" Text='<%# Eval("Project_Number") %>' Visible="false" ReadOnly="true"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Project Name" ItemStyle-Width = "150">
            <ItemTemplate>
                <asp:Label runat="server" Text='<%# Eval("Project_Name") %>'></asp:Label>
                <asp:TextBox ID="txtProject_Name" runat="server" Text='<%# Eval("Project_Name") %>' Visible="false" ReadOnly="true"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Scheduled Date" ItemStyle-Width = "150">
            <ItemTemplate>
                <asp:Label ID = "Scheduled_Date" runat="server" Text='<%# Eval("Migrate_Scheduled_Date") %>'></asp:Label>
                <asp:TextBox ID="txtScheduled_Date" runat="server" Text='<%# Eval("Migrate_Scheduled_Date") %>' Visible="false"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="ceScheduled_Date" runat="server" TargetControlID="txtScheduled_Date" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Completed Date" ItemStyle-Width = "150">
            <ItemTemplate>
                <asp:Label ID = "Completed_Date" runat="server" Text='<%# Eval("Migrate_Completed_Date") %>'></asp:Label>
                <asp:TextBox ID="txtCompleted_Date" runat="server" Text='<%# Eval("Migrate_Completed_Date") %>' Visible="false"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="ceCompleted_Date" runat="server" TargetControlID="txtCompleted_Date" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    </asp:GridView>
    </div>

    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick = "Update" Visible = "false"/>
</asp:Content>

