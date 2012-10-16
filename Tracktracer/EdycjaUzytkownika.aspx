<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EdycjaUzytkownika.aspx.cs" Inherits="Tracktracer.EdycjaUzytkownika" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" Height="347px" style="text-align: right" 
        Width="272px">
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            style="text-align: left" Text="Powrót" />
        <br />
        <br />
        <asp:Label ID="status_Label" runat="server" Text="Label"></asp:Label>
        <asp:Button ID="blokuj_Button" runat="server" onclick="blokuj_Button_Click" 
            Text="Zablokuj konto" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Login: "></asp:Label>
        &nbsp;<asp:TextBox ID="login_TextBox" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Imię: "></asp:Label>
        &nbsp;<asp:TextBox ID="imie_TextBox" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label6" runat="server" Text="Nazwisko: "></asp:Label>
        &nbsp;<asp:TextBox ID="nazwisko_TextBox" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Aktualizuj dane" />
        <br />
        <asp:Label ID="kom_Label" runat="server" Text="Wybierz inny login." 
            Visible="False"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="Zmiana hasła:"></asp:Label>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Nowe hasło:"></asp:Label>
        &nbsp;<asp:TextBox ID="pass_TextBox" runat="server" MaxLength="19" 
            TextMode="Password" ValidationGroup="1"></asp:TextBox>
        <br />
        <asp:Label ID="Label4" runat="server" Text="Powtórz hasło: "></asp:Label>
        &nbsp;<asp:TextBox ID="passRep_TextBox" runat="server" MaxLength="19" 
            TextMode="Password" ValidationGroup="1"></asp:TextBox>
        <asp:CompareValidator ID="CompareValidator1" runat="server" 
            ErrorMessage="Wprowadzone hasła muszą być identyczne." 
            ControlToCompare="passRep_TextBox" ControlToValidate="pass_TextBox" 
            ValidationGroup="1"></asp:CompareValidator>
        <br />
        <asp:Label ID="haslo_Label" runat="server" Text="Label" Visible="False"></asp:Label>
        <br />
        <asp:Button ID="password_Button" runat="server" onclick="password_Button_Click" 
            Text="Zmień hasło" ValidationGroup="1" />
    </asp:Panel>
</asp:Content>
