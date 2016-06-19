Public Class Departamento
    Private _Id As String
    Private _Descricao As String
    Private _MembroLiderDpto As Membro
    Private _Ativo As String

    Public Property Ativo() As String
        Get
            Return _Ativo
        End Get
        Set(ByVal value As String)
            _Ativo = value
        End Set
    End Property

    Public Property MembroLiderDpto() As Membro
        Get
            Return _MembroLiderDpto
        End Get
        Set(ByVal value As Membro)
            _MembroLiderDpto = value
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

    Public Property Id() As String
        Get
            Return _Id
        End Get
        Set(ByVal value As String)
            _Id = value
        End Set
    End Property


End Class
