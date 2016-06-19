<%@ Page Title="Contato" Language="VB" MasterPageFile="~/Paginas/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.vb" Inherits="IBLRwebforms1.Contact" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Contatos com a sede IBLR.</h2>
    </hgroup>

    <section class="contact">
        <header>
            <h3>Telefones:</h3>
        </header>
        <p>
            <span class="label">Fixo:</span>
            <span>(62)3280-9409</span>
        </p>
        <p>
            <span class="label">Pr.Antônio Conceição:</span>
            <span>(62)8585-1031</span>
            <span>(62)9291-5880</span>
        </p>
    </section>

    <section class="contact">
        <header>
            <h3>Email:</h3>
        </header>
        <p>
            <span class="label">IBLR - SEDE:</span>
            <span>iblr@gmail.com</span>
        </p>        
    </section>

    <section class="contact">
        <header>
            <h3>Endereço:</h3>
        </header>
        <p>
            Rua Paraguaçu<br />
            Vila Brasília - Aparecida de Goiânia - GO
        </p>
    </section>
</asp:Content>