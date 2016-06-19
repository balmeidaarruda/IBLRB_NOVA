<%@ Page Title="Logar" Language="VB" MasterPageFile="~/Paginas/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.vb" Inherits="IBLRwebforms1.Login" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
       <h1><img src="../Images/lock.png" style="vertical-align:bottom" /></h1>
        <h1><%: Title %></h1>
    </hgroup>
    
    <section id="loginForm">
        
            
                <p class="validation-summary-errors">
                    <asp:Label ID="lblErro" runat="server" Text="Usuário ou senha incorretos" Visible="False"></asp:Label>
                </p>
        
                <fieldset id="fieldForm" >
                    <legend>Log in Form</legend>
                    <ol>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="UserName">Usuário</asp:Label>
                            <asp:TextBox runat="server" ID="UserName" BorderStyle="Solid" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName" CssClass="field-validation-error" ErrorMessage="Usuário obrigatório." />
                        </li>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="Password">Senha</asp:Label>
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" BorderStyle="Solid" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="field-validation-error" ErrorMessage="Senha obrigatória." />
                        </li>                       
                    </ol>
                    <asp:Button runat="server" CommandName="Login" Text="Entrar" ID="btnLogar" />
                </fieldset>
            
        
        <p>
            <asp:HyperLink runat="server" ID="RegisterHyperLink" ViewStateMode="Disabled">Cadastrar</asp:HyperLink>
            Se você não possui acesso
        </p>
        </section>
    <section>
        <p>
           <img src="Images/logoIblr.png" style="padding-left:5px;padding-top:45px;" />
        </p>
    </section>
    </asp:Content>