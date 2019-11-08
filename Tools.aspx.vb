
Partial Class Tools
    Inherits System.Web.UI.Page

    Protected Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        Dim strDownloadpath As String = "~/Tools/Migration Script.sql"
        DownloadFile(strDownloadpath)
    End Sub

    Protected Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        Dim strDownloadpath As String = "~/Tools/MigrationTool.zip"
        DownloadFile(strDownloadpath)
    End Sub

    Protected Sub LinkButton3_Click(sender As Object, e As EventArgs) Handles LinkButton3.Click
        Dim strDownloadpath As String = "~/Tools/Migration Checklist - new DRAFT.xlsx"
        DownloadFile(strDownloadpath)
    End Sub

    Private Sub DownloadFile(ByVal filename As String)
        Response.Clear()
        Response.ContentType = "application\octet-stream"
        Dim file = New System.IO.FileInfo(Server.MapPath(filename))
        Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name)
        Response.AddHeader("Content-Length", file.Length.ToString())
        Response.ContentType = "application/octet-stream"
        Response.WriteFile(file.FullName)
        Response.Flush()
    End Sub

    Protected Sub LinkButton4_Click(sender As Object, e As EventArgs) Handles LinkButton4.Click
        Dim strDownloadpath As String = "~/Tools/Project Assessments.sql"
        DownloadFile(strDownloadpath)
    End Sub

    Private Sub Tools_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Page.Title = "Tools"
    End Sub

    Protected Sub LinkButton5_Click(sender As Object, e As EventArgs) Handles LinkButton5.Click
        Dim strDownloadpath As String = "~/Tools/Triggers.zip"
        DownloadFile(strDownloadpath)
    End Sub

    Protected Sub LinkButton6_Click(sender As Object, e As EventArgs) Handles LinkButton6.Click
        Dim strDownloadpath As String = "~/Tools/Migration.zip"
        DownloadFile(strDownloadpath)
    End Sub

    Protected Sub LinkButton7_Click(sender As Object, e As EventArgs) Handles LinkButton7.Click
        Dim strDownloadpath As String = "~/Tools/ProjectDatabase.zip"
        DownloadFile(strDownloadpath)
    End Sub
End Class
