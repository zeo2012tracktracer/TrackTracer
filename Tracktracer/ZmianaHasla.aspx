<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ZmianaHasla.aspx.cs" Inherits="Tracktracer.ZmianaHasla" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" Height="270px" style="text-align: right" 
        Width="256px">
        <asp:Label ID="Label3" runat="server" Text="Zmiana hasła"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Nowe hasło: " 
            style="text-align: right"></asp:Label>
        <asp:TextBox ID="pass_TextBox" runat="server" TextMode="Password" 
            ValidationGroup="1"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Powtórz hasło: "></asp:Label>
        <asp:TextBox ID="repPass_TextBox" runat="server" TextMode="Password" 
            ValidationGroup="1"></asp:TextBox>
        <br />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="pass_TextBox" ErrorMessage="Wprowadź nowe hasło." 
            ValidationGroup="1" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <asp:CompareValidator ID="CompareValidator1" runat="server" 
            ControlToCompare="repPass_TextBox" ControlToValidate="pass_TextBox" 
            ErrorMessage="Wprowadzone hasła muszą być identyczne." ValidationGroup="1" 
            ForeColor="Red"></asp:CompareValidator>
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Anuluj" />
        &nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Zmień hasło" ValidationGroup="1" />
    </asp:Panel>
</asp:Content>
