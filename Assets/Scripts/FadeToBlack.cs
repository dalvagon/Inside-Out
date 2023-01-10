using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public GameObject blackOutSquare;
    public GameObject cabin;
    public GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeToBlackSquare(false));
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckIfCabinWasTouched())
        {
            blackOutSquare.SetActive(true);
            StartCoroutine(FadeToBlackSquare(true));
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            StartCoroutine(FadeToBlackSquare(false));
        }
    }

    public bool CheckIfCabinWasTouched()
    {
        return cabin.GetComponent<Collider>().bounds.Contains(Camera.main.transform.position);
    }

    public IEnumerator FadeToBlackSquare(bool fadeToBlack = true, int fadeSpeed = 5)
    {
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (blackOutSquare.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        else
        {
            while (blackOutSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
    }
}
