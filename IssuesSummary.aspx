<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" CodeFile="IssuesSummary.aspx.cs" Inherits="IssuesSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        a.x:visited, a.x:link, a.x:active, a.x:hover {text-decoration: none; color: white; font-size:large;}
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />

    <table >
        <tr>
            <td>

                <table>
                    <tr>
                        <td align="right">
                            <asp:Panel ID="PanelSearchIncident" runat="server">

                                <asp:DropDownList ID="ddSearchIncident" runat="server" OnSelectedIndexChanged="ddSearchIncident_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem>Incident</asp:ListItem>
                                    <asp:ListItem>Status</asp:ListItem>
                                    <asp:ListItem>Raised By</asp:ListItem>
                                </asp:DropDownList>
                                <asp:DropDownList ID="ddSearchStatus" runat="server" Visible="false">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>Closed</asp:ListItem>
                                    <asp:ListItem>Open</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtSearchIncident" runat="server"></asp:TextBox>
                                <asp:TextBox ID="txtSearchIncidentRaisedBy" runat="server" Visible="false"></asp:TextBox>
                                <asp:Button ID="btnSearchIncident" runat="server" Text="Search" OnClick="btnSearchIncident_Click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <div style =" background-color:#5D7B9D;  
                            height:30px;width:1450px; margin:0;padding:0">
                                <table cellspacing="0" cellpadding = "0" rules="all" border="1" id="tblHeader" 
                                style="font-family:Arial;font-size:x-small;width:1450px;color:white;
                                border-collapse:collapse;height:100%;">
                                    <tr>
                                    <td style ="width:50px;text-align:center"><asp:LinkButton ID="lplus" runat="server" OnClick="lplus_Click" CssClass="x">+</asp:LinkButton></td>
                                    <td style ="width:150px;text-align:center">Requirement Id</td>
                                    <td style ="width:150px;text-align:center">Raised By</td>
                                    <td style ="width:150px;text-align:center">Office</td>
                                    <td style ="width:200px;text-align:center">Project Number</td>
                                    <td style ="width:150px;text-align:center">Incident Description</td>
                                    <td style ="width:150px;text-align:center">Solution</td>
                                    <td style ="width:150px;text-align:center">POL/Coreworx</td>
                                    <td style ="width:150px;text-align:center">Status</td>
                                    <td style ="width:150px;text-align:center">Assigned To</td>
                                    </tr>
                                </table>
                            </div>

                            <div style ="height:500px; width:1467px; overflow:auto;">
                                <asp:GridView ID="gvSummary" runat="server" 
                                AutoGenerateColumns = "false" Font-Names = "Arial" ShowHeader = "false" 
                                Font-Size = "Smaller"  AlternatingRowStyle-BackColor = "#F7F6F3" OnRowCommand="gvSummary_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ItemStyle-Width = "50px" Text="Edit" CommandName="Select" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:boundfield ItemStyle-Width = "150px" datafield="ReqNo" headertext="Requirement Id"/>
                                        <asp:boundfield ItemStyle-Width = "150px" datafield="RaisedBy" headertext="Raised By"/>
                                        <asp:boundfield ItemStyle-Width = "150px" datafield="Office" headertext="Office"/>
                                        <asp:boundfield ItemStyle-Width = "200px" datafield="Project" headertext="Project Number"/>
                                        <asp:boundfield ItemStyle-Width = "150px" datafield="Detailed_Issue" headertext="Incident Description"/>
                                        <asp:boundfield ItemStyle-Width = "150px" datafield="Solution" headertext="Solution"/>
                                        <asp:boundfield ItemStyle-Width = "150px" datafield="POL_Coreworx" headertext="POL/Coreworx" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:boundfield ItemStyle-Width = "150px" datafield="Status" headertext="Status" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:boundfield ItemStyle-Width = "150px" datafield="AssignedTo" headertext="Assigned To" ItemStyle-HorizontalAlign="Center"/>
                                    </Columns> 
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
    </table>
</asp:Content>

