Public Class Login
    Inherits Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Session("usuarioLogado") = "sim" Then
            Response.Redirect("../Default.aspx")
        Else
            RegisterHyperLink.NavigateUrl = "Registro.aspx"
        End If


        'RegisterHyperLink.NavigateUrl = "Default.aspx"

        'Session("usuario") = 

        'OpenAuthLogin.ReturnUrl = Request.QueryString("ReturnUrl")

        'Dim returnUrl = HttpUtility.UrlEncode(Request.QueryString("ReturnUrl"))
        'If Not String.IsNullOrEmpty(returnUrl) Then
        'RegisterHyperLink.NavigateUrl &= "?ReturnUrl=" & returnUrl
        'End If
    End Sub

    Protected Sub btnLogar_Click(sender As Object, e As EventArgs) Handles btnLogar.Click

        Dim mapeadorDeUsuario As New MapeadorDeUsuarios
        Dim usuario As New Usuario

        usuario.Usuario = UserName.Text
        usuario.Senha = Password.Text

        If mapeadorDeUsuario.UsuarioExiste(usuario) Then
            If mapeadorDeUsuario.Login(usuario) Then
                Session("usuarioLogado") = "sim"
                Response.Redirect("../Default.aspx")
            Else
                lblErro.Text = "Usuário ou senha incorretos"
                lblErro.Visible = True

            End If
        Else
            lblErro.Text = "Usuário não cadastrado"
            lblErro.Visible = True
        End If
    End Sub

End Class