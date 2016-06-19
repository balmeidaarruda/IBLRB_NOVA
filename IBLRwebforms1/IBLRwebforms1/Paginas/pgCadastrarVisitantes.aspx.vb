Imports System.IO
Public Class pgCadastrarVisitantes
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

            Log.Acao = "Acesso"
            log.Funcao = "pgCadastrarVisitantes"
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
            txtDataVisita.Attributes.Add("onKeyPress", "return MascaraData(this,event);")
            btnNovoRegistro.Visible = False
            pnConsulta.Visible = False
            Dim _MapeadorDeCongregacoes As New MapeadorDeCongregacoes
            ddCongregacao.DataSource = _MapeadorDeCongregacoes.getListaCongregacoes(0, "")
            ddCongregacao.DataBind()
            ddCongregacao.Items.Insert(0, "")

            ddCongregacaoConsulta.DataSource = _MapeadorDeCongregacoes.getListaCongregacoes(0, "")
            ddCongregacaoConsulta.DataBind()
            ddCongregacaoConsulta.Items.Insert(0, "")

        End If
    End Sub

    Protected Sub gdContatos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdContatos.RowCommand
        Dim index As Integer = CInt(e.CommandArgument)
        Select Case e.CommandName
            Case "ExcluirContato"
                Dim LisCont As List(Of Contato) = CType(Session("Cont"), Global.System.Collections.Generic.List(Of Contato))
                If LisCont.Count > 0 Then
                    LisCont.RemoveAt(index)
                    gdContatos.DataSource = LisCont
                    gdContatos.DataBind()
                End If
            Case "EditarContato"
                Dim LisCont As List(Of Contato) = CType(Session("Cont"), Global.System.Collections.Generic.List(Of Contato))
                If LisCont.Count > 0 Then
                    txtContato.Text = LisCont(index).Descricao
                    ddTipoContato.SelectedValue = LisCont(index).TipoContato.Id
                    LisCont.RemoveAt(index)
                    gdContatos.DataSource = LisCont
                    gdContatos.DataBind()
                End If

        End Select
    End Sub

    Protected Sub gdContatos_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gdContatos.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.FindControl("btnEditarContato"), ImageButton).CommandArgument = e.Row.RowIndex.ToString
            CType(e.Row.FindControl("btnExcluirContato"), ImageButton).CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

    Protected Sub ddTipoContato_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddTipoContato.SelectedIndexChanged
        If ddTipoContato.SelectedValue = 1 Or ddTipoContato.SelectedValue = 2 Then
            txtContato.Attributes.Add("onKeyPress", "return MascaraTelefone(this);")
            txtContato.ToolTip = "Formato (22)2222-2222"
            txtContato.MaxLength = "14"
        Else
            txtContato.Attributes.Add("onKeyPress", "")
            txtContato.ToolTip = ""
            txtContato.MaxLength = "500"
        End If
    End Sub

    Protected Sub btnAddContato_Click(sender As Object, e As ImageClickEventArgs) Handles btnAddContato.Click

        If IsNothing(ListaContatos) Then
            ListaContatos = New List(Of Contato)
        End If
        Dim c As Contato = New Contato
        If txtContato.Text.Equals(String.Empty) Or ddTipoContato.SelectedValue = "" Then
            Exit Sub
        Else
            c.Descricao = txtContato.Text
        End If
        Dim tipoContato As TipoContato = New TipoContato
        tipoContato.Id = ddTipoContato.SelectedValue
        tipoContato.Descricao = ddTipoContato.Text
        c.TipoContato = tipoContato
        c.id = ddTipoContato.SelectedValue
        c.DescricaoTipoContato = ddTipoContato.Items(ddTipoContato.SelectedIndex).Text
        ListaContatos.Add(c)
        ListaContatos = ListaContatos 'Apenas para dar Bind no grid
        LimparCamposContatos()
    End Sub

    Public Property ListaContatos As System.Collections.Generic.List(Of Contato)
        Get
            Return CType(Session("Cont"), List(Of Contato))
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of Contato))
            Session("Cont") = value
            gdContatos.DataSource = value
            gdContatos.DataBind()
        End Set
    End Property

    Public Sub LimparCamposContatos()
        txtContato.Text = String.Empty
        ddTipoContato.SelectedIndex = -1
    End Sub

    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        pnCadastro.Visible = False
        pnConsulta.Visible = True
        btnNovoRegistro.Visible = True
        btnSalvar.Visible = False
        btnConsultar.Visible = False
        txtDataInicio.Text = String.Empty
        txtDataFim.Text = String.Empty
        btnExportarExcel.Visible = False
        gdVisitantes.DataSource = String.Empty
        gdVisitantes.DataBind()
    End Sub

    Protected Sub btnNovoRegistro_Click(sender As Object, e As EventArgs) Handles btnNovoRegistro.Click
        pnCadastro.Visible = True
        pnConsulta.Visible = False
        btnNovoRegistro.Visible = False
        btnSalvar.Visible = True
        btnConsultar.Visible = True
        btnSalvar.Text = "Salvar"
        lblIdDados.Text = String.Empty
        limparCamposCadastro(Me)
    End Sub

    Protected Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        Try
            If Page.IsValid Then
                Dim _MapeadorDeVisitantes As New MapeadorDeVisitantes
                Dim _Visitante As New Visitante

                _Visitante.ID = lblIdDados.Text
                _Visitante.DATA_VISITA = txtDataVisita.Text
                _Visitante.NOME = txtNome.Text
                _Visitante.QUEM_CONVIDOU = txtConvidadoPor.Text
                _Visitante.ENDERECO = txtEndereco.Text
                Dim _congregacao As New Congregacao
                _congregacao.Id = ddCongregacao.SelectedValue
                _Visitante.CONGREGACAO = _congregacao
                If gdContatos.Rows.Count > 0 Then
                    _Visitante.listaContatos = New List(Of Contato)
                    For x As Integer = 0 To gdContatos.Rows.Count - 1
                        Dim _contato As New Contato
                        _contato.DescricaoTipoContato = gdContatos.Rows.Item(x).Cells(1).Text
                        _contato.Descricao = gdContatos.Rows.Item(x).Cells(2).Text.Replace("(", "").Replace(")", "").Replace("-", "")
                        _Visitante.listaContatos.Add(_contato)
                    Next
                End If

                If lblIdDados.Text = String.Empty Then
                    _MapeadorDeVisitantes.Salvar(_Visitante)
                    log.Funcao = "CadastrarVisitantes"
                    log.Acao = "Inserir"
                    log.Valor = _MapeadorDeVisitantes.getVisitantesUltimoInserido() + "-" + _Visitante.NOME
                    log.ModificadoPor = Session("usuario")
                    log.DataAlteracao = DateTime.Now
                    _MapeadorDeLog.GravarLogs(log)
                    Utilitarios.EnviarMensagem("Cadastro realizado com sucesso", Me)
                    limparCamposCadastro(Me)
                Else
                    _MapeadorDeVisitantes.Atualizar(_Visitante)
                    log.Funcao = "CadastrarVisitantes"
                    log.Acao = "Atualizar"
                    log.Valor = _Visitante.ID + "-" + _Visitante.NOME
                    log.ModificadoPor = Session("usuario")
                    log.DataAlteracao = DateTime.Now
                    _MapeadorDeLog.GravarLogs(log)
                    Utilitarios.EnviarMensagem("Cadastro Atualizado com sucesso", Me)
                End If

            End If
        Catch ex As Exception
            Utilitarios.EnviarMensagem("Erro " & ex.Message, Me)
        End Try
    End Sub

    Protected Sub btnConsultarVisitantes_Click(sender As Object, e As ImageClickEventArgs) Handles btnConsultarVisitantes.Click
        Try
            Dim _MapeadorDeVisitantes As New MapeadorDeVisitantes
            gdVisitantes.DataSource = _MapeadorDeVisitantes.getListaVisitantes(ddCongregacaoConsulta.SelectedValue, txtDataInicio.Text, txtDataFim.Text)
            gdVisitantes.DataBind()
            btnExportarExcel.Visible = True
        Catch ex As Exception
            Utilitarios.EnviarMensagem("Erro " & ex.Message, Me)
        End Try
    End Sub

    Private Sub gdVisitantes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdVisitantes.RowCommand
        Dim index As Integer = CInt(e.CommandArgument)

        Select Case e.CommandName
            Case "EditarVisitantes"
                Try
                    Dim _MapeadorDeVisitantes As New MapeadorDeVisitantes
                    Dim _Visitante As Visitante = _MapeadorDeVisitantes.getVisitanteID(gdVisitantes.Rows(index).Cells(1).Text)
                    lblIdDados.Text = _Visitante.ID
                    txtDataVisita.Text = _Visitante.DATA_VISITA
                    txtNome.Text = _Visitante.NOME
                    txtEndereco.Text = _Visitante.ENDERECO
                    txtConvidadoPor.Text = _Visitante.QUEM_CONVIDOU
                    ddCongregacao.SelectedValue = _Visitante.CONGREGACAO.Id
                    gdContatos.DataSource = _Visitante.listaContatos
                    gdContatos.DataBind()
                    pnCadastro.Visible = True
                    pnConsulta.Visible = False
                    btnConsultar.Visible = True
                    btnSalvar.Visible = True
                    btnSalvar.Text = "Atualizar"
                Catch ex As Exception
                    Utilitarios.EnviarMensagem("Erro " & ex.Message, Me)
                End Try
        End Select

    End Sub


    Private Sub gdVisitantes_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gdVisitantes.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.FindControl("btnEditarVisitantes"), ImageButton).CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

    Public Sub limparCamposCadastro(ByVal controlP As Control)
        Dim ctl As Control
        For Each ctl In controlP.Controls
            If TypeOf ctl Is TextBox Then
                DirectCast(ctl, TextBox).Text = String.Empty
            ElseIf TypeOf ctl Is DropDownList Then
                DirectCast(ctl, DropDownList).SelectedIndex = -1
            ElseIf TypeOf ctl Is GridView Then
                DirectCast(ctl, GridView).DataSource = String.Empty
                DirectCast(ctl, GridView).DataBind()
            ElseIf ctl.Controls.Count > 0 Then
                limparCamposCadastro(ctl)
            End If
        Next

    End Sub

    Protected Sub btnExportarExcel_Click(sender As Object, e As ImageClickEventArgs) Handles btnExportarExcel.Click

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Visitantes " & txtDataInicio.Text & " à " & txtDataFim.Text & ".xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Dim sWriter As New StringWriter()
        Dim hWriter As New HtmlTextWriter(sWriter)
        gdVisitantes.RenderControl(hWriter)
        Response.Output.Write(sWriter.ToString())
        Response.Flush()
        Response.End()
    End Sub
    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Verifies that the control is rendered
    End Sub
End Class