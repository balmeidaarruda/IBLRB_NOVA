<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="IBLRwebforms1.login1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>IBLRB - Igreja Batista Livre Renovada do Brasil</title>
    <meta http-equiv="Pragma" content="no-cache"/>
    <meta http-equiv="Expires" content="-1"/>
    <meta name="keywords" content="no-cache" />
	<meta name="description" content="no-cache" />
	<meta name="robots" content="no-cache" />
	<meta charset="utf-8" />

    <script type="text/javascript" src="../Scripts/jquery.js"></script>
	<script type="text/javascript" src="../Scripts/jquery.query-2.1.7.js"></script>
	<script type="text/javascript" src="../Scripts/rainbows.js"></script>

    <link type="text/css" rel="stylesheet" href="css/style.css" media="screen" />

    <script>


        $(document).ready(function () {

            $("#submit1").hover(
            function () {
                $(this).animate({ "opacity": "0" }, "slow");
            },
            function () {
                $(this).animate({ "opacity": "1" }, "slow");
            });
        });


</script>


</head>
<body>
    <form runat="server" id="frmLogin" autocomplete="off">
	<div id="wrapper">
		<div id="wrappertop"></div>
        <div id="wrappermiddle">
            <div style="padding-left:40px;">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Informe o usuário!" ControlToValidate="UserName" ForeColor="Red"></asp:RequiredFieldValidator><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Informe a senha!" ControlToValidate="Password" ForeColor="Red"></asp:RequiredFieldValidator>     
            </div>

			<div id="username_input">
                

				<div id="username_inputleft"></div>

				<div id="username_inputmiddle">
				<%--<form action="">--%>
                    
                    <asp:TextBox ID="UserName" runat="server" CssClass="url" AutoCompleteType="Disabled"></asp:TextBox>
					<%--<input type="text" name="link" class="url" runat="server" id="UserName">--%>
					<img id="url_user" src="./images/mailicon.png">
				<%--</form>--%>
				</div>

				<div id="username_inputright"></div>

			</div>

			<div id="password_input">
                
				<div id="password_inputleft"></div>

				<div id="password_inputmiddle" aria-autocomplete="none">
				<%--<form action="">--%>
                   
                    <asp:TextBox ID="Password" runat="server" CssClass="url" TextMode="Password" AutoCompleteType="Disabled"></asp:TextBox>
					<%--<input type="password" name="link" class="url" runat="server" id="Password">--%>
					<img id="url_password" src="./images/passicon.png">
				<%--</form>--%>
				</div>

				<div id="password_inputright"></div>

			</div>

			<div id="submit">
				<form action="">
                    <asp:ImageButton ID="ImageButton1" ImageUrl="./images/submit_hover.png" CssClass="submit2"  runat="server" />
				<%--<input type="image" src="./images/submit_hover.png" id="submit1" value="Sign In">
				<input type="image" src="./images/submit.png" id="submit2" value="Sign In">--%>
				</form>
			</div>


			<div id="links_left">

			<a href="#" style="font-style:italic;" >Esqueceu sua senha?</a>

			</div>

			<div id="links_right"><a href="#" style="font-style:italic;">Cadastrar?</a></div>

		</div>

		<div id="wrapperbottom"></div>
		
		<div id="powered">
		<p><span style="font-family:'Agency FB';"> Copyright  &copy; <span style="font-family:'Agency FB';font-style:"italic";> <%: DateTime.Now.Year%> - IBLR </span></p>
		</div>
	</div>
    </form>
</body>
</html>
