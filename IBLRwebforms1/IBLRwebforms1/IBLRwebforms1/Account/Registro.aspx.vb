Public Class WebForm2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim usuario As New Usuario
        Dim mapeadorDeUsuario As New MapeadorDeUsuarios

        usuario.Usuario = UserName.Text
        usuario.Senha = Password.Text
        usuario.Email = Email.Text
        usuario.Ativo = "True"
        If mapeadorDeUsuario.UsuarioExiste(usuario) = True Then
            lblMsg.Text = "Usuário já existe!"
            lblMsg.Visible = True
            lblMsg.ForeColor = Drawing.Color.Red
        Else
            mapeadorDeUsuario.Inserir(usuario)
            lblMsg.Text = "Cadastro realizado com sucesso!"
            lblMsg.Visible = True
            lblMsg.ForeColor = Drawing.Color.DarkGreen
        End If

    End Sub
End Class