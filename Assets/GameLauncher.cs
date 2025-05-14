
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

internal class GameLauncher : MonoBehaviourPunCallbacks
{
    byte maxPlayerPerRoom = 2; //liczba graczy w pokoju
    bool isConnecting; //czy istnieje połączenie
    public TMP_Text networkText; //nasz network text
    string gameVersion = "0.1"; //wersja gry

    public override void OnConnectedToMaster()
    {
        if (isConnecting)
        {
            networkText.text += "OnConnectToMaster...\n";
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        networkText.text += "Disconnected because " + cause + "\n";
        isConnecting = false;
    }

    public override void OnJoinedRoom()
    {
        networkText.text = "Joined Room with " + PhotonNetwork.CurrentRoom.PlayerCount + "players.\n";
        //Uruchomienie sceny z grą
        PhotonNetwork.LoadLevel("PongOnline");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        networkText.text += "Failed to join random room. \n";
        //Stworzenie nowego pokoju
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = this.maxPlayerPerRoom });
    }

    public void ConnectNetwork()
    {
        if (isConnecting) return;


        networkText.text = "";
        isConnecting = true;
        if (PhotonNetwork.IsConnected)
        {
            networkText.text += "Joining Room...\n";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            networkText.text += "Connecting...\n";
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void PlayOffline()
    {
        SceneManager.LoadScene("PongOffline");
    }
}

