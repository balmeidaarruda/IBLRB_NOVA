Public Class CadastrarCongregacao
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("usuarioLogado") = "sim" Then
            Dim menu As Menu = CType(Master.FindControl("Menu1"), Menu)
            menu.Visible = True
            Dim image As Image = CType(Master.FindControl("statusLogin"), Image)
            image.ImageUrl = "../Images/green-button_24x24.png"
            image.ImageAlign = ImageAlign.Right
            image.ToolTip = "Usuário logado"
        End If
        If Not IsPostBack Then
            Me.ClientScript.RegisterClientScriptInclude("scripts", "Scripts/JavaScript.js")
            btnNovoRegistro.Visible = False
            pnConsulta.Visible = False
            txtCEP.Attributes.Add("onKeyPress", "return MascaraCEP(this);")
            txtDataFundacao.Attributes.Add("onKeyPress", "return MascaraData(this,event);")
            Dim _MapeadorDeCidades As New MapeadorDeCidades
            ddUf.DataSource = _MapeadorDeCidades.getListaUf()
            ddUf.DataBind()
            ddUf.Items.Insert(0, "")
            'ddUf.SelectedIndex = -1
            Dim _MapeadorDeCongregacoes As New MapeadorDeCongregacoes
            ddCampo.DataSource = _MapeadorDeCongregacoes.getListaCampos
            ddCampo.DataBind()
            ddCampo.Items.Insert(0, "")
            'ddCampo.SelectedIndex = -1
            'txtDtNascimento1.Attributes.Add("onKeyPress", "return MascaraData(this, event);")
            'txtDtNascimentoConjugue1.Attributes.Add("onKeyPress", "return MascaraData(this, event);")
        End If

    End Sub

#Region "EVENTOS BOTÕES"

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
        c.Id = ddTipoContato.SelectedValue
        c.DescricaoTipoContato = ddTipoContato.Items(ddTipoContato.SelectedIndex).Text
        ListaContatos.Add(c)
        ListaContatos = ListaContatos 'Apenas para dar Bind no grid
        LimparCamposContatos()
    End Sub

    Protected Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click

        Try
            If Page.IsValid Then

                Dim _MapeadorDeCongregacoes As New MapeadorDeCongregacoes
                Dim _congrecao As New Congregacao

                _congrecao.Nome = txtNome.Text
                _congrecao.PastorResponsavel = txtPastorResponsavel.Text
                _congrecao.DataFundacao = txtDataFundacao.Text

                Dim _campo As New Campo
                _campo.Nome = ddCampo.SelectedItem.Text
                _campo.Id = ddCampo.SelectedItem.Value
                _congrecao.Campo = _campo
                _congrecao.Endereco = txtEndereco.Text
                _congrecao.Bairro = txtBairro.Text

                Dim _cidade As New Cidade
                _cidade.Id = ddCidade.SelectedItem.Value
                _cidade.Descricao = ddCidade.SelectedItem.Text
                _congrecao.Cidade = _cidade
                _congrecao.Cep = txtCEP.Text.Replace(".", "").Replace("-", "")
                _congrecao.Ativo = "1"

                If gdContatos.Rows.Count > 0 Then
                    _congrecao.ListaContatos = New List(Of Contato)
                    For x As Integer = 0 To gdContatos.Rows.Count - 1
                        Dim _contato As New Contato
                        _contato.DescricaoTipoContato = gdContatos.Rows.Item(x).Cells(1).Text
                        _contato.Descricao = gdContatos.Rows.Item(x).Cells(2).Text.Replace("(", "").Replace(")", "").Replace("-", "")
                        _congrecao.ListaContatos.Add(_contato)
                    Next

                End If

                If lblIdDados.Text = String.Empty Then
                    _MapeadorDeCongregacoes.Salvar(_congrecao)
                    MsgBox("Cadastro realizado com sucesso!")
                    LimparCampos()
                Else
                    _congrecao.Id = lblIdDados.Text
                    _MapeadorDeCongregacoes.Atualizar(_congrecao)
                    MsgBox("Cadastro atualizado com sucesso!")
                End If

            End If

        Catch ex As Exception
            MsgBox(ex.Message.ToString, MsgBoxStyle.Critical, "Error")
        End Try


    End Sub

    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        MontaTelaConsulta()
    End Sub

    Protected Sub btnNovoRegistro_Click(sender As Object, e As EventArgs) Handles btnNovoRegistro.Click
        MontaTelaCadastro()
        btnSalvar.Text = "Salvar"
        lblIdDados.Text = String.Empty
    End Sub

    Protected Sub btnConsultarCongregacoes_Click(sender As Object, e As ImageClickEventArgs) Handles btnConsultarCongregacoes.Click
        If (ddTipoConsulta.SelectedItem.Value = 1 And txtCampoConsulta.Text = String.Empty) Or (ddTipoConsulta.SelectedItem.Value = 2 And txtCampoConsulta.Text = String.Empty) Then
            MsgBox("Informe o nome da congregação ou o pastor responsável, ou selecione a opção Todos")
        Else
            Try
                Dim _MapeadorDeCongregacoes As New MapeadorDeCongregacoes
                gdCongregacoes.DataSource = _MapeadorDeCongregacoes.getListaCongregacoes(ddTipoConsulta.SelectedItem.Value, txtCampoConsulta.Text)
                gdCongregacoes.DataBind()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Erro")
            End Try
        End If


    End Sub

#End Region

#Region "EVENTOS GRIDVIEWS E DROPBOXS"

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

    Private Sub gdCongregacoes_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdCongregacoes.RowCommand
        Dim index As Integer = CInt(e.CommandArgument)
        Select Case e.CommandName
            Case "EditarCongregacoes"
                Try
                    MontaTelaCadastro()
                    Dim _MapeadorDeCongregacoes As New MapeadorDeCongregacoes
                    Dim _congregacao As New Congregacao
                    _congregacao = _MapeadorDeCongregacoes.getCongregacaoID(gdCongregacoes.Rows(index).Cells(1).Text)
                    txtNome.Text = _congregacao.Nome
                    txtDataFundacao.Text = _congregacao.DataFundacao
                    txtPastorResponsavel.Text = _congregacao.PastorResponsavel
                    ddCampo.SelectedValue = _congregacao.Campo.Id
                    txtEndereco.Text = _congregacao.Endereco
                    txtBairro.Text = _congregacao.Bairro
                    txtCEP.Text = _congregacao.Cep
                    lblIdDados.Text = _congregacao.Id
                    Dim _MapeadorDeCidades As New MapeadorDeCidades
                    ddUf.DataSource = _MapeadorDeCidades.getListaUf()
                    ddUf.DataBind()
                    ddUf.SelectedValue = _congregacao.Cidade.IdEstado
                    ddCidade.DataSource = _MapeadorDeCidades.getListaCidades(_congregacao.Cidade.UF)
                    ddCidade.DataBind()
                    ddCidade.SelectedValue = _congregacao.Cidade.Id
                    ListaContatos = _congregacao.ListaContatos
                    LimparCamposConsulta()
                    btnSalvar.Text = "Atualizar"
                    btnNovoRegistro.Visible = True
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Critical, "Erro")
                End Try


        End Select
    End Sub

    Private Sub gdCongregacoes_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gdCongregacoes.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.FindControl("btnEditarCongregacoes"), ImageButton).CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

    Protected Sub ddTipoContato_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddTipoContato.SelectedIndexChanged
        If ddTipoContato.SelectedValue = 1 Or ddTipoContato.SelectedValue = 2 Then
            txtContato.Attributes.Add("onKeyPress", "return MascaraTelefone(this);")
            txtContato.ToolTip = "Formato (22)2222-2222"
            txtContato.MaxLength = "13"
        Else
            txtContato.Attributes.Add("onKeyPress", "")
            txtContato.ToolTip = ""
            txtContato.MaxLength = "500"
        End If
    End Sub

    Private Sub ddUf_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddUf.SelectedIndexChanged
        Dim _MapeadorDeCidades As New MapeadorDeCidades
        ddCidade.DataSource = _MapeadorDeCidades.getListaCidades(ddUf.SelectedItem.Text)
        ddCidade.DataBind()
        'ddCidade.SelectedIndex = -1
        ddCidade.Items.Insert(0, "")
    End Sub

#End Region

#Region "FUNÇÕES"

    Public Sub LimparCamposContatos()
        txtContato.Text = String.Empty
        ddTipoContato.SelectedIndex = -1
    End Sub

    Public Sub MontaTelaConsulta()
        pnCadastro.Visible = False
        pnConsulta.Visible = True
        btnSalvar.Visible = False
        btnConsultar.Visible = False
        btnNovoRegistro.Visible = True
        lblIdDados.Text = String.Empty
        gdCongregacoes.DataSource = Nothing
        gdCongregacoes.DataBind()
    End Sub

    Public Sub MontaTelaCadastro()
        LimparCampos()
        LimparCamposConsulta()
        pnCadastro.Visible = True
        pnConsulta.Visible = False
        btnSalvar.Visible = True
        btnConsultar.Visible = True
        btnNovoRegistro.Visible = False
        If lblIdDados.Text = String.Empty Then
            btnSalvar.Text = "Salvar"
        Else
            btnSalvar.Text = "Atualizar"
        End If

    End Sub

    Public Sub LimparCampos()
        txtNome.Text = String.Empty
        txtPastorResponsavel.Text = String.Empty
        txtDataFundacao.Text = String.Empty
        'ddCampo.Items.Insert(0, "")
        ddCampo.SelectedIndex = -1
        txtEndereco.Text = String.Empty
        txtBairro.Text = String.Empty
        ddUf.Items.Insert(0, "")
        ddUf.SelectedIndex = -1
        ddCidade.Items.Clear()
        ddCidade.DataSource = Nothing
        ddCidade.DataBind()
        txtCEP.Text = String.Empty
        LimparCamposContatos()
        ListaContatos = Nothing
    End Sub

    Public Sub LimparCamposConsulta()
        ddTipoConsulta.SelectedValue = "1"
        txtCampoConsulta.Text = String.Empty
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

#End Region

End Class