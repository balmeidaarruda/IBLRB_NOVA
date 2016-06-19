Public Class Visitante
    Private _ID As String
    Private _DATA_VISITA As String
    Private _NOME As String
    Private _ENDERECO As String
    Private _QUEM_CONVIDOU As String
    Private _listaContatos As List(Of Contato)
    Private _CONGREGACAO As Congregacao
    Public Property CONGREGACAO() As Congregacao
        Get
            Return _CONGREGACAO
        End Get
        Set(ByVal value As Congregacao)
            _CONGREGACAO = value
        End Set
    End Property

    Public Property listaContatos() As List(Of Contato)
        Get
            Return _listaContatos
        End Get
        Set(ByVal value As List(Of Contato))
            _listaContatos = value
        End Set
    End Property


    Public Property QUEM_CONVIDOU() As String
        Get
            Return _QUEM_CONVIDOU
        End Get
        Set(ByVal value As String)
            _QUEM_CONVIDOU = value
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

    Public Property NOME() As String
        Get
            Return _NOME
        End Get
        Set(ByVal value As String)
            _NOME = value
        End Set
    End Property

    Public Property DATA_VISITA() As String
        Get
            Return _DATA_VISITA
        End Get
        Set(ByVal value As String)
            _DATA_VISITA = value
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
