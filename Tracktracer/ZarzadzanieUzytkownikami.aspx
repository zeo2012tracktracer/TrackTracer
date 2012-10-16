<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ZarzadzanieUzytkownikami.aspx.cs" Inherits="Tracktracer.ZarzadzanieUzytkownikami" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Aktywni użytkownicy"></asp:Label>
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource1" 
        onselectedindexchanged="GridView1_SelectedIndexChanged"
        CellPadding="4" ForeColor="Black" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="login" HeaderText="Login" SortExpression="login" >
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="150px" />
            </asp:BoundField>
            <asp:BoundField DataField="imie" HeaderText="Imię" SortExpression="imie" >
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="150px" />
            </asp:BoundField>
            <asp:BoundField DataField="nazwisko" HeaderText="Nazwisko" SortExpression="nazwisko" >
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="150px" />
            </asp:BoundField>
            <asp:CommandField SelectText="Edytuj" ShowSelectButton="True">
                <ItemStyle ForeColor="Blue" Width="100px" HorizontalAlign="Center"/>
            </asp:CommandField>
        </Columns>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#FFA347" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFF99" />
        <EmptyDataTemplate>
            Brak aktywnych użytkowników.
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
        SelectCommand="SELECT [login], [imie], [nazwisko] FROM [Uzytkownicy] WHERE (([status_konta] = @status_konta) AND ([id] &lt;&gt; @id))">
        <SelectParameters>
            <asp:Parameter DefaultValue="aktywne" Name="status_konta" Type="String" />
            <asp:Parameter DefaultValue="1" Name="id" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Zablokowane konta"></asp:Label>
    <br />
    <br />
    <asp:GridView ID="zabl_GridView" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource2" 
        onselectedindexchanged="GridView2_SelectedIndexChanged1"
        CellPadding="4" ForeColor="Black" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="login" HeaderText="Login" SortExpression="login" >
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="150px" />
            </asp:BoundField>
            <asp:BoundField DataField="imie" HeaderText="Imię" SortExpression="imie" >
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="150px" />
            </asp:BoundField>
            <asp:BoundField DataField="nazwisko" HeaderText="Nazwisko" SortExpression="nazwisko" >
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="150px" />
            </asp:BoundField>
            <asp:CommandField SelectText="Edytuj" ShowSelectButton="True">
                <ItemStyle ForeColor="Blue" Width="100px" HorizontalAlign="Center"/>
            </asp:CommandField>
        </Columns>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#FFA347" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFF99" />
        <EmptyDataTemplate>
            Brak zablokowanych kont.
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
        SelectCommand="SELECT [login], [imie], [nazwisko] FROM [Uzytkownicy] WHERE (([status_konta] &lt;&gt; @status_konta) AND ([id] &lt;&gt; @id))">
        <SelectParameters>
            <asp:Parameter DefaultValue="aktywne" Name="status_konta" Type="String" />
            <asp:Parameter DefaultValue="1" Name="id" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="Nowe konto" />
</asp:Content>
