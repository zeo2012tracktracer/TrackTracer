<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistoryjkiIteracji.aspx.cs" Inherits="Tracktracer.HistoryjkiIteracji" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="projekt_Label" runat="server" Text="Label"></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Historyjki użytkownika"></asp:Label>
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="id" DataSourceID="SqlDataSource1" OnRowCommand="GridView_RowCommand" 
        AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Historyjki wybranej iteracji" SortExpression="nazwa">
                <HeaderStyle HorizontalAlign="Center" Width="900px" />
                <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:LinkButton ID="wymLink" CommandName="Historyjka" CommandArgument='<%#Eval("id") %>' runat="server">
                        <%#Eval("nazwa") %>
                        </asp:LinkButton>                        
                    </ItemTemplate>
            </asp:TemplateField>            
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" BorderColor="Blue" Font-Bold="True" 
            ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />                        
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
        <EmptyDataTemplate>
            <asp:Label ID="Label2" Text="Nie dodano jeszcze historyjek użytkownika." Visible="true" runat="server"></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
        SelectCommand="SELECT nazwa, id FROM Historyjki_uzytkownikow WHERE Projekty_id = @projekt_id AND nr_wydania = @wydanie_nr AND nr_iteracji = @iteracja_nr">
        <SelectParameters>
            <asp:SessionParameter Name="projekt_id" SessionField="projekt_id" Type="Int32" />
            <asp:SessionParameter Name="wydanie_nr" SessionField="wydanie_nr" Type="Int32" />
            <asp:SessionParameter Name="iteracja_nr" SessionField="iteracja_nr" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>        
    <br />
    <br />
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="id" DataSourceID="SqlDataSource4" OnRowCommand="GridView_RowCommand" 
        AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>                    
            <asp:TemplateField HeaderText="Zadania programistyczne wybranej iteracji" SortExpression="nazwa">
                <HeaderStyle HorizontalAlign="Center" Width="742px" />
                <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:LinkButton ID="zadLink" CommandName="Zadanie" CommandArgument='<%#Eval("id") %>' runat="server">
                        <%#Eval("nazwa") %>
                        </asp:LinkButton>                        
                    </ItemTemplate>
            </asp:TemplateField>            
            <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" > 
                <HeaderStyle Width="150px" HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" BorderColor="Blue" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />                        
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
        <EmptyDataTemplate>
            <asp:Label ID="Label2" Text="Nie dodano jeszcze zadań." Visible="true" runat="server"></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
        SelectCommand="SELECT DISTINCT z.nazwa, z.id, z.status FROM Zadania_programistyczne z, Historyjki_uzytkownikow h WHERE h.Projekty_id = @projekt_id AND h.nr_wydania = @wydanie_nr AND h.nr_iteracji = @iteracja_nr">
        <SelectParameters>
            <asp:SessionParameter Name="projekt_id" SessionField="projekt_id" Type="Int32" />
            <asp:SessionParameter Name="wydanie_nr" SessionField="wydanie_nr" Type="Int32" />
            <asp:SessionParameter Name="iteracja_nr" SessionField="iteracja_nr" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <br />
    <asp:Button ID="powrot_Button" runat="server" onclick="powrot_Button_Click" Text="Powrót" />    
</asp:Content>
