<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RejestrProduktowy.aspx.cs" Inherits="Tracktracer.RejestrProduktowy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label4" runat="server" Text="Projekt: "></asp:Label>
    <asp:Label ID="nazwa_Label" runat="server" Text="Label" ForeColor="Black"></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Rejestr produktowy"></asp:Label>
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource1" DataKeyNames="id" OnRowCommand="GridView_RowCommand"
        onselectedindexchanged="GridView1_SelectedIndexChanged"
        CellPadding="4" ForeColor="#333333" GridLines="None" AllowSorting="true">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField >
                <EditItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="id" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="id">
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" Width="40px" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Nazwa wymagania" SortExpression="nazwa">
                <HeaderStyle HorizontalAlign="Left" Width="600px" />
                <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:LinkButton ID="wymLink" CommandName="Wymaganie" CommandArgument='<%#Eval("id") %>' runat="server">                                        
                        <%#Eval("nazwa") %>
                        </asp:LinkButton>                        
                    </ItemTemplate>
            </asp:TemplateField>            
           <asp:BoundField DataField="[Wydanie].[Iteracja]" HeaderText="Wydania.Iteracja" >
            <HeaderStyle HorizontalAlign="Center" />
            <ItemStyle HorizontalAlign="Center" Width="220px" />
            </asp:BoundField>
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
        <EmptyDataTemplate>
            <asp:Label ID="pustoLabel" runat="server" Text="Do tego projektu nie dodano jeszcze wymagań"></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
                               
        SelectCommand="SELECT w.id, w.nazwa, STUFF((SELECT ',  ' +convert(VARCHAR,wd.nr_wydania)+'.'+convert(VARCHAR,i.nr_iteracji) FROM  Iteracje_Wymagania iw, Iteracje i, Wydania wd WHERE iw.WymaganieId=w.id and i.id=iw.IteracjaId and wd.id = i.Wydania_id FOR XML PATH ('')) , 1, 1, '') AS '[Wydanie].[Iteracja]'  FROM Wymagania w WHERE w.Projekty_id = @proj_id AND w.status != 'usunięte';">
        <SelectParameters>
            <asp:SessionParameter Name="proj_id" SessionField="projekt_id" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <asp:CheckBox ID="usuniete_CheckBox" runat="server" Text="Pokaż usunięte" 
        AutoPostBack="True" oncheckedchanged="usuniete_CheckBox_CheckedChanged" />
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Wydanie: "></asp:Label>
    <asp:DropDownList ID="wydanie_DropDownList" runat="server" AutoPostBack="True" 
        DataSourceID="SqlDataSource2" DataTextField="nr_wydania" 
        DataValueField="nr_wydania" 
        onselectedindexchanged="wydanie_DropDownList_SelectedIndexChanged">
    </asp:DropDownList>
&nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label3" runat="server" Text="Iteracja: "></asp:Label>
    <asp:DropDownList ID="iteracja_DropDownList" runat="server" AutoPostBack="True" 
        DataSourceID="SqlDataSource3" DataTextField="nr_iteracji" 
        DataValueField="nr_iteracji" 
        onselectedindexchanged="iteracja_DropDownList_SelectedIndexChanged">
    </asp:DropDownList>
&nbsp;&nbsp;&nbsp;
    <asp:Label ID="cel_Label" runat="server" Text="Cel iteracji: " Visible="False"></asp:Label>
    <asp:Label ID="celIt_Label" runat="server"></asp:Label>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
        SelectCommand="SELECT [nr_wydania] FROM [Wydania] WHERE ([Projekty_id] = @Projekty_id)">
        <SelectParameters>
            <asp:SessionParameter Name="Projekty_id" SessionField="projekt_id" 
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
        SelectCommand="SELECT i.nr_iteracji FROM Iteracje i, Wydania w WHERE i.Wydania_id = w.id AND w.Projekty_id = @proj_id AND w.nr_wydania = @nr_wyd;">
        <SelectParameters>
            <asp:SessionParameter Name="proj_id" SessionField="projekt_id" />
            <asp:ControlParameter ControlID="wydanie_DropDownList" Name="nr_wyd" 
                PropertyName="SelectedValue" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <br />
    <asp:Button ID="przypisz_Button" runat="server" onclick="przypisz_Button_Click" 
        Text="Przypisz zaznaczone wymagania do wybranej iteracji" Width="350px" />
    <br />
    <br />
     <asp:Button ID="usun_Button" runat="server" onclick="usun_Button_Click" 
        Text="Usun zaznaczone wymagania do wybranej iteracji" Width="350px" />
    <br />
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
            <HeaderStyle HorizontalAlign="Center" Width="800px"/>
            <ItemStyle HorizontalAlign="Left" />                            
                <ItemTemplate>
                    <asp:LinkButton ID="myWymLink" CommandName="Wymaganie" CommandArgument='<%#Eval("id") %>' runat="server">
                    <%#Eval("nazwa") %>
                    </asp:LinkButton>                                    
                </ItemTemplate>
            </asp:TemplateField>            
            <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" >
                <HeaderStyle HorizontalAlign="Center" Width="100px"/>
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
            <asp:Label ID="empty_Label2" Text="Nie realizujesz jeszcze żadnych wymagań w tym projekcie." Visible="true" runat="server"></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
        SelectCommand="SELECT [id], [nazwa], [status] FROM [Wymagania] WHERE (([Uzytkownik_id] = @Uzytkownik_id) AND ([Projekty_id] = @Projekty_id))">
        <SelectParameters>
            <asp:SessionParameter Name="Uzytkownik_id" SessionField="user_id" 
                Type="Int32" />
            <asp:SessionParameter Name="Projekty_id" SessionField="projekt_id" 
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <br />
    <asp:Button ID="powrot_Button" runat="server" onclick="powrot_Button_Click" 
        Text="Powrót" />
&nbsp;&nbsp;
    <asp:Button ID="wymaganie_Button" runat="server" 
        onclick="wymaganie_Button_Click" Text="Nowe wymaganie" />
    <br />
    <br />
&nbsp;<br />
</asp:Content>