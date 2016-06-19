Public Class Congregacao

    Private _Id As String
    Public Property Id() As String
        Get
            Return _Id
        End Get
        Set(ByVal value As String)
            _Id = value
        End Set
    End Property

    Private _Nome As String
    Public Property Nome() As String
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

    Private _PastorResponsavel As String
    Public Property PastorResponsavel() As String
        Get
            Return _PastorResponsavel
        End Get
        Set(ByVal value As String)
            _PastorResponsavel = value
        End Set
    End Property

    Private _DataFundacao As String
    Public Property DataFundacao() As String
        Get
            Return _DataFundacao
        End Get
        Set(ByVal value As String)
            _DataFundacao = value
        End Set
    End Property

    Private _Endereco As String
    Public Property Endereco() As String
        Get
            Return _Endereco
        End Get
        Set(ByVal value As String)
            _Endereco = value
        End Set
    End Property

    Private _Bairro As String
    Public Property Bairro() As String
        Get
            Return _Bairro
        End Get
        Set(ByVal value As String)
            _Bairro = value
        End Set
    End Property

    Private _Cep As String
    Public Property Cep() As String
        Get
            Return _Cep
        End Get
        Set(ByVal value As String)
            _Cep = value
        End Set
    End Property

    Private _Cidade As Cidade
    Public Property Cidade() As Cidade
        Get
            Return _Cidade
        End Get
        Set(ByVal value As Cidade)
            _Cidade = value
        End Set
    End Property

    Private _Campo As Campo
    Public Property Campo() As Campo
        Get
            Return _Campo
        End Get
        Set(ByVal value As Campo)
            _Campo = value
        End Set
    End Property

    Private _DataCadastro As String
    Public Property DataCadastro() As String
        Get
            Return _DataCadastro
        End Get
        Set(ByVal value As String)
            _DataCadastro = value
        End Set
    End Property

    Private _Observacao As String
    Public Property Observacao() As String
        Get
            Return _Observacao
        End Get
        Set(ByVal value As String)
            _Observacao = value
        End Set
    End Property

    Private _Ativo As String
    Public Property Ativo() As String
        Get
            Return _Ativo
        End Get
        Set(ByVal value As String)
            _Ativo = value
        End Set
    End Property

    Private _ListaContatos As List(Of Contato)
    Public Property ListaContatos() As List(Of Contato)
        Get
            Return _ListaContatos
        End Get
        Set(ByVal value As List(Of Contato))
            _ListaContatos = value
        End Set
    End Property










End Class
