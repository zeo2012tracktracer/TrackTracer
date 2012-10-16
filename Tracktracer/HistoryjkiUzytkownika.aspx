<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HistoryjkiUzytkownika.aspx.cs" Inherits="Tracktracer.HistoryjkiUzytkownika" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="projekt_Label" runat="server" Text="Label"></asp:Label>
    <br /><br />
    <asp:Label ID="Label1" runat="server" Text="Historyjki użytkownika"></asp:Label>
    <br /><br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="id" DataSourceID="SqlDataSource1" OnRowCommand="GridView_RowCommand" 
        AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField>                        
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="id" HeaderText="ID" SortExpression="id" />
            <asp:TemplateField HeaderText="Nazwa historyjki użytkownika" SortExpression="nazwa">
                <HeaderStyle HorizontalAlign="Center" Width="700px" />
                <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:LinkButton ID="wymLink" CommandName="Historyjka" CommandArgument='<%#Eval("id") %>' runat="server">
                        <%#Eval("nazwa") %>
                        </asp:LinkButton>                        
                    </ItemTemplate>
            </asp:TemplateField>            
            <asp:BoundField DataField="nr_wydania" HeaderText="Nr wydania" SortExpression="nr_wydania" >
                <HeaderStyle HorizontalAlign="Center" Width="70px"/>
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="nr_iteracji" HeaderText="Nr iteracji" SortExpression="nr_iteracji" >
                <HeaderStyle HorizontalAlign="Center" Width="70px"/>
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
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
        SelectCommand="SELECT [nazwa], [id], [nr_wydania], [nr_iteracji] FROM [Historyjki_uzytkownikow] WHERE ([Projekty_id] = @projekt_id)">
        <SelectParameters>
            <asp:SessionParameter Name="projekt_id" SessionField="projekt_id" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <asp:CheckBox ID="usuniete_CheckBox" runat="server" Text="Pokaż usunięte" 
        AutoPostBack="True" oncheckedchanged="usuniete_CheckBox_CheckedChanged" />
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Wydanie: "></asp:Label>
    <asp:DropDownList ID="wydanie_DropDownList" runat="server" AutoPostBack="True" 
        DataSourceID="SqlDataSource2" DataTextField="nr_wydania" DataValueField="nr_wydania" 
        onselectedindexchanged="wydanie_DropDownList_SelectedIndexChanged">
    </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label3" runat="server" Text="Iteracja: "></asp:Label>
    <asp:DropDownList ID="iteracja_DropDownList" runat="server" AutoPostBack="True" 
        DataSourceID="SqlDataSource3" DataTextField="nr_iteracji" DataValueField="nr_iteracji" 
        onselectedindexchanged="iteracja_DropDownList_SelectedIndexChanged">
    </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;
    <asp:Label ID="cel_Label" runat="server" Text="Cel iteracji: " Visible="False"></asp:Label>
    <asp:Label ID="celIt_Label" runat="server"></asp:Label>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
        SelectCommand="SELECT [nr_wydania] FROM [Wydania] WHERE ([Projekty_id] = @Projekty_id)">
        <SelectParameters>
            <asp:SessionParameter Name="Projekty_id" SessionField="projekt_id" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
        SelectCommand="SELECT i.nr_iteracji FROM Iteracje i, Wydania w WHERE i.Wydania_id = w.id AND w.Projekty_id = @projekt_id AND w.nr_wydania = @nr_wyd;">
        <SelectParameters>
            <asp:SessionParameter Name="projekt_id" SessionField="projekt_id" />
            <asp:ControlParameter ControlID="wydanie_DropDownList" Name="nr_wyd" PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <br />
    <asp:Button ID="przypisz_Button" runat="server" onclick="przypisz_Button_Click" 
        Text="Przypisz zaznaczone historyjki do wybranej iteracji" Width="350px" />
    <br />   
    <br />
    <asp:Button ID="powrot_Button" runat="server" onclick="powrot_Button_Click" Text="Powrót" />
        &nbsp;&nbsp;
    <asp:Button ID="historyjka_Button" runat="server" onclick="historyjka_Button_Click" Text="Nowa historyjka użytkownika" />
    <br />
    <br />
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="id" DataSourceID="SqlDataSource4" OnRowCommand="GridView_RowCommand" 
        AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>                    
            <asp:TemplateField HeaderText="Nazwa zadania" SortExpression="nazwa">
                <HeaderStyle HorizontalAlign="Center" Width="745px" />
                <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:LinkButton ID="zadLink" CommandName="Zadanie" CommandArgument='<%#Eval("id") %>' runat="server">
                        <%#Eval("nazwa") %>
                        </asp:LinkButton>                        
                    </ItemTemplate>
            </asp:TemplateField>            
            <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" > 
                <HeaderStyle HorizontalAlign="Center" Width="150px" />
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
        SelectCommand="SELECT nazwa, id, status FROM Zadania_programistyczne WHERE Projekty_id = @projekt_id">
        <SelectParameters>
            <asp:SessionParameter Name="projekt_id" SessionField="projekt_id" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:CheckBox ID="usuniete2_CheckBox" runat="server" Text="Pokaż usunięte zadania" 
        AutoPostBack="True" oncheckedchanged="usuniete2_CheckBox_CheckedChanged" />
</asp:Content>
