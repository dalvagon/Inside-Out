using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class SetAudioLevel : MonoBehaviour
{
    public Slider mainSlider;
    public GameObject audiosource;

    public void Start()
    {
        //Adds a listener to the main slider and invokes a method when the value changes.
        mainSlider.onValueChanged.AddListener(
            delegate
            {
                ValueChangeCheck();
            }
        );
    }

    // Invoked when the value of the slider changes.
    public void ValueChangeCheck()
    {
        audiosource.GetComponent<AudioSource>().volume = mainSlider.value;
        Debug.Log(mainSlider.value);
    }
}
