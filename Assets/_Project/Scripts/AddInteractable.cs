using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddInteractable : MonoBehaviour
{
    public GameObject prefab;

    public void InstantiatePrefab()
    {
        GameObject.Instantiate(prefab, new Vector3(0, 2, 0), Quaternion.identity);
    }
}
