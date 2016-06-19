Imports System.IO

Public Class Utilitarios
    Inherits System.Web.UI.Page
    Public Shared Sub EnviarMensagem(ByVal mensagem As String, ByVal control As System.Web.UI.Control)
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Popup", "ShowPopup('" + mensagem + "');", True)
        ScriptManager.RegisterStartupScript(control, control.GetType(), "Popup", "ShowPopup('" + mensagem + "');", True)
    End Sub

End Class
