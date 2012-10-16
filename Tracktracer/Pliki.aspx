<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pliki.aspx.cs" Inherits="Tracktracer.Pliki" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <asp:Table ID="Table1" runat="server">
        <asp:TableRow>
            <asp:TableCell>
                Nazwa projektu: 
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="nazwa_Label" runat="server" Text="Label" ForeColor="Black" ></asp:Label>                
            </asp:TableCell>                
        </asp:TableRow>
        <asp:TableRow>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                Numer aktualnej rewizji:&nbsp;&nbsp;
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="rew_Label" runat="server" Text="Label" ForeColor="Black"></asp:Label>                 
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>    
    <br />
    <br />
    <asp:Label ID="pliki_Label" runat="server" Text="Pliki projektu:" ></asp:Label>
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="id" DataSourceID="SqlDataSource1" AllowSorting="True" CellPadding="4" ForeColor="#333333" 
        OnRowCommand="GridView_RowCommand" GridLines="None">                    
        <AlternatingRowStyle BackColor="White" />
        <Columns>                        
            <asp:TemplateField HeaderText="Nazwa pliku" SortExpression="nazwa">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:LinkButton ID="plikLink" CommandName="Plik" CommandArgument='<%#Eval("id") %>' runat="server">                                        
                        <%#Eval("nazwa") %>
                        </asp:LinkButton>                        
                    </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="sciezka" HeaderText="Położenie pliku" SortExpression="sciezka" >
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Left" />
            </asp:BoundField>            
        </Columns>       
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" BorderColor="Blue" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />       
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
        SelectCommand="SELECT [id], [nazwa], [sciezka] FROM [Pliki] WHERE ([Projekty_id] = @Projekty_id)">
        <SelectParameters>
            <asp:SessionParameter Name="Projekty_id" SessionField="projekt_id" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <br />
    <br />
    <asp:Button ID="powrot_Button" runat="server" onclick="powrot_Button_Click" Text="Powrót" />
&nbsp;&nbsp;&nbsp;
    <asp:Button ID="test_Button" runat="server" onclick="test_Button_Click" Text="Aktualizuj listę plików" />
    &nbsp;&nbsp;&nbsp;
    <br />
    <br />
    </asp:Content>
