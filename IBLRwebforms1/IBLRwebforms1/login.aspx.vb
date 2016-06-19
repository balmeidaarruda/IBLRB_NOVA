Public Class login1
    Inherits System.Web.UI.Page

    Private _MapeadorDeLog As New MapeadorDeLog
    Private log As New Log
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click

        Dim mapeadorDeUsuario As New MapeadorDeUsuarios

        Dim usuario As New Usuario

        usuario.Usuario = UserName.Text
        usuario.Senha = Password.Text

        If mapeadorDeUsuario.UsuarioExiste(usuario) Then
            If mapeadorDeUsuario.Login(usuario) Then
                Session("usuarioLogado") = "sim"
                Session("usuario") = usuario.Usuario
                log.Acao = "Logar"
                log.Funcao = "Login"
                log.ModificadoPor = Session("usuario")
                log.DataAlteracao = DateAndTime.Now
                Try
                    _MapeadorDeLog.GravarLogs(log)
                    Response.Redirect("~/Paginas/Default.aspx")
                Catch ex As Exception
                    Utilitarios.EnviarMensagem(ex.Message, Me)
                End Try
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "ShowPopup('" + "Usuário ou senha incorretos" + "');", True)
                'Utilitarios.EnviarMensagem("Usuário ou senha incorretos", Me)
            End If
        Else
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "ShowPopup('" + "Usuário não cadastrado" + "');", True)
            'Utilitarios.EnviarMensagem("Usuário não cadastrado", Me)
        End If
    End Sub
End Class