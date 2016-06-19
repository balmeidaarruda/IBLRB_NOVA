Public Class pgCadastrarTipoOferta
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
            Log.Acao = "Acesso"
            log.Funcao = "pgCadastrarTipoOferta"
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
            gdTipoOfertas.DataSource = _MapeadorFinanceiro.getTipoOferta()
            gdTipoOfertas.DataBind()
        End If
    End Sub

    Private Sub gdTipoOfertas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdTipoOfertas.RowCommand
        Dim index As Integer = CInt(e.CommandArgument)
        Dim _MapeadorFinanceiro As New MapeadorFinanceiro
        Select Case e.CommandName
            Case "ExcluirTipoOferta"
                Try
                    _MapeadorFinanceiro.ExcluirTipoOferta(gdTipoOfertas.Rows(index).Cells(1).Text)
                    gdTipoOfertas.DataSource = _MapeadorFinanceiro.getTipoOferta()
                    log.Funcao = "CadastrarTipoOferta"
                    log.Acao = "Excluir"

                    log.Valor = gdTipoOfertas.Rows(index).Cells(1).Text + "-" + gdTipoOfertas.Rows(index).Cells(2).Text
                    log.ModificadoPor = Session("usuario")
                    log.DataAlteracao = DateTime.Now
                    _MapeadorDeLog.GravarLogs(log)
                    gdTipoOfertas.DataBind()
                    Utilitarios.EnviarMensagem("Registro inativado com sucesso", Me)
                Catch ex As Exception
                    If ex.Message.ToString.Contains("REFERENCE") Then
                        Utilitarios.EnviarMensagem("Erro: Este tipo de despesa está relacionada com uma despesa cadastrada " & vbCrLf & "Detalhes" & ex.Message, Me)
                    Else
                        Utilitarios.EnviarMensagem("Erro " & ex.Message, Me)
                    End If

                End Try

            Case "EditarTipoOferta"
                Dim _despesa As New Despesa
                lblIdDados.Text = gdTipoOfertas.Rows(index).Cells(1).Text
                txtTipoOferta.Text = Server.HtmlDecode(gdTipoOfertas.Rows(index).Cells(2).Text)
                ckbAtivo.Checked = gdTipoOfertas.Rows(index).Cells(3).Text
                btnSalvar.Text = "Atualizar"
                gdTipoOfertas.DataSource = _MapeadorFinanceiro.getTipoOferta()
                gdTipoOfertas.DataBind()
        End Select
    End Sub

    Private Sub gdTipoOfertas_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gdTipoOfertas.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.FindControl("btnEditarTipoOferta"), ImageButton).CommandArgument = e.Row.RowIndex.ToString
            CType(e.Row.FindControl("btnExcluirTipoOferta"), ImageButton).CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        Try
            Dim _MapeadorFinanceiro As New MapeadorFinanceiro
            Dim _tpOferta As New TipoOferta
            _tpOferta.Descricao = txtTipoOferta.Text
            _tpOferta.ID = lblIdDados.Text
            _tpOferta.Ativo = ckbAtivo.Checked
            If lblIdDados.Text = String.Empty Then
                _MapeadorFinanceiro.SalvarTipoOferta(_tpOferta)
                log.Funcao = "CadastrarTipoDespesa"
                log.Acao = "Inserir"
                log.Valor = _MapeadorFinanceiro.getTipoOfertaUltimoInserido() + "-" + _tpOferta.Descricao
                log.ModificadoPor = Session("usuario")
                log.DataAlteracao = DateTime.Now
                _MapeadorDeLog.GravarLogs(log)
                Utilitarios.EnviarMensagem("Cadastro realizado com sucesso.", Me)
            Else
                _MapeadorFinanceiro.AtualizarTipoOferta(_tpOferta)
                log.Funcao = "CadastrarTipoOferta"
                log.Acao = "Atualizar"
                log.Valor = _tpOferta.id + "-" + _tpOferta.Descricao
                log.ModificadoPor = Session("usuario")
                log.DataAlteracao = DateTime.Now
                _MapeadorDeLog.GravarLogs(log)
                Utilitarios.EnviarMensagem("Cadastro atualizado com sucesso.", Me)
            End If

            gdTipoOfertas.DataSource = _MapeadorFinanceiro.getTipoOferta()
            gdTipoOfertas.DataBind()
            txtTipoOferta.Text = String.Empty
            lblIdDados.Text = String.Empty
            ckbAtivo.Checked = False
            btnSalvar.Text = "Salvar"
        Catch ex As Exception
            Utilitarios.EnviarMensagem("Erro " & ex.Message, Me)
        End Try
    End Sub

    Private Sub btnNovoRegistro_Click(sender As Object, e As EventArgs) Handles btnNovoRegistro.Click
        btnSalvar.Text = "Salvar"
        txtTipoOferta.Text = String.Empty
        lblIdDados.Text = String.Empty
        ckbAtivo.Checked = False
    End Sub


End Class