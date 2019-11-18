using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddInteractable : MonoBehaviour
{
    public GameObject prefab;
    PlayerInteractable pi;
    int objectID = 0;

    void Start()
    {
        pi = GameObject.FindGameObjectWithTag("PlayerInteractable").GetComponent<PlayerInteractable>(); 
    }

    public void InstantiatePrefab()
    {
        GameObject objectClone = GameObject.Instantiate(prefab, new Vector3(0, 2, 0), Quaternion.identity);
        GetNextObjectID();
        objectClone.name = prefab.gameObject.name + "_" + objectID;
        PlayerInteractable.AssetInScene go = new PlayerInteractable.AssetInScene(objectClone.gameObject.name, objectClone.transform.localScale, objectClone.transform.position, objectClone.transform.rotation);
        pi.assetsInScene.listOfAssets.Add(go);
        pi.UpdateAssetsPosition();
    }

    private void GetNextObjectID()
    {
        foreach (PlayerInteractable.AssetInScene asset in pi.assetsInScene.listOfAssets)
        {
            if (asset.name.Split('_')[0] == prefab.name)
            {
                if (asset.name.Split('_').Length>1)
                {
                    objectID = Mathf.Max(int.Parse(asset.name.Split('_')[1]), objectID);
                }
            }
        }
        objectID++;
    }
}
