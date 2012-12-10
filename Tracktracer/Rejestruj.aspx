<%@ Page Title="TrackTracer" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Rejestruj.aspx.cs" Inherits="Tracktracer.Rejestruj" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="nowyUzytkownik_Label" runat="server" Text="Nowy użytkownik"></asp:Label>
    <asp:Panel ID="Panel1" runat="server">
            <table>
               <tr>
                <td><asp:Label ID="login_Label" runat="server" Text="Login: "></asp:Label></td>
                <td><asp:TextBox ID="login_TextBox" runat="server" MaxLength="10" 
                    style="text-align: right" ValidationGroup="1"></asp:TextBox></td>
                    <td><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="login_TextBox" ErrorMessage="Musisz podać login." CssClass="error"
                    ValidationGroup="1"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                <td><asp:Label ID="Label2" runat="server" Text="Hasło: "></asp:Label></td>
                <td><asp:TextBox ID="haslo_TextBox" runat="server" MaxLength="19" 
                    style="text-align: right" TextMode="Password" ValidationGroup="1"></asp:TextBox></td>
                    <td> <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="haslo_TextBox" ErrorMessage="Musisz podać hasło." CssClass="error"
                    ValidationGroup="1"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                <td><asp:Label ID="Label3" runat="server" Text="Powtórz hasło: "></asp:Label></td>
                <td><asp:TextBox ID="powtHaslo_TextBox" runat="server" MaxLength="19" 
                    style="text-align: right" TextMode="Password" ValidationGroup="1"></asp:TextBox></td>
                    <td> <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToCompare="powtHaslo_TextBox" ControlToValidate="haslo_TextBox" CssClass="error"
                    ErrorMessage="Wprowadzone hasła muszą się zgadzać." ValidationGroup="1"></asp:CompareValidator></td>
                </tr>
                <tr>
                <td><asp:Label ID="Label4" runat="server" Text="Imię: "></asp:Label></td>
                <td><asp:TextBox ID="Imie_TextBox" runat="server" MaxLength="29" 
                    style="text-align: right"></asp:TextBox></td>
                    <td><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="error"
                    ControlToValidate="Imie_TextBox" ErrorMessage="Musisz podać imię." 
                    ValidationGroup="1"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                <td><asp:Label ID="Label5" runat="server" Text="Nazwisko: "></asp:Label></td>
                <td><asp:TextBox ID="Nazwisko_TextBox" runat="server" MaxLength="29" 
                    style="text-align: right"></asp:TextBox></td>
                    <td><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="Nazwisko_TextBox" ErrorMessage="Musisz podać nazwisko." CssClass="error"
                    ValidationGroup="1"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                <td><asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                    style="text-align: right" Text="Anuluj" /></td>
                <td><asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
                    Text="Utwórz konto" ValidationGroup="1" /></td>
                    <td></td>

                </tr>         
           </table>
    </asp:Panel>
<br />
</asp:Content>
