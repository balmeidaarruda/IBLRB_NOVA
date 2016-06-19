<%@ Page Title="Relatório Aniversariantes" Language="vb" AutoEventWireup="false" MasterPageFile="~/Paginas/Site.Master" CodeBehind="relAniversariantes.aspx.vb" Inherits="IBLRwebforms1.relAniversariantes" EnableEventValidation="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <style type="text/css">
        .style7 {
            width: 100%;
        }

        .style13 {
        }

        .style19 {
            width: 451px;
        }

        .style20 {
            font-size: 11px;
            font-weight: 700;
            padding-left: 3px;
            background-color: #dde4ec;
            color: #000;
        }

        .style21 {
        }

        .style22 {
            font-size: 11px;
            font-weight: 700;
            padding-left: 3px;
            background-color: #dde4ec;
            color: #000;
        }

        .style23 {
            font-size: 11px;
            font-weight: 700;
            padding-left: 3px;
            background-color: #dde4ec;
            color: #000;
            width: 260px;
        }

        .style24 {
        }

        .auto-style1 {
            font-size: 11px;
            font-weight: 700;
            padding-left: 3px;
            color: #000;
            height: 61px;
            width: 550px;
        }

        .auto-style2 {
            font-size: 11px;
            font-weight: 700;
            padding-left: 3px;
            background-color: #dde4ec;
            color: #000;
            width: 550px;
        }

        .auto-style3 {
            width: 550px;
        }

        .auto-style4 {
            font-size: 11px;
            font-weight: 700;
            padding-left: 3px;
            background-color: #dde4ec;
            color: #000;
            width: 150px;
        }

        .auto-style5 {
            width: 150px;
        }

        .auto-style6 {
            font-size: 11px;
            font-weight: 700;
            padding-left: 3px;
            background-color: #dde4ec;
            color: #000;
            width: 170px;
        }

        .auto-style7 {
            width: 170px;
        }

        .auto-style8 {
            font-size: 11px;
            font-weight: 700;
            padding-left: 3px;
            background-color: #dde4ec;
            color: #000;
            width: 216px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<script type="text/javascript">
        function mascaraMoeda() {
            $(document).ready(function () {
                var valor = $('#<%=txtValorDespesa.ClientID%>');
                valor.maskMoney({ showSymbol: true, symbol: "R$", decimal: ",", thousands: "." });
            });
        }

    </script>
    <script type="text/javascript">
        function DatePicker() {
            $(document).ready(
        $(function () {
            //,#<=%txtDataInicio.ClientID%>,#<=%txtDataFim.ClientID%>'
            var datas = $('#<%=txtDataLancamento.ClientID%>,#<%=txtDataInicio.ClientID%>,#<%=txtDataFim.ClientID%>');
            datas.datepicker({
                showOn: 'button',
                dateFormat: 'dd/mm/yy',
                buttonImageOnly: true,
                buttonImage: '../Images/calendar_24x24.png',
                dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado', 'Domingo'],
                dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
                dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
                monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez']

            });
        }));
        }

    </script>--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <%--<script type="text/javascript">
                Sys.Application.add_load(DatePicker);
                Sys.Application.add_load(mascaraMoeda);
            </script>--%>
            <div id="dialog" style="display: none;">
            </div>
            <h2 class="barraTituloSessao">Relatório de Aniversariantes</h2>

            <div>
                <table>
                    <tr>
                        <td class="auto-style1">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="* Campos obrigatórios" ValidationGroup="2" DisplayMode="BulletList" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style22" colspan="3">
                            <asp:Label ID="lblCongregacao" runat="server" Text="Congregação"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddCongregacao" ErrorMessage="Congregação" ForeColor="red" ValidationGroup="2">*
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">
                            <asp:DropDownList ID="ddCongregacao" runat="server" BackColor="White" DataTextField="Nome" DataValueField="id" Width="100%" Height="25px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">
                            <asp:Label ID="lblMesAniversario" runat="server" Text="Mês Aniversário"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddMesAniversario" ErrorMessage="Mês Aniversário" ForeColor="red" ValidationGroup="2">*
                            </asp:RequiredFieldValidator>
                        </td>
                    </tr>

                    <tr>
                        <td class="auto-style3">
                            <asp:DropDownList ID="ddMesAniversario" runat="server" BackColor="White" Height="25px" >
                                 <asp:ListItem Text="" Value=""></asp:ListItem>
                                <asp:ListItem Text="Janeiro" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Fevereiro" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Março" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Abril" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Maio" Value="5"></asp:ListItem>
                                <asp:ListItem Text="Junho" Value="6"></asp:ListItem>
                                <asp:ListItem Text="Julho" Value="7"></asp:ListItem>
                                <asp:ListItem Text="Agosto" Value="8"></asp:ListItem>
                                <asp:ListItem Text="Setembro" Value="9"></asp:ListItem>
                                <asp:ListItem Text="Outubro" Value="10"></asp:ListItem>
                                <asp:ListItem Text="Novembro" Value="11"></asp:ListItem>
                                <asp:ListItem Text="Dezembro" Value="12"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnConsultar" runat="server" CausesValidation="true" ValidationGroup="2" CssClass="botoesBarraAcoes" Style="background-image: url('Images/searchpb.png'); background-repeat: no-repeat; background-position: left; padding-left: 20px;" Text="Consultar" />
                        </td>
                    </tr>

                </table>

            </div>
            <div>
                <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" ShowBackButton="False" ShowFindControls="False" ShowRefreshButton="False">
                </rsweb:ReportViewer>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="IBLRDataSetTableAdapters.IBLR_MEMBROSTableAdapter"></asp:ObjectDataSource>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
