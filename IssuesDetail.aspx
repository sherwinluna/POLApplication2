<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" CodeFile="IssuesDetail.aspx.cs" Inherits="IssuesDetail" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

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

    <center><b>Incident Details</b></center>
    <hr width="80%"/>

    <asp:ScriptManager ID="smDetail" runat="server"></asp:ScriptManager>

    <table>
        <tr>
            <td>Requirement No</td>
            <td><asp:TextBox ID="txtReqNo" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Raised By</td>
            <td><asp:TextBox ID="txtRaisedBy" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Office</td>
            <td><asp:TextBox ID="txtOffice" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Project</td>
            <td><asp:TextBox ID="txtProject" runat="server" Height="90px" TextMode="MultiLine" Width="318px"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Area</td>
            <td><asp:TextBox ID="txtArea" runat="server" Height="90px" TextMode="MultiLine" Width="318px"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Date</td>
            <td>
                <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="cDate" runat="server" TargetControlID="txtDate" />
            </td>
        </tr>
        <tr>
            <td>Time</td>
            <td><asp:TextBox ID="txtTime" runat="server"></asp:TextBox>
                <ajaxToolkit:MaskedEditExtender ID="txtTime_MaskedEditExtender" runat="server" TargetControlID="txtTime" MaskType="Time" Mask="99:99:99" AcceptAMPM="false" />
            </td>
        </tr>
        <tr>
            <td>Detailed Issue</td>
            <td>
                <asp:TextBox ID="txtIssue" runat="server" Height="100px" TextMode="MultiLine" Width="320px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>POL/Coreworx</td>
            <td>
                <asp:DropDownList ID="ddPolCwx" runat="server" Width="200">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>POL</asp:ListItem>
                    <asp:ListItem>CWX</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Status</td>
            <td>
                <asp:DropDownList ID="ddStatus" runat="server" Width="200">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem>Open</asp:ListItem>
                    <asp:ListItem>Close</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Assigned To</td>
            <td><asp:TextBox ID="txtAssignedTo" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Solution</td>
            <td>
                <asp:TextBox ID="txtSolution" runat="server" Height="100px" TextMode="MultiLine" Width="320px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Date Completed</td>
            <td>
                <asp:TextBox ID="txtDateCompleted" runat="server"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="cDateCompleted" runat="server" TargetControlID="txtDateCompleted" />
            </td>
        </tr>
        <tr>
            <td>Time Completed</td>
            <td><asp:TextBox ID="txtTimeCompleted" runat="server"></asp:TextBox>
                <ajaxToolkit:MaskedEditExtender ID="txtTimeCompleted_MaskedEditExtender" runat="server" TargetControlID="txtTimeCompleted" MaskType="Time" Mask="99:99:99" AcceptAMPM="false" />
            </td>
        </tr>
        <tr>
            <td>Completed By</td>
            <td><asp:TextBox ID="txtCompletedBy" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSaveAdd" runat="server" Text="Save" Visible ="false" OnClick="btnSaveAdd_Click" />
                <asp:Button ID="btnSaveEdit" runat="server" Text="Save" Visible ="false" OnClick="btnSaveEdit_Click" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfUserId" runat="server" />


    <asp:Panel ID="pnlFiles" runat="server" BorderWidth ="0"  Visible="false">
        <center><b>Attachments</b></center>
        <hr width="80%"/>

        <table>
            <tr>
                <td>File</td>
                <td><asp:FileUpload ID="fuUpload" runat="server" /></td>
            </tr>
            <tr>
                <td>Description</td>
                <td><asp:TextBox ID="txtUpload" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2"><asp:Button ID="btnUpload" runat="server" Text="Upload" Height="22px" OnClick="btnUpload_Click" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvFiles" runat="server"  CellPadding="4" Font-Size="Smaller" Font-Names="Arial" ForeColor="#333333" BorderWidth="1px" AutoGenerateColumns="False" EmptyDataText = "No files uploaded" ShowHeader="true" >
                    <Columns>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkDownload" runat="server" Text="Download"  OnClick="DownloadFile"
                                    CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                                <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete"  OnClick="DeleteFile"
                                    CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DocumentDescription" HeaderText="Description" />
                        <asp:BoundField DataField="DocumentDateUploaded" HeaderText="Date" />
                        <asp:BoundField DataField="DocumentOwner" HeaderText="Owner" />
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
                </td>
            </tr>
        </table>
    </asp:Panel> 

    </div>

</asp:Content>

