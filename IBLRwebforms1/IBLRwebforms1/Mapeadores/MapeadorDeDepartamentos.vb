Imports System.Text
Imports System.Data.SqlClient
Public Class MapeadorDeDepartamentos
    Public Function getListaDepartamentos() As List(Of Departamento)
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        Dim listaDepartamentos As New List(Of Departamento)

        sql.AppendLine("SELECT D.[ID]")
        sql.AppendLine("      ,[DESCRICAO]")
        sql.AppendLine("      ,CASE WHEN [ID_MEMBRO_LIDER_DPTO] IS NULL THEN 0 ELSE ID_MEMBRO_LIDER_DPTO END ID_MEMBRO_LIDER_DPTO")
        sql.AppendLine("      ,CASE WHEN M.NOME IS NULL THEN 'Líder não informado' ELSE M.NOME END NOME")
        sql.AppendLine("      ,D.[ATIVO]")
        sql.AppendLine("  FROM [IBLR].[dbo].[IBLR_DEPARTAMENTOS] D")
        sql.AppendLine("  LEFT JOIN [IBLR].[dbo].[IBLR_MEMBROS] M ON D.ID_MEMBRO_LIDER_DPTO = M.ID ")
        sql.AppendLine("  ORDER BY DESCRICAO")

        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)

        While dr.Read
            Dim _departamento As New Departamento
            _departamento.Id = dr("ID").ToString
            _departamento.Descricao = dr("DESCRICAO").ToString
            _departamento.Ativo = dr("ATIVO").ToString

            Dim _membro As New Membro
            _membro.ID = dr("ID_MEMBRO_LIDER_DPTO").ToString
            _membro.NOME = dr("NOME").ToString
            _departamento.MembroLiderDpto = _membro
            listaDepartamentos.Add(_departamento)

        End While

        Return listaDepartamentos
    End Function

    Public Function getDepartamentoID(ByVal id As String) As Departamento
        Dim _dep As New Departamento
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        sql.AppendLine("SELECT * FROM IBLR_DEPARTAMENTOS WHERE ID='" + id + "'")
        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)
        If dr.Read Then
            _dep.Id = dr("ID").ToString
            _dep.Descricao = dr("DESCRICAO").ToString
            _dep.Ativo = dr("ATIVO").ToString
            Dim _membro As New Membro
            _membro.ID = dr("ID_MEMBRO_LIDER_DPTO").ToString
            _dep.MembroLiderDpto = _membro
        End If
        Return _dep
    End Function

    Public Sub Salvar(ByVal _departamento As Departamento)

        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        sql = New StringBuilder

        sql.AppendLine("INSERT INTO [IBLR].[dbo].[IBLR_DEPARTAMENTOS]")
        sql.AppendLine("           ([DESCRICAO]")
        sql.AppendLine("           ,[ID_MEMBRO_LIDER_DPTO]")
        sql.AppendLine("           ,[ATIVO])")
        sql.AppendLine("     VALUES")
        sql.AppendLine("           ('" & _departamento.Descricao & "'")
        sql.AppendLine("           ,'" & _departamento.MembroLiderDpto.ID & "'")
        sql.AppendLine("           ,1)")

        ConexaoDB.ExecuteSQL(conn, sql.ToString)
    End Sub

    Public Sub Atualizar(ByVal _departamento As Departamento)
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        sql.AppendLine("UPDATE [IBLR].[dbo].[IBLR_DEPARTAMENTOS]")
        sql.AppendLine("   SET [DESCRICAO] = '" + _departamento.Descricao + "'")
        sql.AppendLine("      ,[ID_MEMBRO_LIDER_DPTO] = '" + _departamento.MembroLiderDpto.ID + "'")
        sql.AppendLine("      ,[ATIVO] = '" + IIf(_departamento.Ativo = "True", "1", "0") + "'")
        sql.AppendLine(" WHERE ID=" + _departamento.Id)
        ConexaoDB.ExecuteSQL(conn, sql.ToString)
    End Sub
    Public Function getDepartamentoUltimoInserido() As String
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        Dim idMax As String = ""

        sql.AppendLine("SELECT max(ID) as ID")
        sql.AppendLine("  FROM [IBLR].[dbo].[IBLR_DEPARTAMENTOS]")
        
        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)
        If dr.Read Then
            idMax = dr("ID").ToString()
        End If
        Return idMax
    End Function
End Class
