<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NowyUzytkownik.aspx.cs" Inherits="Tracktracer.NowyUzytkownik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="nowyUzytkownik_Label" runat="server" Text="Nowy użytkownik"></asp:Label>
    <br />
    <br />
    <asp:Panel ID="Panel1" runat="server" Height="164px" Width="284px">
        <div class="style1">
            <asp:Label ID="login_Label" runat="server" Text="Login: "></asp:Label>
            <asp:TextBox ID="login_TextBox" runat="server" MaxLength="10" 
                style="text-align: right" ValidationGroup="1"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Hasło: "></asp:Label>
            <asp:TextBox ID="haslo_TextBox" runat="server" MaxLength="19" 
                style="text-align: right" TextMode="Password" ValidationGroup="1"></asp:TextBox>
            <br />
            <asp:Label ID="Label3" runat="server" Text="Powtórz hasło: "></asp:Label>
            <asp:TextBox ID="powtHaslo_TextBox" runat="server" MaxLength="19" 
                style="text-align: right" TextMode="Password" ValidationGroup="1"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Imię: "></asp:Label>
            <asp:TextBox ID="Imie_TextBox" runat="server" MaxLength="29" 
                style="text-align: right"></asp:TextBox>
            <br />
            <asp:Label ID="Label5" runat="server" Text="Nazwisko: "></asp:Label>
            <asp:TextBox ID="Nazwisko_TextBox" runat="server" MaxLength="29" 
                style="text-align: right"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                style="text-align: right" Text="Anuluj" />
            &nbsp;<asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
                Text="Utwórz konto" ValidationGroup="1" />
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="login_TextBox" ErrorMessage="Musisz podać login." 
                ValidationGroup="1"></asp:RequiredFieldValidator>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="haslo_TextBox" ErrorMessage="Musisz podać hasło." 
                ValidationGroup="1"></asp:RequiredFieldValidator>
            <br />
            <asp:CompareValidator ID="CompareValidator1" runat="server" 
                ControlToCompare="powtHaslo_TextBox" ControlToValidate="haslo_TextBox" 
                ErrorMessage="Wprowadzone hasła muszą się zgadzać." ValidationGroup="1"></asp:CompareValidator>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="Imie_TextBox" ErrorMessage="Musisz podać imię." 
                ValidationGroup="1"></asp:RequiredFieldValidator>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                ControlToValidate="Nazwisko_TextBox" ErrorMessage="Musisz podać nazwisko." 
                ValidationGroup="1"></asp:RequiredFieldValidator>
        </div>
    </asp:Panel>
</asp:Content>
