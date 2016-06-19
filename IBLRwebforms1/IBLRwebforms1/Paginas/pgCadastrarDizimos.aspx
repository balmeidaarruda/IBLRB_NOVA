<%@ Page Title="Dízimos" Language="vb" AutoEventWireup="false" MasterPageFile="~/Paginas/Site.Master" CodeBehind="pgCadastrarDizimos.aspx.vb" Inherits="IBLRwebforms1.pgCadastrarDizimos" EnableEventValidation="false" %>

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

    <script type="text/javascript">
        function mascaraMoeda() {
            $(document).ready(function () {
                var valor = $('#<%=txtValorDizimo.ClientID%>');
                valor.maskMoney({ showSymbol: true, symbol: "R$", decimal: ",", thousands: "." });
            });
        }

    </script>
    <script type="text/javascript">
        function DatePicker() {
            $(document).ready(
        $(function () {
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

    </script>
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <script type="text/javascript">
                Sys.Application.add_load(DatePicker);
                Sys.Application.add_load(mascaraMoeda);
            </script>
            <div id="dialog" style="display: none;">
            </div>
            <h2 class="barraTituloSessao">Cadastro de Dízimos</h2>
            <div>
                <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="botoesBarraAcoes" ValidationGroup="1" CausesValidation="true" Style="background-image: url('Images/savepb.png'); background-repeat: no-repeat; background-position: left; padding-left: 20px;" />
                <asp:Button ID="btnNovoRegistro" runat="server" Text="Novo Registro"
                    CssClass="botoesBarraAcoes" CausesValidation="False" Style="background-image: url('Images/file-add.png'); background-repeat: no-repeat; background-position: left; padding-left: 20px;" />
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar"
                    CssClass="botoesBarraAcoes" CausesValidation="False" Style="background-image: url('Images/searchpb.png'); background-repeat: no-repeat; background-position: left; padding-left: 20px;" />
            </div>
            <asp:Panel ID="pnCadastro" runat="server" Width="110%">

                <table style="width: 100%;">
                    <tr>
                        <td class="auto-style1">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="* Campos obrigatórios" ValidationGroup="2" DisplayMode="BulletList" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">
                            <asp:Label ID="lblMembro" runat="server" Text="Membro"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddMembro" ErrorMessage="Nome Membro" ForeColor="red" ValidationGroup="2">*
                            </asp:RequiredFieldValidator>
                        </td>
                        <td class="auto-style4">
                            <asp:Label ID="lblValorDizimo" runat="server" Text="Valor Dízimo(R$)"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtValorDizimo" ErrorMessage="Valor Dízimo" ForeColor="red" ValidationGroup="2">*
                            </asp:RequiredFieldValidator>
                            <asp:Label ID="lblIdDados" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td class="style22">
                            <asp:Label ID="lblDataLancamento" runat="server" Text="Data Lançamento"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDataLancamento" ErrorMessage="Data Lançamento" ForeColor="red" ValidationGroup="2">*
                            </asp:RequiredFieldValidator>

                        </td>
                        <tr>
                            <td class="auto-style3"  style="padding-right:10px;">
                                <asp:DropDownList ID="ddMembro" runat="server" BackColor="White" DataTextField="Nome" DataValueField="id" Width="100%" Height="25px">
                                </asp:DropDownList>
                            </td>
                            <td class="auto-style5">
                                <asp:TextBox ID="txtValorDizimo" runat="server" Width="90%"></asp:TextBox>
                            </td>
                            <td class="style13">
                                <asp:TextBox ID="txtDataLancamento" runat="server" Width="70%"></asp:TextBox>
                            </td>
                            <td class="style13" >
                                <asp:ImageButton ID="btnAddLancamento" runat="server" ImageUrl="~/images/add.png"
                                    Height="20px" Width="21px" ToolTip="Adicionar Lançamento" CausesValidation="true"
                                    ValidationGroup="2" />
                            </td>
                        </tr>
                    <tr>
                        <td class="style22" colspan="3">
                            <asp:Label ID="lblDizimosRelacionados" runat="server" Text="Dízimos Relacionados"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="labelCampos" colspan="3">
                            <asp:GridView ID="gdDizimos" runat="server" AutoGenerateColumns="False"
                                CellPadding="1" ForeColor="#333333" GridLines="Both" ShowHeaderWhenEmpty="False"
                                HeaderStyle-HorizontalAlign="Justify" DataKeyNames="id" CellSpacing="2" ShowFooter="true" Width="100%">
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
                                    <asp:BoundField DataField="membro.id" HeaderText="Cod. Membro" />
                                    <asp:BoundField DataField="membro.nome" HeaderText="Nome" />
                                    <asp:BoundField DataField="VALOR_DIZIMO" HeaderText="Valor Dízimo" />
                                    <asp:BoundField DataField="DATA_LANCAMENTO" HeaderText="Data Lançamento" />

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

            </asp:Panel>
            <asp:Panel ID="pnConsulta" runat="server">
                <table style="width: 100%;">
                        <tr>
                            <td class="barraTituloSessao" colspan="3">
                                <asp:Label ID="lblConsultarDizimos" runat="server" Text="Consultar Dízimos"></asp:Label>
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
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDataFim" ErrorMessage="Data Fim" ForeColor="red" ValidationGroup="1">*
                                </asp:RequiredFieldValidator>
                             </td>
                            <td class="style22" style="width: 160px">
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red" HeaderText="* Campos obrigatórios" ValidationGroup="1" DisplayMode="SingleParagraph" Width="390px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style7">
                                <asp:TextBox ID="txtDataInicio" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td style="width:216px">
                                <asp:TextBox ID="txtDataFim" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td style="width: 700px">
                                <asp:ImageButton ID="btnConsultarDizimo" runat="server" CausesValidation="true" CommandName="ConsultarDizimo" Height="16" ImageUrl="~/images/searchpb.png" ToolTip="Buscar dados" Width="16px" ValidationGroup="1"/>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" class="labelCampos">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            
                            <td class="labelCampos" colspan="3" style="background-color: White;">
                                <div style="overflow: scroll; height: 300px; background-color: White;">
                                    
                                    <asp:ImageButton runat="server" ID="btnExportarExcel" ImageUrl="~/Images/page_excel.png" ToolTip="Exportar Excel" Visible="false"/>
                                    <asp:GridView ID="gdDizimoConsulta" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" HeaderStyle-HorizontalAlign="Left" Width="100%" DataKeyNames="id" ShowFooter="true">
                                        <HeaderStyle HorizontalAlign="Left"/>
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />  
                                                                              
                                        <Columns>
                                            <asp:TemplateField ShowHeader="true" HeaderText="Editar" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEditarDizimos" runat="server" CausesValidation="false"
                                                        CommandName="EditarDizimos" ImageUrl="~/images/editpb.png" ToolTip="Editar dados" Width="16px" Height="16" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Id" HeaderText="Id" />
                                            <asp:BoundField DataField="Membro.ID" HeaderText="Cod.Membro" />
                                            <asp:BoundField DataField="Membro.nome" HeaderText="Nome" />
                                            <asp:BoundField DataField="DATA_LANCAMENTO" HeaderText="Data Lançamento" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="VALOR_DIZIMO" HeaderText="Valor" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>