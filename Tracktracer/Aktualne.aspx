<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Aktualne.aspx.cs" Inherits="Tracktracer.Aktualne" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<asp:Table ID="akt_Table" runat="server">
    <asp:TableRow>
        <asp:TableCell>
            Aktywny projekt:&nbsp;        
        </asp:TableCell>
        <asp:TableCell>
            <asp:LinkButton ID="projekt_Link" runat="server" OnClick="projekt_Link_Click" ></asp:LinkButton>            
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            Aktualnie realizujesz wymaganie:&nbsp;        
        </asp:TableCell>
        <asp:TableCell>
            <asp:LinkButton ID="wymaganie_Link" runat="server" OnClick="wymaganie_Link_Click" ></asp:LinkButton>
            <asp:Label ID="brak_wymagania" runat="server" Text="Nie wybrano wymagania"></asp:Label>            
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>

<br />

Twoje wymagania w tym projekcie:
    <br />
    <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
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
                    <asp:LinkButton ID="wykonujLink" CommandName="Realizuj" CommandArgument='<%#Eval("id") %>' runat="server">
                        Realizuj
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
            <asp:Label ID="empty_Label2" Text="Nie realizujesz żadnych wymagań w tym projekcie." Visible="true" runat="server"></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
        SelectCommand="SELECT w.id, w.nazwa, w.status FROM Wymagania w WHERE w.Projekty_id= @projekt_id AND w.Uzytkownik_id= @user_id AND status!= 'usunięte' AND status!= 'zakończone'" >
        <SelectParameters>
            <asp:SessionParameter Name="user_id" SessionField="user_id" Type="Int32" />
            <asp:SessionParameter Name="projekt_id" SessionField="aktywny_projekt" Type="Int32" />        
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <br />
    Wymagania aktualnie realizowane przez innych użytkowników w tym projekcie:
    <br />
    <br />
     <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource1" AllowSorting="true" DataKeyNames="id" OnRowCommand="GridView_RowCommand" 
        CellPadding="4" ForeColor="#333333" GridLines="None">                        
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Nazwa wymagania" SortExpression="nazwa">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Left" Width="550px"/>                            
                <ItemTemplate>
                    <asp:LinkButton ID="inneWymLink" CommandName="Wymaganie" CommandArgument='<%#Eval("id") %>' runat="server">
                    <%#Eval("nazwa") %>
                    </asp:LinkButton>                                    
                </ItemTemplate>
            </asp:TemplateField>            
            <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" >
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="150px"/>
            </asp:BoundField>
            <asp:TemplateField SortExpression="login" HeaderText="Użytkownik">            
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" Width="200px"/>
                <ItemTemplate>
                    <%#Eval("imie") %>&nbsp;<%#Eval("nazwisko") %>&nbsp(login:&nbsp;<%#Eval("login") %>)
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
            <asp:Label ID="empty_Label2" Text="Inni użytkownicy nie realizują wymagań w tym projekcie." Visible="true" runat="server"></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
        SelectCommand="SELECT w.id, w.nazwa, w.status, u.login, u.imie, u.nazwisko FROM Wymagania w, Uzytkownicy u, Uzytkownicy_Projekty up WHERE w.id = u.aktywne_wymaganie AND u.id = up.Uzytkownik_id AND up.Projekt_id= @projekt_id AND up.Uzytkownik_id!= @user_id AND w.status!= 'usunięte' AND status!= 'zakończone'" >
        <SelectParameters>
            <asp:SessionParameter Name="user_id" SessionField="user_id" Type="Int32" />
            <asp:SessionParameter Name="projekt_id" SessionField="aktywny_projekt" Type="Int32" />        
        </SelectParameters>
    </asp:SqlDataSource>

    <br />
    <br />
    Pliki powiązane z aktualnie realizowanymi wymaganiami w tym projekcie: 
    <br />
    <br />
         <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource2" AllowSorting="true" DataKeyNames="id" OnRowCommand="GridView_RowCommand" 
        CellPadding="4" ForeColor="#333333" GridLines="None">                        
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Nazwa pliku" SortExpression="nazwa">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Left" Width="900px"/>                            
                <ItemTemplate>
                    <asp:LinkButton ID="innePlikiLink" CommandName="Plik" CommandArgument='<%#Eval("id") %>' runat="server">
                    <%#Eval("nazwa") %>
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
            <asp:Label ID="empty_Label2" Text="Aktualnie żadne pliki nie są powiązane z realizowanymi wymaganiami." Visible="true" runat="server"></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
        SelectCommand="SELECT p.id, p.nazwa FROM Pliki p, Pliki_wymagania pw, Wymagania w, Uzytkownicy u, Uzytkownicy_Projekty up WHERE p.id = pw.Plik_id AND pw.Wymaganie_id = w.id AND w.id = u.aktywne_wymaganie AND u.id = up.Uzytkownik_id AND up.Projekt_id= @projekt_id AND up.Uzytkownik_id!= @user_id AND w.status!= 'usunięte' AND w.status!= 'zakończone'" >
        <SelectParameters>
            <asp:SessionParameter Name="user_id" SessionField="user_id" Type="Int32" />
            <asp:SessionParameter Name="projekt_id" SessionField="aktywny_projekt" Type="Int32" />        
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
