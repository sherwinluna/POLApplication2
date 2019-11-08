<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" CodeFile="Users.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />

    <div style="text-align:left;">
        <table>
            <tr>
                <td>First Name</td><td><asp:TextBox ID="txtFname" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Last Name</td><td><asp:TextBox ID="txtLname" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Role</td>
                <td>
                    <asp:DropDownList ID="ddRole" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2"><asp:Button ID="btnAddUser" runat="server" Text="Add User" OnClick="btnAddUser_Click" /></td>
            </tr>
            <tr>
                <td colspan="2"><asp:Label ID="lblStatus" runat="server" ForeColor="Red"></asp:Label></td>
            </tr>
            <tr><td colspan="2">&nbsp;</td></tr>
            <tr>
                <td colspan="2">
                    <%--<asp:GridView ID="gvUserList" runat="server" CellPadding="4" Font-Size="Smaller" Font-Names="Arial" ForeColor="#333333" BorderWidth="1px" AllowPaging="False" AllowSorting="False" AutoGenerateColumns="True" >            
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775"  />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="Smaller" Font-Names="Arial" />
                        <PagerStyle BackColor="#284775" ForeColor="White" Font-Size="Smaller" Font-Names="Arial" HorizontalAlign="Left" VerticalAlign="Middle" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>--%>


                    <table>
                        <tr>
                        <td align="left">
                            <div style =" background-color:#5D7B9D;  
                            height:30px;width:800px; margin:0;padding:0">
                                <table cellspacing="0" cellpadding = "0" rules="all" border="1" id="tblHeader" 
                                style="font-family:Arial;font-size:x-small;width:800px;color:white;
                                border-collapse:collapse;height:100%;" runat="server">
                                    <tr>
                                    <td style ="width:50px;text-align:center" id="Row1"></td>
                                    <td style ="width:150px;text-align:center" id="Row2">Username</td>
                                    <td style ="width:150px;text-align:center" id="Row3">First Name</td>
                                    <td style ="width:150px;text-align:center" id="Row4">Last Name</td>
                                    <td style ="width:150px;text-align:center" id="Row5">Date Created</td>
                                    <td style ="width:150px;text-align:center" id="Row6">Role</td>
                                    </tr>
                                </table>
                            </div>

                            <div style ="height:500px; width:817px; overflow:auto;">
                                <asp:GridView ID="gvUserList" runat="server" 
                                AutoGenerateColumns = "false" Font-Names = "Arial" ShowHeader = "false" 
                                Font-Size = "Smaller"  AlternatingRowStyle-BackColor = "#F7F6F3" OnRowCommand="gvUserList_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ItemStyle-Width = "50px" Text="Delete" CommandName="Select" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:boundfield ItemStyle-Width = "150px" datafield="UserName" headertext="Username"/>
                                        <asp:boundfield ItemStyle-Width = "150px" datafield="FirstName" headertext="First Name"/>
                                        <asp:boundfield ItemStyle-Width = "150px" datafield="LastName" headertext="Last Name"/>
                                        <asp:boundfield ItemStyle-Width = "150px" datafield="Date_Created" headertext="Date Created"/>
                                        <asp:boundfield ItemStyle-Width = "150px" datafield="RoleName" headertext="Role"/>                                        
                                    </Columns> 
                                </asp:GridView>
                            </div>
                        </td>
                        </tr>
                </table>

                </td>
            </tr>
        </table>
    </div>
</asp:Content>

