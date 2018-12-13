using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;

namespace PruebaTecnicaUruIT
{
    public partial class Index : System.Web.UI.Page
    {
        SqlConnection SqlCon = new SqlConnection(@"Data Source=.;Initial Catalog=dbPruebaTecnica;Integrated Security=True;MultipleActiveResultSets=True;");
        //TableRow row = new TableRow();
        Dictionary<string, string> Rounds = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                HttpContext.Current.Session["idP1"] = 0;
                HttpContext.Current.Session["idP2"] = 0;
                HttpContext.Current.Session["round"] = 1;
                HttpContext.Current.Session["moveP1"] = 0;
                HttpContext.Current.Session["moveP2"] = 0;
                HttpContext.Current.Session["winP1"] = 0;
                HttpContext.Current.Session["winP2"] = 0;
                HttpContext.Current.Session["currentPlayer"] = 1;
                HttpContext.Current.Session["roundList"] = Rounds;
                FillGridView();
                ResetGame();                
            }
            


        }

        public void FillGridView()
        {
            try
            {
                if (SqlCon.State == ConnectionState.Closed)
                {
                    SqlCon.Open();
                    SqlDataAdapter SqlDa = new SqlDataAdapter("GetWinners", SqlCon);
                    SqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                    DataTable Datatbl = new DataTable();
                    SqlDa.Fill(Datatbl);
                    SqlCon.Close();
                    gvLastWinners.DataSource = Datatbl;
                    gvLastWinners.DataBind();
                }

            }
            catch (Exception ex)
            {

                lblMessage.Text = ex.Message;
                GenerateLog(this, ex);

            }
        }

        public void ResetGame()
        {
            divStart.Visible = true;
            divRounds.Visible = false;
            divGrid.Visible = false;
            btnPlayAgain.Visible = false;
            txtPlayer1.Text = string.Empty;
            txtPlayer2.Text = string.Empty;
            iconWinnerTrophy.Visible = false;
            Session["idP1"] = 0;
            Session["idP2"] = 0;
            Session["round"] = 1;
            Session["moveP1"] = 0;
            Session["moveP2"] = 0;
            Session["winP1"] = 0;
            Session["winP2"] = 0;
            Session["currentPlayer"] = 1;
        }

      

        protected void btnRock_Click(object sender, EventArgs e)
        {
           

            if (Session["currentPlayer"].Equals(1))
            {
                Session["moveP1"] = 1;
                Session["currentPlayer"] = 2;
            }
            else if (Session["currentPlayer"].Equals(2))
            {
                Session["moveP2"] = 1;
                Session["currentPlayer"] = 1;
            }

            if (!Session["moveP1"].Equals(0) && !Session["moveP2"].Equals(0))
            {
                DetermineRoundWinner();

            }

            Round();
        }

        protected void btnPaper_Click(object sender, EventArgs e)
        {
            if (Session["currentPlayer"].Equals(1))
            {
                Session["moveP1"] = 2;
                Session["currentPlayer"] = 2;
            }
            else if (Session["currentPlayer"].Equals(2))
            {
                Session["moveP2"] = 2;
                Session["currentPlayer"] = 1;
            }

            if (!Session["moveP1"].Equals(0) && !Session["moveP2"].Equals(0))
                DetermineRoundWinner();

            Round();
        }

        protected void btnScissors_Click(object sender, EventArgs e)
        {
            if (Session["currentPlayer"].Equals(1))
            {
                Session["moveP1"] = 3;
                Session["currentPlayer"] = 2;
            }
            else if (Session["currentPlayer"].Equals(2))
            {
                Session["moveP2"] = 3;
                Session["currentPlayer"] = 1;
            }

            if (!Session["moveP1"].Equals(0) && !Session["moveP2"].Equals(0))
                DetermineRoundWinner();

            Round();
        }

        protected void btnPlayAgain_Click(object sender, EventArgs e)
        {
            ResetGame();
            FillGridView();
        }



        public void GenerateLog(object obj, Exception ex)
        {
            //In this method we generate a .txt file with any error ocurred during the execution of the app
            string date = System.DateTime.Now.ToString("yyyyMMdd");
            string time = System.DateTime.Now.ToString("HH:mm:ss");
            string path = HttpContext.Current.Request.MapPath("~/log/" + date + ".txt");

            StreamWriter sw = new StreamWriter(path, true);

            StackTrace stacktrace = new StackTrace();
            sw.WriteLine(obj.GetType().FullName + " " + time);
            sw.WriteLine(stacktrace.GetFrame(1).GetMethod().Name + " - " + ex.Message);
            sw.WriteLine("");
            sw.Flush();
            sw.Close();
        }

        public void DetermineRoundWinner()
        {
            //Creamos hashmap para guardar las rondas y el winner (1,"Juan")
            //Map<Int, String> Rounds = new HashMap<Int, String>();

            //Comprobamos si la variable de sesion que guarda la lista está vacia
            if (Session["roundList"] != null)
            {
                //Si no está vacia, le guardamos la variable a la lista que acabamos de crear
                Rounds = (Dictionary<String, String>)Session["roundList"];
            }

            //Si la variable está vacia, no hacemos nada, porque igual la lista está vacia también
            
            //TableCell cell1 = new TableCell();
            //TableCell cell2 = new TableCell();
            //cell1.Text = Session["round"].ToString();
            //1Piedra
            //2Papel
            //3Tijeras
            spWinner.InnerText = "";
            if (((Session["moveP1"].Equals(1)) && (Session["moveP2"].Equals(3))) || ((Session["moveP1"].Equals(2)) && (Session["moveP2"].Equals(1))) || ((Session["moveP1"].Equals(3)) && (Session["moveP2"].Equals(2))))
            {
                Session["winP1"] = (int)Session["winP1"] + 1;
                //cell2.Text = txtPlayer1.Text.Trim();
                //Añadimos la nueva ronda a la lista
                Rounds.Add(Session["round"].ToString(), txtPlayer1.Text.Trim());
            }

            else if (((Session["moveP2"].Equals(1)) && (Session["moveP1"].Equals(3))) || ((Session["moveP2"].Equals(2)) && (Session["moveP1"].Equals(1))) || ((Session["moveP2"].Equals(3)) && (Session["moveP1"].Equals(2))))
            {
                Session["winP2"] = (int)Session["winP2"] + 1;
                //cell2.Text = txtPlayer2.Text.Trim();
                Rounds.Add(Session["round"].ToString(), txtPlayer2.Text.Trim());
            }
            else
            {
                //Le asignamos la lista a la variable, para que persista en el tiempo
                //cell2.Text = "Draw";
                Rounds.Add(Session["round"].ToString(), "Draw");
            }

            Session["roundList"] = Rounds;

            gvRoundWinner.DataSource = Rounds;
            gvRoundWinner.DataBind();

            //row.Cells.Add(cell1);
            //row.Cells.Add(cell2);
            //tblWinnerRounds.Rows.Add(row);

            if ((int)Session["winP1"] >= 3 || (int)Session["winP2"] >= 3)
            {
                PrintWinner();
            }
            
            

            Session["round"] = (int)Session["round"] + 1;
            Session["moveP1"] = 0;
            Session["moveP2"] = 0;
            Round();
        }

        public void PrintWinner()
        {
            try
            {
                if (SqlCon.State == ConnectionState.Closed)
                {
                    SqlCon.Open();
                    SqlCommand SqlCmd = new SqlCommand("InsertWinner", SqlCon);
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    if ((int)Session["winP1"] > (int)Session["winP2"])
                    {
                        spWinner.InnerText = "The Emperor is: " + txtPlayer1.Text;


                        SqlCmd.Parameters.AddWithValue("@PlayerID", Session["idP1"]);
                        SqlCmd.ExecuteNonQuery();
                        SqlCon.Close();

                    } else if ((int)Session["winP2"] > (int)Session["winP1"])
                    {
                        spWinner.InnerText = "The Emperor is: " + txtPlayer2.Text;
                        SqlCmd.Parameters.AddWithValue("@PlayerID", Session["idP2"]);
                        SqlCmd.ExecuteNonQuery();
                        SqlCon.Close();
                    }
                    
                }

                btnPlayAgain.Visible = true;
                divRounds.Visible = false;
                iconWinnerTrophy.Visible = true;

            }
            catch (Exception ex)
            {

                lblMessage.Text=ex.Message;
                GenerateLog(this, ex);
            }
            

            //End game
        }

        public void Round()
        {
            Console.WriteLine(Session["round"].ToString());
            spRound.Text = " " + Session["round"].ToString();

            if (Session["currentPlayer"].Equals(1))
            {                
                spPlayer.InnerText = txtPlayer1.Text + " Plase select your move";
            }
            else if (Session["currentPlayer"].Equals(2))
            {                
                spPlayer.InnerText = txtPlayer2.Text + " Plase select your move";
            }
        }


        protected void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtPlayer1.Text.Trim() == "" || this.txtPlayer2.Text.Trim() == "")
                {
                    lblMessage.Text = "Por favor llene todos los campos";
                    return;
                }

                if (SqlCon.State == ConnectionState.Closed)
                {
                    SqlCon.Open();
                    SqlCommand SqlCmd = new SqlCommand("SavePlayer", SqlCon);
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlCmd.Parameters.AddWithValue("@NamePlayer", txtPlayer1.Text.Trim());
                    //SqlCmd.ExecuteNonQuery();
                    Session["idP1"] = Convert.ToInt32(SqlCmd.ExecuteScalar());

                    SqlCmd = new SqlCommand("SavePlayer", SqlCon);
                    SqlCmd.CommandType = CommandType.StoredProcedure;
                    SqlCmd.Parameters.AddWithValue("@NamePlayer", txtPlayer2.Text.Trim());
                    //SqlCmd.ExecuteNonQuery();
                    Session["idP2"] = Convert.ToInt32(SqlCmd.ExecuteScalar());
                    SqlCon.Close();

                    divStart.Visible = false;
                    divRounds.Visible = true;
                    divGrid.Visible = true;


                    Round();
                }

            }
            catch (Exception ex)
            {

                lblMessage.Text = ex.Message;
                GenerateLog(this, ex);

            }
        }


    }

}