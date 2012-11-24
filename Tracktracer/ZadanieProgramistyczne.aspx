<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ZadanieProgramistyczne.aspx.cs" Inherits="Tracktracer.ZadanieProgramistyczne" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="false">  
</asp:ToolkitScriptManager>  
     
   <asp:Accordion   
    ID="Accordion1"   
    CssClass="accordion"  
    HeaderCssClass="accordionHeader"  
    HeaderSelectedCssClass="accordionHeaderSelected"  
    ContentCssClass="accordionContent"   
    runat="server">
    <Panes>
    <asp:AccordionPane ID="AccordionPane3" runat="server">
        <Header>Zadanie programistyczne</Header>
        <Content>
            <asp:Table ID="info_Table" runat="server">            
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label1" runat="server" Text="Nazwa projektu: "></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Label ID="projNazwa_Label" runat="server" Text="Label"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label2" runat="server" Text="Nazwa zadania: "></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Label ID="zadNazwa_Label" runat="server" Text="Label"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label6" runat="server" Text="Historyjka użytkownika: "></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:LinkButton ID="historyjka_Link" runat="server" OnClick="historyjka_Link_Click" ></asp:LinkButton>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="przypisanie_Label" runat="server" Text="Przypisanie do iteracji: "></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:Label ID="wydanie_Label" runat="server" Text="Wydanie: X"></asp:Label>
                        &nbsp;&nbsp;
                        <asp:Label ID="iteracja_Label" runat="server" Text="Iteracja: X"></asp:Label>        
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label4" runat="server" Text="Status: "></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            <asp:ListItem Value="Aktywne" Text="Aktywne"></asp:ListItem>
                            <asp:ListItem Value="Zakończone" Text="Zakończone"></asp:ListItem>
                            <asp:ListItem Value="Usunięte" Enabled="False" Text="Usunięte"></asp:ListItem>
                            <asp:ListItem Value="Do weryfikacji" Enabled="False" Text="Do weryfikacji"></asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="status_Button" runat="server" OnClick="status_Button_Click" Text="Zmień status" Visible="false" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>                                                                                              
            <asp:Label ID="realizator_Label" runat="server" Text="Osoba realizująca zadanie: "></asp:Label>            
            <asp:LinkButton ID="realizator1_LinkButton" runat="server" OnClick="realizator1_Link_Click" Text="Usuń realizatora" ></asp:LinkButton>
            <asp:Label ID="realizator2_Label" runat="server" Text="Osoba realizująca zadanie: "></asp:Label>
            <asp:LinkButton ID="realizator2_LinkButton" runat="server" OnClick="realizator2_Link_Click" Text="Usuń realizatora" ></asp:LinkButton>            
            <br />
            <br />
            <asp:Label ID="tresc_Label" runat="server" Text="Treść zadania"></asp:Label>
            <br />
            <br />
            <asp:TextBox ID="tresc_TextBox" runat="server" BorderStyle="None" Width="600px" TextMode="MultiLine"></asp:TextBox>
            <br />           
            <br />
            <asp:Label ID="wysWersja_Label" runat="server" Text="Wyświetlana wersja zadania: "></asp:Label>
            <asp:DropDownList ID="wersje_DropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="wersje_DropDownList_SelectedIndexChanged">
            </asp:DropDownList>                                      
            <br />
            <br />
            <asp:Button ID="nowaWersja_Button" runat="server" onclick="nowaWersja_Button_Click" Text="Zapisz nową wersję zadania" />                             
            <asp:Label ID="trescR_Label" runat="server" Text="<br />Maksymalna długość treści to 500 znaków." Visible="False" ForeColor="Red"></asp:Label>                         
            <br />
        </Content>
        </asp:AccordionPane>
    <asp:AccordionPane ID="AccordionPane1" runat="server">
        <Header>Istniejące powiązania</Header>
        <Content>
            <ajaxToolkit:TabContainer ID="TabContainer2" runat="server">
                <ajaxToolkit:TabPanel ID="TabPanel4" runat="server">
                    <HeaderTemplate>Zadania programistyczne</HeaderTemplate>
                    <ContentTemplate>
                        <asp:GridView ID="GridView2" runat="server" DataSourceID="SqlDataSource2"
                        AutoGenerateColumns="False" DataKeyNames="id" OnRowCommand="GridView_RowCommand" 
                        AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>                                                        
                            <asp:TemplateField HeaderText="Nazwa zadania" SortExpression="nazwa">
                                <HeaderStyle HorizontalAlign="Center" Width="559px" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="myLink" CommandName="Zadanie" CommandArgument='<%#Eval("id") %>' runat="server">                                        
                                    <%#Eval("nazwa") %>
                                    </asp:LinkButton>
                                    <div id="Div1" Visible='<%# Eval("opis") != DBNull.Value %>' runat="server">
                                    <asp:Label ID="lab" Text="Komentarz: " runat="server"><%#Eval("opis") %></asp:Label>          
                                    </div>                                                                                                           
                                    <asp:LinkButton ID="weryfikacjaLink" CommandName="Weryfikuj" CommandArgument='<%#Eval("id") %>' runat="server"
                                         Visible='<%# ( Eval("Zadanie1_id").ToString() == Eval("id").ToString() ? Eval("wersja1").ToString() : Eval("wersja2").ToString() ) != Eval("wersja").ToString() %>' >
                                        <asp:Label ID="przerwaLabel" runat="server" Text="<br />" Visible='<%# Eval("opis") == DBNull.Value %>' ></asp:Label> 
                                        <asp:Label ID="weryfikacjaLabel" Text="Oznacz jako zweryfikowane" runat="server" ForeColor="Red" ></asp:Label>
                                    </asp:LinkButton>                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                                                                                    
                            <asp:BoundField DataField="nr_wydania" HeaderText="Wydanie" SortExpression="nr_wydania" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nr_iteracji" HeaderText="Iteracja" SortExpression="nr_iteracji" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>    
                            <asp:TemplateField>                                
                                <ItemStyle HorizontalAlign="Center" Width="150px" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="myZadLink" CommandName="usunPowiazaneZadanie" CommandArgument='<%#Eval("id") %>' runat="server">
                                        Usuń powiązanie
                                    </asp:LinkButton>                                                                        
                                </ItemTemplate>
                            </asp:TemplateField>
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
                            <asp:Label ID="Label1" Text="Brak powiązanych zadań" Visible="true" runat="server"></asp:Label>
                        </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
                            SelectCommand="SELECT z.id, z.nazwa, z.wersja, h.nr_wydania, h.nr_iteracji, pz.opis, pz.Zadanie1_id, pz.wersja1, pz.wersja2 FROM Zadania_programistyczne z, Powiazane_zadania pz, Historyjki_uzytkownikow h WHERE z.id != @Zadanie_id AND h.id = z.Historyjka_uzytkownika_id AND ((pz.Zadanie1_id = z.id AND pz.Zadanie2_id =@Zadanie_id) OR (pz.Zadanie2_id = z.id AND pz.Zadanie1_id=@zadanie_id));">
           <SelectParameters>
               <asp:SessionParameter Name="zadanie_id" SessionField="zadanie_id" />
           </SelectParameters>
    </asp:SqlDataSource>
                        
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanel7" runat="server">
                    <HeaderTemplate>Zadania wywodzące się z tej samej historyjki</HeaderTemplate>
                    <ContentTemplate>

                    <asp:GridView ID="GridView6" runat="server" DataSourceID="SqlDataSource6"
                        AutoGenerateColumns="False" DataKeyNames="id" OnRowCommand="GridView_RowCommand" 
                        AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>                                                        
                            <asp:TemplateField HeaderText="Nazwa zadania" SortExpression="nazwa">
                                <HeaderStyle HorizontalAlign="Center" Width="720px" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="wsplHistLink" CommandName="Zadanie" CommandArgument='<%#Eval("id") %>' runat="server">                                        
                                    <%#Eval("nazwa") %>
                                    </asp:LinkButton>                                                                                                     
                                </ItemTemplate>
                            </asp:TemplateField>                                                                                    
                            <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="150px" />
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
                            <asp:Label ID="Label1" Text="Brak innych zadań wywodzących się z tej samej historyjki użytkownika." Visible="true" runat="server"></asp:Label>
                        </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
                            SelectCommand="SELECT z.id, z.nazwa, z.status FROM Zadania_programistyczne z WHERE z.id != @Zadanie_id AND z.Historyjka_uzytkownika_id = @historyjka_id;">
                           <SelectParameters>
                               <asp:SessionParameter Name="historyjka_id" SessionField="historyjka_id" />
                               <asp:SessionParameter Name="Zadanie_id" SessionField="zadanie_id" />
                           </SelectParameters>
                        </asp:SqlDataSource>

                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanel5" runat="server">
                    <HeaderTemplate>Pliki</HeaderTemplate>
                    <ContentTemplate>                        
                        <asp:GridView ID="GridView3" runat="server" DataSourceID="SqlDataSource3"
                        AutoGenerateColumns="False" DataKeyNames="id" OnRowCommand="GridView_RowCommand" 
                        AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>                                                        
                            <asp:TemplateField HeaderText="Nazwa pliku" SortExpression="nazwa">
                            <HeaderStyle HorizontalAlign="Center" Width="350px" />
                            <ItemStyle HorizontalAlign="Left" />                            
                                <ItemTemplate>
                                    <asp:LinkButton ID="myLinkPlik" CommandName="Plik" CommandArgument='<%#Eval("id") %>' runat="server">
                                    <%#Eval("nazwa") %>
                                    </asp:LinkButton>                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                                                                                    
                            <asp:TemplateField HeaderText="Ścieżka" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" SortExpression="sciezka" >
                                <ItemTemplate>
                                    <div style="word-wrap: break-word; width: 360px" >
                                        <%#Eval("sciezka") %></div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField >                            
                                <ItemStyle HorizontalAlign="Center" Width="150px"/>
                                <ItemTemplate>
                                    <asp:LinkButton ID="usunPlik" CommandName="UsunPlik" CommandArgument='<%#Eval("id") %>' runat="server">                                        
                                    Usuń powiązanie
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
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        <EmptyDataTemplate>
                            <asp:Label ID="Label2" Text="Brak powiązanych plików" Visible="true" runat="server"></asp:Label>
                        </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
                            SelectCommand="SELECT p.id, p.nazwa, p.sciezka FROM Pliki p, Pliki_zadania_programistyczne pz WHERE p.id = pz.Plik_id AND pz.Zadanie_programistyczne_id = @zadanie_id">
                        <SelectParameters>
                            <asp:SessionParameter Name="zadanie_id" SessionField="zadanie_id" />
                        </SelectParameters>
                        </asp:SqlDataSource>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanel6" runat="server">
                    <HeaderTemplate>Przypadki testowe</HeaderTemplate>
                    <ContentTemplate>
                        
                        <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" 
                            DataSourceID="SqlDataSource5" DataKeyNames="id" OnRowCommand="GridView_RowCommand" 
                            AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>                                                                
                                    <asp:TemplateField HeaderText="Nazwa przypadku testowego" SortExpression="nazwa">
                                        <HeaderStyle HorizontalAlign="Center" Width="720px" />
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
                            <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                            <EmptyDataTemplate>
                                <asp:Label ID="Label3" Text="Nie utworzono przypadków testowych." Visible="true" runat="server"></asp:Label>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
                            SelectCommand="SELECT id, nazwa, status FROM Przypadki_testowe WHERE Projekty_id = @projekt_id AND Zadanie_programistyczne_id = @zadanie_id">
                            <SelectParameters>
                                <asp:SessionParameter Name="projekt_id" SessionField="projekt_id" Type="Int32" />
                                <asp:SessionParameter Name="zadanie_id" SessionField="zadanie_id" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>                    
                        
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
        
        </Content>
        </asp:AccordionPane>
        <asp:AccordionPane ID="AccordionPane2" runat="server">
        <Header>Tworzenie nowego powiązania</Header>
        <Content>    
        <ajaxToolkit:TabContainer ID="TabContainer1" runat="server">
            <ajaxToolkit:TabPanel ID="TabPanel1" runat="server">
                <HeaderTemplate>Zadania programistyczne</HeaderTemplate>
                <ContentTemplate>
                    <asp:Label ID="komentarz_Label" runat="server" Text="Uwagi do powiązania:"></asp:Label>
                    <asp:TextBox ID="komentarz_TextBox" runat="server" MaxLength="255" Width="734px"></asp:TextBox>
                    <br /><br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="SqlDataSource1" DataKeyNames="id" OnRowCommand="GridView_RowCommand" 
                        AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>                                                        
                            <asp:TemplateField HeaderText="Nazwa zadania" SortExpression="nazwa">
                                <HeaderStyle HorizontalAlign="Center" Width="530px" />
                                <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="myLink" CommandName="Zadanie" CommandArgument='<%#Eval("id") %>' runat="server">                                        
                                    <%#Eval("nazwa") %>
                                    </asp:LinkButton>                                    
                                </ItemTemplate>
                            </asp:TemplateField>                                                                                    
                            <asp:BoundField DataField="nr_wydania" HeaderText="Wydanie" SortExpression="nr_wydania" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nr_iteracji" HeaderText="Iteracja" SortExpression="nr_iteracji" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                            </asp:BoundField>

                            <asp:TemplateField>                                
                                <ItemStyle HorizontalAlign="Center" Width="180px" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="newZadLink" CommandName="PowiazZadanie" CommandArgument='<%#Eval("id") %>' runat="server">
                                        Powiąż z tym zadaniem
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
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        <EmptyDataTemplate>
                            <asp:Label ID="Label3" Text="Brak zadań do przypisania" Visible="true" runat="server"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>"                         
                        SelectCommand="SELECT DISTINCT z.id, z.nazwa, h.nr_wydania, h.nr_iteracji FROM Zadania_programistyczne z, Historyjki_uzytkownikow h WHERE z.Projekty_id =@projekt_id AND z.id !=@zadanie_id AND h.id = z.Historyjka_uzytkownika_id AND z.id NOT IN (SELECT pz.Zadanie1_id FROM Powiazane_zadania pz WHERE pz.Zadanie2_id=@zadanie_id) AND z.id NOT IN (SELECT pz.Zadanie2_id FROM Powiazane_zadania pz WHERE pz.Zadanie1_id=@zadanie_id);">
                        <SelectParameters>
                            <asp:SessionParameter Name="projekt_id" SessionField="projekt_id" />
                            <asp:SessionParameter Name="zadanie_id" SessionField="zadanie_id" />
                        </SelectParameters>
                    </asp:SqlDataSource>                    
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel2" runat="server">
                <HeaderTemplate>Pliki</HeaderTemplate>
                <ContentTemplate>

                 <asp:GridView ID="GridView4" runat="server" DataSourceID="SqlDataSource4"
                        AutoGenerateColumns="False" DataKeyNames="id" OnRowCommand="GridView_RowCommand" 
                        AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>                                                                                   
                            <asp:TemplateField HeaderText="Nazwa pliku" SortExpression="nazwa">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left"/>
                                <ItemTemplate>
                                    <asp:LinkButton ID="myLinkPlik" CommandName="Plik" CommandArgument='<%#Eval("id") %>' runat="server" Width="365px"  Style="word-wrap: break-word;">
                                    <%#Eval("nazwa") %>
                                    </asp:LinkButton>                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                                            
                            <asp:TemplateField HeaderText="Ścieżka" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" SortExpression="sciezka">
                                <ItemTemplate>
                                    <div style="word-wrap: break-word; width: 375px" >
                                        <%#Eval("sciezka") %></div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField>                            
                            <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>                                                                
                                    <asp:LinkButton ID="PowiazPlik" CommandName="Powiaz" CommandArgument='<%#Eval("id") %>' runat="server" Width="115px">
                                    Powiąż z plikiem
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
                            <asp:Label ID="Label5" Text="Brak plików do powiązania" Visible="true" runat="server"></asp:Label>
                        </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
                            SelectCommand="SELECT p.id, p.nazwa, p.sciezka FROM Pliki p WHERE p.Projekty_id = @projekt_id AND p.id NOT IN (SELECT Plik_id FROM Pliki_zadania_programistyczne WHERE Zadanie_programistyczne_id = @zadanie_id);">
                        <SelectParameters>
                            <asp:SessionParameter Name="zadanie_id" SessionField="zadanie_id" />
                            <asp:SessionParameter Name="projekt_id" SessionField="projekt_id" />
                        </SelectParameters>
                        </asp:SqlDataSource>
                
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel3" runat="server">
                <HeaderTemplate>Przypadki testowe</HeaderTemplate>
                <ContentTemplate>

                <asp:Button ID="nowyPT_Button" runat="server" OnClick="nowyPT_Button_Click" Text="Dodaj nowy przypadek testowy" />
                
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel8" runat="server">
                <HeaderTemplate>Realizatorzy</HeaderTemplate>
                <ContentTemplate>
                    <asp:GridView ID="GridView7" runat="server" DataSourceID="SqlDataSource7"
                        AutoGenerateColumns="False" DataKeyNames="id" OnRowCommand="GridView_RowCommand" 
                        AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>                                                                                
                        <asp:TemplateField HeaderText="Użytkownik" SortExpression="login">
                        <HeaderStyle HorizontalAlign="Center" Width="350px"/>
                        <ItemStyle HorizontalAlign="Left"/>
                            <ItemTemplate>
                                <%#Eval("imie") %>  <%#Eval("nazwisko") %> (login: <%#Eval("login") %> )                                
                            </ItemTemplate>
                        </asp:TemplateField>
                                                                        
                        <asp:TemplateField>                            
                        <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>                                                                
                                <asp:LinkButton ID="dodajRealizatoraLink" CommandName="DodajRealizatora" CommandArgument='<%#Eval("id") %>' runat="server" Width="115px">
                                Dodaj realizatora
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
                        <asp:Label ID="Label5" Text="Brak użytkowników do przypisania." Visible="true" runat="server"></asp:Label>
                    </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource7" runat="server" ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
                        SelectCommand="SELECT u.id, u.imie, u.nazwisko, u.login FROM Uzytkownicy u, Uzytkownicy_Projekty up WHERE u.id = up.Uzytkownik_id AND up.Projekt_id = @projekt_id AND u.id NOT IN (SELECT zp.Realizator1_id FROM Zadania_programistyczne zp, Uzytkownicy u WHERE zp.id = @zadanie_id AND u.id = zp.Realizator1_id) AND u.id NOT IN (SELECT zp.Realizator2_id FROM Zadania_programistyczne zp, Uzytkownicy u WHERE zp.id = @zadanie_id AND u.id = zp.Realizator2_id)">
                    <SelectParameters>                        
                        <asp:SessionParameter Name="projekt_id" SessionField="projekt_id" />
                        <asp:SessionParameter Name="zadanie_id" SessionField="zadanie_id" />
                    </SelectParameters>
                    </asp:SqlDataSource>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer></Content>
        </asp:AccordionPane>
        <asp:AccordionPane ID="AccordionPane4" runat="server">
        <Header>Usunięcię zadania</Header>
        <Content>    
            <asp:Label ID="usuniecie_Label" Text="Czy na pewno chcesz usunąć to zadanie?" Visible="true" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Button ID="usun_Button" Text="Tak, usuń zadanie." Visible="true" runat="server" OnClick="usun_Button_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="nie_usuwaj_Button" Text="Nie usuwaj." Visible="true" runat="server" OnClick="nie_usuwaj_Button_Click" />
        </Content>
        </asp:AccordionPane>
    </Panes>
    </asp:Accordion>
    <br />
    <div style="text-align:right">
    <asp:Button ID="powrot_Button" runat="server" onclick="powrot_Button_Click" Text="Powrót" />
    </div>
     <br />
    <br />
</asp:Content>
