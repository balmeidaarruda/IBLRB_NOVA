Public Class Membro

    Private _ID As String
    Private _NOME As String
    Private _APELIDO As String
    Private _RG As String
    Private _CPF As String
    Private _DATA_NASCIMENTO As String
    Private _ESTADO_CIVIL As String
    Private _SEXO As String
    Private _NATURALIDADE As Cidade
    Private _ENDERECO As String
    Private _BAIRRO As String
    Private _CEP As String
    Private _NOME_PAI As String
    Private _NOME_MAE As String
    Private _CONJUGUE As String
    Private _DATA_ADMISSAO As String
    Private _DATA_BATISMO As String
    Private _DATA_CONSAGRACAO As String
    Private _CREDENCIAL As String
    Private _NUMERO_CREDENCIAL As String
    Private _CARGO As Cargo
    Private _CIDADE As Cidade
    Private _CONGREGACAO As Congregacao
    Private _ULTIMA_ALTERACAO As String
    Private _DATA_CADASTRO As String
    Private _LISTACONTATOS As List(Of Contato)
    Private _ATIVO As String
    Private _DEPARTAMENTO As Departamento
    Private _DES_CARGO As String
    Private _DES_CONGREGACAO As String
    Private _DES_DEPARTAMENTO As String
    Public Property DES_DEPARTAMENTO() As String
        Get
            Return _DES_DEPARTAMENTO
        End Get
        Set(ByVal value As String)
            _DES_DEPARTAMENTO = value
        End Set
    End Property

    Public Property DES_CONGREGACAO() As String
        Get
            Return _DES_CONGREGACAO
        End Get
        Set(ByVal value As String)
            _DES_CONGREGACAO = value
        End Set
    End Property

    Public Property DES_CARGO() As String
        Get
            Return _DES_CARGO
        End Get
        Set(ByVal value As String)
            _DES_CARGO = value
        End Set
    End Property


    Public Property DEPARTAMENTO() As Departamento
        Get
            Return _DEPARTAMENTO
        End Get
        Set(ByVal value As Departamento)
            _DEPARTAMENTO = value
        End Set
    End Property

    Public Property ATIVO() As String
        Get
            Return _ATIVO
        End Get
        Set(ByVal value As String)
            _ATIVO = value
        End Set
    End Property

    Public Property LISTACONTATOS() As List(Of Contato)
        Get
            Return _LISTACONTATOS
        End Get
        Set(ByVal value As List(Of Contato))
            _LISTACONTATOS = value
        End Set
    End Property

    Public Property DATA_CADASTRO() As String
        Get
            Return _DATA_CADASTRO
        End Get
        Set(ByVal value As String)
            _DATA_CADASTRO = value
        End Set
    End Property

    Public Property ULTIMA_ALTERACAO() As String
        Get
            Return _ULTIMA_ALTERACAO
        End Get
        Set(ByVal value As String)
            _ULTIMA_ALTERACAO = value
        End Set
    End Property

    Public Property CONGREGACAO() As Congregacao
        Get
            Return _CONGREGACAO
        End Get
        Set(ByVal value As Congregacao)
            _CONGREGACAO = value
        End Set
    End Property

    Public Property CIDADE() As Cidade
        Get
            Return _CIDADE
        End Get
        Set(ByVal value As Cidade)
            _CIDADE = value
        End Set
    End Property

    Public Property CARGO() As Cargo
        Get
            Return _CARGO
        End Get
        Set(ByVal value As Cargo)
            _CARGO = value
        End Set
    End Property

    Public Property NUMERO_CREDENCIAL() As String
        Get
            Return _NUMERO_CREDENCIAL
        End Get
        Set(ByVal value As String)
            _NUMERO_CREDENCIAL = value
        End Set
    End Property

    Public Property CREDENCIAL() As String
        Get
            Return _CREDENCIAL
        End Get
        Set(ByVal value As String)
            _CREDENCIAL = value
        End Set
    End Property

    Public Property DATA_CONSAGRACAO() As String
        Get
            Return _DATA_CONSAGRACAO
        End Get
        Set(ByVal value As String)
            _DATA_CONSAGRACAO = value
        End Set
    End Property

    Public Property DATA_BATISMO() As String
        Get
            Return _DATA_BATISMO
        End Get
        Set(ByVal value As String)
            _DATA_BATISMO = value
        End Set
    End Property

    Public Property DATA_ADMISSAO() As String
        Get
            Return _DATA_ADMISSAO
        End Get
        Set(ByVal value As String)
            _DATA_ADMISSAO = value
        End Set
    End Property

    Public Property CONJUGUE() As String
        Get
            Return _CONJUGUE
        End Get
        Set(ByVal value As String)
            _CONJUGUE = value
        End Set
    End Property

    Public Property NOME_MAE() As String
        Get
            Return _NOME_MAE
        End Get
        Set(ByVal value As String)
            _NOME_MAE = value
        End Set
    End Property

    Public Property NOME_PAI() As String
        Get
            Return _NOME_PAI
        End Get
        Set(ByVal value As String)
            _NOME_PAI = value
        End Set
    End Property

    Public Property CEP() As String
        Get
            Return _CEP
        End Get
        Set(ByVal value As String)
            _CEP = value
        End Set
    End Property

    Public Property BAIRRO() As String
        Get
            Return _BAIRRO
        End Get
        Set(ByVal value As String)
            _BAIRRO = value
        End Set
    End Property

    Public Property ENDERECO() As String
        Get
            Return _ENDERECO
        End Get
        Set(ByVal value As String)
            _ENDERECO = value
        End Set
    End Property
    ''' <summary>
    ''' Naturalidade é o ID da cidade onde o Membro nasceu
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NATURALIDADE() As Cidade
        Get
            Return _NATURALIDADE
        End Get
        Set(ByVal value As Cidade)
            _NATURALIDADE = value
        End Set
    End Property

    Public Property SEXO() As String
        Get
            Return _SEXO
        End Get
        Set(ByVal value As String)
            _SEXO = value
        End Set
    End Property

    Public Property ESTADO_CIVIL() As String
        Get
            Return _ESTADO_CIVIL
        End Get
        Set(ByVal value As String)
            _ESTADO_CIVIL = value
        End Set
    End Property

    Public Property DATA_NASCIMENTO() As String
        Get
            Return _DATA_NASCIMENTO
        End Get
        Set(ByVal value As String)
            _DATA_NASCIMENTO = value
        End Set
    End Property

    Public Property CPF() As String
        Get
            Return _CPF
        End Get
        Set(ByVal value As String)
            _CPF = value
        End Set
    End Property

    Public Property RG() As String
        Get
            Return _RG
        End Get
        Set(ByVal value As String)
            _RG = value
        End Set
    End Property

    Public Property APELIDO() As String
        Get
            Return _APELIDO
        End Get
        Set(ByVal value As String)
            _APELIDO = value
        End Set
    End Property

    Public Property NOME() As String
        Get
            Return _NOME
        End Get
        Set(ByVal value As String)
            _NOME = value
        End Set
    End Property

    Public Property ID() As String
        Get
            Return _ID
        End Get
        Set(ByVal value As String)
            _ID = value
        End Set
    End Property


End Class
