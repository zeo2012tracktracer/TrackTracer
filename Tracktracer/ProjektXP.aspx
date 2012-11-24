<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProjektXP.aspx.cs" Inherits="Tracktracer.ProjektXP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="false">  
    </asp:ToolkitScriptManager>
    <asp:Accordion ID="Accordion1"   
    CssClass="accordion" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"  
    ContentCssClass="accordionContent" runat="server">
        <Panes>
            <asp:AccordionPane ID="AccordionPane1" runat="server">
                <Header>Informacje ogólne</Header>
                <Content>
                    <asp:Table ID="Table1" runat="server">
                        <asp:TableRow runat="server">
                            <asp:TableCell runat="server">Nazwa: </asp:TableCell>
                            <asp:TableCell runat="server" ForeColor="Black"></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server">
                            <asp:TableCell runat="server">Metodyka: </asp:TableCell>
                            <asp:TableCell runat="server" ForeColor="Black"></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server">
                            <asp:TableCell runat="server">Właściciel: </asp:TableCell>
                            <asp:TableCell runat="server" ForeColor="Black"></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow runat="server">
                            <asp:TableCell runat="server">Opis: </asp:TableCell>
                            <asp:TableCell runat="server" ForeColor="Black"></asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow ID="TableRow1" runat="server">
                            <asp:TableCell ID="TableCell1" runat="server">SVN: </asp:TableCell>
                            <asp:TableCell ID="TableCell2" runat="server">
                                <asp:HyperLink ID="svn_Link" runat="server" Target="_blank"></asp:HyperLink>
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>
                    <br />
                    <asp:Button ID="historyjki_Button" runat="server" Text="Historyjki użytkownika" onclick="historyjki_Button_Click" />
                        &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="pliki_Button" runat="server" Text="Pliki" onclick="pliki_Button_Click" />
                        &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="testy_Button" runat="server" Text="Przypadki testowe" onclick="testy_Button_Click" />
                        &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="svn_Button" runat="server" onclick="svn_Button_Click" Text="Edytuj dane SVN" Visible="False" />
                    <br />
                    <br />
                    <asp:Label ID="Label4" runat="server" Text="Wydanie: "></asp:Label>
                    <asp:DropDownList ID="wydanie_DropDownList" runat="server" 
                        DataSourceID="SqlDataSource2" DataTextField="nr_wydania" DataValueField="nr_wydania" AutoPostBack="True" 
                        onselectedindexchanged="wydanie_DropDownList_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp;&nbsp; <asp:Label ID="Label5" runat="server" Text="Iteracja: "></asp:Label>
                    <asp:DropDownList ID="iteracja_DropDownList" runat="server" 
                        DataSourceID="SqlDataSource3" DataTextField="nr_iteracji" DataValueField="nr_iteracji" 
                        onselectedindexchanged="iteracja_DropDownList_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="cel_Label" runat="server" Visible="False">Cel iteracji: </asp:Label>
                    <asp:Label ID="celIt_Label" runat="server" Text="Label" Visible="False"></asp:Label>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>"         
                        SelectCommand="SELECT [nr_wydania] FROM [Wydania] WHERE ([Projekty_id] = @Projekty_id)">
                        <SelectParameters>
                            <asp:SessionParameter Name="Projekty_id" SessionField="projekt_id" 
                                Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
                        SelectCommand="SELECT i.nr_iteracji FROM Iteracje i, Wydania w WHERE i.Wydania_id = w.id AND w.Projekty_id = @proj_id AND w.nr_wydania = @nr_wyd;">
                        <SelectParameters>
                            <asp:SessionParameter Name="proj_id" SessionField="projekt_id" />
                            <asp:ControlParameter ControlID="wydanie_DropDownList" DefaultValue="" Name="nr_wyd" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                        <br />
                        <br />
                    <asp:Button ID="historyjki_iteracji_Button" runat="server" Text="Historyjki użytkownika wybranej iteracji" 
                        onclick="historyjki_iteracji_Button_Click" />           
                    <br />
                </Content>
            </asp:AccordionPane>
            <asp:AccordionPane ID="AccordionPane2" runat="server">
                <Header>Użytkownicy realizujący projekt: </Header>
                <Content>        
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="SqlDataSource1"
                        DataKeyNames="login" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>                            
                            <asp:BoundField DataField="login" HeaderText="Login" SortExpression="login">
                            <ControlStyle Width="100px" />
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="imie" HeaderText="Imię" SortExpression="imie">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="200px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nazwisko" HeaderText="Nazwisko" 
                                SortExpression="nazwisko">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="200px" />
                            </asp:BoundField>
                        </Columns>        
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#FFA347" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFF99" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
                        SelectCommand="SELECT u.login, u.imie, u.nazwisko FROM Uzytkownicy u, Uzytkownicy_Projekty up WHERE up.Projekt_id=@id_projektu AND u.id = up.Uzytkownik_id AND u.status_konta='aktywne'">
                        <SelectParameters>
                            <asp:SessionParameter Name="id_projektu" SessionField="projekt_id" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <br />    
                    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                        Text="Edytuj przypisanie użytkowników" Visible="False" />    
                    <br />
                </Content>
            </asp:AccordionPane>
            <asp:AccordionPane ID="AccordionPane3" runat="server">
                <Header>Nowe wydanie / iteracja </Header>
                <Content>    
                    <asp:Button ID="wydanie_Button" runat="server" onclick="wydanie_Button_Click" Text="Rozpocznij nowe wydanie" />
                        &nbsp;
                    <asp:Label ID="dodanoWydanie_Label" runat="server" Text="Dodano nowe wydanie do projektu." Visible="False" ForeColor="#FF0066"></asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="Label7" runat="server" Text="Wydanie: "></asp:Label>
                    <asp:DropDownList ID="wydanie2_DropDownList" runat="server" 
                        DataSourceID="SqlDataSource4" DataTextField="nr_wydania" DataValueField="nr_wydania">
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
                        SelectCommand="SELECT [nr_wydania] FROM [Wydania] WHERE ([Projekty_id] = @Projekty_id)">
                        <SelectParameters>
                            <asp:SessionParameter Name="Projekty_id" SessionField="projekt_id" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                        &nbsp;
                    <asp:Label ID="Label6" runat="server" Text="Cel nowej iteracji: "></asp:Label>
                    <asp:TextBox ID="cel_TextBox" runat="server" MaxLength="255" Width="350px"></asp:TextBox>
                        &nbsp;
                    <asp:Button ID="iteracja_Button" runat="server" onclick="iteracja_Button_Click" Text="Rozpocznij nową iterację" />
                    &nbsp;
                    <asp:Label ID="dodanoIteracje_Label" runat="server" Text="Label" Visible="False" ForeColor="#FF0066"></asp:Label>
                </Content>
            </asp:AccordionPane>
            <asp:AccordionPane ID="usuniecie_AccordionPane" runat="server">
                <Header>Usunięcie projektu</Header>
                <Content>
                    <asp:Label ID="usLabel" runat="server" Text="Czy na pewno chcesz usunąć projekt i wszystkie związane z nim artefakty?" ForeColor="Red"></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="usunProjekt_Button" runat="server" Text="Tak" OnClick="usunProjekt_Button_Click"/>
                    <asp:Button ID="nieUsuwaj_Button" runat="server" Text="Nie, nie chcę usuwać tego projektu" OnClick="nieUsuwaj_Button_Click" />
                </Content>
            </asp:AccordionPane>
        </Panes>
    </asp:Accordion>
</asp:Content>
