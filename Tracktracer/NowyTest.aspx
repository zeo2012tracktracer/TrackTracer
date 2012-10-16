<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NowyTest.aspx.cs" Inherits="Tracktracer.NowyTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Label ID="projekt_Label" runat="server" Text="Label"></asp:Label>
    <br />
    <br />
    <asp:Table ID="tab1" runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label1" runat="server" Text="Nazwa: "></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="nazwa_TextBox" runat="server" MaxLength="255" Width="400px" ValidationGroup="1"></asp:TextBox>            
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="Label3" runat="server" Text="Opis: "></asp:Label>            
            </asp:TableCell>
            <asp:TableCell>
                <asp:TextBox ID="opis_TextBox" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox>            
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table> 
    
    <asp:Label ID="opisR_Label" runat="server" Visible="False" 
        Text="Maksymalna długość opisu to 500 znaków" ForeColor="Red"></asp:Label>

    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="nazwa_TextBox" ErrorMessage="Musisz podać nazwę." 
        ForeColor="Red" ValidationGroup="1"></asp:RequiredFieldValidator>            
    <br />
    <br />
    <asp:Button ID="powrot_Button" runat="server" onclick="powrot_Button_Click" 
        Text="Powrót" />
&nbsp;&nbsp;&nbsp;
    <asp:Button ID="dodaj_Button" runat="server" onclick="dodaj_Button_Click" 
        Text="Dodaj przypadek testowy" ValidationGroup="1" />
&nbsp;
</asp:Content>
