using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldSignal : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject desiredForm;
    public bool done=false;
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision col){
        if (col.gameObject.tag==desiredForm.tag)
        {
            Debug.Log("S-a pus bine varule");
            done=true;
            desiredForm.GetComponent<Rigidbody>().velocity=Vector3.zero;
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
