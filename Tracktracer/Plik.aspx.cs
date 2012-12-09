using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Security;

namespace Tracktracer
{
    struct DiffEl
    {
        public int line;
        public int rows;
        public string text;
    }

    public partial class Plik : System.Web.UI.Page
    {
        private int user_id;
        private SqlConnection conn;        
        private int projekt_id;
        private int id_pliku;
        private string nazwa;
        private int akt_rewizja;
        private List<string> powroty;
        private List<int> powroty_id;
        private string svn_url;
        private string svn_user;
        private string svn_pass;
        public List<List<FileOp>> operacje;
        private string metodyka;

        private List<DiffEl> listA;
        private List<DiffEl> listB;
        private string revA;
        private string revB;
        private string diffs;
        private string openSpan;
        private string closeSpan;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                user_id = (int)Session["user_id"];
                conn = (SqlConnection)Session["connection"];
                projekt_id = (int)Session["projekt_id"];
                nazwa = (string)Session["nazwa"];                
                metodyka = (string)Session["metodyka"];
                id_pliku = (int)Session["id_pliku"];                                
                nazwa_Label.Text = "Nazwa projektu: " + nazwa;
            }
            catch
            {
                Server.Transfer("Index.aspx");
            }

            powroty = (List<string>)Session["powroty"];
            powroty_id = (List<int>)Session["powroty_id"];

            if (powroty == null)
            {
                powroty = new List<string>();
                powroty_id = new List<int>();
            }
            if (!IsPostBack)
            {
                powroty.Add("Plik.aspx");
                powroty_id.Add(id_pliku);
            }
            Session["powroty"] = powroty;
            Session["powroty_id"] = powroty_id;
            
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "SELECT p.rewizja, p.svn_url, p.svn_user, p.svn_pass FROM Projekty p WHERE p.id =@projekt_id ;";
            zapytanie.Parameters.AddWithValue("@projekt_id", projekt_id);

            SqlDataReader reader;
            reader = zapytanie.ExecuteReader();
            try
            {
                reader.Read();
                akt_rewizja = reader.GetInt32(0);
                svn_url = reader.GetString(1);
                svn_user = reader.GetString(2);
                svn_pass = reader.GetString(3);
                Session["akt_rewizja"] = akt_rewizja;
                reader.Close();
                rew_Label.Text = "Numer aktualnej rewizji: " + akt_rewizja;
            }
            catch
            {
                reader.Dispose();
                test_Button.Visible = false;
            }

            if (metodyka.CompareTo("Scrum") == 0)
            {
                powiazane_Label.Text = "Powiązane wymagania: <br />";
                SqlDataSource4.SelectCommand = "SELECT w.nazwa, w.id, w.status FROM Wymagania w, Pliki_wymagania pw WHERE pw.Plik_id = @id_pliku AND w.id = pw.Wymaganie_id AND status != 'usunięte'";
            }
            else
            {
                powiazane_Label.Text = "Powiązane zadania programistyczne: <br />";
                SqlDataSource4.SelectCommand = "SELECT z.nazwa, z.id, z.status FROM Zadania_programistyczne z, Pliki_zadania_programistyczne pz WHERE pz.Plik_id = @id_pliku AND z.id = pz.Zadanie_programistyczne_id AND z.status != 'Usunięte'";
            }

            revA = string.Empty;
            revB = string.Empty;
            diffs = string.Empty;
            listA = new List<DiffEl>();
            listB = new List<DiffEl>();
            openSpan = "<span style='color:#C00000;' >";
            closeSpan = "</span>";                        
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int no = 0;
            if (e.CommandName.CompareTo("Powiazanie") == 0)
            {
                if (metodyka.CompareTo("Scrum") == 0)
                {
                    no = Convert.ToInt32(e.CommandArgument);
                    Session["wymaganie_id"] = no.ToString();                    
                    Server.Transfer("Wymaganie.aspx");
                }
                else
                {
                    no = Convert.ToInt32(e.CommandArgument);
                    Session["zadanie_id"] = no.ToString();                    
                    Server.Transfer("ZadanieProgramistyczne.aspx");
                }
            }
        }

        protected void test_Button_Click(object sender, EventArgs e)
        {
            string rev1 = rew1_DropDownList.SelectedValue;
            string rev2 = rew2_DropDownList.SelectedValue;
            string rev1_path = string.Empty;
            string rev2_path = string.Empty;
            string f_path = string.Empty;

            if (string.IsNullOrEmpty(rev2) == false)
            {
                SqlCommand zapytanie = new SqlCommand();
                zapytanie.Connection = conn;
                zapytanie.CommandType = CommandType.Text;
                zapytanie.CommandText = "SELECT hp.nr_rewizji, hp.stara_sciezka FROM Historia_plikow hp WHERE hp.Pliki_id =@id_pliku AND hp.nr_rewizji > @rev1 AND hp.stara_sciezka IS NOT NULL ORDER BY nr_rewizji ASC";
                zapytanie.Parameters.AddWithValue("@id_pliku", id_pliku);
                zapytanie.Parameters.AddWithValue("@rev1", rev1);

                SqlDataReader reader;
                reader = zapytanie.ExecuteReader();

                int pobrac_nazwe = 1;

                if (reader.Read())
                {   
                    pobrac_nazwe = 0;
                    rev1_path = reader.GetString(1);
                    rev2_path = rev1_path;
                    int rev_found = reader.GetInt32(0);

                    while (reader.Read() && rev_found <= Convert.ToInt32(rev2))
                    {                                                
                            rev2_path = reader.GetString(1);
                            rev_found = reader.GetInt32(0);                        
                    }
                    reader.Close();

                    if(rev_found <= Convert.ToInt32(rev2))
                    {
                        zapytanie.CommandText = "SELECT sciezka FROM Pliki WHERE id=@id_pliku ;";
                        zapytanie.Parameters.AddWithValue("@id_pliku", id_pliku);
                        reader = zapytanie.ExecuteReader();
                        reader.Read();
                        rev2_path = reader.GetString(0);
                        reader.Close();
                    }
                }
                reader.Close();
                                                                    
                zapytanie.CommandText = "SELECT sciezka FROM Pliki WHERE id=@id_pliku ;";
                zapytanie.Parameters.AddWithValue("@id_pliku", id_pliku);   
                reader = zapytanie.ExecuteReader();
                reader.Read();
                f_path = reader.GetString(0);                    
                reader.Close();
                if (pobrac_nazwe != 0)
                {
                    rev1_path = f_path;
                    rev2_path = f_path;
                }
                                
                ProcessStartInfo info = new ProcessStartInfo("svn");                 
                info.UseShellExecute = false;                
                info.RedirectStandardInput = true;
                info.RedirectStandardOutput = true;
                info.CreateNoWindow = true;
                info.StandardOutputEncoding = Encoding.UTF8;

                info.Arguments = @"cat "+ svn_url + f_path.Substring(1, f_path.Length-1) + " -r "+ rev1;                
                using (Process process = Process.Start(info))
                {                    
                    revA = process.StandardOutput.ReadToEnd();                    
                }

                nazwa_Label.Text = info.Arguments;

                info.Arguments = @"diff " + svn_url + rev1_path.Substring(1, rev1_path.Length - 1) + "@" + rev1 + " " + svn_url + rev2_path.Substring(1, rev2_path.Length - 1) + "@" + rev2;
                using (Process process = Process.Start(info))
                {
                    diffs = process.StandardOutput.ReadToEnd();
                }

                Label1.Text = info.Arguments;

                info.Arguments = @"cat " + svn_url + f_path.Substring(1, f_path.Length - 1) + " -r " + rev2;
                using (Process process = Process.Start(info))
                {
                    revB = process.StandardOutput.ReadToEnd();
                }

                przetworz_diffs();
                test_Table.Rows[1].Cells[0].Text = kod_poprz_rew();
                revA = string.Empty;
                revA = revB;
                listA = listB;
                openSpan = "<span style='color:#00C000;' >";
                test_Table.Rows[1].Cells[1].Text = kod_poprz_rew();
                test_Table.Rows[0].Cells[0].Text = "Rewizja nr " + rev1 + ":";
                test_Table.Rows[0].Cells[1].Text = "Rewizja nr " + rev2 + ":";
            }
        }

        // Wyciągnięcie informacji uzyskanych z polecenia "svn diff"
        protected void przetworz_diffs()
        {
            int i = diffs.IndexOf("\n@@");
            int j = 0;
            int minus = 0;
            int plus = 0;
            int komma = 0;
            int komma2 = 0;
            int ape = 0;
            while (i < diffs.Length && i >= 0)
            {
                DiffEl deA = new DiffEl();
                DiffEl deB = new DiffEl();
                minus = diffs.IndexOf("-", i);
                komma = diffs.IndexOf(",", minus);                                                
                plus = diffs.IndexOf("+", minus);

                if (komma < plus)
                {
                    deA.line = Convert.ToInt32(diffs.Substring(minus + 1, komma - 1 - minus).Trim());
                    deA.rows = Convert.ToInt32(diffs.Substring(komma + 1, plus - 1 - komma).Trim());
                }
                else
                {
                    komma = diffs.IndexOf(" ", minus);
                    deA.line = Convert.ToInt32(diffs.Substring(minus + 1, komma - 1 - minus).Trim());
                    deA.rows = 0;
                }                
                
                komma2 = diffs.IndexOf(",", plus);
                deB.line = Convert.ToInt32(diffs.Substring(plus + 1, komma2 - 1 - plus).Trim());                
                ape = diffs.IndexOf("@", komma2);
                deB.rows = Convert.ToInt32(diffs.Substring(komma2 + 1, ape - 1 - komma2).Trim());

                j = diffs.IndexOf("\n", ape);
                j = j + 1;
                
                i = diffs.IndexOf("\n@@", j);
                if (i > 0)
                {
                    deA.text = diffs.Substring(j, i - 1 -j);
                    deB.text = diffs.Substring(j, i - 1 -j);
                }
                else
                {
                    deA.text = diffs.Substring(j, diffs.Length - j);
                    deB.text = diffs.Substring(j, diffs.Length - j);
                }
                listA.Add(deA);
                listB.Add(deB);
            }
            oczysc();
        }

        // Funkcja pozwoli wyciągnąć z wyniku polecenia diff linie zaczynające się znakiem - lub + czyli tylko te, 
        //które uległy modyfikacji.
        protected void oczysc()
        {
            for(int i=0; i<listA.Count; i++)
            {
                DiffEl de = listA[i];
                StringReader reader = new StringReader(de.text);
                string linia = string.Empty;                
                string wynik = string.Empty;

                while((linia = reader.ReadLine()) != null)
                {
                    if(linia.StartsWith("-"))
                    {
                        linia = linia.Remove(0, 1);
                        wynik += linia;
                        wynik += "\n";
                    }
                }
                reader.Close();
                de.text = wynik;
                listA[i] = de;
            }

            for (int i = 0; i < listB.Count; i++)
            {
                DiffEl de = listB[i];
                StringReader reader2 = new StringReader(de.text);
                string linia = string.Empty;
                string wynik = string.Empty;

                while ((linia = reader2.ReadLine()) != null)
                {
                    if (linia.StartsWith("+"))
                    {
                        linia = linia.Remove(0, 1);
                        wynik += linia;
                        wynik += "\n";
                    }
                }
                reader2.Close();
                de.text = wynik;
                listB[i] = de;
            }

            string rev = string.Empty;
            StringReader readerA = new StringReader(revA);
            string a = string.Empty;
            while ((a = readerA.ReadLine()) != null)
            {
                rev += a;
                rev += "\n";
            }
            revA = rev;

            string rev2 = string.Empty;
            StringReader readerB = new StringReader(revB);
            string b = string.Empty;
            while ((b = readerB.ReadLine()) != null)
            {
                rev2 += b;
                rev2 += "\n";
            }
            revB = rev2;
        }

        // Funkcja zwraca zawartość pliku z danej rewizji w formie odpowiedniej do wyświetlenia w znaczniku Label.
        protected string kod_poprz_rew()
        {            
            int n = 0;            
            int line = 1;
            string code = string.Empty;
            string newline = string.Empty;
            string revLine = string.Empty;
            StringReader reader = new StringReader(revA);
            while (n < listA.Count)
            {                
                StringReader difReader = new StringReader(listA[n].text);
                while (line < listA[n].line)
                {
                    newline = Server.HtmlEncode(reader.ReadLine());
                    code += newline.Replace(" ", "&nbsp;");
                    code += "<br>";
                    line++;
                }                
                
                string difLine = string.Empty;
                revLine = string.Empty;
                int zgodne = 0;
                while ((difLine = difReader.ReadLine()) != null)
                {
                    zgodne = 0;
                    while (zgodne != 1)
                    {
                        if ((revLine = reader.ReadLine()) != null)
                        {
                            if ((difLine.Length > 1) && revLine.EndsWith(difLine.Remove(0, 1)))                                      
                            {
                                //dodaj znacznik otwierajacy
                                code += openSpan;
                                newline = Server.HtmlEncode(revLine);
                                code += newline.Replace(" ", "&nbsp;");
                                //dodaj znacznik zamykajacy
                                code += closeSpan;
                                zgodne = 1;
                            }
                            else
                            {                                                                   
                                newline = Server.HtmlEncode(revLine);                                    
                                code += newline.Replace(" ", "&nbsp;");                                
                            }
                            
                            if (difLine.Length <= 1) 
                            {
                                zgodne = 1;
                            }
                            line++;
                            code += "<br>";
                        }
                        else
                        {
                            zgodne = 1;
                        }
                    }                    
                }               
                n++;
                difReader.Close();
            }

            while ((revLine = reader.ReadLine()) != null)
            {
                newline = Server.HtmlEncode(revLine);
                code += newline.Replace(" ", "&nbsp;");
                code += "<br>";
            }
            reader.Close();            
            return code;
        }

        protected void rew1_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            rew2_DropDownList.DataBind();
        }

        protected void powrot_Button_Click(object sender, EventArgs e)
        {
            if (powroty.Count > 1)
            {
                // zdjęcie elementów dodanych na tej stronie
                powroty.RemoveAt(powroty.Count - 1);
                powroty_id.RemoveAt(powroty_id.Count - 1);

                string strona = powroty[powroty.Count - 1];
                int elem_id = powroty_id[powroty_id.Count - 1];
                powroty.RemoveAt(powroty.Count - 1);
                powroty_id.RemoveAt(powroty_id.Count - 1);

                if (strona.CompareTo("Wymaganie.aspx") == 0)
                {
                    Session["wymaganie_id"] = elem_id.ToString();
                }
                else if (strona.CompareTo("Plik.aspx") == 0)
                {
                    Session["id_pliku"] = elem_id;
                }
                else if (strona.CompareTo("PrzypadekTestowy.aspx") == 0)
                {
                    Session["przypadek_id"] = elem_id;
                }
                else if (strona.CompareTo("WykonaniePrzypadku.aspx") == 0)
                {
                    Session["wykonanie_id"] = elem_id;
                }
                else if (strona.CompareTo("ZadanieProgramistyczne.aspx") == 0)
                {
                    Session["zadanie_id"] = elem_id.ToString();
                }
                else if (strona.CompareTo("Historyjka.aspx") == 0)
                {
                    Session["historyjka_id"] = elem_id;
                }                

                Session["powroty"] = powroty;
                Session["powroty_id"] = powroty_id;

                Server.Transfer(strona);
            }
        }       
    }
}