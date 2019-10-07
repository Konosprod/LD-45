using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Text text;
    public int scaledSize = 30;
    private int oldSize;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        oldSize = text.fontSize;
        text.fontSize = scaledSize;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.fontSize = oldSize;
    }
}
