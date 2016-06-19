Imports System.Text
Imports System.Data.SqlClient

Public Class MapeadorDeCargos
    Public Sub Salvar(ByVal _cargo As Cargo)

        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        sql.AppendLine("INSERT INTO [IBLR].[dbo].[IBLR_CARGOS]")
        sql.AppendLine("           ([DESCRICAO]")
        sql.AppendLine("           ,[ATIVO])")
        sql.AppendLine("     VALUES")
        sql.AppendLine("            ('" & _cargo.Descricao & "'")
        sql.AppendLine("           ,'" & _cargo.Ativo & "')")

        ConexaoDB.ExecuteSQL(conn, sql.ToString)

    End Sub

    Public Sub Atualizar(ByVal _cargo As Cargo)

        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        sql.AppendLine("UPDATE [IBLR].[dbo].[IBLR_CARGOS]")
        sql.AppendLine("   SET [DESCRICAO] = '" & _cargo.Descricao & "'")
        sql.AppendLine("      ,[ATIVO] = '" & _cargo.Ativo & "'")
        sql.AppendLine(" WHERE ID='" & _cargo.Id & "'")

        ConexaoDB.ExecuteSQL(conn, sql.ToString)

    End Sub

    Public Function getListaCargos(ByVal desc As String) As List(Of Cargo)
        Dim _listaCargos As New List(Of Cargo)

        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        Dim dr As SqlDataReader

        sql.AppendLine("SELECT [ID]")
        sql.AppendLine("      ,[DESCRICAO]")
        sql.AppendLine("      ,[ATIVO]")
        sql.AppendLine("  FROM [IBLR].[dbo].[IBLR_CARGOS]")
        If Not desc = String.Empty Then
            sql.AppendLine("WHERE DESCRICAO LIKE '%" & desc & "%'")
            sql.AppendLine("ORDER BY DESCRICAO ASC")
        Else
            sql.AppendLine("ORDER BY ID DESC")
        End If

        dr = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)

        While dr.Read
            Dim _cargo As New Cargo
            _cargo.Id = dr("ID").ToString
            _cargo.Descricao = dr("DESCRICAO").ToString
            _cargo.Ativo = dr("ATIVO").ToString
            _listaCargos.Add(_cargo)
        End While


        Return _listaCargos

    End Function

    Public Function getCargoID(ByVal id As String) As Cargo
        Dim _cargo As New Cargo

        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        Dim dr As SqlDataReader

        sql.AppendLine("SELECT [ID]")
        sql.AppendLine("      ,[DESCRICAO]")
        sql.AppendLine("      ,[ATIVO]")
        sql.AppendLine("  FROM [IBLR].[dbo].[IBLR_CARGOS]")
        sql.AppendLine("  WHERE ID='" & id & "'")

        dr = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)

        While dr.Read
            _cargo.Id = dr("ID").ToString
            _cargo.Descricao = dr("DESCRICAO").ToString
            _cargo.Ativo = dr("ATIVO").ToString
        End While

        Return _cargo
    End Function

End Class
