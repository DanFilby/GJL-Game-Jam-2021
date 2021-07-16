using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSword : MonoBehaviour
{
    //controls whether the sword has an idle anim. which is done through code
    public bool idleAnim;

    [Header("Idle Bobbing Settings")]
    public float bobHeight;

    [Header("Idle Rotation Settings")]
    [Tooltip("Which axis to rotate sword in")]public Vector3 rotationVec;
    public float rotationSpeed;

    private Vector3 startPos;

    void Start()
    {
        rotationVec = rotationVec.normalized * rotationSpeed;
        startPos = transform.position;
    }

    void Update()
    {
        if (idleAnim)
        {
            transform.Rotate(rotationVec, Space.Self);
            transform.position = startPos + new Vector3(0, Mathf.Sin(Time.time) * bobHeight, 0);
        }


    }
}
