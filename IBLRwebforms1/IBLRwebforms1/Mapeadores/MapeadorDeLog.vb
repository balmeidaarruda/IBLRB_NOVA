Imports System.Text
Imports System.Data.SqlClient
Public Class MapeadorDeLog

    Public Sub GravarLogs(ByVal log As Log)

        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        sql.AppendLine("INSERT INTO [IBLR].[dbo].[IBLR_LOG]")
        sql.AppendLine("           ([FUNCAO]")
        sql.AppendLine("           ,[ACAO]")
        sql.AppendLine("           ,[VALOR]")
        sql.AppendLine("           ,[MODIFICADO_POR]")
        sql.AppendLine("           ,[DATA_ALTERACAO])")
        sql.AppendLine("     VALUES")
        sql.AppendLine("           ('" + log.Funcao + "'")
        sql.AppendLine("           ,'" + log.Acao + "'")
        sql.AppendLine("           ,'" + log.Valor + "'")
        sql.AppendLine("           ,'" + log.ModificadoPor + "'")
        sql.AppendLine("           ,convert(datetime,'" & log.DataAlteracao & "',103))")

        ConexaoDB.ExecuteSQL(conn, sql.ToString)

    End Sub

End Class
