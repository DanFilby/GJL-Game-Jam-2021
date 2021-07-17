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

    [Header("Dash Attack")]
    public Rigidbody playerRb;
    public float dashAtkRange;

    [Header("Animation")]
    private Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    void Update()
    {
        //triggered when attack key is pressed and also check to make sure its within attack rate 
        if (Input.GetKeyDown(attackKey) && Time.time - lastAttackTime >= attackRate)
        {
            //will do a leap attack when sprinting
            if (Input.GetKey(sprintKey)) { 
                DashSwordAttack(); 
            }
            else SwordAttack();

        }
    }

    /// <summary>
    /// Plays attacking sword animation and breaks the sword when finished
    /// </summary>
    IEnumerator PlaySwordAnim(bool breakSword)
    {
        //This is hard coded because i dont know much about animation
        //didnt have time to learn :/

        anim.enabled = true;

        yield return new WaitForSeconds(1f);        

        anim.enabled = false;

        //destroy sword
        if (breakSword) gameObject.SetActive(false);

        transform.parent.GetComponent<Collider>().enabled = true;

    }


    /// <summary>
    /// kills the closest enemy and breaks sword
    /// </summary>
    private void SwordAttack()
    {

        //creates a box hitbox for the sword swing and then gets all coliders inside that are in specifeid layers
        Vector3 hitBoxExtends = new Vector3(2.5f , 10, attackRange);
        Collider[] hits = Physics.OverlapBox(hitBoxPos.position, hitBoxExtends, Quaternion.identity, swordHitLayers);
        
        EnemyBase closestEnemy = null;
        //iterates over colliders and doing whats needed to each type
        foreach(Collider hit in hits)
        {
            if (hit.gameObject.TryGetComponent<EnemyBase>(out EnemyBase enemy))
            {
                //if no closest enemy or this one is closer to the player
                if(closestEnemy == null ||
                    (Vector3.Distance(transform.position, closestEnemy.transform.position) > Vector3.Distance(transform.position, enemy.transform.position)))
                {
                    closestEnemy = enemy;
                }        
            }
        }
        //TODO: break sword anim

        //whether it hit an enemy. so the sword can break in the anim coroutine and not when it does hit anything
        bool hitEnemy = closestEnemy != null;

        StartCoroutine(PlaySwordAnim(hitEnemy));

        if (hitEnemy) {
            closestEnemy.Kill();
        }

        //keep track of attack to limit attack rate
        lastAttackTime = Time.time;  
    }

    /// <summary>
    /// Player dashes forward killing all enemies in range
    /// </summary>
    private void DashSwordAttack()
    {
        transform.parent.GetComponent<Collider>().enabled = false;
        playerRb.AddForce(transform.parent.forward * 6000, ForceMode.Impulse);

        Vector3 hitBoxExtends = new Vector3(dashAtkRange, 10f, 3f);
        Collider[] hits = Physics.OverlapBox(hitBoxPos.position, hitBoxExtends, Quaternion.identity, swordHitLayers);

        bool hitEnemy = false;

        foreach (Collider hit in hits)
        {
            if (hit.gameObject.TryGetComponent<EnemyBase>(out EnemyBase enemy))
            {
                //if no closest enemy or this one is closer to the player
                enemy.Kill();

                hitEnemy = true;
            }
        }

        StartCoroutine(PlaySwordAnim(hitEnemy));

        lastAttackTime = Time.time;
    }

}
