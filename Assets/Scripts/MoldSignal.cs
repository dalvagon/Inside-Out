using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class MoldSignal : MonoBehaviour
{
    public GameObject desiredForm;
    public bool done = false;

    void OnCollisionEnter(Collision col)
    {
        if (desiredForm.tag=="Star")
        {}
       else 
        if (col.gameObject.tag == desiredForm.tag)
        {
            Debug.Log("S-a pus bine varule");
            done = true;
            desiredForm.GetComponent<Rigidbody>().velocity = Vector3.zero;
            desiredForm.transform.position=this.transform.position;
            this.transform.position=this.transform.position+new Vector3(0,-1,0);
            Destroy(desiredForm.GetComponent<XRGrabInteractable>());
        }
    }
}
