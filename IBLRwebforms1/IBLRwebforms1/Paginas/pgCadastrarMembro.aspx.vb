Public Class pgCadastrarMembro
    Inherits System.Web.UI.Page
    Private _MapeadorDeLog As New MapeadorDeLog
    Private log As New Log
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        If Session("usuarioLogado") = "sim" Then
            Dim image As Image = CType(Master.FindControl("statusLogin"), Image)
            image.ImageUrl = "../Images/green-button_24x24.png"
            image.ImageAlign = ImageAlign.Right
            image.ToolTip = "Usuário logado"
            'log.Acao = "Acesso"
            'log.Funcao = "pgCadastrarMembro"
            'log.Valor = "Logar"
            'log.ModificadoPor = Session("usuario")
            'log.DataAlteracao = DateAndTime.Now
            'Try
            '    _MapeadorDeLog.GravarLogs(log)
            'Catch ex As Exception
            '    Utilitarios.EnviarMensagem(ex.Message, Me)
            'End Try
            Me.ClientScript.RegisterClientScriptInclude("scripts", "Scripts/JavaScript.js")
            
        Else
            Response.Redirect("~/login.aspx")
            Exit Sub
        End If
        If Not IsPostBack Then
            Me.ClientScript.RegisterClientScriptInclude("scripts", "Scripts/JavaScript.js")
            Me.ClientScript.RegisterForEventValidation("btnConsultarMembros", "Click")
            btnNovoRegistro.Visible = False
            pnConsulta.Visible = False

            txtCEP.Attributes.Add("onKeyPress", "return MascaraCEP(this);")
            txtDataAdmissao.Attributes.Add("onKeyPress", "return MascaraData(this,event);")
            txtDataBatismo.Attributes.Add("onKeyPress", "return MascaraData(this,event);")
            txtDataConsagracao.Attributes.Add("onKeyPress", "return MascaraData(this,event);")
            txtDataNascimento.Attributes.Add("onKeyPress", "return MascaraData(this,event);")
            Try

                Dim _MapeadorDeCidades As New MapeadorDeCidades
                ddUf.DataSource = _MapeadorDeCidades.getListaUf()
                ddUf.DataBind()
                ddUf.Items.Insert(0, "")

                ddUfNaturalidade.DataSource = _MapeadorDeCidades.getListaUf()
                ddUfNaturalidade.DataBind()
                ddUfNaturalidade.Items.Insert(0, "")

                Dim _MapeadorDeCongregacoes As New MapeadorDeCongregacoes
                ddCongregacao.DataSource = _MapeadorDeCongregacoes.getListaCongregacoes(0, "")
                ddCongregacao.DataBind()
                ddCongregacao.Items.Insert(0, "")

                Dim _MapeadorDeCargos As New MapeadorDeCargos
                ddCargos.DataSource = _MapeadorDeCargos.getListaCargos("")
                ddCargos.DataBind()
                ddCargos.Items.Insert(0, "")
                Dim item As ListItem = ddCargos.Items.FindByText("Obreiro")
                ddCargos.Items.Remove(item)

                Dim _MapeadorDeDepartamentos As New MapeadorDeDepartamentos
                ddDepartamento.DataSource = _MapeadorDeDepartamentos.getListaDepartamentos()
                ddDepartamento.DataBind()
                ddDepartamento.Items.Insert(0, "")
                log.Acao = "Acesso"
                log.Funcao = "pgCadastrarMembro"
                log.Valor = "Logar"
                log.ModificadoPor = Session("usuario")
                log.DataAlteracao = DateAndTime.Now
                Try
                    _MapeadorDeLog.GravarLogs(log)
                Catch ex As Exception
                    Utilitarios.EnviarMensagem(ex.Message, Me)
                End Try
            Catch ex As Exception
                Utilitarios.EnviarMensagem(ex.Message, Me)
            End Try
        End If
    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        If Not Page.IsValid Then
            selected_tab.Value = -1
        Else
            Try
                Dim _MapeadorDeMembros As New MapeadorDeMembros
                Dim _Membro As New Membro
                Dim _Congregacao As New Congregacao
                Dim _Cidade As New Cidade
                Dim _Cargo As New Cargo
                Dim _Naturalidade As New Cidade
                Session("IDMEMBRO") = lblIdDados.Text
                _Membro.ID = lblIdDados.Text
                _Membro.NOME = txtNome.Text
                _Membro.APELIDO = txtApelido.Text
                _Cargo.Id = ddCargos.SelectedItem.Value
                _Membro.CARGO = _Cargo
                _Membro.RG = txtRG.Text
                _Membro.CPF = txtCpf.Text
                _Membro.DATA_NASCIMENTO = txtDataNascimento.Text
                _Membro.ESTADO_CIVIL = ddEstadoCivil.SelectedItem.Text
                _Membro.SEXO = ddSexo.SelectedItem.Text
                _Membro.NOME_PAI = txtNomePai.Text
                _Membro.NOME_MAE = txtNomeMae.Text
                _Membro.CONJUGUE = txtConjugue.Text
                _Membro.DATA_ADMISSAO = txtDataAdmissao.Text
                _Membro.DATA_BATISMO = txtDataBatismo.Text
                _Membro.DATA_CONSAGRACAO = txtDataConsagracao.Text
                _Naturalidade.Id = ddCidadeNaturalidade.SelectedItem.Value 'naturalidade é o ID da cidade onde o membro nasceu
                _Membro.NATURALIDADE = _Naturalidade
                _Congregacao.Id = ddCongregacao.SelectedItem.Value
                _Membro.CONGREGACAO = _Congregacao
                _Membro.ENDERECO = txtEndereco.Text
                _Membro.BAIRRO = txtBairro.Text
                _Cidade.Id = ddCidade.SelectedItem.Value
                _Membro.CIDADE = _Cidade
                _Membro.CEP = txtCEP.Text
                _Membro.CREDENCIAL = ddCredencial.SelectedItem.Value
                _Membro.NUMERO_CREDENCIAL = txtNumCredencial.Text
                _Membro.ATIVO = ckbAtivo.Checked
                Dim _Departamento As New Departamento
                _Departamento.Id = ddDepartamento.SelectedValue
                _Departamento.Descricao = ddDepartamento.SelectedItem.Text
                _Membro.DEPARTAMENTO = _Departamento

                If gdContatos.Rows.Count > 0 Then
                    _Membro.LISTACONTATOS = New List(Of Contato)
                    For x As Integer = 0 To gdContatos.Rows.Count - 1
                        Dim _contato As New Contato
                        _contato.DescricaoTipoContato = gdContatos.Rows.Item(x).Cells(1).Text
                        _contato.Descricao = gdContatos.Rows.Item(x).Cells(2).Text.Replace("(", "").Replace(")", "").Replace("-", "")
                        _Membro.LISTACONTATOS.Add(_contato)
                    Next

                End If

                If lblIdDados.Text = String.Empty Then
                    _MapeadorDeMembros.Salvar(_Membro)
                    Utilitarios.EnviarMensagem("Cadastro realizado com sucesso.", Me)
                    log.Funcao = "CadastrarMembro"
                    log.Acao = "Inserir"
                    log.Valor = _MapeadorDeMembros.getMembrosUltimoInserido() + "-" + _Membro.NOME
                    log.ModificadoPor = Session("usuario")
                    log.DataAlteracao = DateTime.Now
                    _MapeadorDeLog.GravarLogs(log)
                    'LimparCamposCadastro(Me)
                    'gdContatos.DataSource = Nothing
                    'gdContatos.DataBind()
                    'ListaContatos = Nothing
                    btnNovoRegistro.Visible = True
                Else
                    _MapeadorDeMembros.Atualizar(_Membro)
                    log.Funcao = "CadastrarMembro"
                    log.Acao = "Atualizar"
                    log.Valor = _Membro.ID + "-" + _Membro.NOME
                    log.ModificadoPor = Session("usuario")
                    log.DataAlteracao = DateTime.Now
                    _MapeadorDeLog.GravarLogs(log)

                    Utilitarios.EnviarMensagem("Cadastro atualizado com sucesso.", Me)
                    btnNovoRegistro.Visible = True
                End If
            Catch ex As Exception
                Utilitarios.EnviarMensagem("Erro: " + ex.Message, Me)
            End Try

        End If
    End Sub

    Private Sub ddUf_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddUf.SelectedIndexChanged
        selected_tab.Value = 1
        Dim _MapeadorDeCidades As New MapeadorDeCidades
        ddCidade.DataSource = _MapeadorDeCidades.getListaCidades(ddUf.SelectedItem.Text)
        ddCidade.DataBind()
        ddCidade.Items.Insert(0, "")
    End Sub

    Private Sub ddUfNaturalidade_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddUfNaturalidade.SelectedIndexChanged
        selected_tab.Value = -1
        Dim _MapeadorDeCidades As New MapeadorDeCidades
        ddCidadeNaturalidade.DataSource = _MapeadorDeCidades.getListaCidades(ddUfNaturalidade.SelectedItem.Text)
        ddCidadeNaturalidade.DataBind()
        ddCidadeNaturalidade.Items.Insert(0, "")
    End Sub

    Private Sub ddTipoContato_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddTipoContato.SelectedIndexChanged
        selected_tab.Value = 2

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

    Private Sub btnAddContato_Click(sender As Object, e As ImageClickEventArgs) Handles btnAddContato.Click
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

    Public Sub LimparCamposContatos()
        txtContato.Text = String.Empty
        ddTipoContato.SelectedIndex = -1
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

    Private Sub ddCredencial_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddCredencial.SelectedIndexChanged
        If ddCredencial.SelectedItem.Value = "S" Then
            txtNumCredencial.Enabled = True
        ElseIf ddCredencial.SelectedItem.Value = "N" Then
            txtNumCredencial.Enabled = False
        End If
    End Sub

    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        pnCadastro.Visible = False
        pnConsulta.Visible = True
        btnConsultar.Visible = False
        btnSalvar.Visible = False
        btnNovoRegistro.Visible = True
        gdMembros.DataSource = String.Empty
        gdMembros.DataBind()
        txtCampoConsulta.Text = String.Empty
    End Sub

    Private Sub btnNovoRegistro_Click(sender As Object, e As EventArgs) Handles btnNovoRegistro.Click
        pnCadastro.Visible = True
        pnConsulta.Visible = False
        btnConsultar.Visible = True
        btnSalvar.Visible = True
        btnNovoRegistro.Visible = False
        btnSalvar.Text = "Salvar"
        lblIdDados.Text = String.Empty
        ckbAtivo.Checked = True
        gdContatos.DataSource = Nothing
        gdContatos.DataBind()
        ListaContatos = Nothing
        LimparCamposCadastro(Me)
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

    Private Sub gdMembros_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gdMembros.RowCommand
        Dim index As Integer = CInt(e.CommandArgument)
        Select Case e.CommandName
            Case "EditarMembros"
                Try
                    Dim _MapeadorDeMembro As New MapeadorDeMembros
                    Dim _Membro As Membro
                    _Membro = _MapeadorDeMembro.getMembroID(gdMembros.Rows(index).Cells(1).Text)

                    Session("IDMEMBRO") = _Membro.ID
                    lblIdDados.Text = _Membro.ID
                    txtNome.Text = _Membro.NOME
                    txtApelido.Text = _Membro.APELIDO
                    ddCargos.SelectedValue = _Membro.CARGO.Id
                    txtRG.Text = _Membro.RG
                    txtCpf.Text = _Membro.CPF
                    txtDataNascimento.Text = _Membro.DATA_NASCIMENTO
                    Select Case _Membro.ESTADO_CIVIL
                        Case "Casado"
                            ddEstadoCivil.SelectedValue = 1
                        Case "Solteiro"
                            ddEstadoCivil.SelectedValue = 2
                        Case "Viúvo"
                            ddEstadoCivil.SelectedValue = 3
                        Case "Divorciado"
                            ddEstadoCivil.SelectedValue = 4
                    End Select

                    Select Case _Membro.SEXO
                        Case "M"
                            ddSexo.SelectedValue = 1
                        Case "F"
                            ddSexo.SelectedValue = 2
                    End Select

                    txtNomePai.Text = _Membro.NOME_PAI
                    txtNomeMae.Text = _Membro.NOME_MAE
                    txtConjugue.Text = _Membro.CONJUGUE

                    If _Membro.DATA_ADMISSAO = "01/01/1900" Then
                        txtDataAdmissao.Text = String.Empty
                    Else
                        txtDataAdmissao.Text = _Membro.DATA_ADMISSAO
                    End If

                    If _Membro.DATA_BATISMO = "01/01/1900" Then
                        txtDataBatismo.Text = String.Empty
                    Else
                        txtDataBatismo.Text = _Membro.DATA_BATISMO
                    End If

                    If _Membro.DATA_CONSAGRACAO = "01/01/1900" Then
                        txtDataConsagracao.Text = String.Empty
                    Else
                        txtDataConsagracao.Text = _Membro.DATA_CONSAGRACAO
                    End If

                    ddUfNaturalidade.SelectedValue = _Membro.NATURALIDADE.IdEstado

                    Dim _MapeadorDeCidades As New MapeadorDeCidades
                    ddCidadeNaturalidade.DataSource = _MapeadorDeCidades.getListaCidades(_Membro.NATURALIDADE.UF)
                    ddCidadeNaturalidade.DataBind()
                    ddCidadeNaturalidade.Items.Insert(0, "")
                    ddCidadeNaturalidade.SelectedValue = _Membro.NATURALIDADE.Id
                    ddCongregacao.SelectedValue = _Membro.CONGREGACAO.Id

                    If _Membro.CREDENCIAL Then
                        ddCredencial.SelectedValue = "S"
                    Else
                        ddCredencial.SelectedValue = "N"
                    End If

                    txtNumCredencial.Text = _Membro.NUMERO_CREDENCIAL
                    txtEndereco.Text = _Membro.ENDERECO
                    txtBairro.Text = _Membro.BAIRRO
                    txtCEP.Text = _Membro.CEP
                    ckbAtivo.Checked = _Membro.ATIVO
                    ddUf.SelectedValue = _Membro.CIDADE.IdEstado
                    ddCidade.DataSource = _MapeadorDeCidades.getListaCidades(_Membro.CIDADE.UF)
                    ddCidade.DataBind()
                    ddCidade.Items.Insert(0, "")
                    ddCidade.SelectedValue = _Membro.CIDADE.Id
                    ddDepartamento.SelectedValue = _Membro.DEPARTAMENTO.Id

                    gdContatos.DataSource = _Membro.LISTACONTATOS
                    gdContatos.DataBind()

                    pnConsulta.Visible = False
                    pnCadastro.Visible = True

                    btnConsultar.Visible = True
                    btnNovoRegistro.Visible = True
                    btnSalvar.Visible = True
                    btnSalvar.Text = "Atualizar"
                Catch ex As Exception
                    Utilitarios.EnviarMensagem("Erro " & ex.Message, Me)
                End Try


        End Select
    End Sub

    Protected Sub gdMembros_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gdMembros.RowCreated
        If e.Row.RowType = DataControlRowType.DataRow Then
            CType(e.Row.FindControl("btnEditarMembros"), ImageButton).CommandArgument = e.Row.RowIndex.ToString
        End If
    End Sub

    Protected Sub btnConsultarMembros_Click(sender As Object, e As ImageClickEventArgs) Handles btnConsultarMembros.Click
        Try
            Dim _MapeadorDeMembros As New MapeadorDeMembros
            gdMembros.DataSource = _MapeadorDeMembros.getListaMembros(txtCampoConsulta.Text)
            gdMembros.DataBind()
        Catch ex As Exception
            Utilitarios.EnviarMensagem("Erro: " & ex.Message, Me)
        End Try
    End Sub

    Public Sub LimparCamposCadastro(ByVal controlP As Control)

        Dim ctl As Control
        For Each ctl In controlP.Controls

            If TypeOf ctl Is TextBox Then

                DirectCast(ctl, TextBox).Text = String.Empty
            ElseIf TypeOf ctl Is DropDownList Then

                DirectCast(ctl, DropDownList).SelectedIndex = -1

            ElseIf ctl.Controls.Count > 0 Then

                LimparCamposCadastro(ctl)

            End If

        Next
    End Sub
End Class