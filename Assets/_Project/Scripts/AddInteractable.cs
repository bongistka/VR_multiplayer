using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddInteractable : MonoBehaviour
{
    public GameObject prefab;
    PlayerInteractable pi;
    int objectID = 1;

    void Start()
    {
        pi = GameObject.FindGameObjectWithTag("PlayerInteractable").GetComponent<PlayerInteractable>(); 
    }

    public void InstantiatePrefab()
    {
        GameObject objectClone = GameObject.Instantiate(prefab, new Vector3(0, 2, 0), Quaternion.identity);
        objectClone.name = prefab.gameObject.name + "_" + objectID;
        PlayerInteractable.AssetInScene go = new PlayerInteractable.AssetInScene(objectClone.gameObject.name, objectClone.transform.localScale, objectClone.transform.position, objectClone.transform.rotation);
        pi.assetsInScene.listOfAssets.Add(go);
        objectID++;
        pi.UpdateAssetsPosition();
    }
}
