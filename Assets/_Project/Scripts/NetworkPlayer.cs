using UnityEngine;
using System.Collections;
using Photon.Pun;

// For use with Photon and SteamVR
public class NetworkPlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    #region IPunObservable implementation


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(playerGlobal.position);
            stream.SendNext(playerGlobal.rotation);
            stream.SendNext(playerLocal.localPosition);
            stream.SendNext(playerLocal.localRotation);
        }
        else
        {
            this.transform.position = (Vector3)stream.ReceiveNext();
            this.transform.rotation = (Quaternion)stream.ReceiveNext();
            avatar.transform.localPosition = (Vector3)stream.ReceiveNext();
            avatar.transform.localRotation = (Quaternion)stream.ReceiveNext();
        }
    }


    #endregion

    public GameObject avatar;

    public Transform playerGlobal;
    public Transform playerLocal;

    void Start()
    {
        Debug.Log("Player instantiated");

        if (photonView.IsMine)
        {
            Debug.Log("Player is mine");

            playerGlobal = GameObject.Find("Player").transform;
            playerLocal = GameObject.Find("FollowHead").transform;

            this.transform.SetParent(playerLocal);
            this.transform.localPosition = Vector3.zero;
        }
    }
}