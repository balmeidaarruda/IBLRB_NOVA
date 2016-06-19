Public Class _Default
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("usuarioLogado") = "sim" Then
            'Dim menu As Menu = CType(Master.FindControl("Menu1"), Menu)
            'menu.Visible = True
            'Dim btnCongregacoes As Button = CType(Master.FindControl("btnCongregacoes"), Button)
            'btnCongregacoes.Visible = True
            'Dim btnCampos As Button = CType(Master.FindControl("btnCampos"), Button)
            'btnCampos.Visible = True
            'Dim btnCargos As Button = CType(Master.FindControl("btnCargos"), Button)
            'btnCargos.Visible = True
            'Dim btnMembros As Button = CType(Master.FindControl("btnMembros"), Button)
            'btnMembros.Visible = True

            Dim image As Image = CType(Master.FindControl("statusLogin"), Image)
            image.ImageUrl = "../Images/green-button_24x24.png"
            image.ImageAlign = ImageAlign.Right
            image.ToolTip = "Usuário logado"
        Else
            Response.Redirect("~/login.aspx")
            Exit Sub
        End If
    End Sub
End Class