using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddInteractable : MonoBehaviour
{
    public GameObject prefab;
    PlayerInteractable pi;

    void Start()
    {
        pi = GameObject.FindGameObjectWithTag("PlayerInteractable").GetComponent<PlayerInteractable>(); 
    }

    public void InstantiatePrefab()
    {
        GameObject objectClone = PhotonNetwork.Instantiate("Interactable/" + prefab.name, new Vector3(0, 2, 0), Quaternion.identity);
        objectClone.name = prefab.gameObject.name + "_" + GetNextObjectID(pi, prefab);
        PlayerInteractable.AssetInScene go = new PlayerInteractable.AssetInScene(objectClone.gameObject.name, objectClone.transform.localScale, objectClone.transform.position, objectClone.transform.rotation);
        pi.assetsInScene.listOfAssets.Add(go);
        pi.UpdateAssetsPosition();
    }

    public static int GetNextObjectID(PlayerInteractable pi, GameObject prefab)
    {
        int ID = 0;
        foreach (PlayerInteractable.AssetInScene asset in pi.assetsInScene.listOfAssets)
        {
            if (asset.name.Split('_')[0] == prefab.name)
            {
                if (asset.name.Split('_').Length>1)
                {
                    ID = Mathf.Max(int.Parse(asset.name.Split('_')[1]), ID);
                }
            }
        }
        ID++;
        return ID;
    }
}
