﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

public class Domino : ButtonScript {

    Animator anim;
    public Bridge b;
    private bool fallen = false;
    
    
	// Use this for initialization
	public override void Start () {
        base.Start();
	    anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	public override void Update () {
        base.Update();
	}

    public override void PressButton()
    {
        if (!fallen)
        {
            base.PressButton();
            anim.SetBool("PlayAnim", true);
            StartCoroutine(DominosFall());
            fallen = true;
        }

        
    }


    IEnumerator DominosFall()
    {
        ThirdPersonCharacter p = GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonCharacter>();
        Quaternion look = Camera.main.transform.localRotation;
        Vector3 camLoc = Camera.main.transform.localPosition;
        Camera.main.transform.parent = transform;
        Camera.main.transform.localPosition = new Vector3(0, 20, 0);
        Camera.main.transform.localRotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        p.enabled = false;
        yield return new WaitForSeconds(12);
        b.fall = true;
        yield return new WaitForSeconds(3);
        p.enabled = true;
        Camera.main.transform.parent = p.transform;
        Camera.main.transform.localPosition = camLoc;
        Camera.main.transform.localRotation = look;
    }
}
