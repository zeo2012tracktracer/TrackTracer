<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Historyjka.aspx.cs" Inherits="Tracktracer.Historyjka" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">   
    
    <asp:Table ID="info_Table" runat="server">
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
                Nazwa historyjki:
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="historyjka_Label" runat="server" Text="Label"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                Przypisanie:
            </asp:TableCell>
            <asp:TableCell>
                <asp:Label ID="brak_przypisania_Label" runat="server" Text="Historyjka nie została jeszcze przypisana do żadnej iteracji" Visible="false" ></asp:Label>
                <asp:Label ID="wydanie_Label" runat="server" Text="Wydanie: X"></asp:Label>
                &nbsp;&nbsp;
                <asp:Label ID="iteracja_Label" runat="server" Text="Iteracja: X"></asp:Label>
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="przypisanie_Button" runat="server" Text="Usuń przypisanie" onclick="przypisanie_Button_Click" />
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                Status:
            </asp:TableCell>
            <asp:TableCell>                
                <asp:DropDownList ID="status_DropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="status_DropDownList_SelectedIndexChanged">
                    <asp:ListItem Value="Aktywna" Text="Aktywna"></asp:ListItem>
                    <asp:ListItem Value="Usunięta" Text="Usunięta"></asp:ListItem>
                </asp:DropDownList>
                &nbsp;&nbsp;
                <asp:Button ID="status_Button" runat="server" Text="Zmień status" Visible="false" OnClick="status_Button_Click"/>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>                                   
    <br />
    <asp:Label ID="tresc_Label" runat="server" Text="Treść historyjki"></asp:Label>
    <br />
    <br />
    <asp:TextBox ID="tresc_TextBox" runat="server" Width="600px" TextMode="MultiLine"></asp:TextBox>
    <br />
    <br />
    <asp:table ID="Table1" runat="server">
        <asp:TableRow VerticalAlign="Top">
        <asp:TableCell Width="460px" HorizontalAlign="Left">                                
            <asp:Label ID="uwagi_Label" runat="server" Text="Uwagi: "></asp:Label>
            <br />                    
            <br />
            <asp:TextBox ID="uwagi_TextBox" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox>                    
        </asp:TableCell>
        <asp:TableCell Width="460px" HorizontalAlign="Left">
            <asp:Label ID="udzialowcy_Label" runat="server" Text="Udziałowcy: "></asp:Label>
            <br />
            <br />
            <asp:TextBox ID="udzialowcy_TextBox" runat="server" TextMode="MultiLine" Width="400px"></asp:TextBox>
        </asp:TableCell>
        </asp:TableRow>
    </asp:table>
    <br />
    <asp:Label ID="wysWersja_Label" runat="server" Text="Wyświetlana wersja historyjki: "></asp:Label>
    <asp:DropDownList ID="wersje_DropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="wersje_DropDownList_SelectedIndexChanged">
    </asp:DropDownList>                                      
    <br />
    <br />
    <asp:Button ID="powrot_Button" runat="server" onclick="powrot_Button_Click" Text="Powrót" />
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="nowaWersja_Button" runat="server" onclick="nowaWersja_Button_Click" Text="Zapisz nową wersję historyjki" />                  
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="zadanie_Button" runat="server" onclick="zadanie_Button_Click" Text="Nowe zadanie" />
    <asp:Label ID="opisR_Label" runat="server" Text="<br />Maksymalna długość opisu to 500 znaków." Visible="False" ForeColor="Red"></asp:Label> 
    <asp:Label ID="uwagiR_Label" runat="server" Text="<br />Maksymalna długość pola 'Uwagi' to 255 znaków." Visible="False" ForeColor="Red"></asp:Label>
    <asp:Label ID="udzialowcyR_Label" runat="server" Text="<br />Maksymalna długość pola 'Udziałowcy' to 255 znaków." Visible="False" ForeColor="Red"></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" 
        Text="Zadania programistyczne utworzone na podstawie tej historyjki:"></asp:Label>
    <br />    
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="id" DataSourceID="SqlDataSource1"
        AllowSorting="true" OnRowCommand="GridView_RowCommand" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Nazwa zadania" SortExpression="nazwa">
                <HeaderStyle HorizontalAlign="Center" Width="750px"/>
                <ItemStyle HorizontalAlign="Left" />                            
                    <ItemTemplate>
                        <asp:LinkButton ID="myZadLink" CommandName="Zadanie" CommandArgument='<%#Eval("id") %>' runat="server">
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
        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
        <EmptyDataTemplate>
            <asp:Label ID="empty_Label2" Text="Nie utworzono zadań dla tej historyjki." Visible="true" runat="server"></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
        SelectCommand="SELECT [nazwa], [id], [status] FROM [Zadania_programistyczne] WHERE ([Historyjka_uzytkownika_id] = @Historyjka_uzytkownika_id)">
        <SelectParameters>
            <asp:SessionParameter Name="Historyjka_uzytkownika_id" SessionField="historyjka_id" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
</asp:Content>
