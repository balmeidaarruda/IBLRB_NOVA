Public Class Dizimo
    Private _ID As String
    Private _Membro As Membro
    Private _VALOR_DIZIMO As Decimal
    Private _DATA_LANCAMENTO As String
    Public Property DATA_LANCAMENTO() As String
        Get
            Return _DATA_LANCAMENTO
        End Get
        Set(ByVal value As String)
            _DATA_LANCAMENTO = value
        End Set
    End Property

    Public Property VALOR_DIZIMO() As Double
        Get
            Return _VALOR_DIZIMO
        End Get
        Set(ByVal value As Double)
            _VALOR_DIZIMO = value
        End Set
    End Property

    Public Property Membro() As Membro
        Get
            Return _Membro
        End Get
        Set(ByVal value As Membro)
            _Membro = value
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
