﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace Tracktracer
{
    public partial class Rejestruj : System.Web.UI.Page
    {
        private SqlConnection conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Server.Transfer("Index.aspx");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            conn.ConnectionString = @"Data Source=.\SQLSERVER;Initial Catalog=tracktracer; User ID=tracktracer; Integrated Security=True";
            conn.Open();
            string login = login_TextBox.Text;
            string haslo = haslo_TextBox.Text;
            string imie = Imie_TextBox.Text;
            string nazwisko = Nazwisko_TextBox.Text;

            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "INSERT INTO Uzytkownicy (login, haslo, imie, nazwisko, status_konta) VALUES ('" + login + "', '" + haslo + "', '" + imie + "', '" + nazwisko + "', 'zablokowane');";

            try
            {
                zapytanie.ExecuteNonQuery();
                Server.Transfer("Index.aspx");
            }
            catch (SqlException ex)
            {
                nowyUzytkownik_Label.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }
        }

    }
}