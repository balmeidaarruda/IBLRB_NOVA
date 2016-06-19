Imports System.Text
Imports System.Data.SqlClient
Public Class MapeadorFinanceiro
#Region "Dízimos"
    Public Sub SalvarDizimo(ByVal _listaDizimo As List(Of Dizimo))
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        For x As Integer = 0 To _listaDizimo.Count - 1
            sql = New StringBuilder
            sql.AppendLine("INSERT INTO [IBLR].[dbo].[IBLR_DIZIMOS]")
            sql.AppendLine("           ([ID_MEMBRO]")
            sql.AppendLine("           ,[VALOR_DIZIMO]")
            sql.AppendLine("           ,[DATA_LANCAMENTO])")
            sql.AppendLine("     VALUES")
            sql.AppendLine("           ('" & _listaDizimo.Item(x).Membro.ID & "'")
            sql.AppendLine("           ,convert(decimal(18,2),'" & _listaDizimo.Item(x).VALOR_DIZIMO.ToString.Replace(",", ".") & "')")
            sql.AppendLine("           ,convert(date,'" & _listaDizimo.Item(x).DATA_LANCAMENTO & "',103))")

            ConexaoDB.ExecuteSQL(conn, sql.ToString)
        Next

    End Sub

    Public Sub AtualizarDizimo(ByVal _Dizimo As Dizimo)
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        sql.AppendLine("UPDATE [IBLR].[dbo].[IBLR_DIZIMOS]")
        sql.AppendLine("   SET [ID_MEMBRO] = " & _Dizimo.Membro.ID & "")
        sql.AppendLine("      ,[VALOR_DIZIMO] = convert(decimal(18,2),'" & _Dizimo.VALOR_DIZIMO.ToString.Replace(",", ".") & "')")
        sql.AppendLine("      ,[DATA_LANCAMENTO] =convert(date,'" & _Dizimo.DATA_LANCAMENTO & "',103)")
        sql.AppendLine(" WHERE ID = " & _Dizimo.ID & "")

        ConexaoDB.ExecuteSQL(conn, sql.ToString)
    End Sub

    Public Function getListaDizimo(ByVal dataInicio As String, ByVal dataFim As String) As List(Of Dizimo)
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        Dim _listaDizimo As New List(Of Dizimo)
        sql.AppendLine("SELECT D.[ID]")
        sql.AppendLine("      ,[ID_MEMBRO]")
        sql.AppendLine("      ,[VALOR_DIZIMO]")
        sql.AppendLine("      ,[DATA_LANCAMENTO]")
        sql.AppendLine("      ,M.NOME")
        sql.AppendLine("FROM [IBLR].[dbo].[IBLR_DIZIMOS] D")
        sql.AppendLine("  INNER JOIN [IBLR].[dbo].[IBLR_MEMBROS] M ON D.ID_MEMBRO = M.ID")
        sql.AppendLine("  WHERE D.DATA_LANCAMENTO >= CONVERT(date,'" & dataInicio & "',103) AND")
        sql.AppendLine("		D.DATA_LANCAMENTO <= CONVERT(date,'" & dataFim & "',103)")

        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)
        While dr.Read
            Dim _dizimo As New Dizimo
            _dizimo.ID = dr("ID").ToString
            Dim _membro As New Membro
            _membro.ID = dr("ID_MEMBRO").ToString
            _membro.NOME = dr("NOME").ToString
            _dizimo.Membro = _membro
            _dizimo.VALOR_DIZIMO = dr("VALOR_DIZIMO").ToString
            _dizimo.DATA_LANCAMENTO = Format(dr("DATA_LANCAMENTO"), "dd/MM/yyyy")
            _listaDizimo.Add(_dizimo)
        End While
        Return _listaDizimo
    End Function
    Public Function getDizimoID(ByVal idDizimo As String) As Dizimo

        Dim _dizimo As New Dizimo
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR()

        sql.AppendLine("SELECT D.[ID]")
        sql.AppendLine("      ,[ID_MEMBRO]")
        sql.AppendLine("      ,[VALOR_DIZIMO]")
        sql.AppendLine("      ,[DATA_LANCAMENTO]")
        sql.AppendLine("      ,M.NOME")
        sql.AppendLine("FROM [IBLR].[dbo].[IBLR_DIZIMOS] D")
        sql.AppendLine("  INNER JOIN [IBLR].[dbo].[IBLR_MEMBROS] M ON D.ID_MEMBRO = M.ID")
        sql.AppendLine("  WHERE D.ID=" & idDizimo)

        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)
        While dr.Read
            _dizimo = New Dizimo
            _dizimo.ID = dr("ID").ToString
            Dim _membro As New Membro
            _membro.ID = dr("ID_MEMBRO").ToString
            _membro.NOME = dr("NOME").ToString
            _dizimo.Membro = _membro
            _dizimo.VALOR_DIZIMO = dr("VALOR_DIZIMO").ToString
            _dizimo.DATA_LANCAMENTO = Format(dr("DATA_LANCAMENTO"), "dd/MM/yyyy")
        End While

        Return _dizimo
    End Function
#End Region

#Region "Ofertas"
    Public Function getListaTipoOfertas() As List(Of TipoOferta)
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR()
        Dim _listaTipoOfertas As New List(Of TipoOferta)

        sql.AppendLine("SELECT [ID]")
        sql.AppendLine("      ,[DESCRICAO]")
        sql.AppendLine("      ,[ATIVO]")
        sql.AppendLine("  FROM [IBLR].[dbo].[IBLR_TIPO_OFERTA]")

        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)
        While dr.Read
            Dim _tipoOferta As New TipoOferta

            _tipoOferta.ID = dr("ID").ToString
            _tipoOferta.Descricao = dr("DESCRICAO").ToString
            _tipoOferta.Ativo = dr("ATIVO").ToString
            _listaTipoOfertas.Add(_tipoOferta)

        End While

        Return _listaTipoOfertas

    End Function

    Public Sub SalvarOferta(ByVal _listaOfertas As List(Of Oferta))
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        For x As Integer = 0 To _listaOfertas.Count - 1
            sql = New StringBuilder
            sql.AppendLine("INSERT INTO [IBLR].[dbo].[IBLR_OFERTAS]")
            sql.AppendLine("           ([ID_TIPO_OFERTA]")
            sql.AppendLine("           ,[ID_CONGREGACAO]")
            sql.AppendLine("           ,[VALOR_OFERTA]")
            sql.AppendLine("           ,[DATA_LANCAMENTO])")
            sql.AppendLine("     VALUES")
            sql.AppendLine("           ('" & _listaOfertas(x).TipoOferta.ID & "'")
            sql.AppendLine("           ,'" & _listaOfertas(x).Congregacao.Id & "'")
            sql.AppendLine("           ,convert(decimal(18,2),'" & _listaOfertas.Item(x).ValorOferta.ToString.Replace(",", ".") & "')")
            sql.AppendLine("           ,convert(date,'" & _listaOfertas.Item(x).DataLancamento & "',103))")

            ConexaoDB.ExecuteSQL(conn, sql.ToString)

        Next
    End Sub

    Public Sub AtualizarOferta(ByVal _Oferta As Oferta)

        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        sql.AppendLine("UPDATE [IBLR].[dbo].[IBLR_OFERTAS]")
        sql.AppendLine("   SET [ID_TIPO_OFERTA] =" & _Oferta.TipoOferta.ID)
        sql.AppendLine("      ,[ID_CONGREGACAO] =" & _Oferta.Congregacao.Id)
        sql.AppendLine("      ,[VALOR_OFERTA] = convert(decimal(18,2),'" & _Oferta.ValorOferta.ToString.Replace(",", ".") & "')")
        sql.AppendLine("      ,[DATA_LANCAMENTO] = convert(date,'" & _Oferta.DataLancamento & "',103)")
        sql.AppendLine(" WHERE ID=" & _Oferta.ID & "")

        ConexaoDB.ExecuteSQL(conn, sql.ToString)
    End Sub
    Public Function getListaOfertas(ByVal dataInicio As String, ByVal dataFim As String, Optional idCongregacao As String = "")
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        sql.AppendLine("SELECT O.[ID]")
        sql.AppendLine("      ,[ID_TIPO_OFERTA]")
        sql.AppendLine("      ,[ID_CONGREGACAO]")
        sql.AppendLine("      ,C.[NOME]")
        sql.AppendLine("      ,[VALOR_OFERTA]")
        sql.AppendLine("      ,[DATA_LANCAMENTO]")
        sql.AppendLine("      ,T.[ID] AS ID_TIPO_OFERTA")
        sql.AppendLine("      ,T.[DESCRICAO]")
        sql.AppendLine("  FROM [IBLR].[dbo].[IBLR_OFERTAS] O")
        sql.AppendLine("    INNER JOIN [IBLR].[dbo].[IBLR_CONGREGACOES] C ON O.ID_CONGREGACAO = C.ID")
        sql.AppendLine("    INNER JOIN [IBLR].[dbo].[IBLR_TIPO_OFERTA] T ON O.ID_TIPO_OFERTA = T.ID")
        sql.AppendLine("    WHERE O.DATA_LANCAMENTO >= CONVERT(date,'" & dataInicio & "',103) AND")
        sql.AppendLine("          O.DATA_LANCAMENTO <= CONVERT(date,'" & dataFim & "',103)")
        If idCongregacao <> String.Empty Then
            sql.AppendLine("          AND ID_CONGREGACAO = " & idCongregacao & "")
        End If

        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)
        Dim _listaOfertas As New List(Of Oferta)
        While dr.Read
            Dim _oferta As New Oferta
            Dim _tipoOferta As New TipoOferta
            Dim _congregacao As New Congregacao

            _oferta.ID = dr("ID").ToString
            _oferta.ValorOferta = dr("VALOR_OFERTA").ToString
            _oferta.DataLancamento = Format(dr("DATA_LANCAMENTO"), "dd/MM/yyyy")
            _tipoOferta.ID = dr("ID_TIPO_OFERTA").ToString
            _tipoOferta.Descricao = dr("DESCRICAO").ToString
            _oferta.TipoOferta = _tipoOferta
            _congregacao.Id = dr("ID_CONGREGACAO").ToString
            _congregacao.Nome = dr("NOME").ToString
            _oferta.Congregacao = _congregacao
            _listaOfertas.Add(_oferta)

        End While
        Return _listaOfertas
    End Function

    Public Function getOfertasID(ByVal idOferta As String) As Oferta
        Dim _Oferta As New Oferta
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        sql.AppendLine("SELECT O.[ID]")
        sql.AppendLine("      ,[ID_TIPO_OFERTA]")
        sql.AppendLine("      ,[ID_CONGREGACAO]")
        sql.AppendLine("      ,C.[NOME]")
        sql.AppendLine("      ,[VALOR_OFERTA]")
        sql.AppendLine("      ,[DATA_LANCAMENTO]")
        sql.AppendLine("      ,T.[ID] AS ID_TIPO_OFERTA")
        sql.AppendLine("      ,T.[DESCRICAO]")
        sql.AppendLine("  FROM [IBLR].[dbo].[IBLR_OFERTAS] O")
        sql.AppendLine("    INNER JOIN [IBLR].[dbo].[IBLR_TIPO_OFERTA] T ON O.ID_TIPO_OFERTA = T.ID")
        sql.AppendLine("    INNER JOIN [IBLR].[dbo].[IBLR_CONGREGACOES] C ON O.ID_CONGREGACAO = C.ID")
        sql.AppendLine("  WHERE O.ID=" & idOferta)

        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)

        While dr.Read
            _Oferta.ID = dr("ID").ToString
            Dim _tipoOferta As New TipoOferta
            Dim _congregacao As New Congregacao
            _tipoOferta.ID = dr("ID_TIPO_OFERTA").ToString
            _tipoOferta.Descricao = dr("DESCRICAO").ToString
            _Oferta.TipoOferta = _tipoOferta
            _Oferta.ValorOferta = dr("VALOR_OFERTA").ToString
            _Oferta.DataLancamento = Format(dr("DATA_LANCAMENTO"), "dd/MM/yyyy")
            _congregacao.Id = dr("ID_CONGREGACAO").ToString
            _congregacao.Nome = dr("NOME").ToString
            _Oferta.Congregacao = _congregacao

        End While

        Return _Oferta

    End Function

#End Region

#Region "Despesas"

    Public Sub SalvarDespesas(ByVal _listaDespesas As List(Of Despesa))
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        For x As Integer = 0 To _listaDespesas.Count - 1
            sql = New StringBuilder
            sql.AppendLine("INSERT INTO [IBLR].[dbo].[IBLR_DESPESAS]")
            sql.AppendLine("           ([ID_TIPO_DESPESA]")
            sql.AppendLine("           ,[ID_CONGREGACAO]")
            sql.AppendLine("           ,[VALOR_DESPESA]")
            sql.AppendLine("           ,[DATA_LANCAMENTO])")
            sql.AppendLine("     VALUES")
            sql.AppendLine("           ('" & _listaDespesas(x).TipoDespesa.Id & "'")
            sql.AppendLine("           ,'" & _listaDespesas(x).Congregacao.Id & "'")
            sql.AppendLine("           ,convert(decimal(18,2),'" & _listaDespesas.Item(x).ValorDespesa.ToString.Replace(",", ".") & "')")
            sql.AppendLine("           ,convert(date,'" & _listaDespesas.Item(x).DataLancamento & "',103))")

            ConexaoDB.ExecuteSQL(conn, sql.ToString)

        Next
    End Sub

    Public Sub AtualizarDespesa(ByVal _despesa As Despesa)
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        sql.AppendLine("UPDATE [IBLR].[dbo].[IBLR_DESPESAS]")
        sql.AppendLine("   SET [ID_TIPO_DESPESA] = " & _despesa.TipoDespesa.Id)
        sql.AppendLine("      ,[ID_CONGREGACAO] = " & _despesa.Congregacao.Id)
        sql.AppendLine("      ,[VALOR_DESPESA] = convert(decimal(18,2),'" & _despesa.ValorDespesa.ToString.Replace(",", ".") & "')")
        sql.AppendLine("      ,[DATA_LANCAMENTO] = convert(date,'" & _despesa.DataLancamento & "',103)")
        sql.AppendLine(" WHERE ID=" & _despesa.Id & "")

        ConexaoDB.ExecuteSQL(conn, sql.ToString)
    End Sub

    Public Function getTipoDespesa() As List(Of TipoDespesa)
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR()
        Dim _listaTipoDespesas As New List(Of TipoDespesa)

        sql.AppendLine("SELECT [ID]")
        sql.AppendLine("      ,[DESCRICAO]")
        sql.AppendLine("      ,[ATIVO]")
        sql.AppendLine("  FROM [IBLR].[dbo].[IBLR_TIPO_DESPESA]")

        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)
        While dr.Read
            Dim _tipoDespesa As New TipoDespesa

            _tipoDespesa.Id = dr("ID").ToString
            _tipoDespesa.Descricao = dr("DESCRICAO").ToString
            _tipoDespesa.Ativo = dr("ATIVO").ToString
            _listaTipoDespesas.Add(_tipoDespesa)

        End While

        Return _listaTipoDespesas
    End Function

    Public Function getTipoOferta() As List(Of TipoOferta)
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR()
        Dim _listaTipoOfertas As New List(Of TipoOferta)

        sql.AppendLine("SELECT [ID]")
        sql.AppendLine("      ,[DESCRICAO]")
        sql.AppendLine("      ,[ATIVO]")
        sql.AppendLine("  FROM [IBLR].[dbo].[IBLR_TIPO_OFERTA]")

        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)
        While dr.Read
            Dim _tipoOferta As New TipoOferta

            _tipoOferta.Id = dr("ID").ToString
            _tipoOferta.Descricao = dr("DESCRICAO").ToString
            _tipoOferta.Ativo = dr("ATIVO").ToString
            _listaTipoOfertas.Add(_tipoOferta)

        End While

        Return _listaTipoOfertas
    End Function

    Public Function getListaDespesas(ByVal dataInicio As String, ByVal dataFim As String, Optional idCongregacao As String = "") As List(Of Despesa)
        Dim _listaDespesas As New List(Of Despesa)
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR()

        sql.AppendLine("SELECT D.[ID]")
        sql.AppendLine("      ,[ID_TIPO_DESPESA]")
        sql.AppendLine("      ,TP.[DESCRICAO]")
        sql.AppendLine("      ,[ID_CONGREGACAO]")
        sql.AppendLine("      ,C.[NOME]")
        sql.AppendLine("      ,[VALOR_DESPESA]")
        sql.AppendLine("      ,[DATA_LANCAMENTO]")
        sql.AppendLine("  FROM [IBLR].[dbo].[IBLR_DESPESAS] D")
        sql.AppendLine("  INNER JOIN [IBLR].[dbo].[IBLR_CONGREGACOES] C ON D.ID_CONGREGACAO = C.ID")
        sql.AppendLine("  INNER JOIN [IBLR].[dbo].[IBLR_TIPO_DESPESA] TP ON D.ID_TIPO_DESPESA = TP.ID")
        sql.AppendLine("    WHERE D.DATA_LANCAMENTO >= CONVERT(date,'" & dataInicio & "',103) AND")
        sql.AppendLine("          D.DATA_LANCAMENTO <= CONVERT(date,'" & dataFim & "',103)")
        If idCongregacao <> String.Empty Then
            sql.AppendLine("          AND ID_CONGREGACAO = " & idCongregacao & "")
        End If

        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)

        While dr.Read
            Dim _despesa As New Despesa

            _despesa.Id = dr("ID").ToString
            Dim _tipoDespesa As New TipoDespesa
            _tipoDespesa.Id = dr("ID_TIPO_DESPESA").ToString
            _tipoDespesa.Descricao = dr("DESCRICAO").ToString
            Dim _congregacao As New Congregacao
            _congregacao.Id = dr("ID_CONGREGACAO").ToString
            _congregacao.Nome = dr("NOME").ToString
            _despesa.TipoDespesa = _tipoDespesa
            _despesa.Congregacao = _congregacao
            _despesa.DataLancamento = Format(dr("DATA_LANCAMENTO"), "dd/MM/yyyy")
            _despesa.ValorDespesa = dr("VALOR_DESPESA").ToString
            _listaDespesas.Add(_despesa)

        End While
        Return _listaDespesas
    End Function

    Public Function getDespesaID(ByVal idDespesa As String) As Despesa
        Dim _despesa As New Despesa
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR()

        sql.AppendLine("SELECT D.[ID]")
        sql.AppendLine("      ,[ID_TIPO_DESPESA]")
        sql.AppendLine("      ,TP.[DESCRICAO]")
        sql.AppendLine("      ,[ID_CONGREGACAO]")
        sql.AppendLine("      ,C.[NOME]")
        sql.AppendLine("      ,[VALOR_DESPESA]")
        sql.AppendLine("      ,[DATA_LANCAMENTO]")
        sql.AppendLine("  FROM [IBLR].[dbo].[IBLR_DESPESAS] D")
        sql.AppendLine("  INNER JOIN [IBLR].[dbo].[IBLR_CONGREGACOES] C ON D.ID_CONGREGACAO = C.ID")
        sql.AppendLine("  INNER JOIN [IBLR].[dbo].[IBLR_TIPO_DESPESA] TP ON D.ID_TIPO_DESPESA = TP.ID")
        sql.AppendLine("    WHERE D.ID=" & idDespesa)

        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)

        While dr.Read
            
            _despesa.Id = dr("ID").ToString
            Dim _tipoDespesa As New TipoDespesa
            _tipoDespesa.Id = dr("ID_TIPO_DESPESA").ToString
            _tipoDespesa.Descricao = dr("DESCRICAO").ToString
            Dim _congregacao As New Congregacao
            _congregacao.Id = dr("ID_CONGREGACAO").ToString
            _congregacao.Nome = dr("NOME").ToString
            _despesa.TipoDespesa = _tipoDespesa
            _despesa.Congregacao = _congregacao
            _despesa.DataLancamento = Format(dr("DATA_LANCAMENTO"), "dd/MM/yyyy")
            _despesa.ValorDespesa = dr("VALOR_DESPESA").ToString

        End While

        Return _despesa
    End Function

    Public Sub ExcluirTipoDespesa(ByVal idDespesa As String)
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        sql.AppendLine("UPDATE [IBLR].[dbo].[IBLR_TIPO_DESPESA] SET [ATIVO] = 0 WHERE ID='" & idDespesa & "'")
        ConexaoDB.ExecuteSQL(conn, sql.ToString)
    End Sub

    Public Sub SalvarTipoDespesa(ByVal tpDespesa As TipoDespesa)
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        sql.AppendLine("INSERT INTO [IBLR].[dbo].[IBLR_TIPO_DESPESA]")
        sql.AppendLine("           ([DESCRICAO]")
        sql.AppendLine("           ,[ATIVO])")
        sql.AppendLine("     VALUES")
        sql.AppendLine("           ('" & tpDespesa.Descricao & "'")
        sql.AppendLine("           ,1)")

        ConexaoDB.ExecuteSQL(conn, sql.ToString)

    End Sub

    Public Sub AtualizarTipoDespesa(ByVal tpDespesa As TipoDespesa)
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        sql.AppendLine("UPDATE [IBLR].[dbo].[IBLR_TIPO_DESPESA]")
        sql.AppendLine("   SET [DESCRICAO] = '" & tpDespesa.Descricao & "'")
        sql.AppendLine("      ,[ATIVO] = " & If(tpDespesa.Ativo = "True", "'1'", "'0'") & "")
        sql.AppendLine(" WHERE ID=" & tpDespesa.Id & "")

        ConexaoDB.ExecuteSQL(conn, sql.ToString)

    End Sub

    Public Sub ExcluirTipoOferta(ByVal idOferta As String)
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        sql.AppendLine("UPDATE [IBLR].[dbo].[IBLR_TIPO_OFERTA] SET [ATIVO] = 0 WHERE ID='" & idOferta & "'")
        ConexaoDB.ExecuteSQL(conn, sql.ToString)
    End Sub

    Public Sub SalvarTipoOferta(ByVal tpOferta As TipoOferta)
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        sql.AppendLine("INSERT INTO [IBLR].[dbo].[IBLR_TIPO_OFERTA]")
        sql.AppendLine("           ([DESCRICAO]")
        sql.AppendLine("           ,[ATIVO])")
        sql.AppendLine("     VALUES")
        sql.AppendLine("           ('" & tpOferta.Descricao & "'")
        sql.AppendLine("           ,1)")

        ConexaoDB.ExecuteSQL(conn, sql.ToString)

    End Sub

    Public Sub AtualizarTipoOferta(ByVal tpOferta As TipoOferta)
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        sql.AppendLine("UPDATE [IBLR].[dbo].[IBLR_TIPO_OFERTA]")
        sql.AppendLine("   SET [DESCRICAO] = '" & tpOferta.Descricao & "'")
        sql.AppendLine("      ,[ATIVO] = " & If(tpOferta.Ativo = "True", "'1'", "'0'") & "")
        sql.AppendLine(" WHERE ID=" & tpOferta.Id & "")

        ConexaoDB.ExecuteSQL(conn, sql.ToString)

    End Sub

    Public Function getTipoOfertaUltimoInserido() As String
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        sql.AppendLine("SELECT MAX(ID) AS ID FROM [IBLR].[dbo].[IBLR_TIPO_OFERTA]")

        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)
        Dim idMax As String = ""
        If dr.Read Then
            idMax = dr("ID").ToString
        End If
        Return idMax
    End Function

    Public Function getTipoDespesaUltimoInserido() As String
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        sql.AppendLine("SELECT MAX(ID) AS ID FROM [IBLR].[dbo].[IBLR_TIPO_DESPESA]")

        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, sql.ToString)
        Dim idMax As String = ""
        If dr.Read Then
            idMax = dr("ID").ToString
        End If
        Return idMax
    End Function
#End Region




End Class
