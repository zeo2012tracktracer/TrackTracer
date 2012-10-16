<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PrzypisaniUzytkownicy.aspx.cs" Inherits="Tracktracer.PrzypisaniUzytkownicy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="nLabel" Text="Nazwa projektu: " runat="server"></asp:Label>
    <asp:Label ID="nazwa_Label" runat="server" ForeColor="Black"></asp:Label>
    <br />    
    Przypisani użytkownicy:<br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource1" onselectedindexchanged="GridView1_SelectedIndexChanged" DataKeyNames="login" 
        CellPadding="4" ForeColor="Black" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="login" HeaderText="Login" SortExpression="login" >
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="200px" HorizontalAlign="Center"/>
            </asp:BoundField>                                   
            <asp:BoundField DataField="imie" HeaderText="Imię" SortExpression="imie" ItemStyle-Width="200px" >
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="200px" HorizontalAlign="Center"/>
            </asp:BoundField>
            <asp:BoundField DataField="nazwisko" HeaderText="Nazwisko" SortExpression="nazwisko" ItemStyle-Width="200px">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="200px" HorizontalAlign="Center"/>
            </asp:BoundField>
            <asp:CommandField SelectText="Usuń" ShowSelectButton="True" ItemStyle-ForeColor="Blue" >  
                <ItemStyle Width="100px" HorizontalAlign="Center"/>
            </asp:CommandField>            
        </Columns>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#FFA347" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFF99" />
        <EmptyDataTemplate>
            Brak przypisanych użytkowników.
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
        
        SelectCommand="SELECT u.login, u.imie, u.nazwisko FROM Uzytkownicy u, Uzytkownicy_Projekty up WHERE up.Projekt_id=@id_projektu AND u.id = up.Uzytkownik_id AND u.status_konta='aktywne' AND u.id != @wlasciciel">
        <SelectParameters>
            <asp:SessionParameter Name="id_projektu" SessionField="projekt_id" />
            <asp:SessionParameter Name="wlasciciel" SessionField="wlasciciel" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <asp:Label ID="Label1" runat="server" 
        Text="Użytkownicy nie przypisani do projektu: "></asp:Label>
    <br />
    <br />
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource2" onselectedindexchanged="GridView2_SelectedIndexChanged"
        DataKeyNames="login" CellPadding="4" ForeColor="Black" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="login" HeaderText="Login" SortExpression="login" >
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="200px" HorizontalAlign="Center"/>
            </asp:BoundField>                                   
            <asp:BoundField DataField="imie" HeaderText="Imię" SortExpression="imie" ItemStyle-Width="200px" >
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="200px" HorizontalAlign="Center"/>
            </asp:BoundField>
            <asp:BoundField DataField="nazwisko" HeaderText="Nazwisko" SortExpression="nazwisko" ItemStyle-Width="200px">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle Width="200px" HorizontalAlign="Center"/>
            </asp:BoundField>
            <asp:CommandField SelectText="Przypisz użytkownika" ShowSelectButton="True" ItemStyle-ForeColor="Blue" >  
                <ItemStyle Width="150px" HorizontalAlign="Center"/>
            </asp:CommandField>
        </Columns>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#FFA347" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFF99" />
        <EmptyDataTemplate>
            Brak użytkowników do przypisania.
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
        SelectCommand="SELECT u.login, u.imie, u.nazwisko FROM Uzytkownicy u WHERE u.id != 1 AND u.status_konta = 'aktywne' AND u.id NOT IN ( SELECT up.Uzytkownik_id FROM Uzytkownicy_Projekty up WHERE up.Projekt_id = @proj_id);
">
        <SelectParameters>
            <asp:SessionParameter Name="proj_id" SessionField="projekt_id" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Powrót" />
</asp:Content>
