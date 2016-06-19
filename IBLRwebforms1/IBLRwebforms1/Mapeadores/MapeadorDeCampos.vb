Imports System.Text
Imports System.Data.SqlClient
Public Class MapeadorDeCampos

    Public Sub Salvar(ByVal _campo As Campo)
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        sql.AppendLine("INSERT INTO [IBLR].[dbo].[IBLR_CAMPOS]")
        sql.AppendLine("           ([NOME]")
        sql.AppendLine("           ,[PASTOR_RESPONSAVEL]")
        sql.AppendLine("           ,[ATIVO])")
        sql.AppendLine("     VALUES")
        sql.AppendLine("           ('" & _campo.Nome & "'")
        sql.AppendLine("           ,'" & _campo.PastorResponsavel & "'")
        sql.AppendLine("           ,'" & _campo.Ativo & "')")

        ConexaoDB.ExecuteSQL(conn, sql.ToString)
    End Sub

    Public Sub Atualizar(ByVal _campos As Campo)
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        sql.AppendLine("UPDATE [IBLR].[dbo].[IBLR_CAMPOS]")
        sql.AppendLine("   SET [NOME] = '" & _campos.Nome & "'")
        sql.AppendLine("      ,[PASTOR_RESPONSAVEL] = '" & _campos.PastorResponsavel & "'")
        sql.AppendLine("      ,[ATIVO] = '" & _campos.Ativo & "'")
        sql.AppendLine(" WHERE ID = '" & _campos.Id & "'")

        ConexaoDB.ExecuteSQL(conn, sql.ToString)

    End Sub

    Public Function getCampoId(ByVal id As String) As Campo
        Dim _campo As New Campo
        Dim sql As New StringBuilder

        sql.AppendLine("SELECT [ID]")
        sql.AppendLine("      ,[NOME]")
        sql.AppendLine("      ,[PASTOR_RESPONSAVEL]")
        sql.AppendLine("      ,[ATIVO]")
        sql.AppendLine("  FROM [IBLR].[dbo].[IBLR_CAMPOS]")
        sql.AppendLine("  WHERE ID='" & id & "'")

        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)

        While dr.Read
            _campo.Id = dr("ID").ToString
            _campo.Nome = dr("NOME").ToString
            _campo.PastorResponsavel = dr("PASTOR_RESPONSAVEL").ToString
            _campo.Ativo = dr("ATIVO")
        End While

        Return _campo
    End Function

    Public Function getCampoUltimoInsert() As String

        Dim sql As New StringBuilder

        sql.AppendLine("SELECT max(ID) as ID")
        sql.AppendLine("  FROM [IBLR].[dbo].[IBLR_CAMPOS]")

        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)
        Dim maxID As String = ""
        If dr.Read Then
            maxID = dr("ID").ToString
        Else
        End If

        Return maxID
    End Function

End Class
