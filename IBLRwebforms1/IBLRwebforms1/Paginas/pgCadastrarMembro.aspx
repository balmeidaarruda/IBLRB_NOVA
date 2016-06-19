<%@ Page EnableEventValidation="false" Title="Membros" Language="vb" AutoEventWireup="false" MasterPageFile="~/Paginas/Site.Master" CodeBehind="pgCadastrarMembro.aspx.vb" Inherits="IBLRwebforms1.pgCadastrarMembro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style7 {
            width: 100%;
        }

        .style13 {
            padding: 5px 5px 5px 5px;
            margin-right: 1px;            
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
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="barraTituloSessao">Cadastro de Membros</h2>
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>--%>
    <%--<script src="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/jquery-ui.js" type="text/javascript"></script>--%>
    <%--<link href="http://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.9/themes/start/jquery-ui.css"rel="stylesheet" type="text/css" />--%>
    <script type="text/javascript">
        var selected_tab = 1;
        $(function () {
            var tabs = $("#tabs").tabs({
                select: function (e, i) {
                    selected_tab = i.index;
                }
            });
            selected_tab = $("[id$=selected_tab]").val() != "" ? parseInt($("[id$=selected_tab]").val()) : 0;
            tabs.tabs('select', selected_tab);
            $("form").submit(function () {
                $("[id$=selected_tab]").val(selected_tab);
            });
        });
    </script>

    <script type="text/javascript">
        $(document).ready(
        $(function () {
            var datas = $('#<%=txtDataNascimento.ClientID%>,#<%=txtDataAdmissao.ClientID%>,#<%=txtDataBatismo.ClientID%>,#<%=txtDataConsagracao.ClientID%>');
            datas.datepicker({
                showOn: 'button',
                dateFormat: 'dd/mm/yy',
                buttonImageOnly: true,
                buttonImage: '../Images/calendar.png',
                dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado', 'Domingo'],
                dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
                dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
                monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
                monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez']

            });
        }));
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
        <%--<Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnConsultarMembros" EventName="Click" />
            <asp:PostBackTrigger ControlID="btnSalvar" />
            <asp:PostBackTrigger ControlID ="btnNovoRegistro" />
            <asp:PostBackTrigger ControlID="btnConsultar" />
        </Triggers>--%>
    <contenttemplate>
        <div id="dialog" style="display: none;">
        </div>
            <div>
                <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="botoesBarraAcoes" ValidationGroup="1" CausesValidation="true" Style="background-image: url('~/Images/savepb.png'); background-repeat: no-repeat; background-position: left; padding-left: 20px;" />
                <asp:Button ID="btnNovoRegistro" runat="server" Text="Novo Registro"
                    CssClass="botoesBarraAcoes" CausesValidation="False" Style="background-image: url('../Images/file-add.png'); background-repeat: no-repeat; background-position: left; padding-left: 20px;" />
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar"
                    CssClass="botoesBarraAcoes" CausesValidation="False" Style="background-image: url('../Images/searchpb.png'); background-repeat: no-repeat; background-position: left; padding-left: 20px;" />
            </div>
            <br />
            
            <asp:Panel ID="pnConsulta" runat="server">
                
                <table style="width: 100%;">
                    <tr>
                        <td class="barraTituloSessao" colspan="3">
                            <asp:Label ID="lblConsultarMembros" runat="server" Text="Consultar Membros"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="labelCampos" colspan="2">
                            <asp:Label ID="lblCampoConsulta" runat="server" Text="Descrição"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtCampoConsulta" runat="server" Width="760px"></asp:TextBox>
                        </td>
                        <td style="width: 160px">
                            <asp:ImageButton ID="btnConsultarMembros" runat="server" CausesValidation="false"
                                CommandName="ConsultarMembros" ImageUrl="../images/searchpb.png" ToolTip="Buscar dados" Width="16px" Height="16" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="labelCampos">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="labelCampos" colspan="3" style="background-color: White;">
                            <div style="overflow: scroll; height: 300px; background-color: White;">
                                <asp:GridView ID="gdMembros" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" HeaderStyle-HorizontalAlign="Left" Width="100%" DataKeyNames="Id">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

                                    <Columns>
                                        <asp:TemplateField ShowHeader="true" HeaderText="Editar" ItemStyle-Width="20px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditarMembros" runat="server" CausesValidation="false"
                                                    CommandName="EditarMembros" ImageUrl="../images/editpb.png" ToolTip="Editar dados - Detalhar" Width="16px" Height="16" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ID" HeaderText="Código" />
                                        <asp:BoundField DataField="NOME" HeaderText="Nome" />
                                        <asp:BoundField DataField="DATA_NASCIMENTO" HeaderText="Data Nascimento" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="ESTADO_CIVIL" HeaderText="Estado Civil" />
                                        <asp:BoundField DataField="CARGO.DESCRICAO" HeaderText="Função" />
                                        <asp:BoundField DataField="CONGREGACAO.Nome" HeaderText="Congregação" />
                                        <asp:BoundField DataField="ATIVO" HeaderText="Ativo" />
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
        <%--</contenttemplate>--%>
         <%--</asp:UpdatePanel>--%>
            <asp:Panel ID="pnCadastro" runat="server" Width="110%">
                <div id="tabs" style="position:absolute; z-index:0">
                    <ul>
                        <li><a href="#tabs-1">Dados Pessoais</a></li>
                        <li><a href="#tabs-2">Endereço</a></li>
                        <li><a href="#tabs-3">Contatos</a></li>
                    </ul>
                    <div id="tabs-1">
                        <table style="width: 100%;">
                            <tr>
                                <td class="style2">
                                    <table class="style7">
                                        <tr>
                                            <td class="style22">
                                                <asp:Label ID="lblNome" runat="server" Text="Nome"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNome" ErrorMessage="Nome" ForeColor="red" ValidationGroup="1">*
                                    </asp:RequiredFieldValidator>
                                            </td>
                                            <td class="style22">
                                                <asp:Label ID="lblApelido" runat="server" Text="Apelido"></asp:Label>
                                            </td>
                                            <td class="style22">
                                                <asp:Label ID="lblCargo" runat="server" Text="Função"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddCargos" ErrorMessage="Cargo" ForeColor="red" ValidationGroup="1">*
                                                </asp:RequiredFieldValidator>
                                            </td>
                                            <td class="style22">
                                                <asp:Label ID="lblAtivo" runat="server" Text="Ativo"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style13">
                                                <asp:TextBox ID="txtNome" runat="server" Width="95%"></asp:TextBox>
                                                <asp:Label ID="lblIdDados" runat="server" Visible="false"></asp:Label>
                                                <td class="style13">
                                                    <asp:TextBox ID="txtApelido" runat="server" Width="95%"></asp:TextBox>
                                                </td>
                                                <td class="style13">
                                                    <asp:DropDownList ID="ddCargos" runat="server" BackColor="White" CssClass="labelCampos" DataTextField="Descricao" DataValueField="id" Width="100%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="style13">
                                                    <asp:CheckBox ID="ckbAtivo" runat="server" Checked="true" />
                                                </td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                        </table>
                        <table style="width:100%;">
                            <tr>
                                <td class="style2">
                                    <table cellspacing="1" class="style7">
                                        <tr>
                                        <td class="style22">
                                            <asp:Label ID="lblRg" runat="server" Text="RG"></asp:Label>
                                        </td>
                                        <td class="style22">
                                            <asp:Label ID="lblCpf" runat="server" Text="Cpf"></asp:Label>
                                        </td>
                                        <td class="style22">
                                            <asp:Label ID="lblDataNascimento" runat="server" Text="Data Nascimento"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDataNascimento"
                                                ForeColor="red" ErrorMessage="Data Nascimento" ValidationGroup="1">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="style22">
                                            <asp:Label ID="lblEstadoCivil" runat="server" Text="Estado Civil"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddEstadoCivil"
                                                ForeColor="red" ErrorMessage="Estado Civil" ValidationGroup="1">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                            <td class="style22">
                                            <asp:Label ID="lblSexo" runat="server" Text="Sexo"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddSexo"
                                                ForeColor="red" ErrorMessage="Sexo" ValidationGroup="1">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        </tr>
                                        <tr>
                                            <td class="style13">
                                                <asp:TextBox ID="txtRG" runat="server" Width="95%"></asp:TextBox>
                                            </td>
                                            <td class="style13">
                                                <asp:TextBox ID="txtCpf" runat="server" Width="95%" MaxLength="11"></asp:TextBox>                                                
                                            </td>
                                            <td class="style13">                                                
                                                <asp:TextBox ID="txtDataNascimento" runat="server" Width="80%"></asp:TextBox>
                                            </td>
                                            <td class="style13">
                                                <asp:DropDownList ID="ddEstadoCivil" runat="server" Width="95%" CssClass="labelCampos"
                                                BackColor="White">
                                                    <asp:ListItem Value="" Text=""/>
                                                    <asp:ListItem Value="1" Text="Casado"/>
                                                    <asp:ListItem Value="2" Text="Solteiro"/>
                                                    <asp:ListItem Value="3" Text="Viúvo"/>
                                                    <asp:ListItem Value="4" Text="Divorciado"/>
                                            </asp:DropDownList>
                                            </td>
                                            <td class="style13">
                                                <asp:DropDownList ID="ddSexo" runat="server" Width="95%" CssClass="labelCampos"
                                                BackColor="White">
                                                    <asp:ListItem Value="" Text=""/>
                                                    <asp:ListItem Value="1" Text="M"/>
                                                    <asp:ListItem Value="2" Text="F"/>                                                    
                                            </asp:DropDownList>
                                            </td>
                                        </tr>                                        
                                    </table>
                                </td>
                            </tr>
                            </table>
                            <table style="width:100%;" >
                            <tr>
                                <td class="style2" colspan="2">
                                    <table class="style7">
                                        <tr>
                                            <td class="style22">
                                                <asp:Label ID="lblNomePai" runat="server" Text="Nome Pai"></asp:Label>                                                
                                            </td>
                                            <td class="style22">
                                                <asp:Label ID="lblNomeMae" runat="server" Text="Nome Mãe"></asp:Label>                                                
                                            </td>
                                            <td class="style22">
                                                <asp:Label ID="lblConjugue" runat="server" Text="Nome Conjugue"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td class="style13" >
                                                <asp:TextBox ID="txtNomePai" runat="server" Width="95%"></asp:TextBox>
                                            </td>
                                            <td class="style13">
                                                <asp:TextBox ID="txtNomeMae" runat="server" Width="95%"></asp:TextBox>
                                            </td>
                                            <td class="style13">
                                                <asp:TextBox ID="txtConjugue" runat="server" Width="95%"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style22">
                                                <asp:Label ID="lblDataAdmissao" runat="server" Text="Data Admissão"></asp:Label>                                                
                                            </td>
                                            <td class="style22">
                                                <asp:Label ID="lblDataBatismo" runat="server" Text="Data Batismo"></asp:Label>
                                            </td>
                                            <td class="style22">
                                                <asp:Label ID="lblDataConsagracao" runat="server" Text="Data Consagração"></asp:Label>
                                             </td>
                                        </tr>
                                        <tr>
                                            <td class="style13">
                                                <asp:TextBox ID="txtDataAdmissao" runat="server" Width="80%"></asp:TextBox>
                                            </td>
                                            <td class="style13">
                                                <asp:TextBox ID="txtDataBatismo" runat="server" Width="80%"></asp:TextBox>
                                            </td>
                                            <td class="style13">
                                                <asp:TextBox ID="txtDataConsagracao" runat="server" Width="80%"></asp:TextBox>
                                            </td>
                                        </tr>
                             
                                        <tr>
                                            <td class="style22" colspan="3">
                                                <asp:Label ID="lblNaturalidade" runat="server" Text="Natural de"></asp:Label>                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <p class="style22">UF</p>
                                                
                                                <asp:DropDownList ID="ddUfNaturalidade" runat="server" Width="60px" AutoPostBack="true" CssClass="labelCampos"
                                                    BackColor="White" DataTextField="uf" DataValueField="id">                                                
                                                </asp:DropDownList>
                                                
                                            </td>
                                            <td>
                                                <p class="style22">Cidade</p>
                                                <asp:DropDownList ID="ddCidadeNaturalidade" runat="server" Width="260px" CssClass="labelCampos"
                                                    BackColor="White" DataTextField="Descricao" DataValueField="id">
                                                </asp:DropDownList>
                                            </td>
                                            <td>                                               
                                                <p class="style22">Congregação</p>                                                
                                                <asp:DropDownList ID="ddCongregacao" runat="server" Width="260px" CssClass="labelCampos"
                                                    BackColor="White" DataTextField="Nome" DataValueField="id">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddCongregacao"
                                                    ForeColor="red" ErrorMessage="Congregação" ValidationGroup="1">*
                                                </asp:RequiredFieldValidator>
                                            </td>                                            
                                        </tr>
                                        <tr>
                                            <td class="style22">
                                                Credencial
                                            </td>
                                            <td class="style22">
                                                Número Credencial
                                            </td>
                                            <td class="style22">
                                                Departamento
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddCredencial" runat="server" AutoPostBack="true">
                                                    <asp:ListItem Value=""  Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="S">Sim</asp:ListItem>
                                                    <asp:ListItem Value="N">Não</asp:ListItem>
                                                </asp:DropDownList>                                                
                                            </td>
                                            <td class="style13">
                                                <asp:TextBox ID="txtNumCredencial" runat="server" Width="100%"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddDepartamento" runat="server" AutoPostBack="true" CssClass="labelCampos"
                                                    BackColor="White" DataTextField="Descricao" DataValueField="id" Width="260px">                                                    
                                                </asp:DropDownList>  
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddDepartamento"
                                                    ForeColor="red" ErrorMessage="Departamento" ValidationGroup="1">*
                                                </asp:RequiredFieldValidator>                                              
                                            </td>
                                        </tr>

                                    </table>

                                </td>

                            </tr>
                        </table>
                    </div>
                    <div id="tabs-2">
                        <table cellspacing="1" style="width: 100%">
                                    <tr>
                                        <td class="style20">
                                            <asp:Label ID="lblEndereco1" runat="server" Text="Endereço"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtEndereco"
                                                CssClass="labelMensagem" ForeColor="red" ErrorMessage="Endereço" ValidationGroup="1">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="labelCampos">
                                            <asp:Label ID="lblBairro" runat="server" Text="Bairro"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtBairro"
                                                CssClass="labelMensagem" ForeColor="red" ErrorMessage="Bairro" ValidationGroup="1">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="labelCampos">
                                            <asp:Label ID="lblUf" runat="server" Text="UF"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddUf"
                                                CssClass="labelMensagem" ForeColor="red" ErrorMessage="UF" ValidationGroup="1">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="labelCampos">
                                            <asp:Label ID="lblCidade" runat="server" Text="Cidade"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddCidade"
                                                CssClass="labelMensagem" ForeColor="red" ErrorMessage="Cidade" ValidationGroup="1">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style21">
                                            <asp:TextBox ID="txtEndereco" runat="server" Width="90%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBairro" runat="server" Width="70%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddUf" runat="server" Width="60px" AutoPostBack="true"
                                                BackColor="White" DataTextField="uf" DataValueField="id">                                            
                                                
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddCidade" runat="server" Width="95%"
                                                BackColor="White" DataTextField="Descricao" DataValueField="id">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="labelCampos" colspan="4">
                                            <asp:Label ID="lblCep" runat="server" Text="CEP"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style21">
                                            <asp:TextBox ID="txtCEP" runat="server" Width="435px" MaxLength="10"></asp:TextBox>
                                        </td>                                        
                                    </tr>
                                </table>
                    </div>
                    <div id="tabs-3">
                        <table style="width: 100%;">
                        <tr>
                            <td>                                
                                &nbsp;<asp:ValidationSummary ID="ValidationSummary2" runat="server" ForeColor="Red"
                                    CssClass="labelMensagem" ValidationGroup="2" HeaderText="Campos obrigatórios" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellspacing="1" style="width: 100%">
                                    <tr>
                                        <td class="style23">
                                            <asp:Label ID="lblTipoContato" runat="server" Text="Tipo de contato"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddTipoContato"
                                                ForeColor="red" CssClass="labelMensagem" ValidationGroup="2" ErrorMessage="Tipo">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="labelCampos" colspan="2">
                                            <asp:Label ID="lblContato" runat="server" Text="Contato"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtContato"
                                                CssClass="labelMensagem" ValidationGroup="2" ForeColor="red" ErrorMessage="Contato">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style24">
                                            <asp:DropDownList ID="ddTipoContato" runat="server" Width="260px" AutoPostBack="true"
                                                CssClass="labelCampos" BackColor="White">
                                                <asp:ListItem Text=""></asp:ListItem>
                                                <asp:ListItem Text="Fixo" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Celular" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Email" Value="3"></asp:ListItem>                                                                                             
                                            </asp:DropDownList>
                                        </td>
                                        <td class="style19">
                                            <asp:TextBox ID="txtContato" runat="server" Width="449px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnAddContato" runat="server" ImageUrl="../images/add.png"
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
                                                            <asp:ImageButton ID="btnEditarContato" runat="server" ImageUrl="../images/editpb.png"
                                                                CommandName="EditarContato" CausesValidation="false" ToolTip="Editar contato" Width="16px" Height="16"/>
                                                            <asp:ImageButton ID="btnExcluirContato" runat="server" ImageUrl="../images/deletePb.png"
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
                    </div>
                </div>
                <asp:HiddenField ID="selected_tab" runat="server" />
            </asp:Panel>

        
    <%--</asp:UpdatePanel>--%>
</asp:Content>
<%--<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>--%>
