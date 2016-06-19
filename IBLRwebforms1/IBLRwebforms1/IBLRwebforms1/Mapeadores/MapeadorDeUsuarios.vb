Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports IBLRwebforms1.Usuario


Public Class MapeadorDeUsuarios

    Public Sub Inserir(ByVal usuario As Usuario)
        Dim SQL As StringBuilder = New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        Dim trasacao As SqlTransaction = conn.BeginTransaction
        Dim comando As SqlCommand = conn.CreateCommand
        comando.Transaction = trasacao
        Try

            SQL.AppendLine("INSERT INTO [IBLR_USUARIOS]")
            SQL.AppendLine("           ([USUARIO]")
            SQL.AppendLine("           ,[SENHA]")
            SQL.AppendLine("           ,[EMAIL]")
            SQL.AppendLine("           ,[ATIVO]")
            SQL.AppendLine("           ,[DATA_CADASTRO])")
            SQL.AppendLine("     VALUES")
            SQL.AppendLine("           ('" + usuario.Usuario + "'")
            SQL.AppendLine("           ,'" + usuario.Senha + "'")
            SQL.AppendLine("           ,'" + usuario.Email + "'")
            SQL.AppendLine("           ,'" + usuario.Ativo + "'")
            SQL.AppendLine("           ,convert(datetime,'" + Date.Now + "',103))")

            comando.CommandText = SQL.ToString()
            comando.ExecuteNonQuery()
            trasacao.Commit()
        Catch ex As Exception
            trasacao.Rollback()
            Throw (ex)
        End Try
    End Sub

    Function UsuarioExiste(ByVal usuario As Usuario) As Boolean
        Dim id As String = Nothing
        Dim SQL As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR()
        Dim dr As SqlDataReader

        Try
            SQL.AppendLine("SELECT Id FROM IBLR_USUARIOS WHERE USUARIO='" + usuario.Usuario + "'")
            dr = ConexaoDB.ExecuteSelectSQL(conn, SQL.ToString)
            While dr.Read
                id = dr("Id").ToString
            End While

            If id = Nothing Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Login(ByVal usuario As Usuario) As Boolean

        Dim id As String = Nothing
        Dim SQL As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR()
        Dim dr As SqlDataReader

        Try
            SQL.AppendLine("SELECT Id FROM IBLR_USUARIOS WHERE USUARIO='" + usuario.Usuario + "' and senha='" + usuario.Senha + "'")
            dr = ConexaoDB.ExecuteSelectSQL(conn, SQL.ToString)
            While dr.Read
                id = dr("Id").ToString
            End While

            If id = Nothing Then
                Return False
            Else
                Return True


            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Class
