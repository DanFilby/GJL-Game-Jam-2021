using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordControl : MonoBehaviour
{
    [Header("Keybindings")]
    public KeyCode attackKey = KeyCode.Mouse0;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Sword Stats")]
    [Tooltip("Time between swings")] public float attackRate;
    private float lastAttackTime = 0;


    void Start()
    {
        
    }

    void Update()
    {
        //triggered when attack key is pressed and also check to make sure its within attack rate 
        if (Input.GetKeyDown(attackKey) && Time.time - lastAttackTime >= attackRate)
        {
            //will do a leap attack when sprinting
            if (Input.GetKey(sprintKey)) { 
                LeapSwordAttack(); 
            }
            else SwordAttack();

        }
    }

    private void SwordAttack()
    {

        lastAttackTime = Time.time;  
    }

    private void LeapSwordAttack()
    {

        lastAttackTime = Time.time;
    }

}
