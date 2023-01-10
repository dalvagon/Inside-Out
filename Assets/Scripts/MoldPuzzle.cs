using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldPuzzle : MonoBehaviour
{
    private bool win = false;

    void Start() { }

    void Update() { }

    public bool IsWin()
    {
        return win;
    }

    public void Win()
    {
        win = true;
    }
}
