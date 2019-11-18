using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Photon.Pun;

public class OwnershipTransfer : MonoBehaviourPun
{
    private void OnHandHoverBegin(Hand hand)
    {
        base.photonView.RequestOwnership();
    }
}
