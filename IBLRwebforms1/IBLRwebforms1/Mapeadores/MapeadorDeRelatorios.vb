Imports System.Text
Imports System.Data.SqlClient
Public Class MapeadorDeRelatorios
    Public Function relAniversariantes(ByVal idCongregacao As String, ByVal mes As String) As List(Of Membro)
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        sql.AppendLine("SELECT [ID]")
        sql.AppendLine("      ,[NOME]")
        sql.AppendLine("      ,[DATA_NASCIMENTO]")
        sql.AppendLine("  FROM [IBLR].[dbo].[IBLR_MEMBROS] M")
        sql.AppendLine("  WHERE  MONTH(M.DATA_NASCIMENTO)=" & mes & "")
        sql.AppendLine("  AND ID_CONGREGACAO = " & idCongregacao & "")
        sql.AppendLine("  ORDER BY DATA_NASCIMENTO")

        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)
        Dim _listaMembros As New List(Of Membro)
        While dr.Read
            Dim _membro As New Membro
            _membro.NOME = dr("NOME").ToString
            _membro.DATA_NASCIMENTO = Format(dr("DATA_NASCIMENTO"), "dd/MM")
            _listaMembros.Add(_membro)
        End While
        Return _listaMembros
    End Function

    Public Function relObreiros(ByVal idsFuncoes As String) As SqlDataReader
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        sql.AppendLine("SELECT M.[ID]")
        sql.AppendLine("      ,M.[NOME]")
        sql.AppendLine("      ,[APELIDO]")
        sql.AppendLine("      ,convert(char,[DATA_NASCIMENTO],103) AS DATA_NASCIMENTO")
        sql.AppendLine("      ,[ESTADO_CIVIL]")
        sql.AppendLine("      ,[CONJUGUE]")
        sql.AppendLine("      ,convert(char,[DATA_ADMISSAO],103) AS DATA_ADMISSAO")
        sql.AppendLine("      ,case convert(char,[DATA_BATISMO],103) when '01/01/1900' then '' else convert(char,[DATA_BATISMO],103) end AS DATA_BATISMO")
        sql.AppendLine("      ,case convert(char,[DATA_CONSAGRACAO],103) when '01/01/1900' then '' else convert(char,[DATA_CONSAGRACAO],103) end AS DATA_CONSAGRACAO")
        sql.AppendLine("      ,[CREDENCIAL]")
        sql.AppendLine("      ,[NUMERO_CREDENCIAL]")
        sql.AppendLine("      ,C.DESCRICAO AS CARGO")
        sql.AppendLine("      ,CO.NOME AS CONGREGACAO")
        sql.AppendLine("      ,D.DESCRICAO AS DEPARTAMENTO")
        sql.AppendLine("      ,M.[ATIVO]")
        sql.AppendLine("  FROM [IBLR].[dbo].[IBLR_MEMBROS] M ")
        sql.AppendLine("  INNER JOIN [IBLR].[dbo].[IBLR_CARGOS] C ON M.ID_CARGO = C.ID")
        sql.AppendLine("  INNER JOIN [IBLR].[dbo].[IBLR_CONGREGACOES] CO ON M.ID_CONGREGACAO=CO.ID")
        sql.AppendLine("  INNER JOIN [IBLR].[dbo].[IBLR_DEPARTAMENTOS] D ON M.ID_DEPARTAMENTO = D.ID")
        sql.AppendLine("  WHERE C.ID IN(" & idsFuncoes & ") AND M.ATIVO=1")

        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)
        Return dr
        'Dim _listaObreiros As New List(Of Membro)
        'While dr.Read
        '    Dim _membro As New Membro
        '    _membro.ID = dr("ID").ToString
        '    _membro.NOME = dr("NOME").ToString
        '    _membro.APELIDO = dr("APELIDO").ToString
        '    _membro.DATA_NASCIMENTO = Format(dr("DATA_NASCIMENTO"), "dd/MM/yyyy")
        '    _membro.ESTADO_CIVIL = dr("ESTADO_CIVIL").ToString
        '    _membro.CONJUGUE = dr("CONJUGUE").ToString
        '    _membro.DATA_ADMISSAO = Format(dr("DATA_ADMISSAO"))
        '    _membro.DATA_BATISMO = Format(dr("DATA_BATISMO"))
        '    _membro.DATA_CONSAGRACAO = Format(dr("DATA_CONSAGRACAO"))
        '    _membro.NUMERO_CREDENCIAL = dr("NUMERO_CREDENCIAL").ToString
        '    Dim _cargo As New Cargo
        '    _cargo.Descricao = dr("CARGO").ToString
        '    _membro.CARGO = _cargo
        '    _membro.DES_CARGO = dr("CARGO").ToString
        '    Dim _congregacao As New Congregacao
        '    _congregacao.Nome = dr("CONGREGACAO").ToString
        '    _membro.CONGREGACAO = _congregacao
        '    _membro.DES_CONGREGACAO = dr("CONGREGACAO").ToString
        '    Dim _departamento As New Departamento
        '    _departamento.Descricao = dr("DEPARTAMENTO").ToString
        '    _membro.DEPARTAMENTO = _departamento
        '    _membro.DES_DEPARTAMENTO = dr("DEPARTAMENTO").ToString
        '    _listaObreiros.Add(_membro)
        'End While

        'Return _listaObreiros
    End Function

    Public Function relMembros(ByVal idFuncao As String) As SqlDataReader
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        sql.AppendLine("SELECT M.[ID]")
        sql.AppendLine("      ,M.[NOME]")
        sql.AppendLine("      ,convert(char,[DATA_NASCIMENTO],103) AS DATA_NASCIMENTO")
        sql.AppendLine("      ,[ESTADO_CIVIL]")
        sql.AppendLine("      ,[CONJUGUE]")
        sql.AppendLine("      ,convert(char,[DATA_ADMISSAO],103) AS DATA_ADMISSAO")
        sql.AppendLine("      ,case convert(char,[DATA_BATISMO],103) when '01/01/1900' then '' else convert(char,[DATA_BATISMO],103) end AS DATA_BATISMO")
        sql.AppendLine("      ,[NUMERO_CREDENCIAL]")
        sql.AppendLine("      ,CASE WHEN (DATEDIFF(DAY,DATA_ADMISSAO,GETDATE()) < 90) THEN 'Novo convertido' ELSE C.DESCRICAO END AS DES_CARGO")
        sql.AppendLine("      ,D.DESCRICAO AS DES_DEPARTAMENTO")
        sql.AppendLine("      ,M.ATIVO AS ATIVO")
        sql.AppendLine("      FROM [IBLR].[dbo].[IBLR_MEMBROS] M")
        sql.AppendLine("	  INNER JOIN [IBLR].[dbo].[IBLR_CARGOS] C ON M.ID_CARGO = C.ID")
        sql.AppendLine("	  INNER JOIN [IBLR].[dbo].[IBLR_DEPARTAMENTOS] D ON M.ID_DEPARTAMENTO = D.ID")
        If Not String.IsNullOrEmpty(idFuncao) Then
            sql.AppendLine("	  WHERE M.ID_CARGO IN (" & idFuncao & ")")
        End If


        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)
        Return dr

    End Function
End Class
