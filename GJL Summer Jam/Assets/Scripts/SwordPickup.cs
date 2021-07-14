using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPickup : MonoBehaviour
{
    public GameObject playerSword;
    public Transform pickUpPoint;

    public KeyCode pickupKey = KeyCode.E;
    public float swordPickupRange;
    public LayerMask swordPickupLayer;

    private Vector3 boxSize;

    public void Start()
    {
        boxSize = new Vector3(swordPickupRange, 5, swordPickupRange);
    }


    private void Update()
    {
        //each frame this checks the created hitbox for pickup swords
        if(Physics.CheckBox(pickUpPoint.position, boxSize, Quaternion.identity, swordPickupLayer)){
            //TODO: UI when they can pickup sword

            //if the player picks it up
            if (Input.GetKeyDown(pickupKey)){
                //gets the sword in world and destroys it
                Collider[] sword = Physics.OverlapBox(pickUpPoint.position, boxSize, Quaternion.identity, swordPickupLayer);
                Destroy(sword[0].gameObject);
                
                //then enables the players sword
                playerSword.SetActive(true);
            }
        }
    }



}
