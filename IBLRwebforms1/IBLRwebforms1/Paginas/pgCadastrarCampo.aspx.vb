Public Class pgCadastrarCampo
    Inherits System.Web.UI.Page
    Private _MapeadorDeLog As New MapeadorDeLog
    Private log As New Log
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("usuarioLogado") = "sim" Then
            'Dim menu As Menu = CType(Master.FindControl("Menu1"), Menu)
            'menu.Visible = True
            Dim image As Image = CType(Master.FindControl("statusLogin"), Image)
            image.ImageUrl = "../Images/green-button_24x24.png"
            image.ImageAlign = ImageAlign.Right
            image.ToolTip = "Usuário logado"
            log.Acao = "Acesso"
            log.Funcao = "pgCadastrarCampo"
            Log.ModificadoPor = Session("usuario")
            Log.DataAlteracao = DateAndTime.Now

        Else
            Response.Redirect("~/login.aspx")
            Exit Sub
        End If
        If Not IsPostBack Then
            Me.ClientScript.RegisterClientScriptInclude("scripts", "Scripts/JavaScript.js")
            btnNovoRegistro.Visible = False
            pnConsulta.Visible = False
            pnConsulta.Visible = False
        End If
    End Sub

    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        pnCadastro.Visible = False
        pnConsulta.Visible = True
        btnNovoRegistro.Visible = True
        btnSalvar.Visible = False
        btnConsultar.Visible = False
        gdCampos.DataSource = String.Empty
        gdCampos.DataBind()
    End Sub

    Protected Sub btnNovoRegistro_Click(sender As Object, e As EventArgs) Handles btnNovoRegistro.Click
        pnConsulta.Visible = False
        pnCadastro.Visible = True
        btnSalvar.Visible = True
        btnSalvar.Text = "Salvar"
        lblIdDados.Text = String.Empty
        btnConsultar.Visible = True
        btnNovoRegistro.Visible = False
        limparCampos()
    End Sub

    Public Sub limparCampos()
        txtNome.Text = String.Empty
        txtPastorResponsavel.Text = String.Empty
        txtCampoConsulta.Text = String.Empty
    End Sub

    Protected Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        If Page.IsValid Then
            Try
                Dim _MapeadorDeCampos As New MapeadorDeCampos
                Dim _Campo As New Campo

                _Campo.Nome = txtNome.Text
                _Campo.PastorResponsavel = txtPastorResponsavel.Text
                _Campo.Ativo = True

                If lblIdDados.Text = String.Empty Then
                    _MapeadorDeCampos.Salvar(_Campo)
                    limparCampos()
                    log = New Log
                    log.Acao = "Inserir"
                    log.Funcao = "CadastrarCampos"
                    log.ModificadoPor = Session("usuario")
                    log.DataAlteracao = DateAndTime.Now
                    log.Valor = _MapeadorDeCampos.getCampoUltimoInsert() & "-" & "Nome: " & _Campo.Nome & "-Pastor:" & _Campo.PastorResponsavel
                    _MapeadorDeLog.GravarLogs(log)
                    Utilitarios.EnviarMensagem("Campo cadastrado com sucesso", Me)
                Else
                    _Campo.Id = lblIdDados.Text
                    _MapeadorDeCampos.Atualizar(_Campo)
                    log = New Log
                    log.Acao = "Atualizar"
                    log.Funcao = "CadastrarCampos"
                    log.Valor = _Campo.Id & "-" & "Nome: " & _Campo.Nome & "-Pastor:" & _Campo.PastorResponsavel
                    log.ModificadoPor = Session("usuario")
                    log.DataAlteracao = DateAndTime.Now
                    _MapeadorDeLog.GravarLogs(log)
                    Utilitarios.EnviarMensagem("Campo atualizado com sucesso", Me)
                End If
                
            Catch ex As Exception
                Utilitarios.EnviarMensagem("Erro: " & ex.Message, Me)
            End Try
        End If
        
    End Sub

    Protected Sub btnConsultarCampos_Click(sender As Object, e As ImageClickEventArgs) Handles btnConsultarCampos.Click
        Try
            Dim _MapeadorDeCongregacoes As New MapeadorDeCongregacoes
            gdCampos.DataSource = _MapeadorDeCongregacoes.getListaCampos(txtCampoConsulta.Text)
            gdCampos.DataBind()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Erro")
        End Try
    End Sub

    Private Sub gdCampos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdCampos.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Select Case e.CommandName
            Case "EditarCampos"
                limparCampos()
                Dim _MapeadorDeCampos As New MapeadorDeCampos

                Dim _campo As New Campo

                _campo = _MapeadorDeCampos.getCampoId(gdCampos.Rows(index).Cells(1).Text)
                lblIdDados.Text = _campo.Id
                txtNome.Text = _campo.Nome
                txtPastorResponsavel.Text = _campo.PastorResponsavel
                pnCadastro.Visible = True
                pnConsulta.Visible = False
                btnSalvar.Text = "Atualizar"
                btnSalvar.Visible = True
                btnNovoRegistro.Visible = True
                btnConsultar.Visible = True


        End Select
    End Sub

    Private Sub gdCampos_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gdCampos.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.FindControl("btnEditarCampos"), ImageButton).CommandArgument = e.Row.RowIndex.ToString
        End If

    End Sub
End Class