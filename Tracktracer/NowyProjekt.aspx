<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NowyProjekt.aspx.cs" Inherits="Tracktracer.NowyProjekt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Tworzenie nowego projektu"></asp:Label>
    <br />
    <br />
    <asp:TextBox ID="Nazwa_TextBox" runat="server" style="margin-bottom: 0px" 
        Width="600px" ValidationGroup="1">Nazwa projektu</asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="Nazwa_TextBox" ErrorMessage="To pole jest wymagane." 
        ForeColor="Red" ValidationGroup="1"></asp:RequiredFieldValidator>
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Metodyka projektu: "></asp:Label>
    <asp:DropDownList ID="Metodyka_DropDownList" runat="server">
        <asp:ListItem>Scrum</asp:ListItem>
        <asp:ListItem>XP</asp:ListItem>
    </asp:DropDownList>
    <br />
    <br />
    <asp:TextBox ID="Opis_TextBox" runat="server" Height="200px" 
        TextMode="MultiLine" Width="600px" Wrap="False" ValidationGroup="1">Opis projektu</asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="Opis_TextBox" ErrorMessage="To pole jest wymagane." 
        ForeColor="Red" ValidationGroup="1"></asp:RequiredFieldValidator>
    <br />
    <br />
    <asp:Button ID="Anuluj_Button" runat="server" onclick="Anuluj_Button_Click" 
        style="text-align: left" Text="Anuluj" />
&nbsp;&nbsp;
    <asp:Button ID="Dodaj_Button" runat="server" onclick="Dodaj_Button_Click" 
        Text="Dodaj projekt" ValidationGroup="1" />
&nbsp;
</asp:Content>
