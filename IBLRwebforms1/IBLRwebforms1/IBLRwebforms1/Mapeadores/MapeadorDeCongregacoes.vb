Imports System.Text
Imports System.Data.SqlClient

Public Class MapeadorDeCongregacoes

#Region "CRUDS"

    Public Sub Salvar(ByVal _congregacao As Congregacao)

        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        sql.AppendLine("INSERT INTO [IBLR].[dbo].[IBLR_CONGREGACOES]")
        sql.AppendLine("           ([NOME]")
        sql.AppendLine("           ,[PASTOR_RESPONSAVEL]")
        sql.AppendLine("           ,[DATA_FUNDACAO]")
        sql.AppendLine("           ,[ENDERECO]")
        sql.AppendLine("           ,[BAIRRO]")
        sql.AppendLine("           ,[CEP]")
        sql.AppendLine("           ,[IBLR_CIDADES_ID]")
        sql.AppendLine("           ,[IBLR_CAMPOS_ID]")
        sql.AppendLine("           ,[DATA_CADASTRO]")
        sql.AppendLine("           ,[OBSERVACAO]")
        sql.AppendLine("           ,[ATIVO])")
        sql.AppendLine("     VALUES")
        sql.AppendLine("           ('" & _congregacao.Nome & "'")
        sql.AppendLine("           ,'" & _congregacao.PastorResponsavel & "'")
        sql.AppendLine("           ,convert(datetime,'" & _congregacao.DataFundacao & "',103)")
        sql.AppendLine("           ,'" & _congregacao.Endereco & "'")
        sql.AppendLine("           ,'" & _congregacao.Bairro & "'")
        sql.AppendLine("           ,'" & _congregacao.Cep & "'")
        sql.AppendLine("           ,'" & _congregacao.Cidade.Id & "'")
        sql.AppendLine("           ,'" & _congregacao.Campo.Id & "'")
        sql.AppendLine("           ,convert(datetime,'" & DateAndTime.Now & "',103)")
        sql.AppendLine("           ,'" & _congregacao.Observacao & "'")
        sql.AppendLine("           ,'1')")

        ConexaoDB.ExecuteSQL(conn, sql.ToString)

        If Not IsNothing(_congregacao.ListaContatos) Then
            InserirContatos(_congregacao.ListaContatos, True)
        End If

    End Sub

    Public Sub Atualizar(ByVal _congregacao As Congregacao)

        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        Sql.AppendLine("UPDATE [IBLR].[dbo].[IBLR_CONGREGACOES]")
        sql.AppendLine("   SET [NOME] ='" & _congregacao.Nome & "'")
        sql.AppendLine("      ,[PASTOR_RESPONSAVEL] ='" & _congregacao.PastorResponsavel & "'")
        sql.AppendLine("      ,[DATA_FUNDACAO] = convert(datetime,'" & _congregacao.DataFundacao & "',103)")
        sql.AppendLine("      ,[ENDERECO] = '" & _congregacao.Endereco & "'")
        sql.AppendLine("      ,[BAIRRO] = '" & _congregacao.Bairro & "'")
        sql.AppendLine("      ,[CEP] = '" & _congregacao.Cep & "'")
        sql.AppendLine("      ,[IBLR_CIDADES_ID] ='" & _congregacao.Cidade.Id & "'")
        sql.AppendLine("      ,[IBLR_CAMPOS_ID] ='" & _congregacao.Campo.Id & "'")
        sql.AppendLine("      ,[DATA_CADASTRO] = convert(datetime,'" & DateAndTime.Now & "',103)")
        sql.AppendLine("      ,[OBSERVACAO] ='" & _congregacao.Observacao & "'")
        sql.AppendLine("      ,[ATIVO] ='" & _congregacao.Ativo & "' ")
        sql.AppendLine(" WHERE ID='" & _congregacao.Id & "'")

        sql.AppendLine("DELETE FROM [IBLR].[dbo].[IBLR_CONTATOS_CONGREGACAO]")
        sql.AppendLine("      WHERE ID_CONGREGACAO='" & _congregacao.Id & "'")

        ConexaoDB.ExecuteSQL(conn, sql.ToString)

        If Not IsNothing(_congregacao.ListaContatos) Then
            InserirContatos(_congregacao.ListaContatos, False, _congregacao.Id)
        End If

    End Sub

    Public Sub InserirContatos(ByVal _listaContatos As List(Of Contato), ByVal _contatoNovo As Boolean, Optional _idCongregacao As String = "")

        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        sql = New StringBuilder

        sql.AppendLine("Declare @Id AS INT")
        If _contatoNovo Then
            sql.AppendLine("Set @Id = (SELECT max(Id) FROM IBLR_CONGREGACOES)")
        Else
            sql.AppendLine("Set @Id = '" & _idCongregacao & "'")
        End If


        For x As Integer = 0 To _listaContatos.Count - 1

            sql.AppendLine("INSERT INTO [IBLR].[dbo].[IBLR_CONTATOS_CONGREGACAO]")
            sql.AppendLine("           ([ID_CONGREGACAO]")
            sql.AppendLine("           ,[TIPO_CONTATO]")
            sql.AppendLine("           ,[DESCRICAO_CONTATO])")
            sql.AppendLine("     VALUES")
            sql.AppendLine("           (@Id")
            sql.AppendLine("           ,'" & _listaContatos(x).DescricaoTipoContato & "'")
            sql.AppendLine("           ,'" & _listaContatos(x).Descricao & "')")

        Next
        ConexaoDB.ExecuteSQL(conn, sql.ToString)
    End Sub

#End Region

#Region "BUSCA DADOS"

    Public Function getListaCampos() As List(Of Campo)
        Dim sql As New StringBuilder
        Dim listaCampos As New List(Of Campo)

        sql.AppendLine("Select * from IBLR_CAMPOS")
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)

        While dr.Read

            Dim campo As New Campo
            campo.Id = CInt(dr("ID"))
            campo.Nome = dr("NOME").ToString
            campo.PastorResponsavel = dr("PASTOR_RESPONSAVEL").ToString
            campo.Ativo = CInt(dr("ATIVO"))
            listaCampos.Add(campo)

        End While

        Return listaCampos

    End Function

    Public Function getListaCongregacoes(ByVal tpConsulta As String, ByVal descricaoConsulta As String) As List(Of Congregacao)
        Dim _listaCongregacoes As New List(Of Congregacao)

        Dim sql As New StringBuilder
        sql.AppendLine("SELECT CO.[ID]")
        sql.AppendLine("      ,CO.[NOME]")
        sql.AppendLine("      ,CO.[PASTOR_RESPONSAVEL]")
        sql.AppendLine("      ,CO.[DATA_FUNDACAO]")
        sql.AppendLine("      ,CI.[CIDADE] + '-' + CI.UF AS CIDADE")
        sql.AppendLine("      ,CA.NOME AS NOME_CAMPO")
        sql.AppendLine("  FROM [IBLR_CONGREGACOES] CO")
        sql.AppendLine("		INNER JOIN [IBLR_CIDADES] CI ON CO.IBLR_CIDADES_ID = CI.ID")
        sql.AppendLine("		INNER JOIN IBLR_ESTADOS ES ON CI.ESTADO_ID = ES.ID")
        sql.AppendLine("		INNER JOIN IBLR_CAMPOS CA ON CO.IBLR_CAMPOS_ID = CA.ID")
        If tpConsulta = 1 Then
            sql.AppendLine("    WHERE CO.NOME LIKE '%" & descricaoConsulta & "%'")
        ElseIf tpConsulta = 2 Then
            sql.AppendLine("    WHERE CO.PASTOR_RESPONSAVEL LIKE '%" & descricaoConsulta & "%'")
        End If

        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)

        While dr.Read
            Dim _congregacao As New Congregacao
            Dim _campo As New Campo
            Dim _cidade As New Cidade

            _congregacao.Id = dr("ID").ToString
            _congregacao.Nome = dr("NOME").ToString
            _congregacao.PastorResponsavel = dr("PASTOR_RESPONSAVEL").ToString
            _congregacao.DataFundacao = dr("DATA_FUNDACAO").ToString().Remove(10, 9)
            _cidade.Descricao = dr("CIDADE").ToString
            _congregacao.Cidade = _cidade
            _campo.Nome = dr("NOME_CAMPO").ToString
            _congregacao.Campo = _campo
            _listaCongregacoes.Add(_congregacao)

        End While

        Return _listaCongregacoes
    End Function

    Public Function getCongregacaoID(ByVal id As String) As Congregacao
        Dim _Congregacao As New Congregacao

        Dim sql As New StringBuilder
        sql.AppendLine("SELECT CO.[ID]")
        sql.AppendLine("      ,CO.[NOME]")
        sql.AppendLine("      ,CO.[PASTOR_RESPONSAVEL]")
        sql.AppendLine("      ,CO.[DATA_FUNDACAO]")
        sql.AppendLine("      ,CO.[ENDERECO]")
        sql.AppendLine("      ,CO.[BAIRRO]")
        sql.AppendLine("      ,CO.[CEP]")
        sql.AppendLine("      ,CI.[CIDADE]")
        sql.AppendLine("      ,CI.[ID] AS ID_CIDADE")
        sql.AppendLine("      ,ES.[ID] AS ID_ESTADO")
        sql.AppendLine("      ,ES.[UF]")
        sql.AppendLine("      ,CA.[ID] AS ID_CAMPO")
        sql.AppendLine("      ,CA.NOME AS NOME_CAMPO")
        sql.AppendLine("  FROM [IBLR_CONGREGACOES] CO")
        sql.AppendLine("		INNER JOIN [IBLR_CIDADES] CI ON CO.IBLR_CIDADES_ID = CI.ID")
        sql.AppendLine("		INNER JOIN IBLR_ESTADOS ES ON CI.ESTADO_ID = ES.ID")
        sql.AppendLine("		INNER JOIN IBLR_CAMPOS CA ON CO.IBLR_CAMPOS_ID = CA.ID")
        sql.AppendLine("    WHERE CO.ID = '" & id & "'")

        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)

        While dr.Read
            _Congregacao.Id = dr("ID").ToString
            _Congregacao.Nome = dr("NOME").ToString
            _Congregacao.PastorResponsavel = dr("PASTOR_RESPONSAVEL").ToString
            _Congregacao.DataFundacao = Format(dr("DATA_FUNDACAO"), "dd/MM/yyyy")
            _Congregacao.Endereco = dr("ENDERECO").ToString
            _Congregacao.Bairro = dr("BAIRRO").ToString
            _Congregacao.Cep = dr("CEP").ToString.Insert(2, ".").Insert(6, "-")
            Dim _Cidade As New Cidade
            _Cidade.Id = dr("ID_CIDADE").ToString
            _Cidade.Descricao = dr("CIDADE").ToString
            _Cidade.IdEstado = dr("ID_ESTADO").ToString
            _Cidade.UF = dr("UF").ToString
            _Congregacao.Cidade = _Cidade
            Dim _Campo As New Campo
            _Campo.Id = dr("ID_CAMPO").ToString
            _Campo.Nome = dr("NOME_CAMPO").ToString
            _Congregacao.Campo = _Campo
        End While

        sql = New StringBuilder
        sql.AppendLine("SELECT [ID]")
        sql.AppendLine("      ,[ID_CONGREGACAO]")
        sql.AppendLine("      ,[TIPO_CONTATO]")
        sql.AppendLine("      ,[DESCRICAO_CONTATO]")
        sql.AppendLine("  FROM [IBLR].[dbo].[IBLR_CONTATOS_CONGREGACAO]")
        sql.AppendLine("  WHERE ID_CONGREGACAO = '" & id & "'")

        Dim conn1 As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        Dim dr1 As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn1, sql.ToString)
        Dim _listaContatos As New List(Of Contato)

        While dr1.Read
            Dim _Contato As New Contato
            _Contato.id = dr1("ID").ToString
            _Contato.DescricaoTipoContato = dr1("TIPO_CONTATO").ToString
            _Contato.Descricao = dr1("DESCRICAO_CONTATO").ToString
            _listaContatos.Add(_Contato)
        End While

        _Congregacao.ListaContatos = _listaContatos

        Return _Congregacao
    End Function

#End Region


End Class
