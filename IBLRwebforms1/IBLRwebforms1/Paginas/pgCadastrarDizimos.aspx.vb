Imports System.IO
Imports System.Drawing
Imports System.IO.Path

Public Class pgCadastrarDizimos
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
            log.Funcao = "pgCadastrarDizimo"
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

            Dim _MapeadorDeMembros As New MapeadorDeMembros
            ddMembro.DataSource = _MapeadorDeMembros.getListaMembros("")
            ddMembro.DataBind()
            ddMembro.Items.Insert(0, "")

        End If

    End Sub

    Protected Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        If gdDizimos.Rows.Count = 0 Then
            Utilitarios.EnviarMensagem("Não existe registros lançados para salvar", Me)
        Else
            Try
                Dim _listaDizimo As New List(Of Dizimo)

                If gdDizimos.Rows.Count > 0 Then

                    For x As Integer = 0 To gdDizimos.Rows.Count - 1
                        Dim _Dizimo As New Dizimo
                        Dim _Membro As New Membro
                        _Membro.ID = gdDizimos.Rows.Item(x).Cells(1).Text()
                        _Dizimo.Membro = _Membro
                        _Dizimo.VALOR_DIZIMO = CDec(gdDizimos.Rows.Item(x).Cells(3).Text())
                        _Dizimo.DATA_LANCAMENTO = gdDizimos.Rows.Item(x).Cells(4).Text()
                        _listaDizimo.Add(_Dizimo)

                    Next

                End If
                Dim _MapeadorFinanceiro As New MapeadorFinanceiro
                Dim s As String = ""
                If lblIdDados.Text = String.Empty Then
                    _MapeadorFinanceiro.SalvarDizimo(_listaDizimo)
                    log.Funcao = "CadastrarDizimos"
                    log.Acao = "Inserir"

                    For i As Integer = 0 To _listaDizimo.Count - 1
                        s += " " + _listaDizimo(i).Membro.NOME + "-" + CStr(_listaDizimo(i).VALOR_DIZIMO) + "-" + _listaDizimo(i).DATA_LANCAMENTO
                    Next
                    log.Valor = s
                    log.ModificadoPor = Session("usuario")
                    log.DataAlteracao = DateTime.Now
                    _MapeadorDeLog.GravarLogs(log)
                    Utilitarios.EnviarMensagem("Cadastro realizado com sucesso", Me)
                Else
                    _listaDizimo(0).ID = lblIdDados.Text
                    _MapeadorFinanceiro.AtualizarDizimo(_listaDizimo(0))
                    log.Funcao = "CadastrarDizimos"
                    log.Acao = "Atualizar"
                    s += " " + _listaDizimo(0).Membro.NOME + "-" + CStr(_listaDizimo(0).VALOR_DIZIMO) + "-" + _listaDizimo(0).DATA_LANCAMENTO
                    log.Valor = s
                    log.ModificadoPor = Session("usuario")
                    log.DataAlteracao = DateTime.Now
                    _MapeadorDeLog.GravarLogs(log)
                    Utilitarios.EnviarMensagem("Cadastro atualizado com sucesso", Me)
                    lblIdDados.Text = String.Empty
                    btnNovoRegistro.Visible = False
                    btnSalvar.Text = "Salvar"
                End If
                gdDizimos.DataSource = Nothing
                gdDizimos.DataBind()
                ListaDizimos = Nothing

            Catch ex As Exception
                Utilitarios.EnviarMensagem("Erro " & ex.Message, Me)
            End Try
        End If
    End Sub

    Protected Sub btnAddLancamento_Click(sender As Object, e As ImageClickEventArgs) Handles btnAddLancamento.Click
        If Page.IsValid Then
            If IsNothing(ListaDizimos) Then
                ListaDizimos = New List(Of Dizimo)
            End If
            Dim _membro As New Membro
            Dim d As Dizimo = New Dizimo

            d.DATA_LANCAMENTO = txtDataLancamento.Text
            _membro.ID = ddMembro.SelectedValue
            _membro.NOME = ddMembro.SelectedItem.Text
            d.Membro = _membro
            d.VALOR_DIZIMO = CDec(txtValorDizimo.Text)
            ListaDizimos.Add(d)
            ListaDizimos = ListaDizimos 'Apenas para dar Bind no grid
            LimparCamposDizimo()
        End If

    End Sub

    Public Property ListaDizimos As System.Collections.Generic.List(Of Dizimo)
        Get
            Return CType(Session("dizimo"), List(Of Dizimo))
        End Get
        Set(ByVal value As System.Collections.Generic.List(Of Dizimo))
            Session("dizimo") = value
            gdDizimos.DataSource = value
            gdDizimos.DataBind()
        End Set
    End Property

    Public Sub LimparCamposDizimo()
        txtDataLancamento.Text = String.Empty
        txtValorDizimo.Text = String.Empty
        ddMembro.SelectedIndex = -1
    End Sub

    Private Sub gdDizimos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdDizimos.RowCommand
        Dim index As Integer = CInt(e.CommandArgument)
        Select Case e.CommandName
            Case "ExcluirContato"
                Dim LisDizimo As List(Of Dizimo) = CType(Session("dizimo"), Global.System.Collections.Generic.List(Of Dizimo))
                If LisDizimo.Count > 0 Then
                    LisDizimo.RemoveAt(index)
                    gdDizimos.DataSource = LisDizimo
                    gdDizimos.DataBind()
                End If
            Case "EditarContato"
                Dim LisDizimo As List(Of Dizimo) = CType(Session("dizimo"), Global.System.Collections.Generic.List(Of Dizimo))
                If LisDizimo.Count > 0 Then
                    ddMembro.SelectedValue = LisDizimo(index).Membro.ID
                    txtValorDizimo.Text = LisDizimo(index).VALOR_DIZIMO
                    txtDataLancamento.Text = LisDizimo(index).DATA_LANCAMENTO
                    LisDizimo.RemoveAt(index)
                    gdDizimos.DataSource = LisDizimo
                    gdDizimos.DataBind()
                End If

        End Select
    End Sub

    Private Sub gdDizimos_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gdDizimos.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.FindControl("btnEditarContato"), ImageButton).CommandArgument = e.Row.RowIndex.ToString
            CType(e.Row.FindControl("btnExcluirContato"), ImageButton).CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        pnCadastro.Visible = False
        pnConsulta.Visible = True
        btnSalvar.Visible = False
        btnConsultar.Visible = False
        btnNovoRegistro.Visible = True
        'gdDizimoConsulta.DataSource = Nothing
        'gdDizimoConsulta.DataBind()
        btnExportarExcel.Visible = False
        lblIdDados.Text = String.Empty
        txtDataInicio.Text = String.Empty
        txtDataFim.Text = String.Empty
    End Sub

    Protected Sub btnNovoRegistro_Click(sender As Object, e As EventArgs) Handles btnNovoRegistro.Click
        pnCadastro.Visible = True
        pnConsulta.Visible = False
        btnSalvar.Visible = True
        btnConsultar.Visible = True
        btnNovoRegistro.Visible = False
        ddMembro.SelectedIndex = -1
        txtValorDizimo.Text = String.Empty
        txtDataLancamento.Text = String.Empty
        btnSalvar.Text = "Salvar"
    End Sub

    Protected Sub btnConsultarDizimo_Click(sender As Object, e As ImageClickEventArgs) Handles btnConsultarDizimo.Click
        Try
            Dim _MapeadorFinanceiro As New MapeadorFinanceiro
            gdDizimoConsulta.DataSource = _MapeadorFinanceiro.getListaDizimo(txtDataInicio.Text, txtDataFim.Text)
            gdDizimoConsulta.DataBind()
            btnExportarExcel.Visible = True
        Catch ex As Exception
            Utilitarios.EnviarMensagem("Erro " & ex.Message, Me)
        End Try
    End Sub

    Private Sub gdDizimoConsulta_DataBound(sender As Object, e As EventArgs) Handles gdDizimoConsulta.DataBound
        If gdDizimoConsulta.Rows.Count > 0 Then
            Dim total As Decimal = 0
            For Each row As GridViewRow In gdDizimoConsulta.Rows
                Dim valorDizimo As Decimal = CDec(row.Cells(5).Text)
                total += valorDizimo
                row.Cells(5).Text = "R$ " & row.Cells(5).Text
            Next
            Dim footer As GridViewRow = gdDizimoConsulta.FooterRow
            footer.Cells(0).ColumnSpan = 3
            'footer.Cells(0).HorizontalAlign = HorizontalAlign.Center
            footer.Cells.RemoveAt(1)
            'footer.Cells.RemoveAt(4)
            footer.Cells(2).Text = "Total"
            footer.Cells(3).Text = "R$ " & total
        Else
            btnExportarExcel.Visible = False
            'Utilitarios.EnviarMensagem("Não existe registros para o período informado", Me)
            
        End If

    End Sub

    Protected Sub btnExportarExcel_Click(sender As Object, e As ImageClickEventArgs) Handles btnExportarExcel.Click

        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=Visitantes " & txtDataInicio.Text & " à " & txtDataFim.Text & ".xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Dim sWriter As New StringWriter()
        Dim hWriter As New HtmlTextWriter(sWriter)
        gdDizimoConsulta.RenderControl(hWriter)
        Response.Output.Write(sWriter.ToString())
        Response.Flush()
        Response.End()
    End Sub
    Public Overloads Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' Verifies that the control is rendered
    End Sub

    Private Sub gdDizimoConsulta_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdDizimoConsulta.RowCommand

        Dim index As Integer = CInt(e.CommandArgument)
        Try
            Select Case e.CommandName
                Case "EditarDizimos"
                    Dim _MapeadorFinaceiro As New MapeadorFinanceiro
                    Dim _Dizimo As New Dizimo
                    _Dizimo = _MapeadorFinaceiro.getDizimoID(gdDizimoConsulta.Rows(index).Cells(1).Text)
                    lblIdDados.Text = _Dizimo.ID
                    txtDataLancamento.Text = _Dizimo.DATA_LANCAMENTO
                    txtValorDizimo.Text = _Dizimo.VALOR_DIZIMO
                    ddMembro.SelectedValue = _Dizimo.Membro.ID
                    pnCadastro.Visible = True
                    pnConsulta.Visible = False
                    txtDataFim.Text = String.Empty
                    txtDataInicio.Text = String.Empty
                    btnSalvar.Text = "Atualizar"
                    btnSalvar.Visible = True
                    btnConsultar.Visible = True
                    gdDizimoConsulta.DataSource = String.Empty
                    gdDizimoConsulta.DataBind()
            End Select

        Catch ex As Exception
            Utilitarios.EnviarMensagem("Erro " & ex.Message, Me)
        End Try
    End Sub

    Private Sub gdDizimoConsulta_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gdDizimoConsulta.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.FindControl("btnEditarDizimos"), ImageButton).CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub
End Class