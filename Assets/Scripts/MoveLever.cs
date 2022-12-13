using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLever : MonoBehaviour
{
    Animator animator;
    const float TARGET_DISTANCE = 10.0f;
    const float TARGET_ANGLE = 30.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public bool IsLeverInFront(Vector3 cameraPosition, Vector3 cameraForward, int lever_idx)
    {
        Vector3 leverPosition = transform.position;
        float distance = Vector3.Distance(cameraPosition, leverPosition);
        return distance < TARGET_DISTANCE;
        // return distance < TARGET_DISTANCE && Vector3.Angle(cameraForward, leverPosition - cameraPosition) < TARGET_ANGLE;
    }

    void Update()
    {
    }
}
