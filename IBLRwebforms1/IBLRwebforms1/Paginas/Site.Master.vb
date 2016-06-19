Public Class SiteMaster
    Inherits MasterPage


    'Const AntiXsrfTokenKey As String = "__AntiXsrfToken"
    'Const AntiXsrfUserNameKey As String = "__AntiXsrfUserName"
    'Dim _antiXsrfTokenValue As String

    'Protected Sub Page_Init(sender As Object, e As System.EventArgs)
    '    ' The code below helps to protect against XSRF attacks
    '    Dim requestCookie As HttpCookie = Request.Cookies(AntiXsrfTokenKey)
    '    Dim requestCookieGuidValue As Guid
    '    If ((Not requestCookie Is Nothing) AndAlso Guid.TryParse(requestCookie.Value, requestCookieGuidValue)) Then
    '        ' Use the Anti-XSRF token from the cookie
    '        _antiXsrfTokenValue = requestCookie.Value
    '        Page.ViewStateUserKey = _antiXsrfTokenValue
    '    Else
    '        ' Generate a new Anti-XSRF token and save to the cookie
    '        _antiXsrfTokenValue = Guid.NewGuid().ToString("N")
    '        Page.ViewStateUserKey = _antiXsrfTokenValue

    '        Dim responseCookie As HttpCookie = New HttpCookie(AntiXsrfTokenKey) With {.HttpOnly = True, .Value = _antiXsrfTokenValue}
    '        If (FormsAuthentication.RequireSSL And Request.IsSecureConnection) Then
    '            responseCookie.Secure = True
    '        End If
    '        Response.Cookies.Set(responseCookie)
    '    End If

    '    AddHandler Page.PreLoad, AddressOf master_Page_PreLoad
    'End Sub

    'Private Sub master_Page_PreLoad(sender As Object, e As System.EventArgs)
    '    If Session("usuarioLogado") = "não" Then
    '        Response.Redirect("~/Account/Login.aspx")
    '    Else

    '    End If
    'End Sub

    Protected Sub logoff_Click(sender As Object, e As EventArgs)
        Session("usuarioLogado") = "nao"
        Response.Redirect("~/login.aspx")
    End Sub

    'Private Sub btnCongregacoes_Click(sender As Object, e As EventArgs) Handles btnCongregacoes.Click
    '    Response.Redirect("~/pgCadastrarCongregacao.aspx")
    'End Sub

    'Protected Sub btnCampos_Click(sender As Object, e As EventArgs) Handles btnCampos.Click
    '    Response.Redirect("~/pgCadastrarCampo.aspx")
    'End Sub

    'Private Sub btnCargos_Click(sender As Object, e As EventArgs) Handles btnCargos.Click
    '    Response.Redirect("~/pgCadastrarCargo.aspx")
    'End Sub

    'Private Sub btnMembros_Click(sender As Object, e As EventArgs) Handles btnMembros.Click
    '    Response.Redirect("~/pgCadastrarMembro.aspx")
    'End Sub
End Class