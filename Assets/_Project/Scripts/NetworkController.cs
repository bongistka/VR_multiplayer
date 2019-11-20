using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class NetworkController : MonoBehaviourPunCallbacks
{
    string _room = "VR_starter";
    public bool mockHMD;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("joined lobby");

        RoomOptions roomOptions = new RoomOptions() { };
        PhotonNetwork.JoinOrCreateRoom(_room, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("VR_multiplayer");
        //PhotonNetwork.Instantiate("NetworkPlayer", Vector3.zero, Quaternion.identity, 0);
    }

    public void ConnectToPhotonServer()
    {
        if (!PhotonNetwork.IsConnected)
        {
            // Connects us to the Photon server using settings in 
            // Photon /PhotonUnityNetworking/Resources/PhotonServerSettings.
            if (mockHMD)
            {
                UnityEngine.XR.XRSettings.LoadDeviceByName("None");
            }
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("connected");
        }

    }
}