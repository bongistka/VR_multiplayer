using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddIconToUI : MonoBehaviour
{
    public GameObject[] objectsInScene;
    public Sprite[] textures;
    public string[] names;
    
    // Start is called before the first frame update
    void Start()
    {
        LoadObjects();
        LoadAllIcons();
        DisplayIcons();
    }

    private void LoadObjects()
    {
        UnityEngine.Object[] assets = Resources.LoadAll("Interactable");
        objectsInScene = new GameObject[assets.Length];
        for (int i = 0; i < objectsInScene.Length; i++)
            objectsInScene[i] = assets[i] as GameObject;
    }

    private void LoadAllIcons()
    {
        UnityEngine.Object[] assets = Resources.LoadAll("InteractableIcons", typeof(Texture));
        textures = new Sprite[assets.Length];
        names = new string[assets.Length];
        for (int i = 0; i < assets.Length; i++)
        {
            Texture2D texture = (Texture2D)assets[i];
            textures[i] = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            names[i] = assets[i].name;
        }       
    }

    private void DisplayIcons()
    {
        for (int i = 0; i < textures.Length; i++)
        {
            Debug.Log("Iterating over " + i + "th element");
            this.transform.GetChild(i*3).gameObject.SetActive(true);
            this.transform.GetChild(i*3).GetComponent<Text>().text = names[i];
            this.transform.GetChild(i*3 + 1).gameObject.SetActive(true);
            this.transform.GetChild(i*3 + 1).GetComponent<Image>().sprite = textures[i];
            this.transform.GetChild(i * 3 + 2).gameObject.SetActive(true);
            this.transform.GetChild(i * 3 + 2).GetComponent<AddInteractable>().prefab = objectsInScene[i];
        }
    }
}
