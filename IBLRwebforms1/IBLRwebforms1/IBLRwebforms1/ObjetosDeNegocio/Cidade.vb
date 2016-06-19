Public Class Cidade
    '<Serializable()>
        Private _Id As Integer
        Private _Descricao As String
    Private _UF As String

    Private _IdEstado As String
    Public Property IdEstado() As String
        Get
            Return _IdEstado
        End Get
        Set(ByVal value As String)
            _IdEstado = value
        End Set
    End Property


        Public Property Descricao As String
            Get
                Return _Descricao
            End Get
            Set(ByVal value As String)
                _Descricao = value
            End Set
        End Property

        Public Property Id As Integer
            Get
                Return _Id
            End Get
            Set(ByVal value As Integer)
                _Id = value
            End Set
        End Property

        Public Property UF As String
            Get
                Return _UF
            End Get
            Set(ByVal value As String)
                _UF = value
            End Set
        End Property
    End Class
