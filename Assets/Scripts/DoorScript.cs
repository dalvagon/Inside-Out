using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject star;
    private MoldSignal starScript;
    public GameObject galaxy;
    public GameObject cube;
    private bool disappeared = false;

    void Start()
    {
        starScript = star.GetComponent<MoldSignal>();
    }

    /*
   IEnumerator DoCheck()
    {
    for(;;)
    {
        if (starHolder.GetComponent<MoldSignal>().done==true)
        {
            
            // Perform some action here
        }
        yield return new WaitForSeconds(.1f);
    }
    }*/
    // Update is called once per frame
    void Update()
    {
        if (!disappeared)
            if (starScript.done && cube.GetComponent<MoldSignal>().done && galaxy.GetComponent<MoldSignal>().done)
            {
                Debug.Log("HELLO");
                this.GetComponent<Renderer>().enabled = false;
                // Perform some action here
                disappeared = true;
            }
    }
}
