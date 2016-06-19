Public Class WebForm2
    Inherits System.Web.UI.Page

    Private _MapeadorDeLog As New MapeadorDeLog
    Private log As New Log
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("usuarioLogado") = "sim" Then
            'Dim btnCongregacoes As Button = CType(Master.FindControl("btnCongregacoes"), Button)
            'btnCongregacoes.Visible = True
            'Dim btnCampos As Button = CType(Master.FindControl("btnCampos"), Button)
            'btnCampos.Visible = True
            'Dim btnCargos As Button = CType(Master.FindControl("btnCargos"), Button)
            'btnCargos.Visible = True
            'Dim btnMembros As Button = CType(Master.FindControl("btnMembros"), Button)
            'btnMembros.Visible = True

            Dim image As Image = CType(Master.FindControl("statusLogin"), Image)
            image.ImageUrl = "~/Images/green-button_24x24.png"
            image.ImageAlign = ImageAlign.Right
            image.ToolTip = "Usuário logado"

            Log.Acao = "Acesso"
            log.Funcao = "Registro"
            log.Valor = "Acesso"
            Log.ModificadoPor = Session("usuario")
            Log.DataAlteracao = DateAndTime.Now
            Try
                _MapeadorDeLog.GravarLogs(Log)
            Catch ex As Exception
                Utilitarios.EnviarMensagem(ex.Message, Me)
            End Try
        Else
            Response.Redirect("~/login.aspx")
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

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim usuario As New Usuario
        Dim mapeadorDeUsuario As New MapeadorDeUsuarios

        usuario.Usuario = UserName.Text
        usuario.Senha = Password.Text
        usuario.Email = Email.Text
        usuario.Ativo = "True"
        Try
            If mapeadorDeUsuario.UsuarioExiste(usuario) = True Then
                lblMsg.Text = "Usuário já existe!"
                lblMsg.Visible = True
                lblMsg.ForeColor = Drawing.Color.Red
            Else
                mapeadorDeUsuario.Inserir(usuario)
                log.Funcao = "Registro"
                log.Acao = "Inserir"
                log.Valor = mapeadorDeUsuario.getUsuarioUltimoInserido + "-" + usuario.Usuario
                log.ModificadoPor = Session("usuario")
                log.DataAlteracao = DateTime.Now
                _MapeadorDeLog.GravarLogs(log)

                lblMsg.Text = "Cadastro realizado com sucesso!"
                lblMsg.Visible = True
                lblMsg.ForeColor = Drawing.Color.DarkGreen
            End If
        Catch ex As Exception
            Utilitarios.EnviarMensagem("Cadastro realizado com sucesso!", Me)
        End Try
        

    End Sub
End Class