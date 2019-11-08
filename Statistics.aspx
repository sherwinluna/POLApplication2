<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" CodeFile="Statistics.aspx.cs" Inherits="Statistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <br />

    <div style="text-align:left;">

        <table>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Summary List" Font-Bold="True"></asp:Label>
                    <asp:GridView ID="gvStatistics" runat="server" CellPadding="4" Font-Size="Small" Font-Names="Arial" ForeColor="#333333" BorderWidth="1px" AllowPaging="False" AllowSorting="False" AutoGenerateColumns="False" >
                        <Columns>
                            <asp:hyperlinkfield datatextfield="Item" headertext="Item" DataNavigateUrlFields="Ordinal" DataNavigateUrlFormatString="Summary.aspx?Type=&Id={0}"/>
                            <asp:boundfield datafield="Count" headertext="Count" ItemStyle-Font-Size="Smaller" ItemStyle-Font-Names="Arial"/>
                        </Columns>
            
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775"  />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="Smaller" Font-Names="Arial" />
                        <PagerStyle BackColor="#284775" ForeColor="White" Font-Size="Small" Font-Names="Arial" HorizontalAlign="Left" VerticalAlign="Middle" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>

                    <br />
                    <asp:GridView ID="gvSiteCategory" runat="server" CellPadding="4" Font-Size="Small" Font-Names="Arial" ForeColor="#333333" BorderWidth="1px" AllowPaging="False" AllowSorting="False" AutoGenerateColumns="False" >
                        <Columns>
                            <asp:hyperlinkfield datatextfield="Item" headertext="Item" DataNavigateUrlFields="Ordinal" DataNavigateUrlFormatString="Summary2.aspx?Type=&Id={0}"/>
                            <asp:boundfield datafield="Type" headertext="Type" ItemStyle-Font-Size="Smaller" ItemStyle-Font-Names="Arial"/>
                            <asp:boundfield datafield="Count" headertext="Count" ItemStyle-Font-Size="Smaller" ItemStyle-Font-Names="Arial"/>
                        </Columns>
            
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775"  />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="Smaller" Font-Names="Arial" />
                        <PagerStyle BackColor="#284775" ForeColor="White" Font-Size="Small" Font-Names="Arial" HorizontalAlign="Left" VerticalAlign="Middle" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>

                    <br />
                    <asp:Label ID="Label9" runat="server" Text="Migration Status" Font-Bold="True"></asp:Label>
                    <asp:GridView ID="gvMigrationStatus" runat="server" CellPadding="4" Font-Size="Small" Font-Names="Arial" ForeColor="#333333" BorderWidth="1px" AllowPaging="False" AllowSorting="False" AutoGenerateColumns="False" >
                        <Columns>
                            <asp:hyperlinkfield datatextfield="Item" headertext="Item" DataNavigateUrlFields="Ordinal" DataNavigateUrlFormatString="Summary3.aspx?Type=&Id={0}"/>
                            <asp:boundfield datafield="Count" headertext="Count" ItemStyle-Font-Size="Smaller" ItemStyle-Font-Names="Arial"/>
                        </Columns>
            
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775"  />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Font-Size="Smaller" Font-Names="Arial" />
                        <PagerStyle BackColor="#284775" ForeColor="White" Font-Size="Small" Font-Names="Arial" HorizontalAlign="Left" VerticalAlign="Middle" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
    
                    <br />
                    <asp:Label ID="Label2" runat="server" Text="Historical Data" Font-Bold="True"></asp:Label>
                    <asp:GridView ID="gvStatisticsHis" runat="server" CellPadding="4" Font-Size="Smaller" Font-Names="Arial" ForeColor="#333333" BorderWidth="1px" AllowPaging="False" AllowSorting="False" AutoGenerateColumns="True" >            
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
                    </asp:GridView>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td>
                    <table>
                        <tr><td colspan="5"><b>Data Center</b></td></tr>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="Label3" runat="server" Text="RTP Summary"></asp:Label>
                                <asp:GridView ID="gvDataCenterStatistics1" runat="server" CellPadding="4" Font-Size="Smaller" Font-Names="Arial" ForeColor="#333333" BorderWidth="1px" AllowPaging="False" AllowSorting="False" AutoGenerateColumns="True" >            
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
                                </asp:GridView>
                            </td>
                            <td>&nbsp;</td>
                            <td valign="top">
                                <asp:Label ID="Label4" runat="server" Text="SG Summary"></asp:Label>
                                <asp:GridView ID="gvDataCenterStatistics2" runat="server" CellPadding="4" Font-Size="Smaller" Font-Names="Arial" ForeColor="#333333" BorderWidth="1px" AllowPaging="False" AllowSorting="False" AutoGenerateColumns="True" >            
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
                            </td>
                            <td>&nbsp;</td>
                            <td valign="top">
                                <asp:Label ID="Label5" runat="server" Text="Perth Summary"></asp:Label>
                                <asp:GridView ID="gvDataCenterStatistics3" runat="server" CellPadding="4" Font-Size="Smaller" Font-Names="Arial" ForeColor="#333333" BorderWidth="1px" AllowPaging="False" AllowSorting="False" AutoGenerateColumns="True" >            
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="ERR Request" Font-Bold="True"></asp:Label>
                    <table>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="Label7" runat="server" Text="RTP Summary"></asp:Label>
                                <asp:GridView ID="gvStatisticsERR1" runat="server" CellPadding="4" Font-Size="Smaller" Font-Names="Arial" ForeColor="#333333" BorderWidth="1px" AllowPaging="False" AllowSorting="False" AutoGenerateColumns="True" >            
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
                                </asp:GridView>
                            </td>
                            <td>&nbsp;</td>
                            <td valign="top">
                                <asp:Label ID="Label8" runat="server" Text="SG Summary"></asp:Label>
                                <asp:GridView ID="gvStatisticsERR2" runat="server" CellPadding="4" Font-Size="Smaller" Font-Names="Arial" ForeColor="#333333" BorderWidth="1px" AllowPaging="False" AllowSorting="False" AutoGenerateColumns="True" >            
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
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Text="Project Deletion Request" Font-Bold="True"></asp:Label>
                    <table>
                        <tr>
                            <td valign="top">
                                <asp:Label ID="Label11" runat="server" Text="RTP Summary"></asp:Label>
                                <asp:GridView ID="gvStatisticsPD1" runat="server" CellPadding="4" Font-Size="Smaller" Font-Names="Arial" ForeColor="#333333" BorderWidth="1px" AllowPaging="False" AllowSorting="False" AutoGenerateColumns="True" >            
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
                                </asp:GridView>
                            </td>
                            <td>&nbsp;</td>
                            <td valign="top">
                                <asp:Label ID="Label12" runat="server" Text="SG Summary"></asp:Label>
                                <asp:GridView ID="gvStatisticsPD2" runat="server" CellPadding="4" Font-Size="Smaller" Font-Names="Arial" ForeColor="#333333" BorderWidth="1px" AllowPaging="False" AllowSorting="False" AutoGenerateColumns="True" >            
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
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>



            <tr><td>&nbsp;</td></tr>
            <tr>
                <td>
                    <asp:Label ID="Label13" runat="server" Text="Deleted and Closed Projects" Font-Bold="True"></asp:Label>
                    <table>
                        <tr>
                            <td valign="top">
                                <asp:GridView ID="gvStatisticsPD_Closed" runat="server" CellPadding="4" Font-Size="Smaller" Font-Names="Arial" ForeColor="#333333" BorderWidth="1px" AllowPaging="False" AllowSorting="False" AutoGenerateColumns="True" >            
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
                                </asp:GridView>
                            </td>
                            <td>&nbsp;</td>
                            <td valign="top">&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>


        </table>  
          
    </div>

</asp:Content>

