<%@ Page Title="" Language="VB" MasterPageFile="~/Master/Main.master" AutoEventWireup="false" CodeFile="Tools.aspx.vb" Inherits="Tools" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />

    <table border="1" cellpadding="1" cellspacing="1">
        <tr>
            <td align="left" colspan="3"><b><u>Migration Tools</u></b></td>
        </tr>
        <tr>
            <th>File</th>
            <th>Description</th>
            <th>Revision Date</th>
        </tr>
        <tr>
            <td align="left">
                <asp:LinkButton ID="LinkButton4" runat="server">Project Assessments</asp:LinkButton>
            </td>
            <td align="left">
                Script use to retrieve the project statistics
            </td>
            <td>July 14, 2016&nbsp;</td>
        </tr>
        <tr>
            <td align="left">
                <asp:LinkButton ID="LinkButton1" runat="server">Migration Tool (Database)</asp:LinkButton>
            </td>
            <td align="left">
                Common SQL script to convert External Validation, Initialization Rules, Result List, Report and Workflow
            </td>
            <td>July 14, 2016&nbsp;</td>
        </tr>
        <tr>
            <td align="left">
                <asp:LinkButton ID="LinkButton2" runat="server">Migration Tool (Reports)</asp:LinkButton>
            </td>
            <td align="left">
                Executable program to convert Project Report Files
            </td>
            <td>August 3, 2016&nbsp;</td>
        </tr>
        <tr>
            <td align="left">
                <asp:LinkButton ID="LinkButton3" runat="server">Coreworx Global Project Migration Schedule Checklist</asp:LinkButton>
            </td>
            <td align="left">
                Master File of Coreworx Global Project Migration Schedule Checklist
            </td>
            <td>July 14, 2016&nbsp;</td>
        </tr>
        <tr>
            <td align="left">
                <asp:LinkButton ID="LinkButton5" runat="server">Migration Database (Triggers)</asp:LinkButton>
            </td>
            <td align="left">
                Common Triggers use by the project. The zip file contains the Oracle trigger use as basis of the converted MSSQL trigger. 
            </td>
            <td>May 23, 2017&nbsp;</td>
        </tr>
        <tr>
            <td align="left">
                <asp:LinkButton ID="LinkButton6" runat="server">Migration Training Materials</asp:LinkButton>
            </td>
            <td align="left">
                 Migration Training Materials
            </td>
            <td>May 23, 2017&nbsp;</td>
        </tr>
        <tr><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="left" colspan="3"><b><u>Project Database</u></b></td>
        </tr>
        <tr>
            <th>File</th>
            <th>Description</th>
            <th>Revision Date</th>
        </tr>
        <tr>
            <td align="left">
                <asp:LinkButton ID="LinkButton7" runat="server">Project Database Training Materials</asp:LinkButton>
            </td>
            <td align="left">
                 Project Database Training Materials
            </td>
            <td>May 23, 2017&nbsp;</td>
        </tr>
    </table>
</asp:Content>

