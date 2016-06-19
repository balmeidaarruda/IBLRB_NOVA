Imports System.Text
Imports System.Data.SqlClient
Public Class MapeadorDeCidades

    Public Function getListaCidades(ByVal uf As String) As System.Collections.Generic.List(Of Cidade)
        Dim listaCidades As List(Of Cidade) = New List(Of Cidade)
        Dim SQL As New StringBuilder

        SQL.AppendLine("SELECT * FROM IBLR_CIDADES ")
        SQL.AppendLine("WHERE UF ='" & uf & "'")

        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, SQL.ToString)

        While dr.Read

            Dim cid As Cidade = New Cidade

            cid.Id = dr("id").ToString
            cid.Descricao = dr("cidade").ToString
            cid.UF = dr("Uf").ToString
            listaCidades.Add(cid)

        End While

        Return listaCidades

    End Function

    Public Function getListaUf() As System.Collections.Generic.List(Of Cidade)
        Dim listaUF As List(Of Cidade) = New List(Of Cidade)
        Dim SQL As New StringBuilder

        SQL.AppendLine("SELECT * FROM IBLR_ESTADOS ")

        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, SQL.ToString)

        While dr.Read

            Dim cid As Cidade = New Cidade

            cid.Id = dr("id").ToString
            cid.UF = dr("UF").ToString
            listaUF.Add(cid)

        End While

        Return listaUF

    End Function



End Class
