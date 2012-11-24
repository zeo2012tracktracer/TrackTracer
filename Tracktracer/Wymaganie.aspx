<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit"%>
<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Wymaganie.aspx.cs" Inherits="Tracktracer.Wymaganie" %>
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
        <Header>Wymaganie</Header>
        <Content>
            <asp:Table ID="wym_Table" runat="server">
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label1" runat="server" Text="Nazwa projektu: "></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell ForeColor="Black">
                        <asp:Label ID="projNazwa_Label" runat="server" Text="Label"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label7" runat="server" Text="Numer aktualnej rewizji: "></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell ForeColor="Black">
                        <asp:Label ID="rew_Label" runat="server" ></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label2" runat="server" Text="Nazwa wymagania: "></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell ForeColor="Black">
                        <asp:Label ID="wymNazwa_Label" runat="server" Text="Label"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label6" runat="server" Text="Osoba realizująca wymaganie: "></asp:Label>
                        &nbsp;
                    </asp:TableCell>
                    <asp:TableCell ForeColor="Black">
                        <asp:Label ID="realizator_Label" runat="server" ></asp:Label>                        
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="przypisanie_Label" runat="server" Text="Przypisanie do rejestru: "></asp:Label>        
                    </asp:TableCell>
                    <asp:TableCell ForeColor="Black">
                        <asp:Label ID="brak_przypisania_Label" runat="server" Text="Wymaganie nie zostało jeszcze przypisane do żadnej iteracji" Visible="false"></asp:Label>
                        <asp:Label ID="iteracja_Label" runat="server" Text="Iteracja: X"></asp:Label>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="przypisanie_Button" runat="server" Text="Usuń przypisania" onclick="przypisanie_Button_Click" />        
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label ID="Label4" runat="server" Text="Status: "></asp:Label>        
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            <asp:ListItem Value="aktywne" Text="Aktywne"></asp:ListItem>
                            <asp:ListItem Value="zakończone" Text="Zakończone"></asp:ListItem>
                            <asp:ListItem Value="usunięte" Enabled="False" Text="Usunięte"></asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="status_Button" runat="server" OnClick="status_Button_Click" Text="Zmień status" Visible="false" />        
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>                                                                                                                                           
            <br />
            <asp:Label ID="opis_Label" runat="server" Text="Opis:"></asp:Label>    
            <br />
            <br />
            <asp:TextBox ID="opis_TextBox" runat="server" BorderStyle="None" Width="600px" TextMode="MultiLine"></asp:TextBox>
            <br />
            <br />
            <asp:table ID="Table1" runat="server">
                <asp:TableRow VerticalAlign="Top">
                <asp:TableCell Width="460px" HorizontalAlign="Left">                                
                    <asp:Label ID="uwagi_Label" runat="server" Text="Uwagi: "></asp:Label>
                    <br />                    
                    <br />
                    <asp:TextBox ID="uwagi_TextBox" runat="server" BorderStyle="None" 
                        TextMode="MultiLine" Width="400px"></asp:TextBox>                    
                </asp:TableCell>
                <asp:TableCell Width="460px" HorizontalAlign="Left">
                    <asp:Label ID="udzialowcy_Label" runat="server" Text="Udziałowcy: "></asp:Label>
                    <br />
                    <br />
                    <asp:TextBox ID="udzialowcy_TextBox" runat="server" BorderStyle="None" 
                        TextMode="MultiLine" Width="400px"></asp:TextBox>
                </asp:TableCell>
                </asp:TableRow>
            </asp:table>
            <br />
            <asp:Label ID="wysWersja_Label" runat="server" Text="Wyświetlana wersja wymagania: "></asp:Label>
            <asp:DropDownList ID="wersje_DropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="wersje_DropDownList_SelectedIndexChanged">
            </asp:DropDownList>
            &nbsp;&nbsp;
            <asp:CheckBox ID="rew_CheckBox" Checked="false" runat="server" Text="Tylko sprzed rewizji: " oncheckedchanged="rew_CheckBox_CheckedChanged" 
            CausesValidation="true" AutoPostBack="true" ValidationGroup="1"/>            
            &nbsp;
            <asp:TextBox ID="rew_TextBox" runat="server" MaxLength="4" Width="40px" ValidationGroup="1"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="nowaWersja_Button" runat="server" onclick="nowaWersja_Button_Click" Text="Zapisz nową wersję wymagania" />                             
            <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="<br />Niepoprawny nr rewizji" ControlToValidate="rew_TextBox" MinimumValue="0" ValidationGroup="1" Type="Integer" ForeColor="Red"></asp:RangeValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<br />Podaj nr rewizji" ValidationGroup="1" ControlToValidate="rew_TextBox" ForeColor="Red"></asp:RequiredFieldValidator>                                    
            <asp:Label ID="opisR_Label" runat="server" Text="<br />Maksymalna długość opisu to 500 znaków." Visible="False" ForeColor="Red"></asp:Label> 
            <asp:Label ID="uwagiR_Label" runat="server" Text="<br />Maksymalna długość pola 'Uwagi' to 255 znaków." Visible="False" ForeColor="Red"></asp:Label>
            <asp:Label ID="udzialowcyR_Label" runat="server" Text="<br />Maksymalna długość pola 'Udziałowcy' to 255 znaków." Visible="False" ForeColor="Red"></asp:Label>
            <br />
        </Content>
        </asp:AccordionPane>
    <asp:AccordionPane ID="AccordionPane1" runat="server">
        <Header>Istniejące powiązania</Header>
        <Content>
            <ajaxToolkit:TabContainer ID="TabContainer2" runat="server">
                <ajaxToolkit:TabPanel ID="TabPanel4" runat="server">
                    <HeaderTemplate>Wymagania</HeaderTemplate>
                    <ContentTemplate>
                        <asp:GridView ID="GridView2" runat="server" DataSourceID="SqlDataSource2"
                        AutoGenerateColumns="False" DataKeyNames="id" OnRowCommand="GridView_RowCommand" 
                        AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>                                                        
                            <asp:TemplateField HeaderText="Nazwa wymagania" SortExpression="nazwa">
                            <HeaderStyle HorizontalAlign="Left" Width="565px" />
                            <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="myLink2" CommandName="Wymaganie" CommandArgument='<%#Eval("id") %>' runat="server">                                        
                                    <%#Eval("nazwa") %>
                                    </asp:LinkButton>
                                    <div id="Div1" Visible='<%# Eval("opis") != DBNull.Value %>' runat="server">
                                    <asp:Label ID="lab" Text="Komentarz: " runat="server"><%#Eval("opis") %></asp:Label>          
                                    </div>                                                                                                           
                                    <asp:LinkButton ID="weryfikacjaLink" CommandName="Weryfikuj" CommandArgument='<%#Eval("id") %>' runat="server"
                                         Visible='<%# ( Eval("Wymaganie1_id").ToString() == Eval("id").ToString() ? Eval("wersja1").ToString() : Eval("wersja2").ToString() ) != Eval("wersja").ToString() %>' >
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
                            <asp:TemplateField >                            
                                <ItemStyle HorizontalAlign="Center" Width="150px"/>
                                <ItemTemplate>
                                    <asp:LinkButton ID="noweWym" CommandName="Usun_wym" CommandArgument='<%#Eval("id") %>' runat="server">                                        
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
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        <EmptyDataTemplate>
                            <asp:Label ID="Label3" Text="Brak powiązanych wymagań" Visible="true" runat="server"></asp:Label>
                        </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
        SelectCommand="SELECT w.id, w.wersja, w.nazwa, w.nr_wydania, w.nr_iteracji, pw.opis, pw.Wymaganie1_id, pw.wersja1, pw.wersja2 FROM Wymagania w, Powiazane_wymagania pw WHERE w.id != @wymaganie_id AND ((pw.Wymaganie1_id = w.id AND pw.Wymaganie2_id =@wymaganie_id) OR (pw.Wymaganie2_id = w.id AND pw.Wymaganie1_id=@wymaganie_id));">
           <SelectParameters>
               <asp:SessionParameter Name="wymaganie_id" SessionField="wymaganie_id" />
           </SelectParameters>
    </asp:SqlDataSource>
                        
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="TabPanel5" runat="server">
                    <HeaderTemplate>Pliki</HeaderTemplate>
                    <ContentTemplate>                        
                        <asp:GridView ID="GridView3" runat="server" DataSourceID="SqlDataSource3"
                        AutoGenerateColumns="False" DataKeyNames="id" OnRowCommand="GridView_RowCommand" 
                        onselectedindexchanged="GridView3_SelectedIndexChanged" AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>                                                        
                            <asp:TemplateField HeaderText="Nazwa pliku" SortExpression="nazwa">
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />                            
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
                                                        
                            <asp:CommandField SelectText="Usuń powiązanie" ShowSelectButton="True">
                                <ItemStyle HorizontalAlign="Center" Width="120px"/>
                            </asp:CommandField>
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
                            <asp:Label ID="Label5" Text="Brak powiązanych plików" Visible="true" runat="server"></asp:Label>
                        </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
                            SelectCommand="SELECT p.id, p.nazwa, p.sciezka FROM Pliki p, Pliki_wymagania pw WHERE p.id = pw.Plik_id AND pw.Wymaganie_id = @wymaganie_id">
                        <SelectParameters>
                            <asp:SessionParameter Name="wymaganie_id" SessionField="wymaganie_id" />
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
                                        <HeaderStyle HorizontalAlign="Center" Width="750px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="testLink" CommandName="PrzypadekTestowy" CommandArgument='<%#Eval("id") %>' runat="server">                                        
                                            <%#Eval("nazwa") %>
                                            </asp:LinkButton>                                    
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status" >
                                        <HeaderStyle HorizontalAlign="Center" Width="150px"/>
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
                                <asp:Label ID="Label8" Text="Nie utworzono przypadków testowych." Visible="true" runat="server"></asp:Label>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
                            SelectCommand="SELECT id, nazwa, status FROM Przypadki_testowe WHERE Projekty_id = @projekt_id AND Wymaganie_id = @wymaganie_id">
                            <SelectParameters>
                                <asp:SessionParameter Name="projekt_id" SessionField="projekt_id" Type="Int32" />
                                <asp:SessionParameter Name="wymaganie_id" SessionField="wymaganie_id" Type="Int32" />
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
                <HeaderTemplate>Wymagania</HeaderTemplate>
                <ContentTemplate>
                    <asp:Label ID="komentarz_Label" runat="server" Text="Uwagi do powiązania:"></asp:Label>
                    <asp:TextBox ID="komentarz_TextBox" runat="server" MaxLength="255" Width="734px"></asp:TextBox>
                    <br /><br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                        DataSourceID="SqlDataSource1" DataKeyNames="id" OnRowCommand="GridView_RowCommand" 
                        AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>                                                        
                            <asp:TemplateField HeaderText="Nazwa wymagania">
                            <HeaderStyle HorizontalAlign="Left" Width="550px" />
                            <ItemStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="noweZadLink" CommandName="Wymaganie" CommandArgument='<%#Eval("id") %>' runat="server">                                        
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
                            <asp:TemplateField >                            
                                <ItemStyle HorizontalAlign="Center" Width="170px"/>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" CommandName="Powiaz_wym" CommandArgument='<%#Eval("id") %>' runat="server">                                        
                                    Powiąż z wymaganiem
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
                            <asp:Label ID="Label9" Text="Brak wymagań do przypisania" Visible="true" runat="server"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>"                         
                        SelectCommand="SELECT DISTINCT w.id, w.nazwa, w.nr_wydania, w.nr_iteracji FROM Wymagania w WHERE w.Projekty_id =@proj_id AND w.id !=@wymaganie_id AND w.id NOT IN (SELECT pw.Wymaganie1_id FROM Powiazane_wymagania pw WHERE pw.Wymaganie2_id=@wymaganie_id) AND w.id NOT IN (SELECT pw.Wymaganie2_id FROM Powiazane_wymagania pw WHERE pw.Wymaganie1_id=@wymaganie_id);">
                        <SelectParameters>
                            <asp:SessionParameter Name="proj_id" SessionField="projekt_id" />
                            <asp:SessionParameter Name="wymaganie_id" SessionField="wymaganie_id" />
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
                                    <asp:LinkButton ID="myLinkPlik2" CommandName="Plik" CommandArgument='<%#Eval("id") %>' runat="server" Width="365px"  Style="word-wrap: break-word;">
                                    <%#Eval("nazwa") %>
                                    </asp:LinkButton>                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                                            
                            <asp:TemplateField HeaderText="Ścieżka" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" SortExpression="sciezka">
                                <ItemTemplate>
                                    <div style="word-wrap: break-word; width: 390px" >
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
                            <asp:Label ID="Label10" Text="Brak plików do powiązania" Visible="true" runat="server"></asp:Label>
                        </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:test5ConnectionString %>" 
                            SelectCommand="SELECT p.id, p.nazwa, p.sciezka FROM Pliki p WHERE p.Projekty_id = @projekt_id AND p.id NOT IN (SELECT Plik_id FROM Pliki_wymagania WHERE Wymaganie_id = @wymaganie_id);">
                        <SelectParameters>
                            <asp:SessionParameter Name="wymaganie_id" SessionField="wymaganie_id" />
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
        </ajaxToolkit:TabContainer></Content>
        </asp:AccordionPane>
        <asp:AccordionPane ID="AccordionPane4" runat="server">
        <Header>Usunięcię wymagania</Header>
        <Content>    
            <asp:Label ID="usuniecie_Label" Text="Czy na pewno chcesz usunąć to wymaganie?" Visible="true" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Button ID="usun_Button" Text="Tak, usuń wymaganie." Visible="true" runat="server" OnClick="usun_Button_Click" />
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
    <br />     
</asp:Content>
