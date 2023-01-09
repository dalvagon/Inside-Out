using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldSignal : MonoBehaviour
{
    public GameObject desiredForm;
    public bool done = false;

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == desiredForm.tag)
        {
            Debug.Log("S-a pus bine varule");
            done = true;
            desiredForm.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
