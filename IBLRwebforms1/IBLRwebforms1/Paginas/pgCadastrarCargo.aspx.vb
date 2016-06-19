Public Class pgCadastrarCargo
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
            log.Funcao = "pgCadastrarCargo"
            log.Valor = "Logar"
            Log.ModificadoPor = Session("usuario")
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
            btnNovoRegistro.Visible = False
            pnConsulta.Visible = False
            pnConsulta.Visible = False
        End If
    End Sub

    Private Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        pnCadastro.Visible = False
        pnConsulta.Visible = True
        btnNovoRegistro.Visible = True
        btnSalvar.Visible = False
        btnConsultar.Visible = False
        gdCargos.DataSource = String.Empty
        gdCargos.DataBind()
    End Sub

    Private Sub btnNovoRegistro_Click(sender As Object, e As EventArgs) Handles btnNovoRegistro.Click
        pnConsulta.Visible = False
        pnCadastro.Visible = True
        btnSalvar.Visible = True
        btnSalvar.Text = "Salvar"
        lblIdDados.Text = String.Empty
        btnConsultar.Visible = True
        btnNovoRegistro.Visible = False
        limparCampos()
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        If Page.IsValid Then

            Try
                Dim _MapeadorDeCargos As New MapeadorDeCargos
                Dim _MapeadorDeLog As New MapeadorDeLog
                Dim _Cargo As New Cargo

                _Cargo.Id = lblIdDados.Text
                _Cargo.Descricao = txtDescricao.Text
                _Cargo.Ativo = 1
                Dim log As New Log
                If lblIdDados.Text = String.Empty Then
                    _MapeadorDeCargos.Salvar(_Cargo)

                    limparCampos()
                    Utilitarios.EnviarMensagem("Cadastro realizado com sucesso!", Me)

                    log.Funcao = "CadastrarCargo"
                    log.Acao = "Inserir"
                    Dim lstCargos As New List(Of Cargo)
                    lstCargos = _MapeadorDeCargos.getListaCargos("")
                    Dim idMax = (From c As Cargo In lstCargos
                                Select c.Id).First
                    log.Valor = idMax + "-" + _Cargo.Descricao
                    log.ModificadoPor = Session("usuario")
                    log.DataAlteracao = DateTime.Now
                    _MapeadorDeLog.GravarLogs(log)

                Else
                    _MapeadorDeCargos.Atualizar(_Cargo)
                    Utilitarios.EnviarMensagem("Cargo atualizado com sucesso!", Me)
                    log.Funcao = "AtualizarCargo"
                    log.Acao = "Atualizar"
                    log.Valor = _Cargo.Id + "-" + _Cargo.Descricao
                    log.ModificadoPor = Session("usuario")
                    log.DataAlteracao = DateTime.Now
                    _MapeadorDeLog.GravarLogs(log)

                End If

            Catch ex As Exception
                Utilitarios.EnviarMensagem("Erro: " & ex.Message, Me)
            End Try
        End If
    End Sub

    Public Sub limparCampos()
        txtDescricao.Text = String.Empty
        txtCampoConsulta.Text = String.Empty
    End Sub

    Private Sub btnConsultarCargos_Click(sender As Object, e As ImageClickEventArgs) Handles btnConsultarCargos.Click
        Try
            Dim _MapeadorDeCargos As New MapeadorDeCargos
            gdCargos.DataSource = _MapeadorDeCargos.getListaCargos(txtCampoConsulta.Text)
            gdCargos.DataBind()
        Catch ex As Exception
            Utilitarios.EnviarMensagem("Erro: " & ex.Message, Me)
        End Try
    End Sub

    Private Sub gdCargos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdCargos.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Select e.CommandName
            Case "EditarCargos"
                limparCampos()
                Dim _MapeadorDeCargos As New MapeadorDeCargos

                Dim _cargo As New Cargo

                _cargo = _MapeadorDeCargos.getCargoID(gdCargos.Rows(index).Cells(1).Text)
                lblIdDados.Text = _cargo.Id
                txtDescricao.Text = _cargo.Descricao
                pnCadastro.Visible = True
                pnConsulta.Visible = False
                btnSalvar.Text = "Atualizar"
                btnSalvar.Visible = True
                btnNovoRegistro.Visible = True
                btnConsultar.Visible = True
        End Select
    End Sub

    Private Sub gdCargos_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gdCargos.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.FindControl("btnEditarCargos"), ImageButton).CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub
End Class