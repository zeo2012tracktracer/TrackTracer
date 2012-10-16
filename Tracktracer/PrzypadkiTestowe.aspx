<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PrzypadkiTestowe.aspx.cs" Inherits="Tracktracer.PrzypadkiTestowe" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="nazwa_Label" runat="server" Text="Label"></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Przypadki testowe"></asp:Label>
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        DataSourceID="SqlDataSource1" DataKeyNames="id" OnRowCommand="GridView_RowCommand" 
        AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Nazwa przypadku testowego" SortExpression="nazwa">
                <HeaderStyle HorizontalAlign="Center" Width="760px" />
                <ItemStyle HorizontalAlign="Left" />
                <ItemTemplate>
                    <asp:LinkButton ID="testLink" CommandName="PrzypadekTestowy" CommandArgument='<%#Eval("id") %>' runat="server">                                        
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
        <HeaderStyle BackColor="#507CD1" BorderColor="Blue" Font-Bold="True" 
            ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />        
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
        <EmptyDataTemplate>
            <asp:Label ID="Label3" Text="Nie utworzono przypadków testowych." Visible="true" runat="server"></asp:Label>
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
        SelectCommand="SELECT [id], [nazwa], [status] FROM [Przypadki_testowe] WHERE ([Projekty_id] = @projekt_id)">
        <SelectParameters>
            <asp:SessionParameter Name="projekt_id" SessionField="projekt_id" 
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <br />
    <asp:Button ID="powrot_Button" runat="server" onclick="powrot_Button_Click" 
        Text="Powrót" />
    <br />
</asp:Content>
