using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
public class Service : IService
{
    private DataClassesDataContext db = new DataClassesDataContext();
    private static Dictionary<int,ICallBack> chanels = new Dictionary<int,ICallBack>();
    private static Dictionary<int, bool> aviablePlayers = new Dictionary<int, bool>();

    public int tempBoardSize;
    public  int[] boardGame3x3 = new int[9];
    public  int[] boardGame4x4 = new int[16];
    public  int[] boardGame5x5 = new int[25];

    public List<Champpion> getChampList()
    {
       
        IEnumerable<Championship> x = from c in db.Championships
                                      select c; 
        List<Champpion> lst = new List<Champpion>();
        foreach (var item in x)
        {
            Bitmap bm;
            Image imageIn;
            byte[] test;
            if (item.Picture != null && item.Picture.Length > 10)
            {
                test = (byte[])item.Picture.ToArray();
                MemoryStream ms = new MemoryStream(test);
                imageIn = Image.FromStream(ms);
                bm = new Bitmap(imageIn);
            }
            else
                bm = loadDefualtPic();
            lst.Add(new Champpion { id = item.Id, name = item.Name, location = item.Location, date = item.Date, picture = bm, CreatedBy = item.CreatedBy});
        }
        
        return lst;
    }

    public int regPlayer(string FirstName, string LastName, bool isAdviser,byte[] byteArr)
    {
        int playerId = -1;
       
        using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
        {
            string sql;
            SqlCommand cmd;
            if (byteArr != null)
            {
                sql = string.Format("insert into Players(First_Name, Last_Name,Is_Advisor,picture) values ('{0}','{1}','{2}',@pic)", FirstName, LastName, isAdviser);
                cmd = new SqlCommand(sql, con);
                SqlParameter picParam = cmd.Parameters.Add("@pic", SqlDbType.VarBinary);
                picParam.Value = byteArr;
            }
            else
            {
                sql = string.Format("insert into Players(First_Name, Last_Name,Is_Advisor) values ('{0}','{1}','{2}')", FirstName, LastName, isAdviser);
                cmd = new SqlCommand(sql, con);

            }

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception error)
            {
             // return playerId;
            }

            var x = (from c in db.Players
                     orderby c.Player_Id descending
                     select c).First();
            playerId = x.Player_Id;
            return playerId;
        }
    }

    public string regChampionship(List<int> list, int playerId)
    {
        using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
        {
            try
            {
                con.Open();
                foreach (var champId in list)
                {
                    string sql = string.Format("insert into PlayerChampionship(IdPlayer, IdChamp) values ('{0}','{1}')", playerId, champId);
                 
                    SqlCommand cmd = new SqlCommand(sql, con);
                  
                    cmd.ExecuteNonQuery();
                }
                con.Close();
            }

            catch (Exception error)
            {

                return error.Message;
            }

            return "SUCCESS";
        }
    }

    public List<Champpion> getChampionshipByPlayerId(int playerId)
    {
        IEnumerable<Championship> x = from c in db.Championships
                                      join pl in db.PlayerChampionships on c.Id equals pl.IdChamp
                                      where pl.IdPlayer == playerId
                                      select c; // new Champpion { id = c.Id, name = c.Name, location = c.Location };
        List<Champpion> lst = new List<Champpion>();
        foreach (var item in x)
        {
            Bitmap bm;
            Image imageIn;
            byte[] test;
            if (item.Picture != null && item.Picture.Length > 10)
            {
                test = (byte[])item.Picture.ToArray();
                MemoryStream ms = new MemoryStream(test);
                imageIn = Image.FromStream(ms);
                bm = new Bitmap(imageIn);
            }
            else
                bm = loadDefualtPic();
            lst.Add(new Champpion { id = item.Id, name = item.Name, location = item.Location, date = (DateTime)item.Date,picture=bm });
        }
        //  channel.returnChanpionship(lst);
        return lst;
    }

    public string addNewChampionship(string name, string location, DateTime date, byte[] picture, int playerId)
    {
        using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
        {
            string sql;
            SqlCommand cmd;
       

                if (picture != null)
                {
                     sql = string.Format("insert into Championship(Name, Location, Date,picture,CreatedBy) values ('{0}','{1}','{2}',@pic,'{3}')", name, location, date.Date.ToString("MMddyyyy"), playerId);
                     cmd = new SqlCommand(sql, con);
                    SqlParameter picParam = cmd.Parameters.Add("@pic", SqlDbType.VarBinary);
                    picParam.Value = picture;
                }
                else
                {
                    sql = string.Format("insert into Championship(Name, Location, Date, CreatedBy) values ('{0}','{1}','{2}','{3}')", name, location, date.Date, playerId);
                    cmd = new SqlCommand(sql, con);
                }
            
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            catch (Exception error)
            {

                return error.Message;
            }

            return "SUCCESS";
        }
    }

    public List<Players> getAdvisors(int playerId)
    {
        bool flag = true;
        IEnumerable<Player> x = from c in db.Players
                                where c.Is_Advisor == true && c.Is_Availbale == true && c.Player_Id != playerId && c.Is_Active == true
                                select c;

        List<Players> lst = new List<Players>();

        foreach (var item in x)
        {
            Bitmap bm;
            Image imageIn;
            byte[] test;
            if (item.picture != null && item.picture.Length > 10)
            {
                test = (byte[])item.picture.ToArray();
                MemoryStream ms = new MemoryStream(test);
                imageIn = Image.FromStream(ms);
                bm = new Bitmap(imageIn);
            }
            else
            {

                bm = loadDefualtPic();
            }

            lst.Add(new Players { playerId = item.Player_Id, firstName = item.First_Name, lastName = item.Last_Name, pictureArrByte = bm });
        }

        return lst;
    }

    public void regAdvisors(List<int> list, int playerId)
    {

        using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
        {
            con.Open();
            foreach (var item in list)
            {
                string sql = string.Format("UPDATE Players SET Advisor_To='{0}', Is_Availbale='{1}' WHERE Player_Id='{2}'", playerId, false, item);
                SqlCommand cmd = new SqlCommand(sql, con);
                try
                {
                    
                    cmd.ExecuteNonQuery();
                    
                }
                catch (Exception error)
                {
                    
                }

            }
            con.Close();
        }
    }

    public List<Players> getAllPlayers()
    {
        IEnumerable<Player> x = from p in db.Players
                                where p.Player_Id != 0 && p.Is_Active == true
                                select p;

        List<Players> lst = new List<Players>();

        foreach (var item in x)
        {
            Bitmap bm;
            Image imageIn;
            byte[] test;
            if (item.picture != null && item.picture.Length > 10)
            {
                test = (byte[])item.picture.ToArray();
                MemoryStream ms = new MemoryStream(test);
                imageIn = Image.FromStream(ms);
                bm = new Bitmap(imageIn);
            }
            else
            {

                bm = loadDefualtPic();
            }

            if(!chanels.ContainsKey(item.Player_Id))
                lst.Add(new Players { playerId = item.Player_Id, firstName = item.First_Name, lastName = item.Last_Name, pictureArrByte = bm });
        }

        return lst;
    }

    public string getPlayerName(int playerId)
    {
        string x = (from p in db.Players
                    where p.Player_Id == playerId
                    select p.First_Name).Single().ToString();
        
        return x;
    }

    public void regToServer(int playerId)
    {
        ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();
        chanels.Add(playerId, channel);
        aviablePlayers.Add(playerId, true);
    }

    public List<Players> getAviableOnlinePlayers(int playerId)
    {
        List<Players> lst = new List<Players>();
        foreach (var item in aviablePlayers)
        {

            if (item.Value == true && item.Key != playerId)
            {
                Player x = (from p in db.Players
                            where p.Player_Id == item.Key
                            select p).Single();
             lst.Add(new Players { playerId = x.Player_Id, firstName = x.First_Name, lastName = x.Last_Name });
                
            }
        }
        return lst;
    }

    public void sendMsgToRival(int playerId,int rivalId,string playerName, string gameType)
    {

        if (rivalId != 0)
            if (aviablePlayers.ContainsKey(rivalId) && aviablePlayers[rivalId] == true)
                chanels[rivalId].invitePlayer(playerName, gameType, playerId, rivalId);
            else
                chanels[playerId].answerFromRiavl(false, 0);
        else
        {
            acceptAnswerFromRival(playerId, true, 0, gameType);
            switch (gameType)
            {
                case "Easy":
                    for (int i = 0; i < boardGame3x3.Length; i++)
                        boardGame3x3[i] = 2;
                    tempBoardSize = 9;
                    break;
                case "Medium":
                    for (int i = 0; i < boardGame4x4.Length; i++)
                        boardGame4x4[i] = 2;
                    tempBoardSize = 16;
                    break;
                case "Hard":
                    for (int i = 0; i < boardGame5x5.Length; i++)
                        boardGame5x5[i] = 2;
                    tempBoardSize = 25;
                    break;

            }
        }
       
       
    }
    public void acceptAnswerFromRival(int playerId, bool ans, int rivalId, string gameType)
    {
        int gameId = -1;
        if(ans)
        {
            aviablePlayers[playerId] = false;
            if(rivalId != 0)
                aviablePlayers[rivalId] = false;
            
        //ICallBack channel = OperationContext.Current.GetCallbackChannel<ICallBack>();
              using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
              {

                  string sql = string.Format("INSERT INTO Games(playerIdX, playerIdCircle, gameMode) VALUES ('{0}','{1}','{2}')", playerId, rivalId, gameType);
                  SqlCommand cmd = new SqlCommand(sql, con);
                  try
                  {
                      con.Open();
                      cmd.ExecuteNonQuery();
                      con.Close();

                  }

                  catch (Exception error)
                  {
                      //channel.ErrorCallBack(error.Message,-1);
                     
                  }
              }

              var x = (from g in db.Games
                       orderby g.GameId descending
                       select g).First();
              gameId = x.GameId;

            if(rivalId != 0)
              chanels[rivalId].setGameId(gameId);


        }
        chanels[playerId].answerFromRiavl(ans,gameId);
    }
    public void myMove(int myId, int x, int rivalId, string type, bool win, int shape, int moveNumber, int gameId)
    {

        using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
        {
            string sql;
            SqlCommand cmd;
            try
            {
                con.Open();
            if(win)
            {

                sql = string.Format("UPDATE Games SET winner='{0}' WHERE gameId='{1}' ", myId, gameId);
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

            }
            sql = string.Format("INSERT INTO GameMoves(gameId, moveNumber, shape, place) VALUES ('{0}','{1}','{2}','{3}' )", gameId, moveNumber, shape, x);
            cmd = new SqlCommand(sql, con);
           
                
                cmd.ExecuteNonQuery();
                con.Close();

            
        }

            catch (Exception error)
            {
                //channel.ErrorCallBack(error.Message,-1);

            }
        }
               
        if(rivalId != 0)
            chanels[rivalId].moveFromRival(x, type, win);
        else if (!win && moveNumber < tempBoardSize-1)
            {

                int i = 0;
                int move = 0;
                bool pcWin = false;
                switch (type)
                {
                    case "Easy":
                        boardGame3x3[x] = 1;
                        while ((boardGame3x3[i] == 0 || boardGame3x3[i] == 1) && i < 9)
                        {
                            i++;
                        }
                        boardGame3x3[i] = 0;
                        move = i;
                        pcWin = checkWin3x3();
                        break;
                    case "Medium":
                        boardGame4x4[x] = 1;
                        while (boardGame4x4[i] != 2 && i < 16)
                        {
                            i++;
                        }
                        boardGame4x4[i] = 0;
                        move = i;
                        pcWin = checkWin4x4();
                        break;
                    case "Hard":
                        boardGame5x5[x] = 1;
                        while (boardGame5x5[i] != 2 && i < 25)
                        {
                            i++;
                        }
                        boardGame5x5[i] = 0;
                        move = i;
                        pcWin = checkWin5x5();
                        break;

                }

                myMove(0, move, myId, type, pcWin, 0, ++moveNumber, gameId);
            }




        

      
    
    }

    private bool checkWin5x5()
    {
        for (int i = 0; i < 5; i++)
        {
            if (boardGame5x5[i] == 0 && boardGame5x5[i + 5] == 0 && boardGame5x5[i + 10] == 0 && boardGame5x5[i + 15] == 0 && boardGame5x5[i + 20] == 0)
                return true;
        }
        for (int i = 0; i < 25; i += 5)
        {
            if (boardGame5x5[i] == 0 && boardGame5x5[i + 1] == 0 && boardGame5x5[i + 2] == 0 && boardGame5x5[i + 3] == 0 && boardGame5x5[i + 4] == 0)
                return true;
        }

        if (boardGame5x5[0] == 0 && boardGame5x5[6] == 0 && boardGame5x5[12] == 0 && boardGame5x5[18] == 0 && boardGame5x5[24]==0)
            return true;
        if (boardGame5x5[4] == 0 && boardGame5x5[8] == 0 && boardGame5x5[12] == 0 && boardGame5x5[16] == 0 && boardGame5x5[24] == 0)
            return true;

        return false;
    }

    private bool checkWin3x3()
    {


        for (int i = 0; i < 3; i++)
        {
            if (boardGame3x3[i] == 0 && boardGame3x3[i + 3] == 0 && boardGame3x3[i + 6] == 0)
                return true;
        }
        for (int i = 0; i < 9; i+=3)
        {
            if (boardGame3x3[i] == 0 && boardGame3x3[i + 1] == 0 && boardGame3x3[i + 2] == 0)
                return true;
        }

        if (boardGame3x3[0] == 0 && boardGame3x3[4] == 0 && boardGame3x3[8] == 0)
            return true;
        if (boardGame3x3[2] == 0 && boardGame3x3[4] == 0 && boardGame3x3[6] == 0)
            return true;

        return false;
    }

    private bool checkWin4x4()
    {


        for (int i = 0; i < 4; i++)
        {
            if (boardGame4x4[i] == 0 && boardGame4x4[i + 4] == 0 && boardGame4x4[i + 8] == 0 && boardGame4x4[i + 12] == 0)
                return true;
        }
        for (int i = 0; i < 16; i += 4)
        {
            if (boardGame4x4[i] == 0 && boardGame4x4[i + 1] == 0 && boardGame4x4[i + 2] == 0 && boardGame4x4[i + 3] == 0)
                return true;
        }

        if (boardGame4x4[0] == 0 && boardGame4x4[5] == 0 && boardGame4x4[10] == 0 && boardGame4x4[15] == 0)
            return true;
        if (boardGame4x4[3] == 0 && boardGame4x4[6] == 0 && boardGame4x4[9] == 0 && boardGame4x4[12] == 0)
            return true;

        return false;
    }
    public void logOut(int playerId)
    {
        aviablePlayers.Remove(playerId);
        chanels.Remove(playerId);

    }
    public Bitmap loadDefualtPic()
    {
        FileStream fs = new FileStream(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "/Images/emptyImage.jpg", FileMode.Open, FileAccess.Read);
        BinaryReader br = new BinaryReader(fs);
        MemoryStream ms = new MemoryStream(br.ReadBytes((int)fs.Length));
        Image imageIn = Image.FromStream(ms);
        return new Bitmap(imageIn);
    }
    public void setPlayerOnline(int playerId)
    {
        aviablePlayers[playerId] = true;
    }
    public void forfitGame(int rivalId, int gameId)
    {
         using (SqlConnection con = new SqlConnection(db.Connection.ConnectionString))
         {
             try
             {
                 string sql = string.Format("UPDATE Games SET winner='{0}' WHERE gameId='{1}' ", rivalId, gameId);
                 SqlCommand cmd = new SqlCommand(sql, con);

                 con.Open();
                 cmd.ExecuteNonQuery();
                 con.Close();

             }
             catch (Exception error)
             {
                 //channel.ErrorCallBack(error.Message,-1);

             }
         }

         chanels[rivalId].rivalForfit();



    }
    //Return a list of all games history
    public List<MyGames> getPlayerGames(int playerId)
    {
        IEnumerable<Game> x = from g in db.Games
                                  where g.PlayerIdCircle  == playerId || g.PlayerIdX==playerId
                                  select g; // new Champpion { id = c.Id, name = c.Name, location = c.Location };
       
        List<MyGames> lst = new List<MyGames>();
        foreach (var item in x)
        {
            string player1 = (from p in db.Players
                             where p.Player_Id == item.PlayerIdX
                             select p.First_Name).First().ToString();
            string player2 = (from p in db.Players
                              where p.Player_Id == item.PlayerIdCircle
                              select p.First_Name).First().ToString();
            


            lst.Add(new MyGames {firstPlayer=player1 , secondPlayer = player2, gameId = item.GameId, gameMode = item.GameMode });
        }
        //  channel.returnChanpionship(lst);
        return lst;
    }
    public int[] getMovesGameByGameId(int gameId)
    {
        var x = from i in db.gameMoves
                where i.gameId == gameId
                orderby i.moveNumber ascending
                select i;
        List<int> lst = new List<int>();

        foreach (var item in x)
        {
            lst.Add((int)item.place);
        }
        
        return  lst.ToArray();
    }

    //
    //Wpf Function
    //
    //Return a list of all players
    public List<Players> wpfGetAllPlayers()
    {
        
        IEnumerable<Player> x = from p in db.Players
                                where p.Player_Id != 0 && p.Is_Active ==true
                                select p;

        List<Players> lst = new List<Players>();
        string playerName = "";
        foreach (var item in x)
        {
            Bitmap bm;
            Image imageIn;
            byte[] test;
            if (item.picture != null && item.picture.Length > 10)
            {
                test = (byte[])item.picture.ToArray();
                MemoryStream ms = new MemoryStream(test);
                imageIn = Image.FromStream(ms);
                bm = new Bitmap(imageIn);
            }
            else
                bm = loadDefualtPic();

            if (item.Advisor_To != null)
            {
                playerName = (from p in db.Players
                                     where p.Player_Id == item.Advisor_To
                                     select p.First_Name).First().ToString();
            }

                lst.Add(new Players { playerId = item.Player_Id, firstName = item.First_Name, lastName = item.Last_Name, pictureArrByte = bm, advisorTo = playerName });
        }

        return lst;
    }

    public void updatePlayerInfo(string first, string last, int playerId,byte []  picture)
    {

    var query =
        from p in db.Players
        where p.Player_Id == playerId
        select p;

        // Execute the query, and change the column values 
        foreach (Player pl in query)
        {
            pl.First_Name = first;
            pl.Last_Name = last;
            if (picture != null)
                pl.picture = picture;
        
        }

        // Submit the changes to the database. 
        try
        {
            db.SubmitChanges();
        }
        catch (Exception e)
        {
            chanels[playerId].ErrorCallBack(e.Message, playerId);
        }

        chanels[playerId].ErrorCallBack("success", playerId);

    }


    public void deletePlayer(int playerId)
    {
        var query =
            from p in db.Players
            where p.Player_Id == playerId
            select p;

        query.First().Is_Active = false;
        

        try
        {
            db.SubmitChanges();
        }
        catch (Exception e)
        {
            chanels[playerId].ErrorCallBack(e.Message, playerId);
           
        }
        chanels[playerId].ErrorCallBack("success", playerId);
    }


    public List<MyGames> wpfGetAllGames()
    {
        IEnumerable<Game> x = from g in db.Games 
                              select g; // new Champpion { id = c.Id, name = c.Name, location = c.Location };
        string gameWinner;
        List<MyGames> lst = new List<MyGames>();
        foreach (var item in x)
        {
            string player1 = (from p in db.Players
                              where p.Player_Id == item.PlayerIdX
                              select p.First_Name).First().ToString();
            string player2 = (from p in db.Players
                              where p.Player_Id == item.PlayerIdCircle
                              select p.First_Name).First().ToString();
            if (item.winner == null)
            {
                gameWinner = "Draw";
            }
            else
            {
                if (item.winner == item.PlayerIdX)
                    gameWinner = player1;
                else
                    gameWinner = player2;

            }




            lst.Add(new MyGames { firstPlayer = player1, secondPlayer = player2, gameId = item.GameId, gameMode = item.GameMode, winner = gameWinner });
        }
        
        return lst;
    }


    public List<MyGames> wpfGetGamesByPlayer(int playerId)
    {
        IEnumerable<Game> x = from g in db.Games
                              where g.PlayerIdX == playerId || g.PlayerIdCircle == playerId
                              select g; 
        string gameWinner;
        List<MyGames> lst = new List<MyGames>();
        foreach (var item in x)
        {
            string player1 = (from p in db.Players
                              where p.Player_Id == item.PlayerIdX
                              select p.First_Name).First().ToString();
            string player2 = (from p in db.Players
                              where p.Player_Id == item.PlayerIdCircle
                              select p.First_Name).First().ToString();
            if (item.winner == null)
            {
                gameWinner = "Draw";
            }
            else
            {
                if (item.winner == item.PlayerIdX)
                    gameWinner = player1;
                else
                    gameWinner = player2;

            }

            lst.Add(new MyGames { firstPlayer = player1, secondPlayer = player2, gameId = item.GameId, gameMode = item.GameMode, winner = gameWinner });
        }

        return lst;
    }


    public List<Champpion> wpfGetChampListByPlayer(int playerId)
    {
        var x = 
               from p in db.PlayerChampionships
               from c in db.Championships
               where p.IdPlayer == playerId && p.IdChamp == c.Id
               select c;
                                 
 
       
        List<Champpion> lst = new List<Champpion>();
        foreach (var item in x)
        {
            Bitmap bm;
            Image imageIn;
            byte[] test;
            if (item.Picture != null && item.Picture.Length > 10)
            {
                test = (byte[])item.Picture.ToArray();
                MemoryStream ms = new MemoryStream(test);
                imageIn = Image.FromStream(ms);
                bm = new Bitmap(imageIn);
            }
            else
                bm = loadDefualtPic();
            lst.Add(new Champpion { id = item.Id, name = item.Name, location = item.Location, date = (DateTime)item.Date, picture = bm });
        }
        
        return lst;
    }



    public List<Players> wpfGetPlayerByGames(int gameId)
    {
        var x =            
              from g in db.Games
              from p in db.Players
              where g.GameId == gameId && (g.PlayerIdX == p.Player_Id || g.PlayerIdCircle == p.Player_Id)          
              select p;



        List<Players> lst = new List<Players>();
        string playerName = "";
        foreach (var item in x)
        {
            Bitmap bm;
            Image imageIn;
            byte[] test;
            if (item.picture != null && item.picture.Length > 10)
            {
                test = (byte[])item.picture.ToArray();
                MemoryStream ms = new MemoryStream(test);
                imageIn = Image.FromStream(ms);
                bm = new Bitmap(imageIn);
            }
            else
                bm = loadDefualtPic();

            if (item.Advisor_To != null)
            {
                playerName = (from p in db.Players
                              where p.Player_Id == item.Advisor_To
                              select p.First_Name).First().ToString();
            }

            lst.Add(new Players { playerId = item.Player_Id, firstName = item.First_Name, lastName = item.Last_Name, pictureArrByte = bm, advisorTo = playerName });
        }

        return lst;
    }


    public List<Players> wpfGetAdvisorsByGame(int gameId)
    {
        var x =
               from g in db.Games
               from p in db.Players
               where g.GameId == gameId && (g.PlayerIdX == p.Advisor_To || g.PlayerIdCircle == p.Advisor_To)
               select p;



        List<Players> lst = new List<Players>();
        string playerName = "";
        foreach (var item in x)
        {
            Bitmap bm;
            Image imageIn;
            byte[] test;
            if (item.picture != null && item.picture.Length > 10)
            {
                test = (byte[])item.picture.ToArray();
                MemoryStream ms = new MemoryStream(test);
                imageIn = Image.FromStream(ms);
                bm = new Bitmap(imageIn);
            }
            else
                bm = loadDefualtPic();

            if (item.Advisor_To != null)
            {
                playerName = (from p in db.Players
                              where p.Player_Id == item.Advisor_To
                              select p.First_Name).First().ToString();
            }

            lst.Add(new Players { playerId = item.Player_Id, firstName = item.First_Name, lastName = item.Last_Name, pictureArrByte = bm, advisorTo = playerName });
        }

        return lst;
    }

    public List<Players> wpfGetPlayersByChamp(int champId)
    {
        var x =
               from c in db.PlayerChampionships
               from p in db.Players
               where c.IdChamp == champId && p.Player_Id == c.IdPlayer
               select p;



        List<Players> lst = new List<Players>();
        string playerName = "";
        foreach (var item in x)
        {
            Bitmap bm;
            Image imageIn;
            byte[] test;
            if (item.picture != null && item.picture.Length > 10)
            {
                test = (byte[])item.picture.ToArray();
                MemoryStream ms = new MemoryStream(test);
                imageIn = Image.FromStream(ms);
                bm = new Bitmap(imageIn);
            }
            else
                bm = loadDefualtPic();

            if (item.Advisor_To != null)
            {
                playerName = (from p in db.Players
                              where p.Player_Id == item.Advisor_To
                              select p.First_Name).First().ToString();
            }

            lst.Add(new Players { playerId = item.Player_Id, firstName = item.First_Name, lastName = item.Last_Name, pictureArrByte = bm, advisorTo = playerName });
        }

        return lst;
    }


    public List<Players> wpfGetAmountOfGames()
    {
        var x =         
               from p in db.Players  
               select p;

        List<Players> lst = new List<Players>();
      
        foreach (var item in x)
        {
            var y =
                from g in db.Games
                where g.PlayerIdCircle == item.Player_Id || g.PlayerIdX == item.Player_Id
                select g;        

            lst.Add(new Players { playerId = item.Player_Id, firstName = item.First_Name, lastName = item.Last_Name,  amount = y.Count() });
        }

        return lst;
    }


    public List<cities> wpfGetAmountOfCities()
    {
      
       var x = from c in db.Championships
                group c by c.Location into g
                select new
                {
                    name = g.Key,
                    Count = (from c in g select c.Location).Count() 
                };


        List<cities> lst = new List<cities>();

        foreach (var item in x)
        {
           

            lst.Add(new cities { name = item.name , amount = item.Count});
        }
        lst.RemoveAt(0);

        return lst;
    }


    public bool deletChampByValue(string value, string index, int playerId)
    {

        IQueryable<Championship> x = null;

        switch (index)
        {
            case "Name":
                x = from c in db.Championships
                        where c.Name == value
                        select c;
                break;
            case "Location":
                 x = from c in db.Championships
                        where c.Location == value
                        select c;
                 break;

            case "ID":
                 x = from c in db.Championships
                     where c.Id == Convert.ToInt32(value)
                     select c;
                 break;

        }


        foreach (var champ in x)
        {
            db.Championships.DeleteOnSubmit(champ);
        }

        try
        {
            db.SubmitChanges();
        }
        catch (Exception e)
        {
            chanels[playerId].ErrorCallBack(e.Message, playerId);
        }
       
        return true;
    }


    public void updateChamp(List<Champpion> list, int playerId)
    {
        ImageConverter converter = new ImageConverter();
        foreach (var item in list)
        {
            var query =
                        from c in db.Championships
                        where c.Id == item.id
                        select c;

            foreach (Championship c in query)
            {
                c.Location = item.location;
                c.Name = item.name;                 
                c.Picture = (byte[])(converter.ConvertTo(item.picture, typeof(byte[])));
                c.Date = item.date;         

            }

            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                chanels[playerId].ErrorCallBack(e.Message, playerId);
            }
            chanels[playerId].ErrorCallBack("Success", playerId);
        }
    }
}
