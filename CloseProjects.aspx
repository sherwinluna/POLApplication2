<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Main.master" AutoEventWireup="true" CodeFile="CloseProjects.aspx.cs" Inherits="CloseProjects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <br />

    <table>
        <tr>
            <td align="left">
                <div style =" background-color:#5D7B9D;  
                height:30px;width:1150; margin:0;padding:0">
                    <table cellspacing="0" cellpadding = "0" rules="all" border="1" id="tblHeader" 
                    style="font-family:Arial;font-size:x-small;width:1150px;color:white;
                    border-collapse:collapse;height:100%;">
                        <tr>
                        <td style ="width:200px;text-align:center">Project Location</td>
                        <td style ="width:150px;text-align:center">Project Number</td>
                        <td style ="width:200px;text-align:center">Project Name</td>
                        <td style ="width:150px;text-align:center">Request Date</td>
                        <td style ="width:150px;text-align:center">Close Date</td>
                        <td style ="width:150px;text-align:center">Project Type</td>
                        <td style ="width:150px;text-align:center">Duration (In Years)</td>
                        </tr>
                    </table>
                </div>

                <div style ="height:500px; width:1167px; overflow:auto;">
                    <asp:GridView ID="gvSummary" runat="server" 
                    AutoGenerateColumns = "false" Font-Names = "Arial" ShowHeader = "false" 
                    Font-Size = "Smaller"  AlternatingRowStyle-BackColor = "#F7F6F3">
                        <Columns>
                            <asp:boundfield ItemStyle-Width = "200px" datafield="Project_Location" headertext="Project Location"/>
                            <asp:boundfield ItemStyle-Width = "150px" datafield="Project_No" headertext="Project Number"/>
                            <asp:boundfield ItemStyle-Width = "200px" datafield="Project_Name" headertext="Project Name"/>
                            <asp:boundfield ItemStyle-Width = "150px" datafield="Project_Request_Date" headertext="Request Date" ItemStyle-HorizontalAlign="Center"/>
                            <asp:boundfield ItemStyle-Width = "150px" datafield="Request_Close_Date" headertext="Close Date" ItemStyle-HorizontalAlign="Center"/>
                            <asp:boundfield ItemStyle-Width = "150px" datafield="Project_Type" headertext="Project Type"/>
                            <asp:boundfield ItemStyle-Width = "150px" datafield="Project_Duration_Years" headertext="Duration (In Years)"/>
                        </Columns> 
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>

</asp:Content>

