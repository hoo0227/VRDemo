using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class LaserPointer : MonoBehaviour
{
    private SteamVR_Behaviour_Pose pose;
    private SteamVR_Input_Sources hand;
    private LineRenderer line;


    private SteamVR_Action_Boolean trigger;

    //Line Max Distance
    public float maxDistance = 30.0f;

    //Line Color
    public Color color = Color.blue;
    public Color clickedColor = Color.green;


    //Raycast Variables
    private RaycastHit hit;
    private Transform tr;


    void Start()
    {
        tr = GetComponent<Transform>();
        trigger = SteamVR_Actions.default_InteractUI;

        pose = GetComponent<SteamVR_Behaviour_Pose>();
        hand = pose.inputSource;
        CreateLine();

        
    }


    //LineRenderer Create
    void CreateLine()
    {
        line = this.gameObject.AddComponent<LineRenderer>();

        line.useWorldSpace = false;

        //Start, End Position Setting
        line.positionCount = 2;  //Index 0,1
        line.SetPosition(0, Vector3.zero);  //Index 0(0,0,0)
        line.SetPosition(1, new Vector3(0,0, maxDistance));  //Index 1 (0,0,30)


        //Line Width Setting
        line.startWidth = 0.03f;
        line.endWidth = 0.005f;

        //Materials Seetting
        line.material = new Material(Shader.Find("Unlit/Color"));
        line.material.color = this.color;
    }

    void Update()
    {
        if (Physics.Raycast(tr.position, tr.forward, out hit, maxDistance))
        {
            line.SetPosition(1, new Vector3(0,0,hit.distance));
        }

        else 
        {
             line.SetPosition(1, new Vector3(0,0,maxDistance));
        }
    }



}

