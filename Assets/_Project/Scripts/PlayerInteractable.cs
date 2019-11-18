using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerInteractable : MonoBehaviour
{
    [Serializable]
    public class AssetInScene
    {
        public string name;
        public Vector3 scale;
        public Vector3 position;
        public Quaternion rotation;

        public AssetInScene(string name, Vector3 scale, Vector3 position, Quaternion rotation)
        {
            this.name = name;
            this.scale = scale;
            this.position = position;
            this.rotation = rotation;
        }
    }
    public string json;

    [Serializable]
    public class AssetsInScene
    {
        public List<AssetInScene> listOfAssets;

        public AssetsInScene()
        {
            listOfAssets = new List<AssetInScene>();
        }
    }

    public AssetsInScene assetsInScene;

    //public List<AssetInScene> assetsInScene = new List<AssetInScene>();
    public string saveGamePath;
    public string fileName;

    // Start is called before the first frame update
    void Start()
    {
        assetsInScene = new AssetsInScene();
        UpdateAssetsPosition();
        LoadAssetsFromFile();
        ApplyAssets();
    }

    private void ApplyAssets()
    {
        foreach (AssetInScene asset in assetsInScene.listOfAssets)
        {
            GameObject go = GameObject.Find(asset.name);
            go.transform.localScale = asset.scale;
            go.transform.position = asset.position;
            go.transform.rotation = asset.rotation;
        }
    }

    private void LoadAssetsFromFile()
    {
        var jsonString = File.ReadAllText(saveGamePath + "/" + fileName + ".json");
        JsonUtility.FromJsonOverwrite(jsonString, assetsInScene);
    }

    private void UpdateAssetsPosition()
    {
        foreach (GameObject child in GameObject.FindGameObjectsWithTag("Asset"))
        {
            foreach (AssetInScene asset in assetsInScene.listOfAssets)
            {
                if (asset.name == child.name)
                {
                    asset.scale = child.transform.localScale;
                    asset.position = child.transform.position;
                    asset.rotation = child.transform.rotation;
                    json = JsonUtility.ToJson(assetsInScene);
                    return;
                }
            } 
            AssetInScene go = new AssetInScene(child.gameObject.name, child.transform.localScale, child.transform.position, child.transform.rotation);
            assetsInScene.listOfAssets.Add(go);
        }
        json = JsonUtility.ToJson(assetsInScene);
    }

    private void OnApplicationQuit()
    {
        UpdateAssetsPosition();
        SaveAssetsInScene();
        Debug.Log("Saving assets and quitting");
    }

    private void SaveAssetsInScene()
    {
        File.WriteAllText(saveGamePath + "/" + fileName + ".json", json);
    }
}
