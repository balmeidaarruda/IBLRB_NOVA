Public Class Oferta
    Private _ID As String
    Private _TipoOferta As TipoOferta
    Private _ValorOferta As Double
    Private _DataLacamento As String
    Private _Congregacao As Congregacao
    Public Property Congregacao() As Congregacao
        Get
            Return _Congregacao
        End Get
        Set(ByVal value As Congregacao)
            _Congregacao = value
        End Set
    End Property

    Public Property DataLancamento() As String
        Get
            Return _DataLacamento
        End Get
        Set(ByVal value As String)
            _DataLacamento = value
        End Set
    End Property

    Public Property ValorOferta() As Double
        Get
            Return _ValorOferta
        End Get
        Set(ByVal value As Double)
            _ValorOferta = value
        End Set
    End Property

    Public Property TipoOferta() As TipoOferta
        Get
            Return _TipoOferta
        End Get
        Set(ByVal value As TipoOferta)
            _TipoOferta = value
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
