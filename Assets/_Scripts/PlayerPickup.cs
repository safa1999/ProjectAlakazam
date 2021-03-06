﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ControllerInput;
using static ControllerInput.CONTROLLER_BUTTON;


public class PlayerPickup : MonoBehaviour
{
    public Animator _animator;

    public int WoodAmount = 0;
    public int StoneAmount = 0;
    public int CrystalAmount = 0;

    float timer = 0;
    float pickupDuration = 1f;

    int resourceCollected = 0;
    bool woodCollision = false;
    bool stoneCollision = false;
    bool crystalCollision = false;
    //bool woodSmallCollision = false;
    //bool stoneSmallCollision = false;
    //bool crystalSmallCollision = false;
    public int woodStock = 0;
    public int stoneStock = 0;
    public int crystalStock = 0;
    bool keyPressed = false;
    bool deletThis = false;

    public KeyCode pickupKey = KeyCode.Space;
    public CONTROLLER_BUTTON pickupJoy = X;
    private ushort playerIndex;


    // Update is called once per frame
    void Update()
    {
        playerIndex = GetComponent<PlayerMovement>().playerIndex;
        if (Input.GetKeyDown(pickupKey) || isButtonDown(playerIndex, (int)pickupJoy))
        {
            keyPressed = true;
            timer = Time.time;
        }
        else if (Input.GetKeyUp(pickupKey) || isButtonReleased(playerIndex, (int)pickupJoy))
        {
            keyPressed = false;
            resourceCollected = 0;
            _animator.SetBool("isGather", false);
        }

        //if (!Input.GetKey(pickupKey) && isButtonPressed(playerIndex, (int)pickupJoy))
        //{
        //  _animator.SetBool("isGather", false);
        //}

        //Picks up resource after 1 second of holding the key down (Key is public, it is et in Unity)
        //Also sends a bool to onTriggerStay
        if (keyPressed)
        {
            if (woodCollision == true)

            {
                _animator.SetBool("isGather", true);
                if (Time.time - timer >= pickupDuration)
                {
                    Debug.Log("hi");

                    WoodAmount += 3;
                    resourceCollected += 1;
                    woodCollision = false;
                    timer = 0;
                    deletThis = true;
                    _animator.SetBool("isGather", false);
                }
            }
            else if (stoneCollision == true)
            {
                _animator.SetBool("isGather", true);
                if (Time.time - timer >= pickupDuration)
                {
                    Debug.Log("hi");

                    StoneAmount += 3;
                    resourceCollected += 1;
                    stoneCollision = false;
                    timer = 0;
                    deletThis = true;
                    _animator.SetBool("isGather", false);
                }
            }
            else if (crystalCollision == true)
            {
                _animator.SetBool("isGather", true);
                if (Time.time - timer >= pickupDuration)
                {
                    Debug.Log("hi");

                    CrystalAmount += 3;
                    resourceCollected += 1;
                    crystalCollision = false;
                    timer = 0;
                    deletThis = true;
                    _animator.SetBool("isGather", false);
                }
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        //Sends collision bool to be used in update depending on resource

        if (!keyPressed)
        {
            Debug.Log("Y do we play gaem");
            if (other.gameObject.CompareTag("Resource(Wood)"))
            {
                //Debug.Log("hello");
                woodCollision = true;
            }
            if (other.gameObject.CompareTag("Resource(Stone)"))
            {
                //Debug.Log("hello");
                stoneCollision = true;
            }
            if (other.gameObject.CompareTag("Resource(Crystal)"))
            {
                //Debug.Log("hello");
                crystalCollision = true;
            }
            if (other.gameObject.CompareTag("Resource(WoodSmall)"))
            {
                //Debug.Log("hello");
                //woodSmallCollision = true;
                other.gameObject.SetActive(false);
                WoodAmount += 1;
            }
            if (other.gameObject.CompareTag("Resource(StoneSmall)"))
            {
                //Debug.Log("hello");
                //stoneSmallCollision = true;
                other.gameObject.SetActive(false);
                StoneAmount += 1;
            }
            if (other.gameObject.CompareTag("Resource(CrystalSmall)"))
            {
                //Debug.Log("hello");
                //crystalSmallCollision = true;
                other.gameObject.SetActive(false);
                CrystalAmount += 1;
            }

            if (other.gameObject.CompareTag("Chest(Wood)"))
            {


                woodStock += WoodAmount;
                WoodAmount = 0;
                Debug.Log(woodStock);

            }
            if (other.gameObject.CompareTag("Chest(Stone)"))
            {



                stoneStock += StoneAmount;
                StoneAmount = 0;
                Debug.Log(stoneStock);

            }
            if (other.gameObject.CompareTag("Chest(Crystal)"))
            {



                crystalStock += CrystalAmount;
                CrystalAmount = 0;
                Debug.Log(crystalStock);

            }

        }
    }

    void OnTriggerStay(Collider other)
    {
        if (deletThis)
        {
            //if()
            //{
            if (other.gameObject.tag.Contains("Build Zone"))
            {

            }
            //deletes resource object
            else if (!other.gameObject.tag.Contains("Build Zone"))
            {
                other.gameObject.SetActive(false);
                Debug.Log("Y do we play gaem2");
                deletThis = false;
            }
            //}
        }
    }
}
