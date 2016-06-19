Imports System.Data.SqlClient
Public Class relObreiros
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

            Dim _MapeadorDeCargos As New MapeadorDeCargos
            ddCargo.DataSource = _MapeadorDeCargos.getListaCargos("")
            ddCargo.DataBind()
            ddCargo.Items.Insert(0, "")
            Dim item As ListItem = ddCargo.Items.FindByText("Congregado")
            ddCargo.Items.Remove(item)
            Dim item1 As ListItem = ddCargo.Items.FindByText("Membro")
            ddCargo.Items.Remove(item1)

        End If
    End Sub

    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Dim _MapeadorDeRelatorios As New MapeadorDeRelatorios
        Dim DTable As New DataTable()
        Dim idFuncoes As String = ""
        Dim cargos As String = ""
        Dim contDiacono As Integer = 0
        Dim contEvangelista As Integer = 0
        Dim contMissionario As Integer = 0
        Dim contPastor As Integer = 0
        Dim contPresbitero As Integer = 0

        If ddCargo.SelectedItem.Text = "Obreiro" Then
            For Each item As ListItem In ddCargo.Items

                If Not item.Text = "Obreiro" Then
                    idFuncoes = idFuncoes + item.Value & ","
                End If

            Next
            idFuncoes = idFuncoes.Remove(0, 1).Remove(idFuncoes.Length - 2, 1)
        Else
            idFuncoes = ddCargo.SelectedValue
        End If

        DTable.Load(_MapeadorDeRelatorios.relObreiros(idFuncoes))

        For Each row As DataRow In DTable.Rows
            Select Case row.Item(11)
                Case "Diácono"
                    contDiacono = contDiacono + 1
                Case "Evangelista"
                    contEvangelista = contEvangelista + 1
                Case "Missionário"
                    contMissionario = contMissionario + 1
                Case "Pastor"
                    contPastor = contPastor + 1
                Case "Presbítero"
                    contPresbitero = contPresbitero + 1
            End Select
        Next

        For Each item As ListItem In ddCargo.Items

            If Not item.Text = "Obreiro" Then
                Select Case item.Text
                    Case "Diácono"
                        cargos = cargos + item.Text & "(" & contDiacono & "),"
                    Case "Evangelista"
                        cargos = cargos + item.Text & "(" & contEvangelista & "),"
                    Case "Missionário"
                        cargos = cargos + item.Text & "(" & contMissionario & "),"
                    Case "Pastor"
                        cargos = cargos + item.Text & "(" & contPastor & "),"
                    Case "Presbítero"
                        cargos = cargos + item.Text & "(" & contPresbitero & "),"
                End Select
            End If

        Next
        cargos = cargos.Remove(cargos.Length - 1, 1)
        If Not ddCargo.SelectedItem.Text = "Obreiro" Then
            cargos = String.Empty
            Select Case ddCargo.SelectedItem.Text
                Case "Diácono"
                    cargos = cargos + ddCargo.SelectedItem.Text & "(" & contDiacono & "),"
                Case "Evangelista"
                    cargos = cargos + ddCargo.SelectedItem.Text & "(" & contEvangelista & "),"
                Case "Missionário"
                    cargos = cargos + ddCargo.SelectedItem.Text & "(" & contMissionario & "),"
                Case "Pastor"
                    cargos = cargos + ddCargo.SelectedItem.Text & "(" & contPastor & "),"
                Case "Presbítero"
                    cargos = cargos + ddCargo.SelectedItem.Text & "(" & contPresbitero & "),"
            End Select
            cargos = cargos.Remove(cargos.Length - 1, 1)
        End If

        Dim rds As Microsoft.Reporting.WebForms.ReportDataSource = New Microsoft.Reporting.WebForms.ReportDataSource("dsObreiros", DTable)


        ReportViewer1.LocalReport.ReportPath = "Relatorios/relObreiros.rdlc"
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(rds)

        Dim param(0) As Microsoft.Reporting.WebForms.ReportParameter
        param(0) = New Microsoft.Reporting.WebForms.ReportParameter("txtCargos", cargos, True)
        ReportViewer1.LocalReport.SetParameters(param)
        ReportViewer1.DataBind()
        ReportViewer1.LocalReport.Refresh()
    End Sub
End Class