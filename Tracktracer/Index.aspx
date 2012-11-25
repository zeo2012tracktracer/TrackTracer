<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Tracktracer.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
    .style1
    {
        text-align: right;
        width: 219px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
<asp:Panel ID="Panel1" runat="server" Height="183px" Width="419px">
    <div class="style1">
        <br />
        <asp:Label ID="Username_label" runat="server" Text=" Login: "></asp:Label>
        <asp:TextBox ID="Login_textbox" runat="server" Width="150px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Hasło: " 
        style="text-align: right"></asp:Label>
        <asp:TextBox ID="Password_textbox" runat="server" TextMode="Password" 
            Width="150px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="ButtonRegister" runat="server" onclick="ButtonRegister_Click" 
            Text="Zarejestruj się" />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Zaloguj" />
        <br />
        <br />
    </div>
</asp:Panel>
<br />
</asp:Content>
