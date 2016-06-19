Public Class pgCadastrarTipoDespesa
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
            image.ToolTip = "Usuário logado"
            Log.Acao = "Acesso"
            log.Funcao = "pgCadastrarTipoDespesa"
            Log.Valor = "Logar"
            Log.ModificadoPor = Session("usuario")
            Log.DataAlteracao = DateAndTime.Now
            Try
                _MapeadorDeLog.GravarLogs(Log)
            Catch ex As Exception
                Utilitarios.EnviarMensagem(ex.Message, Me)
            End Try
        Else
            Response.Redirect("~/login.aspx")
            Exit Sub
        End If
        If Not IsPostBack Then
            Me.ClientScript.RegisterClientScriptInclude("scripts", "Scripts/JavaScript.js")
            Dim _MapeadorFinanceiro As New MapeadorFinanceiro
            gdTipoDespesas.DataSource = _MapeadorFinanceiro.getTipoDespesa()
            gdTipoDespesas.DataBind()
        End If
    End Sub

    Private Sub gdTipoDespesas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdTipoDespesas.RowCommand
        Dim index As Integer = CInt(e.CommandArgument)
        Dim _MapeadorFinanceiro As New MapeadorFinanceiro
        Select Case e.CommandName
            Case "ExcluirTipoDespesa"
                Try
                    _MapeadorFinanceiro.ExcluirTipoDespesa(gdTipoDespesas.Rows(index).Cells(1).Text)
                    gdTipoDespesas.DataSource = _MapeadorFinanceiro.getTipoDespesa()

                    log.Funcao = "CadastrarTipoDespesa"
                    log.Acao = "Excluir"

                    log.Valor = gdTipoDespesas.Rows(index).Cells(1).Text + "-" + gdTipoDespesas.Rows(index).Cells(2).Text
                    log.ModificadoPor = Session("usuario")
                    log.DataAlteracao = DateTime.Now
                    _MapeadorDeLog.GravarLogs(log)

                    gdTipoDespesas.DataBind()
                    
                    Utilitarios.EnviarMensagem("Registro inativado com sucesso", Me)
                Catch ex As Exception
                    If ex.Message.ToString.Contains("REFERENCE") Then
                        Utilitarios.EnviarMensagem("Erro: Este tipo de despesa está relacionada com uma despesa cadastrada " & vbCrLf & "Detalhes" & ex.Message, Me)
                    Else
                        Utilitarios.EnviarMensagem("Erro " & ex.Message, Me)
                    End If

                End Try

            Case "EditarTipoDespesa"
                Dim _despesa As New Despesa
                lblIdDados.Text = gdTipoDespesas.Rows(index).Cells(1).Text
                txtTipoDespesa.Text = Server.HtmlDecode(gdTipoDespesas.Rows(index).Cells(2).Text)
                ckbAtivo.Checked = gdTipoDespesas.Rows(index).Cells(3).Text
                btnSalvar.Text = "Atualizar"
                gdTipoDespesas.DataSource = _MapeadorFinanceiro.getTipoDespesa()
                gdTipoDespesas.DataBind()
        End Select
    End Sub

    Private Sub gdTipoDespesas_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gdTipoDespesas.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.FindControl("btnEditarTipoDespesa"), ImageButton).CommandArgument = e.Row.RowIndex.ToString
            CType(e.Row.FindControl("btnExcluirTipoDespesa"), ImageButton).CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

    Protected Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        Try
            Dim _MapeadorFinanceiro As New MapeadorFinanceiro
            Dim _tpDespesa As New TipoDespesa
            _tpDespesa.Descricao = txtTipoDespesa.Text
            _tpDespesa.Id = lblIdDados.Text
            _tpDespesa.Ativo = ckbAtivo.Checked
            If lblIdDados.Text = String.Empty Then
                _MapeadorFinanceiro.SalvarTipoDespesa(_tpDespesa)
                log.Funcao = "CadastrarTipoDespesa"
                log.Acao = "Inserir"
                log.Valor = _MapeadorFinanceiro.getTipoDespesaUltimoInserido() + "-" + _tpDespesa.Descricao
                log.ModificadoPor = Session("usuario")
                log.DataAlteracao = DateTime.Now
                _MapeadorDeLog.GravarLogs(log)
                Utilitarios.EnviarMensagem("Cadastro realizado com sucesso.", Me)
            Else
                _MapeadorFinanceiro.AtualizarTipoDespesa(_tpDespesa)
                log.Funcao = "CadastrarTipoDespesa"
                log.Acao = "Atualizar"
                log.Valor = _tpDespesa.Id + "-" + _tpDespesa.Descricao
                log.ModificadoPor = Session("usuario")
                log.DataAlteracao = DateTime.Now
                _MapeadorDeLog.GravarLogs(log)
                Utilitarios.EnviarMensagem("Cadastro atualizado com sucesso.", Me)
            End If

            gdTipoDespesas.DataSource = _MapeadorFinanceiro.getTipoDespesa()
            gdTipoDespesas.DataBind()
            txtTipoDespesa.Text = String.Empty
            lblIdDados.Text = String.Empty
            ckbAtivo.Checked = False
            btnSalvar.Text = "Salvar"
        Catch ex As Exception
            Utilitarios.EnviarMensagem("Erro " & ex.Message, Me)
        End Try
    End Sub

    Private Sub btnNovoRegistro_Click(sender As Object, e As EventArgs) Handles btnNovoRegistro.Click
        btnSalvar.Text = "Salvar"
        txtTipoDespesa.Text = String.Empty
        lblIdDados.Text = String.Empty
        ckbAtivo.Checked = False
    End Sub
End Class