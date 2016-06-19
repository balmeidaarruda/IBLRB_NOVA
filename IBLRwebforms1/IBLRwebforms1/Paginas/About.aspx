<%@ Page Title="Sobre" Language="VB" MasterPageFile="~/Paginas/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.vb" Inherits="IBLRwebforms1.About" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Sistema de gerenciamento IBLR</h2>
    </hgroup>

    <article>
        <p>        
            O Sistema foi criado para cadastro de membros,<br />
            visitantes, congregações e campos
            
        </p>
        <p>        
            Também proporciona o controle de dízimos e ofertas.
        </p>
        
    </article>

</asp:Content>