<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NoweWykonaniePrzypadku.aspx.cs" Inherits="Tracktracer.WykonaniePrzypadku" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Label ID="projekt_Label" runat="server" Text="Label"></asp:Label>
    <br />
    <br />
    <asp:Label ID="przypadek_Label" runat="server" Text="Label"></asp:Label>
    <br />
    <br />
    <asp:Label ID="wynik_Label" runat="server" Text="Wynik: "></asp:Label>
    <asp:DropDownList ID="wynik_DropDownList" runat="server">
        <asp:ListItem>Zaliczony</asp:ListItem>
        <asp:ListItem>Nie zaliczony</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    <asp:Label ID="komentarz_Label" runat="server" Text="Komentarz: "></asp:Label>
    <asp:TextBox ID="komentarz_TextBox" runat="server" Height="89px" 
        TextMode="MultiLine" Width="400px"></asp:TextBox>
&nbsp;<asp:Label ID="komentarzR_Label" runat="server" Text="Maksymalna długość komentarza to 255 znaków." ForeColor="Red" Visible="false"></asp:Label>
    <br />
    <br />
    <asp:Button ID="anuluj_Button" runat="server" onclick="anuluj_Button_Click" 
        Text="Anuluj" />
&nbsp;&nbsp;
    <asp:Button ID="wykonanie_Button" runat="server" 
        Text="Dodaj wykonanie przypadku testowego" 
        onclick="wykonanie_Button_Click" />
&nbsp;
</asp:Content>
