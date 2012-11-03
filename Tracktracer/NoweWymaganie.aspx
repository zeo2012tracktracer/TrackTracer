﻿<%@ Page Title="TrackTracer"  Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NoweWymaganie.aspx.cs" Inherits="Tracktracer.NoweWymaganie" %>
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
                ErrorMessage="Pole 'Nazwa' nie może pozostać puste." ValidationGroup="1" 
                ForeColor="Red"></asp:RequiredFieldValidator>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Opis: "></asp:Label>
            <asp:TextBox ID="opis_TextBox" runat="server" Height="81px" MaxLength="500" 
                ontextchanged="opis_TextBox_TextChanged" TextMode="MultiLine" Width="400px"></asp:TextBox>
            <br />
            <asp:Label ID="opisR_Label" runat="server" 
                Text="Maksymalna długość opisu to 500 znaków." Visible="False" 
                ForeColor="Red"></asp:Label>
            <br />
            <asp:Label ID="Label3" runat="server" Text="Uwagi: "></asp:Label>
            <asp:TextBox ID="uwagi_TextBox" runat="server" MaxLength="255" Rows="3" 
                TextMode="MultiLine" Width="400px"></asp:TextBox>
            <br />
            <asp:Label ID="uwagiR_Label" runat="server" 
                Text="Maksymalna długość pola 'Uwagi' to 255 znaków." Visible="False" 
                ForeColor="Red"></asp:Label>
            <br />
            Udziałowcy:
            <asp:TextBox ID="udzialowcy_TextBox" runat="server" MaxLength="255" Rows="3" 
                TextMode="MultiLine" Width="400px"></asp:TextBox>
            <br />
            <asp:Label ID="udzialowcyR_Label" runat="server" 
                Text="Maksymalna długość pola 'Udziałowcy' to 255 znaków." Visible="False" 
                ForeColor="Red"></asp:Label>
            <br />
            <asp:Button ID="anuluj_Button" runat="server" Text="Anuluj" 
                onclick="anuluj_Button_Click" />
            &nbsp;&nbsp;
            <asp:Button ID="wymaganie_Button" runat="server" 
                onclick="wymaganie_Button_Click" Text="Dodaj wymaganie" ValidationGroup="1" />
            <br />
            <asp:Label ID="info_Label" runat="server" Text="Label" Visible="False" 
                ForeColor="#FF0066"></asp:Label>
            <br />
        </div>
    </asp:Panel>
&nbsp;
</asp:Content>