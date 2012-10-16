<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PrzypadekTestowy.aspx.cs" Inherits="Tracktracer.PrzypadekTestowy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Table ID="infoTable" runat="server">
        <asp:TableRow>
            <asp:TableCell>
                Projekt:   
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="projekt_Label" runat="server" Text="Label"></asp:Label>        
            </asp:TableCell>
        </asp:TableRow>        
        <asp:TableRow>
            <asp:TableCell>
                Nazwa przypadku testowego:
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="przypadek_Label" runat="server" Text="Label"></asp:Label>        
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="przedmiot_Label" runat="server" Text="Label"></asp:Label>
            </asp:TableCell>
            <asp:TableCell>
                <asp:LinkButton ID="przedmiot_Link" runat="server" OnClick="przedmiot_Link_Click" ></asp:LinkButton>        
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                Autor:
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="autor_Label" runat="server" Text="Label"></asp:Label>        
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="status_Label" runat="server" Text="Status: "></asp:Label>                    
            </asp:TableCell>
            <asp:TableCell>
                <asp:DropDownList ID="status_DropDownList" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="status_DropDownList_SelectedIndexChanged">
                    <asp:ListItem Value="Zaliczony">Zaliczony</asp:ListItem>
                    <asp:ListItem Value="Nie zaliczony">Nie zaliczony</asp:ListItem>
                    <asp:ListItem Enabled="False" Value="Do weryfikacji">Do weryfikacji</asp:ListItem>
                </asp:DropDownList>  
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="status_Button" runat="server" onclick="status_Button_Click" 
                    Text="Zmień status" Visible="False" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>        
    <br />
    <asp:Label ID="opis_Label" runat="server" Text="Opis: "></asp:Label>
    <br />
    <br />
    <asp:TextBox ID="opis_TextBox" runat="server" Height="151px" 
        TextMode="MultiLine" Width="600px"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="powrot_Button" runat="server" onclick="powrot_Button_Click" 
        Text="Powrót" />
&nbsp;&nbsp;&nbsp;
&nbsp;<asp:Button ID="wykonanie_Button" runat="server" 
        onclick="wykonanie_Button_Click" Text="Dodaj wykonanie przypadku testowego" />
&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="zmiana_Button" runat="server" Text="Zapisz zmiany" 
        onclick="zmiana_Button_Click" />
    &nbsp;&nbsp;&nbsp;
    <asp:Button ID="usun_Button" runat="server" onclick="usun_Button_Click" 
        Text="Usuń przypadek testowy" />
    <br />
    <br />
    <asp:Button ID="potwierdz_Button" runat="server" 
        onclick="potwierdz_Button_Click" Text="Potwierdź usunięcie przypadku testowego" 
        Visible="False" Width="300px" />
&nbsp;&nbsp;&nbsp;
    <asp:Button ID="anuluj_Button" runat="server" onclick="anuluj_Button_Click" 
        Text="Nie usuwaj przypadku testowego" Visible="False" Width="400px" />
    <br />    
    <asp:Label ID="wykonania_Label" runat="server" 
        Text="Wykonania przypadku testowego:"></asp:Label>
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
        AutoGenerateColumns="False" DataSourceID="SqlDataSource1" DataKeyNames="id" 
        OnRowCommand="GridView_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None">                        
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Data wykonania" SortExpression="data">
            <HeaderStyle HorizontalAlign="Center" Width="150px"/>
            <ItemStyle HorizontalAlign="Center" />                            
                <ItemTemplate>
                    <asp:LinkButton ID="dateLink" CommandName="Wykonanie" CommandArgument='<%#Eval("id") %>' runat="server">
                    <%#Eval("data") %>
                    </asp:LinkButton>                                    
                </ItemTemplate>
            </asp:TemplateField>              
            <asp:BoundField DataField="wynik" HeaderText="Wynik" SortExpression="wynik" >
                <HeaderStyle HorizontalAlign="Center" Width="120px"/>
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
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
            <asp:Label ID="empty_Label2" Text="Ten przypadek testowy nie był jeszcze wykonywany." Visible="true" runat="server"></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
        SelectCommand="SELECT [id], [data], [wynik] FROM [Wykonanie_przypadku] WHERE ([Przypadek_testowy_id] = @Przypadek_testowy_id)">
        <SelectParameters>
            <asp:SessionParameter Name="Przypadek_testowy_id" SessionField="przypadek_id" 
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
&nbsp;
</asp:Content>
