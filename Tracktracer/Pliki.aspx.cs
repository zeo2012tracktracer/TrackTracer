using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Diagnostics;

namespace Tracktracer
{
    public struct FileOp 
    {
        public string op;
        public string name;
        public string path;
        public string old_path;
        public string rew;
    }

    public partial class Pliki : System.Web.UI.Page
    {
        private int user_id;
        private SqlConnection conn;        
        private int projekt_id;
        private string nazwa;
        private int akt_rewizja;
        private string svn_url;
        private string svn_user;
        private string svn_pass;
        public List<List<FileOp>> operacje;
        private string metodyka;
        private List<string> powroty;
        private List<int> powroty_id;

        protected void Page_Load(object sender, EventArgs e)
        {            
            try
            {
                user_id = (int)Session["user_id"];
                conn = (SqlConnection)Session["connection"];
                projekt_id = (int)Session["projekt_id"];
                nazwa = (string)Session["nazwa"];
                akt_rewizja = (int)Session["akt_rewizja"];
                metodyka = (string)Session["metodyka"];
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
                powroty.Add("Pliki.aspx");
                powroty_id.Add(0);
            }
            Session["powroty"] = powroty;
            Session["powroty_id"] = powroty_id;

            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.CommandText = "SELECT p.svn_url, p.svn_user, p.svn_pass, p.rewizja FROM Projekty p WHERE p.id =@projekt_id ;";
            zapytanie.Parameters.AddWithValue("@projekt_id", projekt_id);

            SqlDataReader reader;
            reader = zapytanie.ExecuteReader();
            try
            {                
                reader.Read();
                svn_url = reader.GetString(0);
                svn_user = reader.GetString(1);
                svn_pass = reader.GetString(2);
                akt_rewizja = reader.GetInt32(3);
                Session["akt_rewizja"] = akt_rewizja;
                Session["svn_url"] = svn_url;
                Session["svn_user"] = svn_user;
                Session["svn_pass"] = svn_pass;
                reader.Close();
                rew_Label.Text = "Numer aktualnej rewizji: " + akt_rewizja;
            }
            catch
            {
                reader.Dispose();                
                pliki_Label.Text = "Połączenie SVN nie zostało skonfigurowane dla tego projektu";
                GridView1.Visible = false;
                test_Button.Visible = false;
                rew_Label.Visible = false;
            }
            nazwa_Label.Text = nazwa;
            operacje = new List<List<FileOp>>();
        }
        
        // Aktualizacja danych z serwera SVN
        protected void test_Button_Click(object sender, EventArgs e)
        {            
            string returnvalue = string.Empty;            
            operacje.Clear();
            int temp_rew = akt_rewizja + 1;

            ProcessStartInfo info = new ProcessStartInfo("svn");
            info.UseShellExecute = false;            
            info.Arguments = @"log " + svn_url 
                            + " -r " + temp_rew + ":HEAD -v --username " + svn_user 
                            + " --password " + svn_pass;            
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.CreateNoWindow = true;            

            using (Process process = Process.Start(info))
            {
                returnvalue = process.StandardOutput.ReadToEnd();
            }
            
            int i = 0, j = 0, n=0, next_n=0, k=0;
            int a = 0, d = 0, m=0;
            string rew = string.Empty;
            while (i < returnvalue.Length && i != -1)
            {
                // osobna lista dla każdej rewizji
                operacje.Add(new List<FileOp>());                

                i = returnvalue.IndexOf("\nr", i);            
                if (i != -1)
                {
                    next_n = 0;
                    i = i + 2;
                    j = returnvalue.IndexOf(" ", i);
                    // numer rewizji
                    rew = returnvalue.Substring(i, j - i);                    
                    i = returnvalue.IndexOf("Zmodyfikowane ścieżki:", i);

                    while (next_n != n + 2 && n != -1)
                    {
                        FileOp fo = new FileOp();                        
                        a = returnvalue.IndexOf(" A ", i);
                        d = returnvalue.IndexOf(" D ", i);
                        m = returnvalue.IndexOf(" M ", i);

                        //if ((a < d && a != -1) || (a != -1 && d == -1))
                        if(a!= -1 && ((a < d && a < m) || (a < d && m == -1) || (a < m && d == -1)))
                        {
                            // kolejny jest Added
                            n = returnvalue.IndexOf("\n", a);
                            next_n = returnvalue.IndexOf("\n", n+1);

                            fo.op = "A";
                            fo.rew = rew;
                            a = returnvalue.IndexOf("/", a);
                            string temp = returnvalue.Substring(0, n);
                            int old = temp.IndexOf("(z", a);
                            int koniec_old = temp.IndexOf(":", a);
                            if (old != -1)
                            {
                                fo.path = returnvalue.Substring(a, (old - 1) - a);
                                fo.old_path = returnvalue.Substring(old + 3, koniec_old - (old + 3));
                            }
                            else
                            {
                                fo.path = returnvalue.Substring(a, n-a-1);
                            }

                            fo.name = is_filename(fo.path);
                            if (string.IsNullOrEmpty(fo.name) == false)
                            {                                
                                operacje[k].Add(fo);
                            }
                        }
                        else if ((d < m && d != -1) || (m == -1 && d != -1))
                        {
                            // kolejny jest Deleted 
                            n = returnvalue.IndexOf("\n", d);
                            next_n = returnvalue.IndexOf("\n", n+1);

                            fo.op = "D";
                            fo.rew = rew;
                            d = returnvalue.IndexOf("/", d);
                            fo.path = returnvalue.Substring(d, n-d-1);
                            fo.name = is_filename(fo.path);
                            if (string.IsNullOrEmpty(fo.name) == false)
                            {                                
                                operacje[k].Add(fo);
                            }
                        } 
                        else if (m != -1)
                        {
                            // kolejny jest Modified
                            n = returnvalue.IndexOf("\n", m);
                            next_n = returnvalue.IndexOf("\n", n + 1);

                            fo.op = "M";
                            fo.rew = rew;
                            m = returnvalue.IndexOf("/", m);
                            fo.path = returnvalue.Substring(m, n - m - 1);
                            fo.name = is_filename(fo.path);
                            if (string.IsNullOrEmpty(fo.name) == false)
                            {
                                operacje[k].Add(fo);
                            }
                        }
                        else
                        {
                            n = returnvalue.IndexOf("\n", i);
                            next_n = returnvalue.IndexOf("\n", n + 1);
                        }

                        i = n+1;
                    }
                    //sortujemy tak by dla każdej rewizji najpierw były operacje dodawania, a dopiero później usuwania
                    usun_nadmiarowe(operacje[k]);
                    operacje[k].Sort(fo_comp);
                    k++;
                }

            }
            update_DB(rew);
            GridView1.DataBind();            
        }

        protected string is_filename(string path)
        {
            string ret = string.Empty;            
            int last_sl;
            last_sl = path.LastIndexOf("/");
            // podciąg od ostatniego slasha
            string name = path.Substring(last_sl+1, path.Length-last_sl-1);
            // pozycja kropki w nazwie
            int dot = name.IndexOf(".");
            if (dot != -1)
            {
                return name;
            }
            else
            {
                return ret;
            }
        }

        private static int fo_comp(FileOp fo1, FileOp fo2)
        {
            if (fo1.op.CompareTo(fo2.op) == 0)
            {
                return 0;
            }
            else if (fo1.op.CompareTo("A") == 0)
            {
                return 1;
            }
            else 
            {
                return -1;
            }
        }

        protected void update_DB(string rew)
        {
            string sql = string.Empty;
            string sql2 = string.Empty;
            SqlTransaction trans = conn.BeginTransaction();
            SqlCommand zapytanie = new SqlCommand();
            zapytanie.Connection = conn;
            zapytanie.CommandType = CommandType.Text;
            zapytanie.Transaction = trans;

            try
            {
                foreach (List<FileOp> rewizja in operacje)
                {
                    foreach (FileOp fo in rewizja)
                    {
                        fo.name.Trim();
                        fo.path.Trim();

                        if (fo.op.CompareTo("A") == 0)
                        {
                            if (string.IsNullOrEmpty(fo.old_path) == false)
                            {
                                fo.old_path.Trim();
                                sql = "UPDATE Pliki SET nazwa =@foname , sciezka=@fopath WHERE sciezka=@foold_path AND Projekty_id =@projekt_id ;";
                                
                                sql2 = "INSERT INTO Historia_plikow (rodzaj_modyfikacji, nr_rewizji, stara_sciezka, Pliki_id) SELECT 'Zmodyfikowano', @forew , @foold_path , p.id FROM Pliki p WHERE p.Projekty_id =@projekt_id AND p.sciezka =@fopath ;";
                            }
                            else
                            {
                                sql = "INSERT INTO Pliki (nazwa, sciezka, Projekty_id) VALUES (@foname , @fo.path , @projekt_id );";
                                sql2 = "INSERT INTO Historia_plikow (rodzaj_modyfikacji, nr_rewizji, Pliki_id) SELECT 'Dodano', @fo.rew , p.id FROM Pliki p WHERE p.Projekty_id =@projekt_id AND p.sciezka =@fopath ;";
                            }
                        }
                        else if (fo.op.CompareTo("D") == 0)// operacja usunięcia
                        {
                            sql = "DELETE FROM Pliki WHERE Projekty_id=@projekt_id AND sciezka=@fopath ";
                            sql2 = string.Empty;
                        }
                        else
                        {
                            sql = "INSERT INTO Historia_plikow (rodzaj_modyfikacji, nr_rewizji, Pliki_id) SELECT 'Zmodyfikowano', @forew , p.id FROM Pliki p WHERE p.Projekty_id =@projekt_id AND p.sciezka =@fopath ;";
                            sql2 = string.Empty;
                        }

                        System.Diagnostics.Debug.WriteLine(sql);

                        zapytanie.CommandText = sql;
                        zapytanie.Parameters.AddWithValue("@foname",fo.name);
                        zapytanie.Parameters.AddWithValue("@fopath", fo.path);
                        zapytanie.Parameters.AddWithValue("@foold_path", fo.old_path);
                        zapytanie.Parameters.AddWithValue("@projekt_id", projekt_id);

                        zapytanie.ExecuteNonQuery();

                        if (string.IsNullOrEmpty(sql2) == false)
                        {
                            zapytanie.CommandText = sql2;
                            zapytanie.Parameters.AddWithValue("@forew", fo.rew);
                            zapytanie.Parameters.AddWithValue("@fopath", fo.path);
                            zapytanie.Parameters.AddWithValue("@foold_path", fo.old_path);
                            zapytanie.Parameters.AddWithValue("@projekt_id", projekt_id);
                            zapytanie.ExecuteNonQuery();    
                        }
                    }
                }

                zapytanie.CommandText = "UPDATE Projekty SET rewizja=@rew WHERE id=@projekt_id ;";
                zapytanie.Parameters.AddWithValue("@projekt_id", projekt_id);
                zapytanie.Parameters.AddWithValue("@rew", rew);
                zapytanie.ExecuteNonQuery();

                akt_rewizja = Convert.ToInt32(rew);
                Session["akt_rewizja"] = akt_rewizja;
                rew_Label.Text = "Numer aktualnej rewizji: " + akt_rewizja;

                trans.Commit();
            }
            catch 
            {
                trans.Rollback();
            }
        }

        protected void GridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int no = 0;
            if (e.CommandName.CompareTo("Plik") == 0)
            {
                no = Convert.ToInt32(e.CommandArgument);
                Session["id_pliku"] = no;
                Session["back"] = "Pliki.aspx";
                Server.Transfer("Plik.aspx");
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sel_id = GridView1.SelectedRow.Cells[0].Text;
            int id_pliku = Convert.ToInt32(sel_id);
            Session["id_pliku"] = id_pliku;

            Server.Transfer("Plik.aspx");
        }

        protected void powrot_Button_Click(object sender, EventArgs e)
        {
            if (metodyka.CompareTo("Scrum") == 0)
            {
                Server.Transfer("SzczegolyProjektu.aspx?id=" + projekt_id);
            }
            else
            {
                Server.Transfer("ProjektXP.aspx?id=" + projekt_id);
            }
        }

        // Usunięcie z listy modyfikacji poleceń usunięcią wskazujących na pliki, które zmieniły nazwę i wywodzą się z innego pliku
        protected void usun_nadmiarowe( List<FileOp> list )
        {
            List<FileOp> do_usuniecia = new List<FileOp>();

            foreach (FileOp fo in list)
            {
                if (fo.op.CompareTo("D") == 0)
                {
                    foreach (FileOp fo2 in list)
                    {
                        if (fo2.op.CompareTo("A") == 0 && string.IsNullOrEmpty(fo2.old_path) == false)
                        {
                            if (fo2.old_path.CompareTo(fo.path) == 0) 
                            {
                                do_usuniecia.Add(fo);
                            }
                        }
                    }
                }
            }

            foreach (FileOp fo in do_usuniecia)
            {
                list.Remove(fo);
            }
        }
    }
}