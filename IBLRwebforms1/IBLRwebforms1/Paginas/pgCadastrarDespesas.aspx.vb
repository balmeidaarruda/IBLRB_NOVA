Imports System.IO
Public Class pgCadastrarDespesas
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

            log.Acao = "Acesso"
            log.Funcao = "pgCadastrarDespesas"
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
            ddTipoDespesa.DataSource = _MapeadorFinanceiro.getTipoDespesa()
            ddTipoDespesa.DataBind()
            ddTipoDespesa.Items.Insert(0, "")

            Dim _MapeadorDeCongregacoes As New MapeadorDeCongregacoes
            ddCongregacao.DataSource = _MapeadorDeCongregacoes.getListaCongregacoes(0, "")
            ddCongregacao.DataBind()
            ddCongregacao.Items.Insert(0, "")

        End If

    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        If gdDespesas.Rows.Count = 0 Then
            Utilitarios.EnviarMensagem("Não existe registros lançados para salvar", Me)
        Else
            Try
                Dim _listaDespesas As New List(Of Despesa)

                If gdDespesas.Rows.Count > 0 Then

                    For x As Integer = 0 To gdDespesas.Rows.Count - 1
                        Dim _tipoDespesa As New TipoDespesa
                        Dim _congregacao As New Congregacao
                        _tipoDespesa.Id = gdDespesas.Rows.Item(x).Cells(1).Text()
                        _congregacao.Id = gdDespesas.Rows.Item(x).Cells(3).Text()
                        Dim _despesa As New Despesa
                        _despesa.TipoDespesa = _tipoDespesa
                        _despesa.Congregacao = _congregacao
                        _despesa.ValorDespesa = CDec(gdDespesas.Rows.Item(x).Cells(6).Text())
                        _despesa.DataLancamento = gdDespesas.Rows.Item(x).Cells(5).Text()
                        _listaDespesas.Add(_despesa)
                    Next

                End If
                Dim _MapeadorFinanceiro As New MapeadorFinanceiro
                Dim s As String = ""
                If lblIdDados.Text = String.Empty Then
                    _MapeadorFinanceiro.SalvarDespesas(_listaDespesas)
                    log.Funcao = "CadastrarDespesas"
                    log.Acao = "Inserir"

                    For i As Integer = 0 To _listaDespesas.Count - 1
                        s += " " + "IdDespesa:" + _listaDespesas(i).TipoDespesa.Id + "-" + CStr(_listaDespesas(i).ValorDespesa) + "-" + _listaDespesas(i).DataLancamento
                    Next
                    log.Valor = s
                    log.ModificadoPor = Session("usuario")
                    log.DataAlteracao = DateTime.Now
                    _MapeadorDeLog.GravarLogs(log)

                    Utilitarios.EnviarMensagem("Cadastro realizado com sucesso", Me)
                Else
                    _listaDespesas(0).Id = lblIdDados.Text
                    _MapeadorFinanceiro.AtualizarDespesa(_listaDespesas(0))

                    log.Funcao = "CadastrarDespesas"
                    log.Acao = "Atualizar"
                    s += " " + "IdDespesa:" + _listaDespesas(0).TipoDespesa.Id + "-" + CStr(_listaDespesas(0).ValorDespesa) + "-" + _listaDespesas(0).DataLancamento
                    log.Valor = s
                    log.ModificadoPor = Session("usuario")
                    log.DataAlteracao = DateTime.Now
                    _MapeadorDeLog.GravarLogs(log)
                    Utilitarios.EnviarMensagem("Cadastro atualizado com sucesso", Me)
                    lblIdDados.Text = String.Empty
                    btnNovoRegistro.Visible = False
                    btnSalvar.Text = "Salvar"
                End If
                gdDespesas.DataSource = Nothing
                gdDespesas.DataBind()
                ListaDespesas = Nothing

            Catch ex As Exception
                Utilitarios.EnviarMensagem("Erro " & ex.Message, Me)
            End Try
        End If
    End Sub

    Protected Sub btnAddLancamento_Click(sender As Object, e As ImageClickEventArgs) Handles btnAddLancamento.Click
        If Page.IsValid Then
            If IsNothing(ListaDespesas) Then
                ListaDespesas = New List(Of Despesa)
            End If
            Dim _tipoDespesa As New TipoDespesa
            Dim _congregacao As New Congregacao
            Dim d As Despesa = New Despesa

            _tipoDespesa.Id = ddTipoDespesa.SelectedValue
            _tipoDespesa.Descricao = ddTipoDespesa.SelectedItem.Text
            _congregacao.Id = ddCongregacao.SelectedValue
            _congregacao.Nome = ddCongregacao.SelectedItem.Text
            d.Congregacao = _congregacao
            d.TipoDespesa = _tipoDespesa
            d.DataLancamento = txtDataLancamento.Text
            d.ValorDespesa = CDec(txtValorDespesa.Text)

            ListaDespesas.Add(d)
            ListaDespesas = ListaDespesas 'Apenas para dar Bind no grid
            LimparCamposDespesas()
        End If

    End Sub

    Public Sub LimparCamposDespesas()
        txtDataLancamento.Text = String.Empty
        txtValorDespesa.Text = String.Empty
        ddCongregacao.SelectedIndex = -1
        ddTipoDespesa.SelectedIndex = -1
    End Sub

    Private Sub gdDespesas_DataBound(sender As Object, e As EventArgs) Handles gdDespesas.DataBound
        If gdDespesas.Rows.Count > 0 Then
            Dim total As Decimal = 0
            For Each row As GridViewRow In gdDespesas.Rows
                Dim valorDizimo As Decimal = CDec(row.Cells(6).Text)
                total += valorDizimo
                row.Cells(6).Text = "R$ " & row.Cells(6).Text
            Next

            Dim footer As GridViewRow = gdDespesas.FooterRow

            footer.Cells(0).ColumnSpan = 3
            ''footer.Cells(0).HorizontalAlign = HorizontalAlign.Center
            footer.Cells.RemoveAt(1)
            'footer.Cells.RemoveAt(6)
            footer.Cells(3).Text = "Total"
            footer.Cells(4).Text = "R$ " & total
        End If
    End Sub

    Public Property ListaDespesas As System.Collections.Generic.List(Of Despesa)
        Get
            Return CType(Session("despesa"), List(Of Despesa))
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of Despesa))
            Session("despesa") = value
            gdDespesas.DataSource = value
            gdDespesas.DataBind()
        End Set
    End Property

    Protected Sub btnConsultarDespesas_Click(sender As Object, e As ImageClickEventArgs) Handles btnConsultarDespesas.Click
        Try
            Dim _MapeadorFinanceiro As New MapeadorFinanceiro
            gdDespesaConsulta.DataSource = _MapeadorFinanceiro.getListaDespesas(txtDataInicio.Text, txtDataFim.Text, IIf(ddCongregacaoConsulta.SelectedItem.Text = "Todas", "", ddCongregacaoConsulta.SelectedValue))
            gdDespesaConsulta.DataBind()
            btnExportarExcel.Visible = True
        Catch ex As Exception
            Utilitarios.EnviarMensagem("Erro " & ex.Message, Me)
        End Try
    End Sub

    Private Sub gdDespesaConsulta_DataBound(sender As Object, e As EventArgs) Handles gdDespesaConsulta.DataBound
        If gdDespesaConsulta.Rows.Count > 0 Then
            Dim total As Decimal = 0
            For Each row As GridViewRow In gdDespesaConsulta.Rows
                Dim valorDizimo As Decimal = CDec(row.Cells(7).Text)
                total += valorDizimo
                row.Cells(7).Text = "R$ " & row.Cells(7).Text
            Next
            Dim footer As GridViewRow = gdDespesaConsulta.FooterRow
            footer.Cells(0).ColumnSpan = 3
            'footer.Cells(0).HorizontalAlign = HorizontalAlign.Center
            footer.Cells.RemoveAt(1)
            'footer.Cells.RemoveAt(4)
            footer.Cells(4).Text = "Total"
            footer.Cells(5).Text = "R$ " & total
        Else
            btnExportarExcel.Visible = False
            'Utilitarios.EnviarMensagem("Não existe registros para o período informado", Me)
        End If
    End Sub

    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        pnCadastro.Visible = False
        pnConsulta.Visible = True
        btnSalvar.Visible = False
        btnConsultar.Visible = False
        btnNovoRegistro.Visible = True
        'gdDespesaConsulta.DataSource = Nothing
        'gdDespesaConsulta.DataBind()
        btnExportarExcel.Visible = False
        lblIdDados.Text = String.Empty
        txtDataInicio.Text = String.Empty
        txtDataFim.Text = String.Empty
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
        btnSalvar.Text = "Salvar"
    End Sub

    Protected Sub btnExportarExcel_Click(sender As Object, e As ImageClickEventArgs) Handles btnExportarExcel.Click
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Despesas " & txtDataInicio.Text & " à " & txtDataFim.Text & ".xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Dim sWriter As New StringWriter()
        Dim hWriter As New HtmlTextWriter(sWriter)
        gdDespesaConsulta.RenderControl(hWriter)
        Response.Output.Write(sWriter.ToString())
        Response.Flush()
        Response.End()
    End Sub
    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Verifies that the control is rendered
    End Sub

    Private Sub gdDespesaConsulta_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdDespesaConsulta.RowCommand
        Dim index As Integer = CInt(e.CommandArgument)
        Try
            Select Case e.CommandName
                Case "EditarDespesas"
                    Dim _MapeadorFinaceiro As New MapeadorFinanceiro
                    Dim _Despesa As New Despesa
                    _Despesa = _MapeadorFinaceiro.getDespesaID(gdDespesaConsulta.Rows(index).Cells(1).Text)
                    lblIdDados.Text = _Despesa.Id
                    txtDataLancamento.Text = _Despesa.DataLancamento
                    txtValorDespesa.Text = _Despesa.ValorDespesa
                    ddTipoDespesa.SelectedValue = _Despesa.TipoDespesa.Id
                    ddCongregacao.SelectedValue = _Despesa.Congregacao.Id
                    pnCadastro.Visible = True
                    pnConsulta.Visible = False
                    txtDataFim.Text = String.Empty
                    txtDataInicio.Text = String.Empty
                    btnSalvar.Text = "Atualizar"
                    btnSalvar.Visible = True
                    btnConsultar.Visible = True
                    gdDespesaConsulta.DataSource = String.Empty
                    gdDespesaConsulta.DataBind()
            End Select

        Catch ex As Exception
            Utilitarios.EnviarMensagem("Erro " & ex.Message, Me)
        End Try
    End Sub


    Private Sub gdDespesaConsulta_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gdDespesaConsulta.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.FindControl("btnEditarDespesas"), ImageButton).CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub
End Class