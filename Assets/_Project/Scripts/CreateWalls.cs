﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

public class CreateWalls : MonoBehaviour
{
    PlayerInteractable pi;

    SteamVR_LaserPointer laserPointer;
    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject wallPrefab;

    GameObject wall;

    bool creating;
    bool laserInitialized;
    // todo: use add interactable to add all objects!!!

    // Start is called before the first frame update
    void Start()
    {
        pi = GameObject.FindGameObjectWithTag("PlayerInteractable").GetComponent<PlayerInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        if(laserInitialized)
            if (laserPointer.isActive)
                GetInput();
    }

    private void GetInput()
    {
        if (SteamVR_Actions._default.GrabPinch.GetStateDown(SteamVR_Input_Sources.Any) && (GetPointerPosition() != Vector3.zero))
        {
            SetStart();
        } else if (SteamVR_Actions._default.GrabPinch.GetStateUp(SteamVR_Input_Sources.Any))
        {
            SetEnd();
        } else
        {
            if (creating && (GetPointerPosition() != Vector3.zero))
            {
                Adjust();
            }
        }
    }

    private void Adjust()
    {
        endPoint.transform.position = GetPointerPosition();
        AdjustWall();
    }

    private void AdjustWall()
    {
        startPoint.transform.LookAt(endPoint.transform.position);
        endPoint.transform.LookAt(startPoint.transform.position);
        float distance = Vector3.Distance(startPoint.transform.position, endPoint.transform.position);
        wall.transform.position = startPoint.transform.position + distance / 2 * startPoint.transform.forward;
        wall.transform.rotation = startPoint.transform.rotation;
        wall.transform.localScale = new Vector3(wall.transform.localScale.x, wall.transform.localScale.y, distance);
    }

    private void SetEnd()
    {
        creating = false;
        if (GetPointerPosition() != Vector3.zero)
            endPoint.transform.position = GetPointerPosition();
    }

    private void SetStart()
    {
        creating = true;
        startPoint.transform.position = GetPointerPosition();
        wall = Instantiate(wallPrefab, startPoint.transform.position, Quaternion.identity) as GameObject;
        wall.name = wallPrefab.name + "_" + AddInteractable.GetNextObjectID(pi, wallPrefab);
        PlayerInteractable.AssetInScene go = new PlayerInteractable.AssetInScene(wall.gameObject.name, wall.transform.localScale, wall.transform.position, wall.transform.rotation);
        pi.assetsInScene.listOfAssets.Add(go);
    }

    public void AddLaserPointer(Hand hand)
    {
        laserPointer = hand.GetComponent<SteamVR_LaserPointer>();
        laserPointer.enabled = true;
        laserInitialized = true;
    }

    public Vector3 GetPointerPosition()
    {
        return laserPointer.pointerHit;
    }
}