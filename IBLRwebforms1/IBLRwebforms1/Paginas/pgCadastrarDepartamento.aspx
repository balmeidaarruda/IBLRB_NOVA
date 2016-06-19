﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Paginas/Site.Master" CodeBehind="pgCadastrarDepartamento.aspx.vb" Inherits="IBLRwebforms1.pgCadastrarDepartamento" %>

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
            padding-top
            padding-bottom:5px;
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
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="dialog" style="display: none;">
            </div>
            <h2 class="barraTituloSessao">Cadastro Departamento</h2>
            <div>
                <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="botoesBarraAcoes" ValidationGroup="1" CausesValidation="true" Style="background-image: url('Images/savepb.png'); background-repeat: no-repeat; background-position: left; padding-left: 20px;" />
                <asp:Button ID="btnNovoRegistro" runat="server" Text="Novo Registro"
                    CssClass="botoesBarraAcoes" CausesValidation="False" Style="background-image: url('Images/file-add.png'); background-repeat: no-repeat; background-position: left; padding-left: 20px;" />
            </div>
            <div>
                <table style="width: 100%;">
                    <tr>
                        <td class="auto-style1">
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" HeaderText="* Campos obrigatórios" ValidationGroup="1" DisplayMode="BulletList" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">
                            <asp:Label ID="lblDepartamento" runat="server" Text="Departamento"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDepartamento" ErrorMessage="Departamento" ForeColor="red" ValidationGroup="1">*
                            </asp:RequiredFieldValidator>
                            <asp:Label ID="lblIdDados" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style5">
                            <asp:TextBox ID="txtDepartamento" runat="server" Width="60%"></asp:TextBox>
                            <asp:CheckBox ID="ckbAtivo" runat="server" Text="Ativo" Width="30%" Checked="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">
                            <asp:Label ID="lblMembro" runat="server" Text="Líder"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddMembros" ErrorMessage="Líder" ForeColor="red" ValidationGroup="1">*
                            </asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style5">
                            <asp:DropDownList ID="ddMembros" runat="server" DataTextField="Nome" DataValueField="id" BackColor="White" Width="60%" Height="25px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="labelCampos">
                            <div style="overflow: scroll; background-color: White; height: 300px;">
                                <asp:GridView ID="gdDepartamentos" runat="server" AutoGenerateColumns="False"
                                    CellPadding="1" ForeColor="#333333" GridLines="Both" ShowHeaderWhenEmpty="False"
                                    HeaderStyle-HorizontalAlign="Justify" DataKeyNames="id" CellSpacing="2" Width="100%">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEditarDepartamento" runat="server" ImageUrl="~/images/editpb.png"
                                                    CommandName="EditarDepartamento" CausesValidation="false" ToolTip="Editar Tipo Oferta" Width="16px" Height="16" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Id" HeaderText="Código" />
                                        <asp:BoundField DataField="Descricao" HeaderText="Tipo Oferta" />
                                        <asp:BoundField DataField="MembroLiderDpto.id" HeaderText="Cod.Líder" />
                                        <asp:BoundField DataField="MembroLiderDpto.nome" HeaderText="Líder" />
                                        <asp:BoundField DataField="Ativo" HeaderText="Ativo" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
