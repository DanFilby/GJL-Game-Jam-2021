using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordControl : MonoBehaviour
{
    [Header("Keybindings")]
    public KeyCode attackKey = KeyCode.Mouse0;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Sword Stats")]
    public Transform hitBoxPos;
    public LayerMask swordHitLayers;
    public float attackRange;
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
        //creates a box hitbox for the sword swing and then gets all coliders inside that are in specifeid layers
        Vector3 hitBoxExtends = new Vector3(2.5f , 10, attackRange);
        Collider[] hits = Physics.OverlapBox(hitBoxPos.position, hitBoxExtends, Quaternion.identity, swordHitLayers);
        
        //iterates over colliders and doing whats needed to each type
        foreach(Collider hit in hits)
        {
            if (hit.gameObject.TryGetComponent<EnemyBase>(out EnemyBase enemy))
            {
                gameObject.SetActive(false);
                enemy.Kill();
                //TODO: break sword anim
            }
        }

        //for attack rate
        lastAttackTime = Time.time;  
    }

    private void LeapSwordAttack()
    {

        lastAttackTime = Time.time;
    }

}
