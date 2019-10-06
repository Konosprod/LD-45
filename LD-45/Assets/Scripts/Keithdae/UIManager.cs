using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image moneyButton;
    public Image robotButton;
    public Image characterButton;

    public Color moneyButtonHighlightedColor;
    public Color robotButtonHighlightedColor;
    public Color characterButtonHighlightedColor;

    private Color moneyButtonBaseColor;
    private Color robotButtonBaseColor;
    private Color characterButtonBaseColor;

    // Start is called before the first frame update
    void Start()
    {
        moneyButtonBaseColor = moneyButton.color;
        robotButtonBaseColor = robotButton.color;
        characterButtonBaseColor = characterButton.color;
        MoneyButtonClick();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoneyButtonClick()
    {
        moneyButton.color = moneyButtonHighlightedColor;
        robotButton.color = robotButtonBaseColor;
        characterButton.color = characterButtonBaseColor;
    }

    public void RobotButtonClick()
    {
        moneyButton.color = moneyButtonBaseColor;
        robotButton.color = robotButtonHighlightedColor;
        characterButton.color = characterButtonBaseColor;
    }

    public void CharacterButtonClick()
    {
        moneyButton.color = moneyButtonBaseColor;
        robotButton.color = robotButtonBaseColor;
        characterButton.color = characterButtonHighlightedColor;
    }
}
