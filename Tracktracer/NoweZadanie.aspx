<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NoweZadanie.aspx.cs" Inherits="Tracktracer.NoweZadanie" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <style type="text/css">
        .style1
        {
            text-align: right;
            height: 412px;
            width: 607px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="Panel1" runat="server" Height="168px" Width="611px">
        <div class="style1">
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Nazwa: "></asp:Label>
            <asp:TextBox ID="nazwa_TextBox" runat="server" Height="22px" MaxLength="255" 
                style="text-align: left" ValidationGroup="1" Width="400px"></asp:TextBox>
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="nazwa_TextBox" 
                ErrorMessage="Pole 'Nazwa' nie może pozostać puste." ValidationGroup="1" ForeColor="Red"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Treść: "></asp:Label>
            <asp:TextBox ID="tresc_TextBox" runat="server" Height="81px" MaxLength="500" TextMode="MultiLine" Width="400px"></asp:TextBox>
            <br />
            <asp:Label ID="trescR_Label" runat="server" 
                Text="Maksymalna długość treści zadania to 500 znaków." Visible="False" ForeColor="Red"></asp:Label>
            <br />                        
            <asp:Button ID="powrot_Button" runat="server" Text="Powrót" 
                onclick="powrot_Button_Click" />
            &nbsp;&nbsp;
            <asp:Button ID="zadanie_Button" runat="server" 
                onclick="zadanie_Button_Click" Text="Dodaj zadanie" ValidationGroup="1" />
            <br />
            <asp:Label ID="info_Label" runat="server" Text="Label" Visible="False" ForeColor="#FF0066"></asp:Label>
            <br />
        </div>
    </asp:Panel>
</asp:Content>
