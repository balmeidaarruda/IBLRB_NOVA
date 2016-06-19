Public Class Despesa
    Private _Id As String
    Private _TipoDespesa As TipoDespesa
    Private _Congregacao As Congregacao
    Private _ValorDespesa As Double
    Private _DataLancamento As String

    Public Property DataLancamento() As String
        Get
            Return _DataLancamento
        End Get
        Set(ByVal value As String)
            _DataLancamento = value
        End Set
    End Property

    Public Property ValorDespesa() As Double
        Get
            Return _ValorDespesa
        End Get
        Set(ByVal value As Double)
            _ValorDespesa = value
        End Set
    End Property

    Public Property Congregacao() As Congregacao
        Get
            Return _Congregacao
        End Get
        Set(ByVal value As Congregacao)
            _Congregacao = value
        End Set
    End Property

    Public Property TipoDespesa() As TipoDespesa
        Get
            Return _TipoDespesa
        End Get
        Set(ByVal value As TipoDespesa)
            _TipoDespesa = value
        End Set
    End Property

    Public Property Id() As String
        Get
            Return _Id
        End Get
        Set(ByVal value As String)
            _Id = value
        End Set
    End Property

End Class
