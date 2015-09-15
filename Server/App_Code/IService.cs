using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// Yoni Maymon 300979333
// Matan Shulman 300735883

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
 [ServiceContract(CallbackContract = typeof(ICallBack), SessionMode = SessionMode.Required)]

public interface IService
{
    [OperationContract(IsOneWay = false)]
    List<Champpion> getChampList();
   
     [OperationContract(IsOneWay = false)]
    int regPlayer(string FirstName, string LastName, bool tick,byte[] byteArr);

     [OperationContract(IsOneWay = false)]
     string regChampionship(List<int> list,int playerId);

     [OperationContract(IsOneWay = false)]
     List<Champpion> getChampionshipByPlayerId(int playerId);

     [OperationContract(IsOneWay = false)]
     string addNewChampionship(string name, string location, DateTime date,byte[] picture, int playerId);

     [OperationContract(IsOneWay = false)]
     List<Players> getAdvisors(int playerId);
     
     [OperationContract(IsOneWay = true)]
     void regAdvisors(List<int> list,int playerId);

     [OperationContract(IsOneWay = false)]
     List<Players> getAllPlayers();

     [OperationContract(IsOneWay = false)]
     string getPlayerName(int playerId);
     
     [OperationContract(IsOneWay = true)]
     void regToServer(int playerId);

     [OperationContract(IsOneWay = false)]
     List<Players> getAviableOnlinePlayers(int playerId);

     [OperationContract(IsOneWay = true)]
     void sendMsgToRival(int playerId,int rivalId, string playerName, string gameType);

     [OperationContract(IsOneWay = true)]
     void acceptAnswerFromRival(int playerId, bool ans, int rivalId, string gameType);
    
     [OperationContract(IsOneWay = true)]
     void myMove(int myId, int x ,int rivalId,string type, bool win, int shape, int moveNumber,int gameId);

     [OperationContract(IsOneWay = true)]
     void logOut(int playerId);

     [OperationContract(IsOneWay = true)]
     void setPlayerOnline(int playerId);

     [OperationContract(IsOneWay = true)]
     void forfitGame(int rivalId, int gameId);

     [OperationContract(IsOneWay = false)]
     List<MyGames> getPlayerGames(int playerId);
    
     [OperationContract(IsOneWay = false)]
     int[] getMovesGameByGameId(int gameId);
     [OperationContract(IsOneWay = false)]
     List<Players> wpfGetAllPlayers();
     [OperationContract(IsOneWay = true)]
     void updatePlayerInfo(string first, string last, int playerId,byte[] picture);
     [OperationContract(IsOneWay = true)]
     void deletePlayer(int playerId);
     [OperationContract(IsOneWay = false)]
     List<MyGames> wpfGetAllGames();
     [OperationContract(IsOneWay = false)]
     List<MyGames> wpfGetGamesByPlayer(int playerId);
     [OperationContract(IsOneWay = false)]
     List<Champpion> wpfGetChampListByPlayer(int playerId);

     [OperationContract(IsOneWay = false)]
     List<Players> wpfGetPlayerByGames(int gameId);

     [OperationContract(IsOneWay = false)]
     List<Players> wpfGetAdvisorsByGame(int gameId);
     [OperationContract(IsOneWay = false)]
     List<Players> wpfGetPlayersByChamp(int champId);
    
     [OperationContract(IsOneWay = false)]
     List<Players> wpfGetAmountOfGames();

     [OperationContract(IsOneWay = false)]
     List<cities> wpfGetAmountOfCities();

     [OperationContract(IsOneWay = false)]
     bool deletChampByValue(string value, string index, int playerId);

     [OperationContract(IsOneWay = true)]
     void updateChamp(List<Champpion> list, int playerId);
   
     



}

//return list of champions to data grid 
public interface ICallBack
{
  //  [OperationContract(IsOneWay = true)]
   // void returnChanpionship(IEnumerable<Champpion> list );

    [OperationContract(IsOneWay = true)]
    void invitePlayer(string playerName, string gameType, int playerId, int rivalId);
    [OperationContract(IsOneWay = true)]
    void answerFromRiavl(bool answer, int gameId);
    [OperationContract(IsOneWay = true)]
    void moveFromRival(int x ,string type, bool win);
    [OperationContract(IsOneWay = true)]
    void setGameId(int gameId);
    [OperationContract(IsOneWay = true)]
    void rivalForfit();
    [OperationContract(IsOneWay = true)]
    void ErrorCallBack(string str, int id);


    

}



[DataContract]
public class Champpion
{
    

    // Apply the DataMemberAttribute to the property.
    [DataMember]
    public int id { get; set; }
     [DataMember]
    public string name { get; set; }
     [DataMember]
    public string location { get; set; }
    [DataMember]
     public DateTime date { get; set; }
    [DataMember]
    public Bitmap picture { get; set; }
    [DataMember]
    public int CreatedBy { get; set; }

}

[DataContract]
public class Players
{
    // Apply the DataMemberAttribute to the property.
    [DataMember]
    public int playerId { get; set; }
    [DataMember]
    public string firstName { get; set; }
    [DataMember]
    public string lastName { get; set; }
    [DataMember]
    public  Bitmap pictureArrByte { get; set; }
    [DataMember]
    public string advisorTo { get; set; }
    [DataMember]
    public int amount { get; set; }



   
}

[DataContract]
public class MyGames
{
    // Apply the DataMemberAttribute to the property.
    [DataMember]
    public string firstPlayer { get; set; }
    [DataMember]
    public string secondPlayer { get; set; }
    [DataMember]
    public int gameId { get; set; }
    [DataMember]
    public string gameMode { get; set; }
    [DataMember]
    public string winner { get; set; }

   
}

[DataContract]
public class cities
{
    // Apply the DataMemberAttribute to the property.
    [DataMember]
    public string name { get; set; }
    [DataMember]
    public int amount { get; set; }



}
