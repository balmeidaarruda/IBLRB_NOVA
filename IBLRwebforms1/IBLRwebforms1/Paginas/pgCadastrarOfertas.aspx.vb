Imports System.IO
Public Class pgCadastrarOfertas
    Inherits System.Web.UI.Page
    Private _MapeadorDeLog As New MapeadorDeLog
    Private log As New Log

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        If Session("usuarioLogado") = "sim" Then
            'Dim menu As Menu = CType(Master.FindControl("Menu1"), Menu)
            'menu.Visible = True
            'selected_tab.Value = Request.Form(selected_tab.UniqueID)
            Dim image As System.Web.UI.WebControls.Image = CType(Master.FindControl("statusLogin"), System.Web.UI.WebControls.Image)
            image.ImageUrl = "../Images/green-button_24x24.png"
            image.ImageAlign = ImageAlign.Right
            image.ToolTip = "Usuário logado"
            Log.Acao = "Acesso"
            log.Funcao = "pgCadastrarOfertas"
            Log.Valor = "Logar"
            Log.ModificadoPor = Session("usuario")
            Log.DataAlteracao = DateAndTime.Now
            Try
                _MapeadorDeLog.GravarLogs(Log)
            Catch ex As Exception
                Utilitarios.EnviarMensagem(ex.Message, Me)
            End Try
            'pnConsulta.Visible = False
            'Me.ClientScript.RegisterClientScriptInclude("scripts", "Scripts/JavaScript.js")
        Else
            Response.Redirect("~/login.aspx")
            Exit Sub
        End If
        If Not IsPostBack Then
            Me.ClientScript.RegisterClientScriptInclude("scripts", "Scripts/JavaScript.js")

            btnNovoRegistro.Visible = False
            pnConsulta.Visible = False
            pnCadastro.Visible = True

            Dim _MapeadorFinanceiro As New MapeadorFinanceiro
            ddTipoOferta.DataSource = _MapeadorFinanceiro.getListaTipoOfertas
            ddTipoOferta.DataBind()
            ddTipoOferta.Items.Insert(0, "")

            Dim _MapeadorDeCongregacoes As New MapeadorDeCongregacoes
            ddCongregacao.DataSource = _MapeadorDeCongregacoes.getListaCongregacoes(0, "")
            ddCongregacao.DataBind()
            ddCongregacao.Items.Insert(0, "")

        End If
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        If gdOfertas.Rows.Count = 0 Then
            Utilitarios.EnviarMensagem("Não existe registros lançados para salvar", Me)
        Else
            Try
                If gdOfertas.Rows.Count > 0 Then
                    Dim _MapeadorFinaceiro As New MapeadorFinanceiro
                    Dim _listaOfertas As New List(Of Oferta)
                    For x As Integer = 0 To gdOfertas.Rows.Count - 1
                        Dim _Oferta As New Oferta
                        Dim _TipoOferta As New TipoOferta
                        '_Oferta.ID = gdOfertas.Rows.Item(x).Cells(0).Text()
                        _TipoOferta.ID = gdOfertas.Rows.Item(x).Cells(3).Text()
                        _Oferta.TipoOferta = _TipoOferta
                        Dim _congregacao As New Congregacao
                        _congregacao.Id = gdOfertas.Rows.Item(x).Cells(1).Text()
                        _Oferta.Congregacao = _congregacao
                        _Oferta.ValorOferta = CDec(gdOfertas.Rows.Item(x).Cells(5).Text())
                        _Oferta.DataLancamento = gdOfertas.Rows.Item(x).Cells(6).Text()
                        _listaOfertas.Add(_Oferta)
                    Next
                    Dim s As String = ""
                    If lblIdDados.Text = String.Empty Then
                        _MapeadorFinaceiro.SalvarOferta(_listaOfertas)
                        log.Funcao = "CadastrarOfertas"
                        log.Acao = "Inserir"

                        For i As Integer = 0 To _listaOfertas.Count - 1
                            s += " " + "IdOferta:" + _listaOfertas(i).TipoOferta.ID + "-" + CStr(_listaOfertas(i).ValorOferta) + "-" + _listaOfertas(i).DataLancamento
                        Next
                        log.Valor = s
                        log.ModificadoPor = Session("usuario")
                        log.DataAlteracao = DateTime.Now
                        _MapeadorDeLog.GravarLogs(log)
                        Utilitarios.EnviarMensagem("Cadastro realizado com sucesso", Me)
                    Else
                        _listaOfertas(0).ID = lblIdDados.Text
                        _MapeadorFinaceiro.AtualizarOferta(_listaOfertas(0))

                        log.Funcao = "CadastrarOfertas"
                        log.Acao = "Atualizar"
                        s += " " + "idOferta:" + _listaOfertas(0).TipoOferta.ID + "-" + CStr(_listaOfertas(0).ValorOferta) + "-" + _listaOfertas(0).DataLancamento
                        log.Valor = s
                        log.ModificadoPor = Session("usuario")
                        log.DataAlteracao = DateTime.Now
                        _MapeadorDeLog.GravarLogs(log)
                        Utilitarios.EnviarMensagem("Cadastro atualizado com sucesso", Me)
                        lblIdDados.Text = String.Empty
                        btnNovoRegistro.Visible = False
                        btnSalvar.Text = "Salvar"

                    End If

                    gdOfertas.DataSource = Nothing
                    gdOfertas.DataBind()
                    ListaOfertas = Nothing

                End If
            Catch ex As Exception
                Utilitarios.EnviarMensagem("Erro " & ex.Message, Me)
            End Try
        End If
    End Sub

    Private Sub btnAddLancamento_Click(sender As Object, e As ImageClickEventArgs) Handles btnAddLancamento.Click
        If Page.IsValid Then
            If IsNothing(ListaOfertas) Then
                ListaOfertas = New List(Of Oferta)
            End If
            Dim _tipoOferta As New TipoOferta
            Dim o As Oferta = New Oferta

            o.DataLancamento = txtDataLancamento.Text
            _tipoOferta.ID = ddTipoOferta.SelectedValue
            _tipoOferta.Descricao = ddTipoOferta.SelectedItem.Text
            o.TipoOferta = _tipoOferta
            o.ValorOferta = CDec(txtValorOferta.Text)
            Dim _congregacao As New Congregacao
            _congregacao.Id = ddCongregacao.SelectedValue
            _congregacao.Nome = ddCongregacao.SelectedItem.Text
            o.Congregacao = _congregacao
            ListaOfertas.Add(o)
            ListaOfertas = ListaOfertas 'Apenas para dar Bind no grid
            LimparCamposOfertas()
        End If
    End Sub
    Public Property ListaOfertas As System.Collections.Generic.List(Of Oferta)
        Get
            Return CType(Session("ofertas"), List(Of Oferta))
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of Oferta))
            Session("ofertas") = value
            gdOfertas.DataSource = value
            gdOfertas.DataBind()
        End Set
    End Property

    Public Sub LimparCamposOfertas()
        txtDataLancamento.Text = String.Empty
        txtValorOferta.Text = String.Empty
        ddTipoOferta.SelectedIndex = -1
        ddCongregacao.SelectedIndex = -1
    End Sub

    Protected Sub btnConsultarOfertas_Click(sender As Object, e As ImageClickEventArgs) Handles btnConsultarOfertas.Click
        Try
            Dim _MapeadorFinaceiro As New MapeadorFinanceiro
            gdOfertasConsulta.DataSource = _MapeadorFinaceiro.getListaOfertas(txtDataInicio.Text, txtDataFim.Text, IIf(ddCongregacaoConsulta.SelectedItem.Text = "Todas", "", ddCongregacaoConsulta.SelectedValue))
            gdOfertasConsulta.DataBind()
            btnExportarExcel.Visible = True
        Catch ex As Exception
            Utilitarios.EnviarMensagem("Erro " & ex.Message, Me)
        End Try
    End Sub

    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        pnCadastro.Visible = False
        pnConsulta.Visible = True
        btnSalvar.Visible = False
        btnConsultar.Visible = False
        btnNovoRegistro.Visible = True
        txtDataInicio.Text = String.Empty
        txtDataFim.Text = String.Empty
        lblIdDados.Text = String.Empty
        btnExportarExcel.Visible = False
        'gdOfertasConsulta.DataSource = Nothing
        'gdOfertasConsulta.DataBind()
        ddCongregacaoConsulta.DataSource = Nothing
        ddCongregacaoConsulta.DataBind()
        Dim _MapeadorDeCongregacoes As New MapeadorDeCongregacoes
        ddCongregacaoConsulta.DataSource = _MapeadorDeCongregacoes.getListaCongregacoes(0, "")
        ddCongregacaoConsulta.DataBind()
        ddCongregacaoConsulta.Items.Insert(0, "")
        ddCongregacaoConsulta.Items.Insert(1, "Todas")
    End Sub

    Protected Sub btnNovoRegistro_Click(sender As Object, e As EventArgs) Handles btnNovoRegistro.Click
        pnCadastro.Visible = True
        pnConsulta.Visible = False
        btnSalvar.Visible = True
        btnConsultar.Visible = True
        btnNovoRegistro.Visible = False
        gdOfertas.DataSource = Nothing
        gdOfertas.DataBind()
        ListaOfertas = Nothing
        btnSalvar.Text = "Salvar"
    End Sub

    Private Sub gdOfertasConsulta_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdOfertasConsulta.RowCommand
        Dim index As Integer = CInt(e.CommandArgument)
        Try
            Select Case e.CommandName
                Case "EditarOfertas"
                    Dim _MapeadorFinaceiro As New MapeadorFinanceiro
                    Dim _Oferta As New Oferta
                    _Oferta = _MapeadorFinaceiro.getOfertasID(gdOfertasConsulta.Rows(index).Cells(1).Text)
                    lblIdDados.Text = _Oferta.ID
                    txtDataLancamento.Text = _Oferta.DataLancamento
                    txtValorOferta.Text = _Oferta.ValorOferta
                    ddTipoOferta.SelectedValue = _Oferta.TipoOferta.ID
                    ddCongregacao.SelectedValue = _Oferta.Congregacao.Id
                    pnCadastro.Visible = True
                    pnConsulta.Visible = False
                    txtDataFim.Text = String.Empty
                    txtDataInicio.Text = String.Empty
                    btnSalvar.Text = "Atualizar"
                    btnSalvar.Visible = True
                    btnConsultar.Visible = True
                    gdOfertasConsulta.DataSource = String.Empty
                    gdOfertasConsulta.DataBind()
            End Select

        Catch ex As Exception
            Utilitarios.EnviarMensagem("Erro " & ex.Message, Me)
        End Try
    End Sub

    Private Sub gdOfertasConsulta_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gdOfertasConsulta.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.FindControl("btnEditarOfertas"), ImageButton).CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

    Private Sub gdOfertasConsulta_DataBound(sender As Object, e As EventArgs) Handles gdOfertasConsulta.DataBound
        If gdOfertasConsulta.Rows.Count > 0 Then
            Dim total As Decimal = 0
            For Each row As GridViewRow In gdOfertasConsulta.Rows
                Dim valorDizimo As Decimal = CDec(row.Cells(6).Text)
                total += valorDizimo
                row.Cells(6).Text = "R$ " & row.Cells(6).Text
            Next
            Dim footer As GridViewRow = gdOfertasConsulta.FooterRow
            footer.Cells(0).ColumnSpan = 3
            'footer.Cells(0).HorizontalAlign = HorizontalAlign.Center
            footer.Cells.RemoveAt(1)
            'footer.Cells.RemoveAt(4)
            footer.Cells(2).Text = "Total"
            footer.Cells(3).Text = "R$ " & total
        End If
    End Sub

    Protected Sub btnExportarExcel_Click(sender As Object, e As ImageClickEventArgs) Handles btnExportarExcel.Click
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Ofertas " & txtDataInicio.Text & " à " & txtDataFim.Text & ".xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Dim sWriter As New StringWriter()
        Dim hWriter As New HtmlTextWriter(sWriter)
        gdOfertasConsulta.RenderControl(hWriter)
        Response.Output.Write(sWriter.ToString())
        Response.Flush()
        Response.End()
    End Sub
    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Verifies that the control is rendered
    End Sub

End Class