Public Class Campo

    Private _Id As Integer
    Private _Nome As String
    Private _PastorResponsavel As String
    Private _Ativo As Integer

    Public Property Ativo() As String
        Get
            Return _Ativo
        End Get
        Set(ByVal value As String)
            _Ativo = value
        End Set
    End Property

    Public Property PastorResponsavel() As String
        Get
            Return _PastorResponsavel
        End Get
        Set(ByVal value As String)
            _PastorResponsavel = value
        End Set
    End Property

    Public Property Nome() As String
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal value As Integer)
            _Id = value
        End Set
    End Property



End Class
