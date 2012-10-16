<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Plik.aspx.cs" Inherits="Tracktracer.Plik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="nazwa_Label" runat="server" Text="Label"></asp:Label>
    <br />
    <br />
<asp:Label ID="rew_Label" runat="server" Text="Label"></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Historia zmian pliku: "></asp:Label>
<br />
<br />
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
    CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" 
    GridLines="None">
    <AlternatingRowStyle BackColor="White" />
    <Columns>
        <asp:BoundField DataField="rodzaj_modyfikacji" HeaderText="Typ operacji" 
            SortExpression="rodzaj_modyfikacji">
        <HeaderStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="nr_rewizji" HeaderText="Nr rewizji" 
            SortExpression="nr_rewizji">
        <HeaderStyle HorizontalAlign="Center" />
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
    </Columns>
    <EditRowStyle BackColor="#2461BF" />
    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
    <RowStyle BackColor="#EFF3FB" />
    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    <SortedAscendingCellStyle BackColor="#F5F7FB" />
    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
    <SortedDescendingCellStyle BackColor="#E9EBEF" />
    <SortedDescendingHeaderStyle BackColor="#4870BE" />
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
    SelectCommand="SELECT [rodzaj_modyfikacji], [nr_rewizji] FROM [Historia_plikow] WHERE ([Pliki_id] = @Pliki_id)">
    <SelectParameters>
        <asp:SessionParameter Name="Pliki_id" SessionField="id_pliku" Type="Int32" />
    </SelectParameters>
</asp:SqlDataSource>
<br />

    <asp:Label ID="powiazane_Label" runat="server"></asp:Label>
    <br />
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="id" DataSourceID="SqlDataSource4" OnRowCommand="GridView_RowCommand" 
        AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>                    
            <asp:TemplateField HeaderText="Nazwa zadania" SortExpression="nazwa">
                <HeaderStyle HorizontalAlign="Center" Width="559px" />
                <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:LinkButton ID="zadLink" CommandName="Powiazanie" CommandArgument='<%#Eval("id") %>' runat="server">
                        <%#Eval("nazwa") %>
                        </asp:LinkButton>                        
                    </ItemTemplate>
            </asp:TemplateField>            
            <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" /> 
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
            <asp:Label ID="Label2" Text="Ten plik nie ma jeszcze powiązań." Visible="true" runat="server"></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" >
        <SelectParameters>
            <asp:SessionParameter Name="id_pliku" SessionField="id_pliku" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />

    <asp:Label ID="Label2" runat="server" Text="Wybór rewizji do porównania: "></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label3" runat="server" Text="Wcześniejsza: "></asp:Label>
    <asp:DropDownList ID="rew1_DropDownList" runat="server" AutoPostBack="True" 
        DataSourceID="SqlDataSource2" DataTextField="nr_rewizji" 
        DataValueField="nr_rewizji" 
        onselectedindexchanged="rew1_DropDownList_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
        SelectCommand="SELECT [nr_rewizji] FROM [Historia_plikow] WHERE ([Pliki_id] = @Pliki_id)">
        <SelectParameters>
            <asp:SessionParameter Name="Pliki_id" SessionField="id_pliku" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label4" runat="server" Text="Późniejsza: "></asp:Label>
    <asp:DropDownList ID="rew2_DropDownList" runat="server" 
        DataSourceID="SqlDataSource3" DataTextField="nr_rewizji" 
        DataValueField="nr_rewizji">
    </asp:DropDownList>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
        SelectCommand="SELECT [nr_rewizji] FROM [Historia_plikow] WHERE (([Pliki_id] = @Pliki_id) AND ([nr_rewizji] &gt; @nr_rewizji))">
        <SelectParameters>
            <asp:SessionParameter Name="Pliki_id" SessionField="id_pliku" Type="Int32" />
            <asp:ControlParameter ControlID="rew1_DropDownList" Name="nr_rewizji" 
                PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <br />
    <asp:Button ID="test_Button" runat="server" onclick="test_Button_Click" 
        Text="Porównaj plik w wybranych rewizjach" />
    &nbsp;&nbsp;&nbsp;
    <asp:Button ID="powrot_Button" runat="server" onclick="powrot_Button_Click" 
        Text="Powrót" />
    <br />
    <br />
    <asp:Table ID="test_Table" runat="server">
        <asp:TableRow runat="server">
            <asp:TableCell runat="server" Font-Bold="True"></asp:TableCell>
            <asp:TableCell runat="server" Font-Bold="True"></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow runat="server" VerticalAlign="Top">
            <asp:TableCell runat="server" Width="450px"></asp:TableCell>
            <asp:TableCell runat="server" Width="450px"></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <br />
    <br />
    <br />
</asp:Content>
