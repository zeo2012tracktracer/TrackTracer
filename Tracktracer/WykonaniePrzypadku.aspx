<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WykonaniePrzypadku.aspx.cs" Inherits="Tracktracer.WykonaniePrzypadku1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Label ID="projekt_Label" runat="server" Text="Projekt: "></asp:Label>
    <br />
    <br />
    <asp:Label ID="przypadek_Label" runat="server" Text="Przypadek testowy: "></asp:Label>
    <br />
    <br />
    <asp:Label ID="tester_Label" runat="server" Text="Tester: "></asp:Label>
    <br />
    <br />
    <asp:Label ID="wynik_Label" runat="server" Text="Wynik: "></asp:Label>
    <br />
    <br />
    <asp:Label ID="data_Label" runat="server" Text="Data wykonania: "></asp:Label>
    <br />
    <br />
    <asp:Label ID="komentarz_Label" runat="server" Text="Komentarz: "></asp:Label>
    <asp:TextBox ID="komentarz_TextBox" runat="server" Height="89px"  Enabled="false"
        TextMode="MultiLine" Width="400px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="powrot_Button" runat="server" onclick="powrot_Button_Click" 
        Text="Powrót" />
</asp:Content>
