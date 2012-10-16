<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RejestrZadaniowy.aspx.cs" Inherits="Tracktracer.RejestrZadaniowy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <br />
    <asp:Label ID="nazwa_Label" runat="server" Text="Label"></asp:Label>
    <br />
    <br />
    <asp:Label ID="rejestr_Label" runat="server" Text="Rejestr zadaniowy"></asp:Label>
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="id" DataSourceID="SqlDataSource1"
        OnRowCommand="GridView_RowCommand" AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Nazwa wymagania" SortExpression="nazwa">
                <HeaderStyle HorizontalAlign="Center" Width="650px" />
                <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:LinkButton ID="wymLink" CommandName="Wymaganie" CommandArgument='<%#Eval("id") %>' runat="server">                                        
                        <%#Eval("nazwa") %>
                        </asp:LinkButton>                        
                    </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" >
                <HeaderStyle HorizontalAlign="Center" Width="150px"/>
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField>                
                <ItemStyle HorizontalAlign="Center" Width="80px"/>
                    <ItemTemplate>
                        <asp:LinkButton ID="myLink" CommandName="Pobierz" CommandArgument='<%#Eval("id") %>' runat="server" Visible='<%# Eval("Uzytkownik_id")!=DBNull.Value ? false: true %>'>  
                        Pobierz
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
            <asp:Label ID="Label2" Text="Nie dodano jeszcze żadnych wymagań." Visible="true" runat="server"></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
        SelectCommand="SELECT w.id, w.nazwa, w.status, w.Uzytkownik_id FROM Wymagania w WHERE w.Projekty_id = @projekt_id AND nr_wydania = @wydanie AND nr_iteracji = @iteracja">
        <SelectParameters>
            <asp:SessionParameter Name="projekt_id" SessionField="projekt_id" />
            <asp:SessionParameter DefaultValue="" Name="wydanie" SessionField="wydanie" />
            <asp:SessionParameter DefaultValue="" Name="iteracja" SessionField="iteracja" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <br />
    <asp:Label ID="moje_Label" runat="server" Text="Moje wymagania: "></asp:Label>
    <br />
    <br />
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource5" AllowSorting="true" DataKeyNames="id" OnRowCommand="GridView_RowCommand" 
        CellPadding="4" ForeColor="#333333" GridLines="None">                        
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Nazwa wymagania" SortExpression="nazwa">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Left" Width="650px"/>                            
                <ItemTemplate>
                    <asp:LinkButton ID="myWymLink" CommandName="Wymaganie" CommandArgument='<%#Eval("id") %>' runat="server">
                    <%#Eval("nazwa") %>
                    </asp:LinkButton>                                    
                </ItemTemplate>
            </asp:TemplateField>            
            <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" >
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="150px"/>
            </asp:BoundField>
            <asp:TemplateField SortExpression="nazwa">            
            <ItemStyle HorizontalAlign="Left" Width="80px"/>
                <ItemTemplate>
                    <asp:LinkButton ID="oddajLink" CommandName="Oddaj" CommandArgument='<%#Eval("id") %>' runat="server">
                    Zwróć
                    </asp:LinkButton>                                    
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" BorderColor="Blue" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
        <EmptyDataTemplate>
            <asp:Label ID="empty_Label2" Text="Nie realizujesz jeszcze żadnych wymagań w tej iteracji." Visible="true" runat="server"></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
        SelectCommand="SELECT [id], [nazwa], [status] FROM [Wymagania] WHERE (([Uzytkownik_id] = @Uzytkownik_id) AND ([Projekty_id] = @Projekty_id)  AND ([nr_wydania] = @wydanie) AND ([nr_iteracji] = @iteracja))">
        <SelectParameters>
            <asp:SessionParameter Name="Uzytkownik_id" SessionField="user_id" Type="Int32" />
            <asp:SessionParameter Name="Projekty_id" SessionField="projekt_id" Type="Int32" />
            <asp:SessionParameter Name="wydanie" SessionField="wydanie" Type="Int32" />
            <asp:SessionParameter Name="iteracja" SessionField="iteracja" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <br />
    <asp:Button ID="powrot_Button" runat="server" Text="Powrót" 
        onclick="powrot_Button_Click" />
</asp:Content>
