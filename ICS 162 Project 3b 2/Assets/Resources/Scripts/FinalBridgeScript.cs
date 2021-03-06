﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class FinalBridgeScript : MonoBehaviour {


    public LeverScript[] levers;
    private bool allLeversReady = false;
    public Bridge b;
    private Transform camParent;
    private Vector3 prevCamPos;
    private Quaternion prevCamRot;
    private bool bridgeFallen = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void OnLeverChange()
    {
        allLeversReady = true;
        foreach(LeverScript l in levers)
        {
            if(l.LeverReady == false)
            {
                allLeversReady = false;
            }
        }
        if (allLeversReady && !bridgeFallen)
        {
            StartCoroutine(DropBridge());
        }
    }

    IEnumerator DropBridge()
    {
        if (b)
        {
            bridgeFallen = true;
            ThirdPersonCharacter p = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonCharacter>();
            p.enabled = false;
            camParent = Camera.main.transform.parent;
            prevCamPos = Camera.main.transform.localPosition;
            prevCamRot = Camera.main.transform.localRotation;
            Camera.main.transform.parent = b.transform;
            Camera.main.transform.localPosition = new Vector3(0, 10, 0);
            Camera.main.transform.localRotation = Quaternion.LookRotation(b.transform.position - Camera.main.transform.position);
            Camera.main.transform.parent = null;
            b.fall = true;
            yield return new WaitForSeconds(3);
            p.enabled = true;
            Camera.main.transform.parent = camParent;
            Camera.main.transform.localPosition = prevCamPos;
            Camera.main.transform.localRotation = prevCamRot;

        }
    }
}
