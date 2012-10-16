<%@ Page Title="TrackTracer" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Tracktracer._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Label ID="aktywny_projekt_Label" runat="server" 
        Text="Nie masz aktualnie aktywnego projektu."></asp:Label>
        <asp:LinkButton ID="akt_projekt_Link" runat="server" OnClick="akt_projekt_Link_Click" ></asp:LinkButton>
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="id" DataSourceID="Projekty" OnRowCommand="GridView_RowCommand" 
        CellPadding="4" ForeColor="#333333" GridLines="None" AllowSorting="true">
        <AlternatingRowStyle BackColor="White" />
    <Columns>        
        <asp:TemplateField HeaderText="Nazwa" SortExpression="nazwa">
            <HeaderStyle HorizontalAlign="Center" Width="450px" />
            <ItemStyle HorizontalAlign="Left" />
            <ItemTemplate>
                <asp:LinkButton ID="projLink" CommandName="Projekt" CommandArgument='<%#Eval("id") %>' runat="server">                                        
                <%#Eval("nazwa") %>
                </asp:LinkButton>                                    
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="metodyka" HeaderText="Metodyka" 
            SortExpression="metodyka" >
            <HeaderStyle HorizontalAlign="Center" Width="120px" />
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>

        <asp:TemplateField SortExpression="login" HeaderText="Właściciel projektu" > 
            <HeaderStyle HorizontalAlign="Center" Width="180px" />
            <ItemStyle HorizontalAlign="Center" />            
            <ItemTemplate>
                <%#Eval("imie") %> <%#Eval("nazwisko") %> (<%#Eval("login") %>)
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField >            
            <ItemStyle HorizontalAlign="Center" Width="150px"/>
            <ItemTemplate>
                <asp:LinkButton ID="aktLink" CommandName="Aktywny" CommandArgument='<%#Eval("id") %>' runat="server">                                        
                Ustaw jako aktywny
                </asp:LinkButton>                                    
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
</asp:GridView>
<asp:SqlDataSource ID="Projekty" runat="server" 
    ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
    
        SelectCommand="SELECT p.id, p.nazwa, p.metodyka, u.imie, u.nazwisko, u.login FROM Projekty p, Uzytkownicy_projekty up, Uzytkownicy u WHERE up.Uzytkownik_id = @id_uzytkownika AND p.id = up.Projekt_id AND u.id = p.wlasciciel;">
    <SelectParameters>
        <asp:SessionParameter Name="id_uzytkownika" SessionField="user_id" />
    </SelectParameters>
</asp:SqlDataSource>
    <br />
    <asp:Button ID="Button1" runat="server" Text="Nowy projekt" 
        onclick="Button1_Click" />
</asp:Content>
