using UnityEngine;
using System.Collections;
using Photon.Pun;
using Photon.Realtime;

public class NetworkController : MonoBehaviourPunCallbacks
{
    string _room = "VR_starter";
    public bool mockHMD;

    void Start()
    {
        if (mockHMD)
        {
            UnityEngine.XR.XRSettings.LoadDeviceByName("Mock HMD");
        }
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("connected");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("joined lobby");

        RoomOptions roomOptions = new RoomOptions() { };
        PhotonNetwork.JoinOrCreateRoom(_room, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.Instantiate("NetworkPlayer", Vector3.zero, Quaternion.identity, 0);
    }
}