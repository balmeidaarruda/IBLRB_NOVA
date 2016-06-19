Public Class Contato
    Private _id As String
    Private _Descricao As String
    Private _TipoContato As TipoContato
    Private _DescricaoTipoContato As String
    Public Property DescricaoTipoContato() As String
        Get
            Return _DescricaoTipoContato
        End Get
        Set(ByVal value As String)
            _DescricaoTipoContato = value
        End Set
    End Property

    Public Property TipoContato() As TipoContato
        Get
            Return _TipoContato
        End Get
        Set(ByVal value As TipoContato)
            _TipoContato = value
        End Set
    End Property

    Public Property Descricao() As String
        Get
            Return _Descricao
        End Get
        Set(ByVal value As String)
            _Descricao = value
        End Set
    End Property

    Public Property id() As String
        Get
            Return _id
        End Get
        Set(ByVal value As String)
            _id = value
        End Set
    End Property


End Class
