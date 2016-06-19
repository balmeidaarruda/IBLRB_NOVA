Public Class About
    Inherits Page
    Private _MapeadorDeLog As New MapeadorDeLog
    Private log As New Log
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("usuarioLogado") = "sim" Then
            'Dim menu As Menu = CType(Master.FindControl("Menu1"), Menu)
            'menu.Visible = True
            Dim image As Image = CType(Master.FindControl("statusLogin"), Image)
            image.ImageUrl = "../Images/green-button_24x24.png"
            image.ImageAlign = ImageAlign.Right
            image.ToolTip = "Usuário logado"
            'Registrar log
            log.Acao = "Acesso"
            log.Funcao = "Sobre"
            log.ModificadoPor = Session("usuario")
            log.DataAlteracao = DateAndTime.Now
            Try
                _MapeadorDeLog.GravarLogs(log)

            Catch ex As Exception
                Utilitarios.EnviarMensagem(ex.Message, Me)
            End Try
        Else
            Response.Redirect("~/Paginas/Default.aspx")
            'Dim btnCongregacoes As Button = CType(Master.FindControl("btnCongregacoes"), Button)
            'btnCongregacoes.Visible = False
            'Dim btnCampos As Button = CType(Master.FindControl("btnCampos"), Button)
            'btnCampos.Visible = False
            'Dim btnCargos As Button = CType(Master.FindControl("btnCargos"), Button)
            'btnCargos.Visible = False
            'Dim btnMembros As Button = CType(Master.FindControl("btnMembros"), Button)
            'btnMembros.Visible = False
        End If
    End Sub
End Class