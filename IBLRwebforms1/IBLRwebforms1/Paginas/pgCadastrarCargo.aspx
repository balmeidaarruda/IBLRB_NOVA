<%@ Page Title="Cargos" Language="vb" AutoEventWireup="false" MasterPageFile="~/Paginas/Site.Master" CodeBehind="pgCadastrarCargo.aspx.vb" Inherits="IBLRwebforms1.pgCadastrarCargo" %>
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
                <h2 class="barraTituloSessao">Cadastro de Cargos</h2>
                <div>
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
                                <asp:Label ID="lblConsultarCampos" runat="server" Text="Consultar Cargos"></asp:Label>
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
                                <asp:ImageButton ID="btnConsultarCargos" runat="server" CausesValidation="false"
                                    CommandName="ConsultarCargos" ImageUrl="~/images/searchpb.png" ToolTip="Buscar dados" Width="16px" Height="16" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" class="labelCampos">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="labelCampos" colspan="3" style="background-color: White;">
                                <div style="overflow: scroll; height: 300px; background-color: White;">
                                    <asp:GridView ID="gdCargos" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        ForeColor="#333333" HeaderStyle-HorizontalAlign="Left" Width="100%" DataKeyNames="Id">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

                                        <Columns>
                                            <asp:TemplateField ShowHeader="true" HeaderText="Editar" ItemStyle-Width="20px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEditarCargos" runat="server" CausesValidation="false"
                                                        CommandName="EditarCargos" ImageUrl="~/images/editpb.png" ToolTip="Editar dados" Width="16px" Height="16" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Id" HeaderText="Código" />
                                            <asp:BoundField DataField="Descricao" HeaderText="Descrição" />
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
                                <asp:Label ID="lblDados" runat="server" Text="Dados do cargo"></asp:Label>
                            </td>
                        </tr>
                        
                                    <tr>
                                        <td class="style22">
                                            <asp:Label ID="lblDescricao" runat="server" Text="Descrição"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescricao"
                                                ForeColor="red" ErrorMessage="Descrição do cargo" ValidationGroup="1">*
                                            </asp:RequiredFieldValidator>
                                        </td>                                        
                                    </tr>
                                    <tr>
                                        <td class="style13">
                                            <asp:TextBox ID="txtDescricao" runat="server" Width="100%"></asp:TextBox>
                                            <asp:Label ID="lblIdDados" runat="server" Visible="false"></asp:Label>
                                        </td>
                                    </tr>                    
                                
                        </table>
                        
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>