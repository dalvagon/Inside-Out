using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject terrain;
    private MoldPuzzle moldPuzzle;
    public GameObject star;
    public GameObject galaxy;
    public GameObject cube;
    public GameObject door;
    private bool disappeared = false;
    private MoldSignal starScript;

    void Start()
    {
        starScript = star.GetComponent<MoldSignal>();
        moldPuzzle = terrain.GetComponent<MoldPuzzle>();
    }

    void Update()
    {
        if (!disappeared)
            if (
                starScript.done
                && cube.GetComponent<MoldSignal>().done
                && galaxy.GetComponent<MoldSignal>().done
            )
            {
                moldPuzzle.Win();
                Destroy(door);
                disappeared = true;
            }
    }
}
