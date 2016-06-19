Imports Microsoft.VisualBasic
Imports System.Data.SqlClient



    Public Class ConexaoDB
    ''' <summary>
    ''' Abre uma conexao passada por parâmetro
    ''' </summary>
    ''' <param name="conexao"></param>
    ''' <remarks></remarks>
    Private Shared Sub OpenConnection(ByVal conexao As SqlConnection)
        Try
            conexao.Open()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    ''' <summary>
    '''  Retorna uma conexão com o banco de dados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function RetornaConexaoIBLR() As SqlConnection
        Dim conexao As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("IBLR").ConnectionString)
        OpenConnection(conexao)
        Return conexao
    End Function

    ''' <summary>
    ''' executa uma instrução SQL retonando um ExecuteReader.   
    ''' </summary>
    ''' <param name="conexao"></param>
    ''' <param name="SQL"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteSelectSQL(ByVal conexao As SqlConnection, ByVal SQL As String) As SqlDataReader
        Dim comando As SqlCommand = conexao.CreateCommand

        Try
            comando.CommandTimeout = 900
            comando.CommandText = SQL
            Return comando.ExecuteReader()
        Finally
            comando.Dispose()
        End Try
    End Function

    Public Shared Function ExecuteSQL(ByVal conexao As SqlConnection, ByVal SQL As String) As Integer
        Dim comando As SqlCommand = conexao.CreateCommand
        Try
            comando.CommandText = SQL
            Return comando.ExecuteNonQuery()
        Finally
            comando.Dispose()
        End Try
    End Function
    End Class
