<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administracja.aspx.cs" Inherits="Tracktracer.Administracja" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Button ID="zarzadzaj_Button" runat="server" onclick="Button1_Click" 
        Text="Zarządzaj użytkownikami" Visible="False" />
    <br />
    <br />
    <asp:Button ID="haslo_Button" runat="server" Text="Zmiana hasła" 
        onclick="haslo_Button_Click" />
    <br />
    <br />
    <asp:Button ID="dane_Button" runat="server" onclick="dane_Button_Click" 
        Text="Zmiana danych" />
</asp:Content>
