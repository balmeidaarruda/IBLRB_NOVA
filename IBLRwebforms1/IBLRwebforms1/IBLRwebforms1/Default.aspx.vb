Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("usuarioLogado") = "sim" Then
            Dim menu As Menu = CType(Master.FindControl("Menu1"), Menu)
            menu.Visible = True
            Dim image As Image = CType(Master.FindControl("statusLogin"), Image)
            image.ImageUrl = "../Images/green-button_24x24.png"
            image.ImageAlign = ImageAlign.Right
            image.ToolTip = "Usuário logado"
        End If
    End Sub
End Class