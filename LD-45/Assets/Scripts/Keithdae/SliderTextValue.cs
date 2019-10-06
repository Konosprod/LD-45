using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderTextValue : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        UpdateValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateValue()
    {
        text.text = slider.value.ToString("F1") + '%';
    }
}
