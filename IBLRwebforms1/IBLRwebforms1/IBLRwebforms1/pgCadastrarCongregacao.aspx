<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="pgCadastrarCongregacao.aspx.vb" Inherits="IBLRwebforms1.CadastrarCongregacao" %>

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
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="barraAcoes">
                    <asp:Button ID="btnSalvar" runat="server"  Text="Salvar" CssClass="botoesBarraAcoes" ValidationGroup="1" CausesValidation="true" style="background-image:url('Images/savepb.png');background-repeat:no-repeat; background-position:left;padding-left:20px;"/>
                    <asp:Button ID="btnNovoRegistro" runat="server" Text="Novo Registro"
                        CssClass="botoesBarraAcoes" CausesValidation="False" style="background-image:url('Images/file-add.png');background-repeat:no-repeat; background-position:left;padding-left:20px;" />
                    <asp:Button ID="btnConsultar" runat="server" Text="Consultar"
                        CssClass="botoesBarraAcoes" CausesValidation="False" style="background-image:url('Images/searchpb.png');background-repeat:no-repeat; background-position:left;padding-left:20px;" />
                </div>
                <asp:Panel ID="pnConsulta" runat="server">
                    <table style="width: 100%;">
                        <tr>
                            <td class="barraTituloSessao" colspan="3">
                                <asp:Label ID="lblConsultarMembros" runat="server" Text="Consultar Membros"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="labelCampos">
                                <asp:Label ID="lblConsultarPor" runat="server" Text="Consultar por"></asp:Label>
                            </td>
                            <td class="labelCampos" colspan="2">
                                <asp:Label ID="lblCampoConsulta" runat="server" Text="Descrição"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:DropDownList ID="ddTipoConsulta" runat="server" Width="260px" CssClass="labelCampos"
                                    BackColor="White">
                                    <asp:ListItem Text="Nome da congregação" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Pastor Responsável" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Todos" Value ="3"></asp:ListItem>
                                    
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCampoConsulta" runat="server" Width="760px"></asp:TextBox>
                            </td>
                            <td style="width: 160px">
                                <asp:ImageButton ID="btnConsultarCongregacoes" runat="server" CausesValidation="false"
                                    CommandName="ConsultarCongregacoes" ImageUrl="~/images/searchpb.png" ToolTip="Buscar dados" Width="16px" Height="16" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" class="labelCampos">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="labelCampos" colspan="3" style="background-color: White;">
                                <div style="overflow: scroll; height: 300px; background-color: White;">
                                    <asp:GridView ID="gdCongregacoes" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" HeaderStyle-HorizontalAlign="Left" Width="100%" DataKeyNames="id">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

                                        <Columns>
                                            <asp:TemplateField ShowHeader="true" HeaderText="Editar" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEditarCongregacoes" runat="server" CausesValidation="false"
                                                        CommandName="EditarCongregacoes" ImageUrl="~/images/editpb.png" ToolTip="Editar dados" Width="16px" Height="16" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Id" HeaderText="Código" />
                                            <asp:BoundField DataField="Nome" HeaderText="Nome" />
                                            <asp:BoundField DataField="PastorResponsavel" HeaderText="Pastor Responsável" />
                                            <asp:BoundField DataField="DataFundacao" HeaderText="Data Fundação" DataFormatString="{0:d}" />
                                            <asp:BoundField DataField="Cidade.Descricao" HeaderText="Cidade" />
                                            <asp:BoundField DataField="Campo.Nome" HeaderText="Campo" />
                                            <%--<asp:BoundField DataField="" HeaderText="" />
                                        <asp:BoundField DataField="Endereco" HeaderText="Endereço" />--%>
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
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="* Campos obrigatórios" ValidationGroup="1"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="barraTituloSessao">
                                <asp:Label ID="lblDadosCongregacao" runat="server" Text="Dados da congregação"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <table cellspacing="1" class="style7">
                                    <tr>
                                        <td class="style22">
                                            <asp:Label ID="lblNome" runat="server" Text="Nome"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNome"
                                                ForeColor="red" ErrorMessage="Nome" ValidationGroup="1">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="labelCampos">
                                            <asp:Label ID="lblDataFundacao" runat="server" Text="Data Fundação"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtDataFundacao"
                                                ForeColor="red" ErrorMessage="Data fundação" ValidationGroup="1">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style13">
                                            <asp:TextBox ID="txtNome" runat="server" Width="100%"></asp:TextBox>
                                            <asp:Label ID="lblIdDados" runat="server" Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDataFundacao" runat="server"
                                                ToolTip="Formato(00/00/0000)" Width="50%"></asp:TextBox>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="style22" colspan="2">
                                            <asp:Label ID="lblPastorResponsavel" runat="server" Text="Pastor Responsável"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPastorResponsavel"
                                                CssClass="labelMensagem" ForeColor="red" ErrorMessage="Pastor responsável" ValidationGroup="1">*
                                            </asp:RequiredFieldValidator>

                                        </td>
                                        <td class="labelCampos">
                                            <asp:Label ID="lblCampo" runat="server" Text="Campo"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddCampo"
                                                CssClass="labelMensagem" ForeColor="red" ErrorMessage="Campo" ValidationGroup="1">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtPastorResponsavel" runat="server" Width="100%"></asp:TextBox>
                                        </td>
                                        
                                        <td>
                                            <asp:DropDownList ID="ddCampo" runat="server" Width="100%" CssClass="labelCampos"
                                                BackColor="White" DataTextField="nome" DataValueField="id">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="barraTituloSessao">
                                <asp:Label ID="lblLocalizacao" runat="server" Text="Localização"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellspacing="1" style="width: 100%">
                                    <tr>
                                        <td class="style20">
                                            <asp:Label ID="lblEndereco1" runat="server" Text="Endereço"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtEndereco"
                                                CssClass="labelMensagem" ForeColor="red" ErrorMessage="Endereço" ValidationGroup="1">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="labelCampos">
                                            <asp:Label ID="lblBairro" runat="server" Text="Bairro"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtBairro"
                                                CssClass="labelMensagem" ForeColor="red" ErrorMessage="Bairro" ValidationGroup="1">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="labelCampos">
                                            <asp:Label ID="lblUf" runat="server" Text="UF"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddUf"
                                                CssClass="labelMensagem" ForeColor="red" ErrorMessage="UF" ValidationGroup="1">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td class="labelCampos">
                                            <asp:Label ID="lblCidade" runat="server" Text="Cidade"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddCidade"
                                                CssClass="labelMensagem" ForeColor="red" ErrorMessage="Cidade" ValidationGroup="1">*
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style21">
                                            <asp:TextBox ID="txtEndereco" runat="server" Width="435px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtBairro" runat="server" Width="160px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddUf" runat="server" Width="60px" AutoPostBack="true" CssClass="labelCampos"
                                                BackColor="White" DataTextField="uf" DataValueField="id">                                            
                                                
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddCidade" runat="server" Width="260px" CssClass="labelCampos"
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
                            </td>
                        </tr>
                        <tr>
                            <td class="labelCampos">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="barraTituloSessao">
                                <asp:Label ID="lblContatos" runat="server" Text="Contatos"></asp:Label>
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
                                                                CommandName="EditarContato" CausesValidation="false" ToolTip="Editar contato" Width="16px" Height="16"/>
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
<%--<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Cadastro de congregaçoes</h3>
    <fieldset id="fieldFormCadastroCongregacao">
        <legend>Cadastro Congregacao</legend>
        <table>
            <tr>
                <td style="width:20%">
                    <asp:Label ID="lblNome" runat="server" AssociatedControlID="txtNome">Nome</asp:Label>
                </td>
                <td style="width:100%">
                    <asp:TextBox runat="server" ID="txtNome" BorderStyle="Solid" Width="100%" />                    
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNome" CssClass="field-validation-error" ErrorMessage="Obrigatório." Font-Italic="True" Font-Size="X-Small" />
                </td>
            </tr>

            <tr>
                <td style="width:20%">
                    <asp:Label ID="lblPastorResponsavel" runat="server" AssociatedControlID="txtPastorResponsavel">Pastor responsável</asp:Label>
                </td>
                <td style="width:100%">
                    <asp:TextBox runat="server" ID="txtPastorResponsavel" BorderStyle="Solid" Width="100%" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPastorResponsavel" CssClass="field-validation-error" ErrorMessage="Obrigatório." Font-Italic="True" Font-Size="X-Small" />
                </td>
            </tr>
            <tr>
                <td style="width:20%">
                    <asp:Label ID="lblDataFundacao" runat="server" AssociatedControlID="txtDataFundacao">Data fundação</asp:Label>
                </td>
                <td style="width:100%">
                    <asp:TextBox runat="server" ID="txtDataFundacao" BorderStyle="Solid" Width="20%" />
                </td>
                <td>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPastorResponsavel" CssClass="field-validation-error" ErrorMessage="Obrigatório." Font-Italic="True" Font-Size="X-Small" />
                </td>
            </tr>
            
            <tr>
                <td style="width:20%">
                    <asp:Label ID="lblEndereco" runat="server" AssociatedControlID="txtEndereco">Endereço</asp:Label>
                </td>
                <td style="width:100%">
                    <asp:TextBox runat="server" ID="txtEndereco" BorderStyle="Solid" Width="100%" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEndereco" CssClass="field-validation-error" ErrorMessage="Obrigatório." Font-Italic="True" Font-Size="X-Small" />
                </td>
            </tr>
            <tr>
                <td style="width:20%">
                    <asp:Label ID="lblBairro" runat="server" AssociatedControlID="lblBairro">Bairro</asp:Label>
                </td>
                <td style="width:80%">
                    <asp:TextBox runat="server" ID="txtBairro" BorderStyle="Solid" Width="50%" />
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtBairro" CssClass="field-validation-error" ErrorMessage="Obrigatório." Font-Italic="True" Font-Size="X-Small" />
                </td>

                <td style="width:auto;">
                    <asp:Label ID="lblCep" runat="server" AssociatedControlID="lblCep">CEP</asp:Label>
                </td>
                <td style="width:auto">
                    <asp:TextBox runat="server" ID="txtCep" BorderStyle="Solid" Width="50%" />
                </td >
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCep" CssClass="field-validation-error" ErrorMessage="Obrigatório." Font-Italic="True" Font-Size="X-Small" />
                </td>
            </tr>

        </table>

          <asp:Button runat="server" CommandName="btnSalvar" Text="Salvar" ID="btnSalvar" />
   </fieldset>
</asp:Content>--%>
