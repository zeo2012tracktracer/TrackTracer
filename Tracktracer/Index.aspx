<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Tracktracer.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
<asp:Panel ID="Panel1" runat="server" Height="183px" Width="419px">
    <table>
    <tr>
        <td>
        <asp:Label ID="Username_label" runat="server" Text=" Login: "></asp:Label></td>
        <td><asp:TextBox ID="Login_textbox" runat="server" Width="150px" ValidationGroup="1"></asp:TextBox></td>
        <td><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="1" 
                    ControlToValidate="Login_textbox" ErrorMessage="Nie podałeś loginu, bądź wpisałeś złe dane." CssClass="error" 
                    ></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td><asp:Label ID="Label1" runat="server" Text="Hasło: " 
        style="text-align: right"></asp:Label></td>
        <td><asp:TextBox ID="Password_textbox" runat="server" TextMode="Password" ValidationGroup="1"
            Width="150px" ></asp:TextBox></td>
        <td><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="1"
                        ControlToValidate="Password_textbox" ErrorMessage="Musisz podać hasło." CssClass="error"
                        ></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td><asp:Button ID="ButtonRegister" runat="server" onclick="ButtonRegister_Click" 
            Text="Zarejestruj się" /></td>
        <td><asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Zaloguj" ValidationGroup="1"/></td>
    </tr>
    </table>
</asp:Panel>
<br />
</asp:Content>
