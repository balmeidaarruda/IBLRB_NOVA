Public Class relMembros
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
            ddCargo.Items.Insert(1, "Todos")
            Dim item As ListItem = ddCargo.Items.FindByText("Obreiro")
            ddCargo.Items.Remove(item)
            Dim item1 As ListItem = ddCargo.Items.FindByText("Presbítero")
            ddCargo.Items.Remove(item1)
            Dim item2 As ListItem = ddCargo.Items.FindByText("Pastor")
            ddCargo.Items.Remove(item2)
            Dim item3 As ListItem = ddCargo.Items.FindByText("Diácono")
            ddCargo.Items.Remove(item3)
            Dim item4 As ListItem = ddCargo.Items.FindByText("Evangelista")
            ddCargo.Items.Remove(item4)
            Dim item5 As ListItem = ddCargo.Items.FindByText("Missionário")
            ddCargo.Items.Remove(item5)

        End If
    End Sub

    Protected Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Dim _MapeadorDeRelatorios As New MapeadorDeRelatorios
        Dim DTable As New DataTable()
        Dim idFuncoes As String = ""
        Dim cargos As String = ""
        Dim contMembro As Integer = 0
        Dim contCongregado As Integer = 0
        Dim contNovoConvertido As Integer = 0

        If ddCargo.SelectedItem.Text = "Todos" Then
            idFuncoes = ""
        Else


            idFuncoes = ddCargo.SelectedValue

            'idFuncoes = idFuncoes.Remove(0, 1).Remove(idFuncoes.Length - 2, 1)
        End If

        DTable.Load(_MapeadorDeRelatorios.relMembros(idFuncoes))

        For Each row As DataRow In DTable.Rows
            Select Case row.Item(8)
                Case "Congregado"
                    contCongregado = contCongregado + 1
                Case "Membro"
                    contMembro = contMembro + 1
                Case "Novo convertido"
                    contNovoConvertido = contNovoConvertido + 1
            End Select
        Next


        If Not contCongregado = 0 Then
            cargos = cargos + "Congregado" + "(" & contCongregado & "),"
        End If

        If Not contMembro = 0 Then
            cargos = cargos + "Membro" & "(" & contMembro & "),"
        End If

        If Not contNovoConvertido = 0 Then
            cargos = cargos + "Novo convertido" & "(" & contNovoConvertido & "),"
        End If

        cargos = cargos.Remove(cargos.Length - 1, 1)

        'For Each item As ListItem In ddCargo.Items

        '    Select Case item.Text
        '        Case "Congregado"
        '            cargos = cargos + item.Text & "(" & contCongregado & "),"
        '        Case "Membro"
        '            cargos = cargos + item.Text & "(" & contMembro & "),"
        '        Case Else
        '            If item.Text <> String.Empty OrElse item.Text <> "Todos" Then
        '                cargos = cargos + "Novo convertido" & "(" & contNovoConvertido & "),"
        '            End If
        '    End Select
        'Next
        'cargos = cargos.Remove(cargos.Length - 1, 1)
        'If Not ddCargo.SelectedItem.Text = "Todos" Then
        '    cargos = String.Empty
        '    Select Case ddCargo.SelectedItem.Text
        '        Case "Congregado"
        '            cargos = cargos + ddCargo.SelectedItem.Text & "(" & contCongregado & "),"
        '        Case "Membro"
        '            cargos = cargos + ddCargo.SelectedItem.Text & "(" & contMembro & "),"
        '        Case Else
        '            If ddCargo.SelectedItem.Text <> String.Empty OrElse ddCargo.SelectedItem.Text <> "Todos" Then
        '                cargos = cargos + "Novo convertido" & "(" & contNovoConvertido & "),"
        '            End If
        '    End Select
        '    cargos = cargos.Remove(cargos.Length - 1, 1)
        'End If

        Dim rds As Microsoft.Reporting.WebForms.ReportDataSource = New Microsoft.Reporting.WebForms.ReportDataSource("dsMembros", DTable)

        ReportViewer1.LocalReport.ReportPath = "Relatorios/relMembros.rdlc"
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.DataSources.Add(rds)

        Dim param(0) As Microsoft.Reporting.WebForms.ReportParameter
        param(0) = New Microsoft.Reporting.WebForms.ReportParameter("txtCargos", cargos, True)
        ReportViewer1.LocalReport.SetParameters(param)
        ReportViewer1.DataBind()
        ReportViewer1.LocalReport.Refresh()
    End Sub
End Class