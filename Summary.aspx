<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" CodeFile="Summary.aspx.cs" Inherits="Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        body {
            font-family: Arial, Helvetica, sans-serif;
            font-size: smaller;
            color: black;
        }

        .hiddencol {
            display: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />

    <table >
        <tr>
            <td>

                <table>
                    <tr>
                        <td align="left">
                            <div style =" background-color:#5D7B9D;  
                            height:30px;width:1450px; margin:0;padding:0">
                                <table cellspacing="0" cellpadding = "0" rules="all" border="1" id="tblHeader" 
                                style="font-family:Arial;font-size:x-small;width:1450px;color:white;
                                border-collapse:collapse;height:100%;">
                                    <tr>
                                    <td style ="width:50px;text-align:center"></td>
                                    <td style ="width:150px;text-align:center">Project Location</td>
                                    <td style ="width:150px;text-align:center">Data Center</td>
                                    <td style ="width:150px;text-align:center">Project Number</td>
                                    <td style ="width:200px;text-align:center">Project Name</td>
                                    <td style ="width:150px;text-align:center">SBU</td>
                                    <td style ="width:150px;text-align:center">Business Line</td>
                                    <td style ="width:150px;text-align:center">Request Date</td>
                                    <td style ="width:150px;text-align:center">Forecast Close Date</td>
                                    <td style ="width:150px;text-align:center">Target Completion</td>
                                    </tr>
                                </table>
                            </div>

                            <div style ="height:500px; width:1467px; overflow:auto;">
                                <asp:GridView ID="gvSummary" runat="server" 
                                AutoGenerateColumns = "false" Font-Names = "Arial" ShowHeader = "false" 
                                Font-Size = "Smaller"  AlternatingRowStyle-BackColor = "#F7F6F3" OnRowCommand="gvSummary_RowCommand">
                                    <Columns>
                                        <asp:ButtonField ItemStyle-Width = "50px" Text="Edit" CommandName="Select" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:boundfield ItemStyle-Width = "150px" datafield="Project_Location" headertext="Project Location"/>
                                        <asp:boundfield ItemStyle-Width = "150px" datafield="Data_Center" headertext="Data Center"/>
                                        <asp:boundfield ItemStyle-Width = "150px" datafield="Project_Number" headertext="Project Number"/>
                                        <asp:boundfield ItemStyle-Width = "200px" datafield="Project_Name" headertext="Project Name"/>
                                        <asp:boundfield ItemStyle-Width = "150px" datafield="SBU" headertext="SBU"/>
                                        <asp:boundfield ItemStyle-Width = "150px" datafield="Business_Line" headertext="Business Line"/>
                                        <asp:boundfield ItemStyle-Width = "150px" datafield="Request_Date" headertext="Request Date" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:boundfield ItemStyle-Width = "150px" datafield="Forecast_Close_Date" headertext="Forecast Close Date" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:boundfield ItemStyle-Width = "150px" datafield="TargetCompletion" headertext="Target Completion" ItemStyle-HorizontalAlign="Center"/>
                                        <asp:boundfield ItemStyle-CssClass="hiddencol" datafield="Is_CWX_671"/>
                                    </Columns> 
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
    </table>
    

    

    
       

    <%--<asp:Panel ID="Panel1" runat="server">
        <asp:GridView ID="gvSummary" runat="server" CellPadding="4" Font-Size="Smaller" Font-Names="Arial" ForeColor="#333333" BorderWidth="1px" AllowPaging="False" AllowSorting="False" AutoGenerateColumns="False">
            <Columns>
                <asp:ButtonField Text="Edit" CommandName="Select" />
                <asp:boundfield datafield="Project_Location" headertext="Project Location"/>
                <asp:boundfield datafield="Data_Center" headertext="Data Center"/>
                <asp:boundfield datafield="Project_Number" headertext="Project Number"/>
                <asp:boundfield datafield="Project_Name" headertext="Project Name"/>
                <asp:boundfield datafield="SBU" headertext="SBU"/>
                <asp:boundfield datafield="Business_Line" headertext="Business Line"/>
                <asp:boundfield datafield="Request_Date" headertext="Request Date"/>
                <asp:boundfield datafield="Forecast_Close_Date" headertext="Forecast Close Date"/>
            </Columns>
            
            <AlternatingRowStyle BackColor="White" ForeColor="#284775"  />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="Smaller" Font-Names="Arial" />
            <PagerStyle BackColor="#284775" ForeColor="White" Font-Size="Smaller" Font-Names="Arial" HorizontalAlign="Left" VerticalAlign="Middle" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
    </asp:Panel>--%>
</asp:Content>

