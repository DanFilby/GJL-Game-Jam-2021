using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [Header("Base Properties")]
    public GameObject cornerParent;
    public GameObject enemyFace;
    public Color cornerColour;  
    Material faceMat;
    Color faceOriginalColour;

    [Header("Attack Properties")]
    public GameObject enemySword;
    public GameObject swordPickupPrefab;
    public float attackRate;
    public float attackTime;
    public float attackRange;

    [Header("Movement Properties")]
    public Transform player;
    public float moveSpeed;
    public float rotationSpeed;
    [Tooltip("How close the enemy gets infront of the player")]
    public float howCloseToPlayer;
    private NavMeshAgent agent;



    void Start()
    {
        //setting properties
        ChanageCornerColours(cornerColour);
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.angularSpeed = rotationSpeed;
        faceMat = enemyFace.GetComponent<Renderer>().material;
        faceOriginalColour = faceMat.color;

    }  


    void Update()
    {
        ManageMovement();
        LookAtPlayer();

    }

    /// <summary>
    /// updates the destination of the enemy to move towards infront of the player
    /// </summary>
    private void ManageMovement()
    {
        //position of slightly infront of the player
        Vector3 destination = player.position + player.forward * howCloseToPlayer;
        agent.SetDestination(destination);
    }

    /// <summary>
    /// turns the enemy to look towards the player
    /// </summary>
    private void LookAtPlayer()
    {
        //get position of player with adjusted height so the enemy doesnt look up or down
        Vector3 playerPos = player.position;
        playerPos.y = transform.position.y;

        //gets the direction of the player, turns it into a quaternion and rotates the enemy to it at a specified rate
        Vector3 direction = (playerPos - transform.position).normalized;
        Quaternion lookDirection = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Change colours of this enemy's corners
    /// </summary>
    public void ChanageCornerColours(Color newColour)
    {
        //gets each corner and changes its colour
        foreach(Transform corner in cornerParent.GetComponentInChildren<Transform>())
        {
            corner.gameObject.GetComponent<Renderer>().material.color = newColour;
        }
    }

    private IEnumerator Attack()
    {
        faceMat.color = Color.Lerp(faceOriginalColour, Color.red, 0.33f);

        yield return new WaitForSeconds(attackTime / 2);
        faceMat.color = Color.Lerp(faceOriginalColour, Color.red, 0.66f);

        yield return new WaitForSeconds(attackTime / 2);
        faceMat.color = Color.Lerp(faceOriginalColour, Color.red, 1);



        yield return null;
    }



    public void Kill()
    {
        //TODO: Death anim
        Instantiate(swordPickupPrefab, enemySword.transform.position, enemySword.transform.rotation);
        Destroy(gameObject);
    }


}
