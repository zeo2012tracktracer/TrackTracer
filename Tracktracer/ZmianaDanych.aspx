<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ZmianaDanych.aspx.cs" Inherits="Tracktracer.ZmianaDanych" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" Height="270px" style="text-align: right" 
        Width="256px">
        <asp:Label ID="Label3" runat="server" Text="Zmiana danych"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Imię: " 
            style="text-align: right"></asp:Label>
        <asp:TextBox ID="imie_TextBox" runat="server" ValidationGroup="1"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Nazwisko: "></asp:Label>
        <asp:TextBox ID="nazwisko_TextBox" runat="server" ValidationGroup="1"></asp:TextBox>                                
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Anuluj" />
        &nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Zmień dane" ValidationGroup="1" />
        <br />
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="imie_TextBox" ErrorMessage="Podaj imię." 
            ValidationGroup="1" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="nazwisko_TextBox" ErrorMessage="Podaj nazwisko." 
        ValidationGroup="1" ForeColor="Red"></asp:RequiredFieldValidator>
    </asp:Panel>
</asp:Content>
