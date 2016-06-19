Imports System.Text
Imports System.Data.SqlClient
Public Class MapeadorDeVisitantes

    Public Sub Salvar(ByVal _visitante As Visitante)
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        sql = New StringBuilder

        sql.AppendLine("INSERT INTO [IBLR].[dbo].[IBLR_VISITANTES]")
        sql.AppendLine("           ([DATA_VISITA]")
        sql.AppendLine("           ,[NOME]")
        sql.AppendLine("           ,[ENDERECO]")
        sql.AppendLine("           ,[QUEM_CONVIDOU]")
        sql.AppendLine("           ,[ID_CONGREGACAO]")
        sql.AppendLine("           ,[DATA_CADASTRO])")
        sql.AppendLine("     VALUES")
        sql.AppendLine("           (convert(datetime,'" & _visitante.DATA_VISITA & "',103)")
        sql.AppendLine("           ,'" & _visitante.NOME & "'")
        sql.AppendLine("           ,'" & _visitante.ENDERECO & "'")
        sql.AppendLine("           ,'" & _visitante.QUEM_CONVIDOU & "'")
        sql.AppendLine("           ,'" & _visitante.CONGREGACAO.Id & "'")
        sql.AppendLine("           ,convert(datetime,'" & DateAndTime.Now & "',103))")

        ConexaoDB.ExecuteSQL(conn, sql.ToString)

        If Not IsNothing(_visitante.ListaContatos) Then
            InserirContatos(_visitante.ListaContatos, True)
        End If

    End Sub

    Public Sub Atualizar(ByVal _visitante As Visitante)

        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        sql = New StringBuilder

        Sql.AppendLine("UPDATE [IBLR].[dbo].[IBLR_VISITANTES]")
        sql.AppendLine("   SET [DATA_VISITA] = convert(datetime,'" & _visitante.DATA_VISITA & "',103)")
        sql.AppendLine("      ,[NOME] = '" & _visitante.NOME & "'")
        sql.AppendLine("      ,[ENDERECO] = '" & _visitante.ENDERECO & "'")
        sql.AppendLine("      ,[QUEM_CONVIDOU] = '" & _visitante.QUEM_CONVIDOU & "'")
        sql.AppendLine("      ,[ID_CONGREGACAO] = '" & _visitante.CONGREGACAO.Id & "'")
        sql.AppendLine(" WHERE ID='" & _visitante.ID & "'")

        sql.AppendLine("DELETE FROM [IBLR].[dbo].[IBLR_CONTATOS_VISITANTES]")
        sql.AppendLine("      WHERE ID_VISITANTE='" & _visitante.ID & "'")

        ConexaoDB.ExecuteSQL(conn, sql.ToString)

        If Not IsNothing(_visitante.listaContatos) Then
            InserirContatos(_visitante.listaContatos, False, _visitante.ID)
        End If

    End Sub

    Public Sub InserirContatos(ByVal _listaContatos As List(Of Contato), ByVal _contatoNovo As Boolean, Optional _idVisitante As String = "")
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        sql = New StringBuilder

        sql.AppendLine("Declare @Id AS INT")
        If _contatoNovo Then
            sql.AppendLine("Set @Id = (SELECT max(Id) FROM IBLR_VISITANTES)")
        Else
            sql.AppendLine("Set @Id = '" & _idVisitante & "'")
        End If


        For x As Integer = 0 To _listaContatos.Count - 1

            sql.AppendLine("INSERT INTO [IBLR].[dbo].[IBLR_CONTATOS_VISITANTES]")
            sql.AppendLine("           ([ID_VISITANTE]")
            sql.AppendLine("           ,[TIPO_CONTATO]")
            sql.AppendLine("           ,[DESCRICAO_CONTATO])")
            sql.AppendLine("     VALUES")
            sql.AppendLine("           (@Id")
            sql.AppendLine("           ,'" & _listaContatos(x).DescricaoTipoContato & "'")
            sql.AppendLine("           ,'" & _listaContatos(x).Descricao & "')")

        Next
        ConexaoDB.ExecuteSQL(conn, sql.ToString)
    End Sub

    Public Function getListaVisitantes(ByVal idCongregacao As String, Optional dataInicio As String = "", Optional dataFim As String = "") As List(Of Visitante)

        Dim _listaVisitantes As New List(Of Visitante)
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        sql.AppendLine("SELECT V.[ID]")
        sql.AppendLine("      ,[DATA_VISITA]")
        sql.AppendLine("      ,V.[NOME]")
        sql.AppendLine("      ,V.[ENDERECO]")
        sql.AppendLine("      ,[QUEM_CONVIDOU]")
        sql.AppendLine("      ,[ID_CONGREGACAO]")
        sql.AppendLine("      ,C.[NOME] AS NOME_CONGREGACAO")
        sql.AppendLine("  FROM [IBLR].[dbo].[IBLR_VISITANTES] V")
        sql.AppendLine("    INNER JOIN [IBLR].[dbo].IBLR_CONGREGACOES C ON V.ID_CONGREGACAO = C.ID")
        sql.AppendLine("    WHERE V.ID_CONGREGACAO = '" & idCongregacao & "'")
        sql.AppendLine("    AND V.DATA_VISITA >= CONVERT(date,'" & dataInicio & "',103)")
        sql.AppendLine("	AND	V.DATA_VISITA <= CONVERT(date,'" & dataFim & "',103)")


        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)
        Dim _visitante As Visitante
        While dr.Read
            _visitante = New Visitante
            _visitante.ID = dr("ID").ToString
            _visitante.DATA_VISITA = Format(dr("DATA_VISITA"), "dd/MM/yyyy")
            _visitante.NOME = dr("NOME").ToString
            _visitante.ENDERECO = dr("ENDERECO").ToString
            _visitante.QUEM_CONVIDOU = dr("QUEM_CONVIDOU").ToString
            Dim _congregacao As New Congregacao
            _congregacao.Id = dr("ID_CONGREGACAO").ToString
            _congregacao.Nome = dr("NOME_CONGREGACAO").ToString
            _visitante.CONGREGACAO = _congregacao
            _listaVisitantes.Add(_visitante)
        End While

        Return _listaVisitantes
    End Function

    Public Function getVisitanteID(ByVal idVisitante As String) As Visitante
        Dim _listaVisitantes As New List(Of Visitante)
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        Dim _visitante As New Visitante

        sql.AppendLine("SELECT V.[ID]")
        sql.AppendLine("      ,[DATA_VISITA]")
        sql.AppendLine("      ,V.[NOME]")
        sql.AppendLine("      ,V.[ENDERECO]")
        sql.AppendLine("      ,[QUEM_CONVIDOU]")
        sql.AppendLine("      ,[ID_CONGREGACAO]")
        sql.AppendLine("      ,C.[NOME] AS NOME_CONGREGACAO")
        sql.AppendLine("  FROM [IBLR].[dbo].[IBLR_VISITANTES] V")
        sql.AppendLine("    INNER JOIN [IBLR].[dbo].IBLR_CONGREGACOES C ON V.ID_CONGREGACAO = C.ID")
        sql.AppendLine("    WHERE V.ID = '" & idVisitante & "'")

        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)

        While dr.Read
            _visitante.ID = dr("ID").ToString
            _visitante.DATA_VISITA = Format(dr("DATA_VISITA"), "dd/MM/yyyy")
            _visitante.NOME = dr("NOME").ToString
            _visitante.ENDERECO = dr("ENDERECO").ToString
            _visitante.QUEM_CONVIDOU = dr("QUEM_CONVIDOU").ToString
            Dim _congregacao As New Congregacao
            _congregacao.Id = dr("ID_CONGREGACAO").ToString
            _congregacao.Nome = dr("NOME_CONGREGACAO").ToString
            _visitante.CONGREGACAO = _congregacao
        End While

        sql = New StringBuilder
        sql.AppendLine("SELECT [ID]")
        sql.AppendLine("      ,[ID_VISITANTE]")
        sql.AppendLine("      ,[TIPO_CONTATO]")
        sql.AppendLine("      ,[DESCRICAO_CONTATO]")
        sql.AppendLine("  FROM [IBLR].[dbo].[IBLR_CONTATOS_VISITANTES]")
        sql.AppendLine("  WHERE ID_VISITANTE = '" & idVisitante & "'")

        Dim conn1 As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        Dim dr1 As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn1, sql.ToString)
        Dim _listaContatos As New List(Of Contato)

        While dr1.Read
            Dim _Contato As New Contato
            _Contato.id = dr1("ID").ToString
            _Contato.DescricaoTipoContato = dr1("TIPO_CONTATO").ToString
            If _Contato.DescricaoTipoContato = "Fixo" Or _Contato.DescricaoTipoContato = "Celular" Then
                _Contato.Descricao = dr1("DESCRICAO_CONTATO").ToString.ToString.Insert(0, "(").Insert(3, ")").Insert(9, "-")
            End If

            _listaContatos.Add(_Contato)
        End While
        _visitante.listaContatos = _listaContatos
        Return _visitante
    End Function

    Public Function getVisitantesUltimoInserido() As String
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        sql.AppendLine("SELECT MAX(ID) AS ID FROM IBLR_VISITANTES")
        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)

        Dim idMAX As String = ""
        If dr.Read Then
            idMAX = dr("").ToString()
        End If
        Return idMAX
    End Function

End Class
