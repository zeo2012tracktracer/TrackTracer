<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UstawieniaSVN.aspx.cs" Inherits="Tracktracer.UstawieniaSVN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" Height="185px" style="text-align: right" Width="401px">
        <br />
        <asp:Label ID="Label1" runat="server" Text="URL:"></asp:Label>
        &nbsp;<asp:TextBox ID="url_TextBox" runat="server" Width="350px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Nazwa użytkownika: "></asp:Label>
        <asp:TextBox ID="username_TextBox" runat="server" Width="200px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Hasło: "></asp:Label>
        &nbsp;<asp:TextBox ID="pass_TextBox" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="powrot_Button" runat="server" onclick="powrot_Button_Click" 
            Text="Powrót" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="aktualizuj_Button" runat="server" onclick="aktualizuj_Button_Click" Text="Zmień dane połączenia z SVN" />
    </asp:Panel>
</asp:Content>
