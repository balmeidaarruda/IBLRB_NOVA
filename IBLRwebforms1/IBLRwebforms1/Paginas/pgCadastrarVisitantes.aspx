<%@ Page EnableEventValidation="false" Title="Visitantes" Language="vb" AutoEventWireup="false" MasterPageFile="~/Paginas/Site.Master" CodeBehind="pgCadastrarVisitantes.aspx.vb" Inherits="IBLRwebforms1.pgCadastrarVisitantes" %>

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
            width: 48%;
        }

        .auto-style6 {
            font-size: 11px;
            font-weight: 700;
            padding-left: 3px;
            background-color: #dde4ec;
            color: #000;
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
        .auto-style9 {
            font-size: 11px;
            font-weight: 700;
            padding-left: 3px;
            background-color: #dde4ec;
            color: #000;
            width: 49%;
        }
        .auto-style10 {
            width: 49%;
        }
        .auto-style11 {
            border-bottom: 1px solid #4b6c9e;
            font-weight: 700;
            font-family: 'Franklin Gothic';
            width: 48%;
        }
        .auto-style12 {
            font-size: 11px;
            font-weight: 700;
            padding-left: 3px;
            background-color: #dde4ec;
            color: #000;
            width: 48%;
        }
        .auto-style13 {
            width: 48%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function DatePicker() {
            $(document).ready(
        $(function () {
            var datas = $('#<%=txtDataVisita.ClientID%>,#<%=txtDataInicio.ClientID%>,#<%=txtDataFim.ClientID%>');
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

    </script>
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">

            <ContentTemplate>
                <script type="text/javascript">
                    Sys.Application.add_load(DatePicker);
                </script>
                <h2 class="barraTituloSessao">Cadastro de Visitantes</h2>
                <div>
                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="botoesBarraAcoes" ValidationGroup="1" CausesValidation="true" Style="background-image: url('Images/savepb.png'); background-repeat: no-repeat; background-position: left; padding-left: 20px;" />
                    <asp:Button ID="btnNovoRegistro" runat="server" Text="Novo Registro"
                        CssClass="botoesBarraAcoes" CausesValidation="False" Style="background-image: url('Images/file-add.png'); background-repeat: no-repeat; background-position: left; padding-left: 20px;" />
                    <asp:Button ID="btnConsultar" runat="server" Text="Consultar"
                        CssClass="botoesBarraAcoes" CausesValidation="False" Style="background-image: url('Images/searchpb.png'); background-repeat: no-repeat; background-position: left; padding-left: 20px;" />
                </div>
                <asp:Panel ID="pnConsulta" runat="server">
                    <table style="width: 100%;">
                        <tr>
                            <td class="barraTituloSessao" colspan="3">
                                <asp:Label ID="lblConsultarCampos" runat="server" Text="Consultar Visitantes"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style22" colspan="3">
                                <asp:Label ID="lblCongregacaoConsulta" runat="server" Text="Congregação"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddCongregacaoConsulta"
                                    ForeColor="red" ErrorMessage="Congregação" ValidationGroup="1">*
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style3" colspan="3">
                                <asp:DropDownList ID="ddCongregacaoConsulta" runat="server" BackColor="White" DataTextField="Nome" DataValueField="id" Width="50%" Height="25px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelCampos" colspan="3">
                                <asp:Label ID="lblPeriodo" runat="server" Text="Período"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style6">
                                <asp:Label ID="lblDataInicio" runat="server" Text="Data início"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDataInicio" ErrorMessage="Data Início" ForeColor="red" ValidationGroup="1">*
                                </asp:RequiredFieldValidator>
                            </td>
                            <td class="auto-style8">
                                <asp:Label ID="lblDataFim" runat="server" Text="Data Fim"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDataFim" ErrorMessage="Data Fim" ForeColor="red" ValidationGroup="1">*
                                </asp:RequiredFieldValidator>
                            </td>
                            <td class="style22" style="width: 160px">
                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="SingleParagraph" ForeColor="Red" HeaderText="* Campos obrigatórios" ValidationGroup="1" Width="390px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style7">
                                <asp:TextBox ID="txtDataInicio" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td style="width: 216px">
                                <asp:TextBox ID="txtDataFim" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td style="width: 700px">
                                <asp:ImageButton ID="btnConsultarVisitantes" runat="server" CausesValidation="true" CommandName="ConsultarVisitantes" Height="16" ImageUrl="~/images/searchpb.png" ToolTip="Buscar dados" ValidationGroup="1" Width="16px" />
                            </td>
                        </tr>

                        <tr>
                            <td colspan="3" class="labelCampos">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="labelCampos" colspan="3" style="background-color: White;">
                                <div style="overflow: scroll; height: 300px; background-color: White;">

                                    <asp:ImageButton runat="server" ID="btnExportarExcel" ImageUrl="~/Images/page_excel.png" ToolTip="Exportar Excel" Visible="false" />

                                    <asp:GridView ID="gdVisitantes" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" HeaderStyle-HorizontalAlign="Left" Width="100%" DataKeyNames="Id">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

                                        <Columns>
                                            <asp:TemplateField ShowHeader="true" HeaderText="Editar" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEditarVisitantes" runat="server" CausesValidation="false"
                                                        CommandName="EditarVisitantes" ImageUrl="~/images/editpb.png" ToolTip="Editar dados" Width="16px" Height="16" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Id" HeaderText="Código" />
                                            <asp:BoundField DataField="Congregacao.Id" HeaderText="CodCong" />
                                            <asp:BoundField DataField="Congregacao.Nome" HeaderText="Congregação" />
                                            <asp:BoundField DataField="DATA_VISITA" HeaderText="Data da Visita" />
                                            <asp:BoundField DataField="NOME" HeaderText="Nome" />
                                            <asp:BoundField DataField="ENDERECO" HeaderText="Endereço" />
                                            <asp:BoundField DataField="QUEM_CONVIDOU" HeaderText="Convidado por" />
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnCadastro" runat="server" Width="110%">
                    <table style="width: 100%;">
                        <tr>
                            <td class="auto-style1">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="* Campos obrigatórios" ValidationGroup="1" />
                            </td>
                        </tr>
                        <tr>
                            <td class="barraTituloSessao" colspan="2">
                                <asp:Label ID="lblDadosCampos" runat="server" Text="Dados do visitante"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style12">
                                <asp:Label ID="lblDataVisita" runat="server" Text="Data da Visita"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDataVisita"
                                    ForeColor="red" ErrorMessage="Data da Visita" ValidationGroup="1">*
                                </asp:RequiredFieldValidator>
                            </td>
                            <td class="style22" style ="width:40%">
                                <asp:Label ID="lblCongregacao" runat="server" Text="Congregação"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddCongregacao"
                                    ForeColor="red" ErrorMessage="Congregação" ValidationGroup="1">*
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style13">
                                <asp:TextBox ID="txtDataVisita" runat="server" Width="20%"></asp:TextBox>
                            </td>
                            <td class="auto-style3">
                                <asp:DropDownList ID="ddCongregacao" runat="server" BackColor="White" DataTextField="Nome" DataValueField="id" Width="100%" Height="25px">
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td class="auto-style12">
                                <asp:Label ID="lblNome" runat="server" Text="Nome"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNome"
                                    ForeColor="red" ErrorMessage="Nome" ValidationGroup="1">*
                                </asp:RequiredFieldValidator>
                                <asp:Label ID="lblIdDados" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td class="style22">
                                <asp:Label ID="lblConvidadoPor" runat="server" Text="Convidado por"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style13">
                                <asp:TextBox ID="txtNome" runat="server" Width="80%"></asp:TextBox>
                            </td>
                            <td class="style13">
                                <asp:TextBox ID="txtConvidadoPor" runat="server" Width="97%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style22" colspan="2">
                                <asp:Label ID="lblEndereco" runat="server" Text="Endereço"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:TextBox ID="txtEndereco" runat="server" Width="99%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style11">
                                <asp:Label ID="lblContatos" runat="server" Text="Contatos"></asp:Label>
                                &nbsp;<asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red"
                                    CssClass="labelMensagem" ValidationGroup="2" HeaderText="Campos obrigatórios" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style13">
                                <table cellspacing="1" style="width: 100%">
                                    <tr>
                                        <td class="style23">
                                            <asp:Label ID="lblTipoContato" runat="server" Text="Tipo de contato"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddTipoContato"
                                                ForeColor="red" CssClass="labelMensagem" ValidationGroup="2" ErrorMessage="Tipo">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="labelCampos" colspan="2">
                                            <asp:Label ID="lblContato" runat="server" Text="Contato"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtContato"
                                                CssClass="labelMensagem" ValidationGroup="2" ForeColor="red" ErrorMessage="Contato">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style24">
                                            <asp:DropDownList ID="ddTipoContato" runat="server" Width="260px" AutoPostBack="true" BackColor="White">
                                                <asp:ListItem Text=""></asp:ListItem>
                                                <asp:ListItem Text="Fixo" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Celular" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Email" Value="3"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style19" style="padding-left:10px; padding-right:5px;">
                                            <asp:TextBox ID="txtContato" runat="server" Width="449px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnAddContato" runat="server" ImageUrl="~/images/add.png"
                                                Height="20px" Width="21px" ToolTip="Adicionar contatos" CausesValidation="true"
                                                ValidationGroup="2" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="labelCampos" colspan="3">
                                            <asp:Label ID="lblContatosRelacionados" runat="server" Text="Contatos relacionados"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="labelCampos" style="background-color: White; width: 100%" colspan="3">
                                            <asp:GridView ID="gdContatos" runat="server" AutoGenerateColumns="False"
                                                CellPadding="4" ForeColor="#333333" GridLines="Both" ShowHeaderWhenEmpty="False"
                                                HeaderStyle-HorizontalAlign="Left" DataKeyNames="id">
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEditarContato" runat="server" ImageUrl="~/images/editpb.png"
                                                                CommandName="EditarContato" CausesValidation="false" ToolTip="Editar contato" Width="16px" Height="16" />
                                                            <asp:ImageButton ID="btnExcluirContato" runat="server" ImageUrl="~/images/deletePb.png"
                                                                CommandName="ExcluirContato" CausesValidation="false" ToolTip="Excluir contato" Width="16px" Height="16" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="DescricaoTipoContato" HeaderText="Tipo Contato" />
                                                    <asp:BoundField DataField="Descricao" HeaderText="Contato" />
                                                </Columns>
                                                <EditRowStyle BackColor="#999999" />
                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
