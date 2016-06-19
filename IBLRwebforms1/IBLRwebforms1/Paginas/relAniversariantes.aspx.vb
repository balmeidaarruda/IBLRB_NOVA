Imports Microsoft.Reporting.WebForms
Public Class relAniversariantes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.MaintainScrollPositionOnPostBack = True
        If Session("usuarioLogado") = "sim" Then
            'Dim menu As Menu = CType(Master.FindControl("Menu1"), Menu)
            'menu.Visible = True
            'selected_tab.Value = Request.Form(selected_tab.UniqueID)
            Dim image As System.Web.UI.WebControls.Image = CType(Master.FindControl("statusLogin"), System.Web.UI.WebControls.Image)
            image.ImageUrl = "../Images/green-button_24x24.png"
            image.ImageAlign = ImageAlign.Right
            image.ToolTip = "Usuário logado"
            'pnConsulta.Visible = False
            'Me.ClientScript.RegisterClientScriptInclude("scripts", "Scripts/JavaScript.js")
        Else
            Response.Redirect("~/login.aspx")
            Exit Sub
        End If
        If Not IsPostBack Then
            Me.ClientScript.RegisterClientScriptInclude("scripts", "Scripts/JavaScript.js")

            Dim _MapeadorDeCongregacoes As New MapeadorDeCongregacoes
            ddCongregacao.DataSource = _MapeadorDeCongregacoes.getListaCongregacoes(0, "")
            ddCongregacao.DataBind()
            ddCongregacao.Items.Insert(0, "")

        End If
    End Sub

    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Dim _relAniversariantes As New relAniversariantes
        Dim _MapeadorDeRelatorios As New MapeadorDeRelatorios

        Dim rds As Microsoft.Reporting.WebForms.ReportDataSource = New Microsoft.Reporting.WebForms.ReportDataSource("dsAniversariantes", _MapeadorDeRelatorios.relAniversariantes(ddCongregacao.SelectedValue, ddMesAniversario.SelectedValue))

        ReportViewer1.LocalReport.ReportPath = "Relatorios/relAniversariantes.rdlc"
        'ReportViewer1.Reset()
        'ReportViewer1.LocalReport.Dispose()
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(rds)
        
        Dim param(0) As Microsoft.Reporting.WebForms.ReportParameter
        param(0) = New Microsoft.Reporting.WebForms.ReportParameter("txtMes", ddMesAniversario.SelectedItem.Text, True)
        ReportViewer1.LocalReport.SetParameters(param)
        ReportViewer1.DataBind()
        ReportViewer1.LocalReport.Refresh()

    End Sub
End Class