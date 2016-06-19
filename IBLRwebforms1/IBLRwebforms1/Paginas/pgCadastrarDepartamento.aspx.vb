Public Class pgCadastrarDepartamento
    Inherits System.Web.UI.Page

    Private _MapeadorDeLog As New MapeadorDeLog
    Private log As New Log

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        If Session("usuarioLogado") = "sim" Then
            'Dim menu As Menu = CType(Master.FindControl("Menu1"), Menu)
            'menu.Visible = True
            Dim image As Image = CType(Master.FindControl("statusLogin"), Image)
            image.ImageUrl = "../Images/green-button_24x24.png"
            image.ImageAlign = ImageAlign.Right
            image.ToolTip = "Usuário logado"

            log.Acao = "Acesso"
            log.Funcao = "pgCadastrarDepartamento"
            log.Valor = "Acesso"
            log.ModificadoPor = Session("usuario")
            log.DataAlteracao = DateAndTime.Now
            Try
                _MapeadorDeLog.GravarLogs(log)
            Catch ex As Exception
                Utilitarios.EnviarMensagem(ex.Message, Me)
            End Try
        Else
            Response.Redirect("~/login.aspx")
            Exit Sub
        End If
        If Not IsPostBack Then
            Me.ClientScript.RegisterClientScriptInclude("scripts", "Scripts/JavaScript.js")
            Dim _MapeadorDeMembros As New MapeadorDeMembros
            ddMembros.DataSource = _MapeadorDeMembros.getListaMembros("")
            ddMembros.DataBind()
            ddMembros.Items.Insert(0, "")

            Dim _MapeadorDeDepartamentos As New MapeadorDeDepartamentos
            gdDepartamentos.DataSource = _MapeadorDeDepartamentos.getListaDepartamentos()
            gdDepartamentos.DataBind()
        End If
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        Try
            Dim _MapeadorDeDepartamentos As New MapeadorDeDepartamentos
            Dim _departamento As New Departamento

            _departamento.Id = lblIdDados.Text
            _departamento.Descricao = txtDepartamento.Text
            _departamento.Ativo = ckbAtivo.Checked

            Dim _membro As New Membro
            _membro.ID = ddMembros.SelectedValue
            _membro.NOME = ddMembros.SelectedItem.Text

            _departamento.MembroLiderDpto = _membro

            If lblIdDados.Text = String.Empty Then
                _MapeadorDeDepartamentos.Salvar(_departamento)
                Utilitarios.EnviarMensagem("Cadastro realizado com sucesso.", Me)
                log.Acao = "Inserir"
                log.Funcao = "CadastrarDepartamento"
                log.Valor = _MapeadorDeDepartamentos.getDepartamentoUltimoInserido() + "-" + _departamento.Descricao
                log.ModificadoPor = Session("usuario")
                log.DataAlteracao = DateAndTime.Now
                _MapeadorDeLog.GravarLogs(log)
            Else
                _MapeadorDeDepartamentos.Atualizar(_departamento)
                Utilitarios.EnviarMensagem("Cadastro atualizado com sucesso.", Me)
                log.Acao = "Atualizar"
                log.Funcao = "CadastrarDepartamento"
                log.Valor = _departamento.Id + "-" + _departamento.Descricao
                log.ModificadoPor = Session("usuario")
                log.DataAlteracao = DateAndTime.Now
                _MapeadorDeLog.GravarLogs(log)
            End If

            gdDepartamentos.DataSource = _MapeadorDeDepartamentos.getListaDepartamentos()
            gdDepartamentos.DataBind()
            txtDepartamento.Text = String.Empty
            ddMembros.SelectedIndex = -1
            lblIdDados.Text = String.Empty
            ckbAtivo.Checked = False
            btnSalvar.Text = "Salvar"
        Catch ex As Exception
            Utilitarios.EnviarMensagem("Erro " & ex.Message, Me)
        End Try
    End Sub

    Private Sub gdDepartamentos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdDepartamentos.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Select Case e.CommandName
            Case "EditarDepartamento"
                limparCampos()
                Dim _MapeadorDeDepartamentos As New MapeadorDeDepartamentos

                Dim _dep As New Departamento

                _dep = _MapeadorDeDepartamentos.getDepartamentoID(gdDepartamentos.Rows(index).Cells(1).Text)
                lblIdDados.Text = _dep.Id
                txtDepartamento.Text = _dep.Descricao
                ddMembros.SelectedValue = _dep.MembroLiderDpto.ID
                ckbAtivo.Checked = _dep.Ativo
                
                btnSalvar.Text = "Atualizar"
                btnSalvar.Visible = True
                btnNovoRegistro.Visible = True

        End Select
    End Sub


    Private Sub gdDepartamentos_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gdDepartamentos.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.FindControl("btnEditarDepartamento"), ImageButton).CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

    Public Sub limparCampos()
        txtDepartamento.Text = String.Empty
        ddMembros.SelectedIndex = -1
    End Sub

    Private Sub btnNovoRegistro_Click(sender As Object, e As EventArgs) Handles btnNovoRegistro.Click
        
        btnSalvar.Visible = True
        btnSalvar.Text = "Salvar"
        lblIdDados.Text = String.Empty
        btnNovoRegistro.Visible = False
        limparCampos()
    End Sub
End Class