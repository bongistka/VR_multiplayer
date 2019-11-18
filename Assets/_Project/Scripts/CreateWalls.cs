using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

public class CreateWalls : MonoBehaviour
{
    SteamVR_LaserPointer laserPointer;
    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject wallPrefab;
    bool creating;
    bool isEnabled;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
            GetInput();
    }

    private void GetInput()
    {
        if (SteamVR_Actions._default.GrabPinch.GetStateDown(SteamVR_Input_Sources.Any))
        {
            SetStart();
        } else if (SteamVR_Actions._default.GrabPinch.GetStateUp(SteamVR_Input_Sources.Any))
        {
            SetEnd();
        } else
        {
            if (creating)
            {
                Adjust();
            }
        }
    }

    private void Adjust()
    {
        endPoint.transform.position = GetPointerPosition();
    }

    private void SetEnd()
    {
        creating = false;
        endPoint.transform.position = GetPointerPosition();
    }

    private void SetStart()
    {
        creating = true;
        startPoint.transform.position = GetPointerPosition();
    }

    public void AddLaserPointer(Hand hand)
    {
        laserPointer = hand.GetComponent<SteamVR_LaserPointer>();
        laserPointer.enabled = true;
        isEnabled = true;
    }

    public Vector3 GetPointerPosition()
    {
        return laserPointer.pointerHit;
    }
}
