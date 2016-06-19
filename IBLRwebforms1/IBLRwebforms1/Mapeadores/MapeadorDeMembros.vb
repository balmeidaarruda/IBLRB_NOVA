Imports System.Text
Imports System.Data.SqlClient
Public Class MapeadorDeMembros

    Public Sub Salvar(ByVal _Membro As Membro)
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        sql.AppendLine("INSERT INTO [IBLR].[dbo].[IBLR_MEMBROS]")
        sql.AppendLine("           ([NOME]")
        sql.AppendLine("           ,[APELIDO]")
        sql.AppendLine("           ,[RG]")
        sql.AppendLine("           ,[CPF]")
        sql.AppendLine("           ,[DATA_NASCIMENTO]")
        sql.AppendLine("           ,[ESTADO_CIVIL]")
        sql.AppendLine("           ,[SEXO]")
        sql.AppendLine("           ,[NATURALIDADE]")
        sql.AppendLine("           ,[ENDERECO]")
        sql.AppendLine("           ,[BAIRRO]")
        sql.AppendLine("           ,[CEP]")
        sql.AppendLine("           ,[NOME_PAI]")
        sql.AppendLine("           ,[NOME_MAE]")
        sql.AppendLine("           ,[CONJUGUE]")
        sql.AppendLine("           ,[DATA_ADMISSAO]")
        sql.AppendLine("           ,[DATA_BATISMO]")
        sql.AppendLine("           ,[DATA_CONSAGRACAO]")
        sql.AppendLine("           ,[CREDENCIAL]")
        sql.AppendLine("           ,[NUMERO_CREDENCIAL]")
        sql.AppendLine("           ,[ID_CARGO]")
        sql.AppendLine("           ,[ID_CIDADE]")
        sql.AppendLine("           ,[ID_CONGREGACAO]")
        sql.AppendLine("           ,[ID_DEPARTAMENTO]")
        sql.AppendLine("           ,[ULTIMA_ALTERACAO]")
        sql.AppendLine("           ,[DATA_CADASTRO]")
        sql.AppendLine("           ,[ATIVO])")
        sql.AppendLine("     VALUES")
        sql.AppendLine("           ('" & _Membro.NOME & "'")
        sql.AppendLine("           ,'" & _Membro.APELIDO & "'")
        sql.AppendLine("           ,'" & _Membro.RG & "'")
        sql.AppendLine("           ,'" & _Membro.CPF & "'")
        sql.AppendLine("           ,convert(datetime,'" & _Membro.DATA_NASCIMENTO & "',103)")
        sql.AppendLine("           ,'" & _Membro.ESTADO_CIVIL & "'")
        sql.AppendLine("           ,'" & _Membro.SEXO & "'")
        sql.AppendLine("           ,'" & _Membro.NATURALIDADE.Id & "'")
        sql.AppendLine("           ,'" & _Membro.ENDERECO & "'")
        sql.AppendLine("           ,'" & _Membro.BAIRRO & "'")
        sql.AppendLine("           ,'" & _Membro.CEP.Replace(".", "").Replace("-", "") & "'")
        sql.AppendLine("           ,'" & _Membro.NOME_PAI & "'")
        sql.AppendLine("           ,'" & _Membro.NOME_MAE & "'")
        sql.AppendLine("           ,'" & _Membro.CONJUGUE & "'")
        If _Membro.DATA_ADMISSAO = String.Empty Then
            sql.AppendLine("           ,'" & String.Empty & "'")
        Else
            sql.AppendLine("           ,convert(datetime,'" & _Membro.DATA_ADMISSAO & "',103)")
        End If

        If _Membro.DATA_BATISMO = String.Empty Then
            sql.AppendLine("           ,'" & String.Empty & "'")
        Else
            sql.AppendLine("           ,convert(datetime,'" & _Membro.DATA_BATISMO & "',103)")
        End If

        If _Membro.DATA_CONSAGRACAO = String.Empty Then
            sql.AppendLine("           ,'" & String.Empty & "'")
        Else
            sql.AppendLine("           ,convert(datetime,'" & _Membro.DATA_CONSAGRACAO & "',103)")
        End If

        If _Membro.CREDENCIAL = "S" Then
            sql.AppendLine("           ,1")
        Else
            sql.AppendLine("           ,0")
        End If

        sql.AppendLine("           ,'" & _Membro.NUMERO_CREDENCIAL & "'")
        sql.AppendLine("           ,'" & _Membro.CARGO.Id & "'")
        sql.AppendLine("           ,'" & _Membro.CIDADE.Id & "'")
        sql.AppendLine("           ,'" & _Membro.CONGREGACAO.Id & "'")
        sql.AppendLine("           ,'" & _Membro.DEPARTAMENTO.Id & "'")
        sql.AppendLine("           ,convert(datetime,'" & DateAndTime.Now & "',103)")
        sql.AppendLine("           ,convert(datetime,'" & DateAndTime.Now & "',103)")
        If _Membro.ATIVO Then
            sql.AppendLine("           ,'1')")
        Else
            sql.AppendLine("           ,'0')")

        End If

        ConexaoDB.ExecuteSQL(conn, sql.ToString)

        If Not IsNothing(_Membro.LISTACONTATOS) Then
            InserirContatos(_Membro.LISTACONTATOS, True)
        End If

    End Sub

    Public Sub Atualizar(ByVal _Membro As Membro)
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        sql.AppendLine("UPDATE [IBLR].[dbo].[IBLR_MEMBROS]")
        sql.AppendLine("   SET [NOME] = '" & _Membro.NOME & "'")
        sql.AppendLine("      ,[APELIDO] = '" & _Membro.APELIDO & "'")
        sql.AppendLine("      ,[RG] = '" & _Membro.RG & "'")
        sql.AppendLine("      ,[CPF] = '" & _Membro.CPF & "'")
        sql.AppendLine("      ,[DATA_NASCIMENTO] = convert(datetime,'" & _Membro.DATA_NASCIMENTO & "',103)")
        sql.AppendLine("      ,[ESTADO_CIVIL] = '" & _Membro.ESTADO_CIVIL & "'")
        sql.AppendLine("      ,[SEXO] = '" & _Membro.SEXO & "'")
        sql.AppendLine("      ,[NATURALIDADE] = '" & _Membro.NATURALIDADE.Id & "'")
        sql.AppendLine("      ,[ENDERECO] = '" & _Membro.ENDERECO & "'")
        sql.AppendLine("      ,[BAIRRO] = '" & _Membro.BAIRRO & "'")
        sql.AppendLine("      ,[CEP] = '" & _Membro.CEP & "'")
        sql.AppendLine("      ,[NOME_PAI] = '" & _Membro.NOME_PAI & "'")
        sql.AppendLine("      ,[NOME_MAE] = '" & _Membro.NOME_MAE & "'")
        sql.AppendLine("      ,[CONJUGUE] = '" & _Membro.CONJUGUE & "'")
        sql.AppendLine("      ,[DATA_ADMISSAO] = convert(datetime,'" & _Membro.DATA_ADMISSAO & "',103)")
        sql.AppendLine("      ,[DATA_BATISMO] = convert(datetime,'" & _Membro.DATA_BATISMO & "',103)")
        sql.AppendLine("      ,[DATA_CONSAGRACAO] = convert(datetime,'" & _Membro.DATA_CONSAGRACAO & "',103)")
        If _Membro.CREDENCIAL = "S" Then
            sql.AppendLine("      ,[CREDENCIAL] = 1")
        Else
            sql.AppendLine("      ,[CREDENCIAL] = 0")
        End If

        sql.AppendLine("      ,[NUMERO_CREDENCIAL] = '" & _Membro.NUMERO_CREDENCIAL & "'")
        sql.AppendLine("      ,[ID_CARGO] = '" & _Membro.CARGO.Id & "'")
        sql.AppendLine("      ,[ID_CIDADE] = '" & _Membro.CIDADE.Id & "'")
        sql.AppendLine("      ,[ID_CONGREGACAO] = '" & _Membro.CONGREGACAO.Id & "'")
        sql.AppendLine("      ,[ID_DEPARTAMENTO] = '" & _Membro.DEPARTAMENTO.Id & "'")
        sql.AppendLine("      ,[ULTIMA_ALTERACAO] = convert(datetime,'" & DateAndTime.Now & "',103)")
        If _Membro.ATIVO Then
            sql.AppendLine("      ,[ATIVO] = '1'")
        Else
            sql.AppendLine("      ,[ATIVO] = '0'")
        End If
        sql.AppendLine("WHERE ID='" & _Membro.ID & "'")

        sql.AppendLine("DELETE FROM IBLR_CONTATOS_MEMBROS WHERE ID IN(SELECT ID_CONTATO_MEMBRO FROM IBLR_MEMBROS_IBLR_CONTATO_MEMBRO WHERE ID_MEMBRO='" & _Membro.ID & "')")

        sql.AppendLine("DELETE FROM [IBLR].[dbo].[IBLR_MEMBROS_IBLR_CONTATO_MEMBRO]")
        sql.AppendLine("      WHERE ID_MEMBRO='" & _Membro.ID & "'")

        ConexaoDB.ExecuteSQL(conn, sql.ToString)

        If Not IsNothing(_Membro.LISTACONTATOS) Then
            InserirContatos(_Membro.LISTACONTATOS, False, _Membro.ID)
        End If


    End Sub

    Public Sub InserirContatos(ByVal _listaContatos As List(Of Contato), ByVal _contatoNovo As Boolean, Optional _idMembro As String = "")
        Dim sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        

        For x As Integer = 0 To _listaContatos.Count - 1
            sql = New StringBuilder
            sql.AppendLine("INSERT INTO [IBLR].[dbo].[IBLR_CONTATOS_MEMBROS]")
            sql.AppendLine("           ([TIPO_CONTATO]")
            sql.AppendLine("           ,[DESCRICAO_CONTATO])")
            sql.AppendLine("     VALUES")
            sql.AppendLine("           ('" & _listaContatos(x).DescricaoTipoContato & "'")
            sql.AppendLine("           ,'" & _listaContatos(x).Descricao & "')")

            sql.AppendLine("Declare @IdMembro AS INT")
            If _contatoNovo Then
                sql.AppendLine("Set @IdMembro = (SELECT max(Id) FROM IBLR_MEMBROS)")
            Else
                sql.AppendLine("Set @IdMembro = '" & _idMembro & "'")
            End If

            sql.AppendLine("Declare @IdContato AS INT")
            sql.AppendLine("Set @IdContato = (SELECT max(Id) FROM IBLR_CONTATOS_MEMBROS)")

            sql.AppendLine("INSERT INTO [IBLR].[dbo].[IBLR_MEMBROS_IBLR_CONTATO_MEMBRO]")
            sql.AppendLine("           ([ID_MEMBRO]")
            sql.AppendLine("           ,[ID_CONTATO_MEMBRO])")
            sql.AppendLine("     VALUES")
            sql.AppendLine("           (@IdMembro")
            sql.AppendLine("           ,@IdContato)")

            ConexaoDB.ExecuteSQL(conn, sql.ToString)
        Next

    End Sub

    Public Function getListaMembros(ByVal _nomeMembro As String, Optional _idMembro As String = "") As List(Of Membro)

        Dim listaMembro As New List(Of Membro)
        Dim Sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR

        Sql.AppendLine("SELECT [M].[ID]")
        Sql.AppendLine("      ,[M].[NOME]")
        Sql.AppendLine("      ,[APELIDO]")
        Sql.AppendLine("      ,[RG]")
        Sql.AppendLine("      ,[CPF]")
        Sql.AppendLine("      ,[DATA_NASCIMENTO]")
        Sql.AppendLine("      ,[ESTADO_CIVIL]")
        Sql.AppendLine("      ,[SEXO]")
        Sql.AppendLine("      ,[NATURALIDADE]")
        Sql.AppendLine("      ,[CI2].CIDADE AS CIDADE_NATURALIDADE")
        Sql.AppendLine("      ,[CI2].ESTADO_ID AS CIDADE_NATURALIDADE_UF_ID")
        Sql.AppendLine("      ,[CI2].UF AS CIDADE_NATURALIDADE_UF")
        Sql.AppendLine("      ,[M].[ENDERECO] AS ENDERECO_MEMBRO")
        Sql.AppendLine("      ,[M].[BAIRRO] AS BAIRRO_MEMBRO")
        Sql.AppendLine("      ,[M].[CEP] AS CEP_MEMBRO")
        Sql.AppendLine("      ,[NOME_PAI]")
        Sql.AppendLine("      ,[NOME_MAE]")
        Sql.AppendLine("      ,[CONJUGUE]")
        Sql.AppendLine("      ,[DATA_ADMISSAO]")
        Sql.AppendLine("      ,[DATA_BATISMO]")
        Sql.AppendLine("      ,[DATA_CONSAGRACAO]")
        Sql.AppendLine("      ,[CREDENCIAL]")
        Sql.AppendLine("      ,[NUMERO_CREDENCIAL]")
        Sql.AppendLine("      ,[ID_CARGO]")
        Sql.AppendLine("      ,[C].DESCRICAO AS CARGO")
        Sql.AppendLine("      ,[ID_CIDADE]")
        Sql.AppendLine("      ,[CI].CIDADE AS NOME_CIDADE")
        Sql.AppendLine("      ,[CI].UF AS UF")
        Sql.AppendLine("      ,[CI].ESTADO_ID")
        Sql.AppendLine("      ,[ID_CONGREGACAO]")
        Sql.AppendLine("      ,[ID_DEPARTAMENTO]")
        Sql.AppendLine("      ,[CO].NOME AS CONGREGACAO")
        Sql.AppendLine("      ,[ULTIMA_ALTERACAO]")
        Sql.AppendLine("      ,[M].[DATA_CADASTRO] AS DATA_CADASTRO_MEMBRO")
        Sql.AppendLine("      ,[M].[ATIVO]")
        Sql.AppendLine("  FROM [IBLR].[dbo].[IBLR_MEMBROS] AS M")
        Sql.AppendLine("		  INNER JOIN IBLR_CARGOS AS C ON C.ID=M.ID_CARGO")
        Sql.AppendLine("		  INNER JOIN IBLR_CIDADES CI ON CI.ID= M.ID_CIDADE")
        Sql.AppendLine("		  INNER JOIN IBLR_CIDADES CI2 ON CI2.ID =M.NATURALIDADE")
        Sql.AppendLine("		  INNER JOIN IBLR_ESTADOS ES ON ES.ID =CI.ESTADO_ID")
        Sql.AppendLine("		  INNER JOIN IBLR_CONGREGACOES CO ON CO.ID = M.ID_CONGREGACAO")
        Sql.AppendLine("  WHERE")
        If _idMembro = String.Empty Then
            Sql.AppendLine("		M.NOME LIKE '%" & _nomeMembro & "%'")
        Else
            Sql.AppendLine("		M.ID = '" & _idMembro & "'")
        End If


        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, Sql.ToString)

        While dr.Read

            Dim _membro As New Membro
            _membro.ID = dr("ID").ToString
            _membro.NOME = dr("NOME").ToString
            _membro.APELIDO = dr("APELIDO").ToString
            _membro.RG = dr("RG").ToString
            _membro.CPF = dr("CPF").ToString
            _membro.DATA_NASCIMENTO = dr("DATA_NASCIMENTO").ToString().Remove(10, 9)
            _membro.ESTADO_CIVIL = dr("ESTADO_CIVIL").ToString
            _membro.SEXO = dr("SEXO").ToString
            Dim _NATURALIDADE As New Cidade
            _NATURALIDADE.Id = dr("NATURALIDADE").ToString
            _NATURALIDADE.IdEstado = dr("CIDADE_NATURALIDADE_UF_ID").ToString
            _NATURALIDADE.UF = dr("CIDADE_NATURALIDADE_UF").ToString
            _membro.NATURALIDADE = _NATURALIDADE
            _membro.ENDERECO = dr("ENDERECO_MEMBRO").ToString
            _membro.BAIRRO = dr("BAIRRO_MEMBRO").ToString
            _membro.CEP = dr("CEP_MEMBRO").ToString
            _membro.NOME_PAI = dr("NOME_PAI").ToString
            _membro.NOME_MAE = dr("NOME_MAE").ToString
            _membro.CONJUGUE = dr("CONJUGUE").ToString
            _membro.DATA_ADMISSAO = dr("DATA_ADMISSAO").ToString().Remove(10, 9)
            _membro.DATA_BATISMO = dr("DATA_BATISMO").ToString().Remove(10, 9)
            _membro.DATA_CONSAGRACAO = dr("DATA_CONSAGRACAO").ToString().Remove(10, 9)
            _membro.CREDENCIAL = dr("CREDENCIAL").ToString
            _membro.NUMERO_CREDENCIAL = dr("NUMERO_CREDENCIAL").ToString
            Dim _CARGO As New Cargo
            _CARGO.Id = dr("ID_CARGO").ToString
            _CARGO.Descricao = dr("CARGO").ToString
            _membro.CARGO = _CARGO
            Dim _CIDADE As New Cidade
            _CIDADE.Id = dr("ID_CIDADE").ToString
            _CIDADE.Descricao = dr("NOME_CIDADE").ToString
            _CIDADE.UF = dr("UF").ToString
            _CIDADE.IdEstado = dr("ESTADO_ID").ToString
            _membro.CIDADE = _CIDADE
            Dim _CONGREGACAO As New Congregacao
            _CONGREGACAO.Id = dr("ID_CONGREGACAO").ToString
            _CONGREGACAO.Nome = dr("CONGREGACAO").ToString
            _membro.CONGREGACAO = _CONGREGACAO
            Dim _DEPARTAMENTO As New Departamento
            _DEPARTAMENTO.Id = dr("ID_DEPARTAMENTO").ToString
            _membro.DEPARTAMENTO = _DEPARTAMENTO
            _membro.ULTIMA_ALTERACAO = dr("ULTIMA_ALTERACAO").ToString
            _membro.DATA_CADASTRO = dr("DATA_CADASTRO_MEMBRO").ToString
            _membro.ATIVO = dr("ATIVO").ToString
            listaMembro.Add(_membro)

        End While

        Return listaMembro
    End Function

    Public Function getMembroID(ByVal idMembro As String) As Membro
        Dim Sql As New StringBuilder
        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        Dim _membro As New Membro

        Sql.AppendLine("SELECT [M].[ID]")
        Sql.AppendLine("      ,[M].[NOME]")
        Sql.AppendLine("      ,[APELIDO]")
        Sql.AppendLine("      ,[RG]")
        Sql.AppendLine("      ,[CPF]")
        Sql.AppendLine("      ,[DATA_NASCIMENTO]")
        Sql.AppendLine("      ,[ESTADO_CIVIL]")
        Sql.AppendLine("      ,[SEXO]")
        Sql.AppendLine("      ,[NATURALIDADE]")
        Sql.AppendLine("      ,[CI2].CIDADE AS CIDADE_NATURALIDADE")
        Sql.AppendLine("      ,[CI2].ESTADO_ID AS CIDADE_NATURALIDADE_UF_ID")
        Sql.AppendLine("      ,[CI2].UF AS CIDADE_NATURALIDADE_UF")
        Sql.AppendLine("      ,[M].[ENDERECO] AS ENDERECO_MEMBRO")
        Sql.AppendLine("      ,[M].[BAIRRO] AS BAIRRO_MEMBRO")
        Sql.AppendLine("      ,[M].[CEP] AS CEP_MEMBRO")
        Sql.AppendLine("      ,[NOME_PAI]")
        Sql.AppendLine("      ,[NOME_MAE]")
        Sql.AppendLine("      ,[CONJUGUE]")
        Sql.AppendLine("      ,[DATA_ADMISSAO]")
        Sql.AppendLine("      ,[DATA_BATISMO]")
        Sql.AppendLine("      ,[DATA_CONSAGRACAO]")
        Sql.AppendLine("      ,[CREDENCIAL]")
        Sql.AppendLine("      ,[NUMERO_CREDENCIAL]")
        Sql.AppendLine("      ,[ID_CARGO]")
        Sql.AppendLine("      ,[C].DESCRICAO AS CARGO")
        Sql.AppendLine("      ,[ID_CIDADE]")
        Sql.AppendLine("      ,[CI].CIDADE AS NOME_CIDADE")
        Sql.AppendLine("      ,[CI].UF AS UF")
        Sql.AppendLine("      ,[CI].ESTADO_ID")
        Sql.AppendLine("      ,[ID_CONGREGACAO]")
        Sql.AppendLine("      ,[ID_DEPARTAMENTO]")
        Sql.AppendLine("      ,[CO].NOME AS CONGREGACAO")
        Sql.AppendLine("      ,[ULTIMA_ALTERACAO]")
        Sql.AppendLine("      ,[M].[DATA_CADASTRO] AS DATA_CADASTRO_MEMBRO")
        Sql.AppendLine("      ,[M].[ATIVO]")
        Sql.AppendLine("  FROM [IBLR].[dbo].[IBLR_MEMBROS] AS M")
        Sql.AppendLine("		  INNER JOIN IBLR_CARGOS AS C ON C.ID=M.ID_CARGO")
        Sql.AppendLine("		  INNER JOIN IBLR_CIDADES CI ON CI.ID= M.ID_CIDADE")
        Sql.AppendLine("		  INNER JOIN IBLR_CIDADES CI2 ON CI2.ID =M.NATURALIDADE")
        Sql.AppendLine("		  INNER JOIN IBLR_ESTADOS ES ON ES.ID =CI.ESTADO_ID")
        Sql.AppendLine("		  INNER JOIN IBLR_CONGREGACOES CO ON CO.ID = M.ID_CONGREGACAO")
        Sql.AppendLine("  WHERE")
        Sql.AppendLine("		M.ID = '" & idMembro & "'")



        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, Sql.ToString)

        While dr.Read

            _membro.ID = dr("ID").ToString
            _membro.NOME = dr("NOME").ToString
            _membro.APELIDO = dr("APELIDO").ToString
            _membro.RG = dr("RG").ToString
            _membro.CPF = dr("CPF").ToString
            _membro.DATA_NASCIMENTO = dr("DATA_NASCIMENTO").ToString().Remove(10, 9)
            _membro.ESTADO_CIVIL = dr("ESTADO_CIVIL").ToString
            _membro.SEXO = dr("SEXO").ToString
            Dim _NATURALIDADE As New Cidade
            _NATURALIDADE.Id = dr("NATURALIDADE").ToString
            _NATURALIDADE.IdEstado = dr("CIDADE_NATURALIDADE_UF_ID").ToString
            _NATURALIDADE.UF = dr("CIDADE_NATURALIDADE_UF").ToString
            _membro.NATURALIDADE = _NATURALIDADE
            _membro.ENDERECO = dr("ENDERECO_MEMBRO").ToString
            _membro.BAIRRO = dr("BAIRRO_MEMBRO").ToString
            _membro.CEP = dr("CEP_MEMBRO").ToString
            _membro.NOME_PAI = dr("NOME_PAI").ToString
            _membro.NOME_MAE = dr("NOME_MAE").ToString
            _membro.CONJUGUE = dr("CONJUGUE").ToString
            _membro.DATA_ADMISSAO = dr("DATA_ADMISSAO").ToString().Remove(10, 9)
            _membro.DATA_BATISMO = dr("DATA_BATISMO").ToString().Remove(10, 9)
            _membro.DATA_CONSAGRACAO = dr("DATA_CONSAGRACAO").ToString().Remove(10, 9)
            _membro.CREDENCIAL = dr("CREDENCIAL").ToString
            _membro.NUMERO_CREDENCIAL = dr("NUMERO_CREDENCIAL").ToString
            Dim _CARGO As New Cargo
            _CARGO.Id = dr("ID_CARGO").ToString
            _CARGO.Descricao = dr("CARGO").ToString
            _membro.CARGO = _CARGO
            Dim _CIDADE As New Cidade
            _CIDADE.Id = dr("ID_CIDADE").ToString
            _CIDADE.Descricao = dr("NOME_CIDADE").ToString
            _CIDADE.UF = dr("UF").ToString
            _CIDADE.IdEstado = dr("ESTADO_ID").ToString
            _membro.CIDADE = _CIDADE
            Dim _CONGREGACAO As New Congregacao
            _CONGREGACAO.Id = dr("ID_CONGREGACAO").ToString
            _CONGREGACAO.Nome = dr("CONGREGACAO").ToString
            _membro.CONGREGACAO = _CONGREGACAO
            Dim _DEPARTAMENTO As New Departamento
            _DEPARTAMENTO.Id = dr("ID_DEPARTAMENTO").ToString
            _membro.DEPARTAMENTO = _DEPARTAMENTO
            _membro.ULTIMA_ALTERACAO = dr("ULTIMA_ALTERACAO").ToString
            _membro.DATA_CADASTRO = dr("DATA_CADASTRO_MEMBRO").ToString
            _membro.ATIVO = dr("ATIVO").ToString

        End While

        Sql = New StringBuilder
        Sql.AppendLine("SELECT CM.[ID]")
        Sql.AppendLine("            ,[TIPO_CONTATO]")
        Sql.AppendLine("            ,[DESCRICAO_CONTATO]")
        Sql.AppendLine("        FROM [IBLR].[dbo].[IBLR_CONTATOS_MEMBROS] CM")
        Sql.AppendLine("        INNER JOIN [IBLR].[dbo].[IBLR_MEMBROS_IBLR_CONTATO_MEMBRO]CMM ON CM.ID=CMM.ID_CONTATO_MEMBRO")
        Sql.AppendLine("        INNER JOIN [IBLR].[dbo].[IBLR_MEMBROS] M ON M.ID = CMM.ID_MEMBRO")
        Sql.AppendLine("  WHERE ID_MEMBRO = '" & idMembro & "'")

        Dim conn1 As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        Dim dr1 As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn1, Sql.ToString)
        Dim _listaContatos As New List(Of Contato)

        While dr1.Read
            Dim _Contato As New Contato
            _Contato.id = dr1("ID").ToString
            _Contato.DescricaoTipoContato = dr1("TIPO_CONTATO").ToString
            If _Contato.DescricaoTipoContato = "Fixo" Or _Contato.DescricaoTipoContato = "Celular" Then
                _Contato.Descricao = dr1("DESCRICAO_CONTATO").ToString.ToString.Insert(0, "(").Insert(3, ")").Insert(9, "-")
            Else
                _Contato.Descricao = dr1("DESCRICAO_CONTATO").ToString
            End If

            _listaContatos.Add(_Contato)
        End While
        _membro.LISTACONTATOS = _listaContatos
        Return _membro
    End Function

    Public Function getMembrosUltimoInserido() As String
        Dim Sql As New StringBuilder
        Sql.AppendLine("SELECT max(ID) as ID")
        Sql.AppendLine("        FROM [IBLR].[dbo].[IBLR_MEMBROS]")

        Dim conn As SqlConnection = ConexaoDB.RetornaConexaoIBLR
        Dim dr As SqlDataReader = ConexaoDB.ExecuteSelectSQL(conn, Sql.ToString)
        Dim idMax As String = ""
        If dr.Read Then
            idMax = dr("ID").ToString
        End If

        Return idMax
    End Function

End Class
