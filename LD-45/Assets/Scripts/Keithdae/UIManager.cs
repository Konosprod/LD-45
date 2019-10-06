using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Main panel")]
    public Image moneyButton;
    public Image robotButton;
    public Image characterButton;

    public TextMeshProUGUI moneyButtonText;
    public TextMeshProUGUI robotButtonText;
    public TextMeshProUGUI characterButtonText;

    public Color moneyButtonHighlightedColor;
    public Color robotButtonHighlightedColor;
    public Color characterButtonHighlightedColor;

    private Color moneyButtonBaseColor;
    private Color robotButtonBaseColor;
    private Color characterButtonBaseColor;

    [Header("Stats panel")]
    public Image robotStatsButton;
    public Image characterStatsButton;

    public TextMeshProUGUI robotStatsButtonText;
    public TextMeshProUGUI characterStatsButtonText;

    public Color robotStatsButtonHighlightedColor;
    public Color characterStatsButtonHighlightedColor;

    private Color robotStatsButtonBaseColor;
    private Color characterStatsButtonBaseColor;


    [Header("Fight panel")]
    public Image challengeBackground1;
    public Image challengeBackground2;
    public Image challengeBackground3;

    public TextMeshProUGUI challengeText1;
    public TextMeshProUGUI challengeText2;
    public TextMeshProUGUI challengeText3;

    public Color challengeBackgroundHighlightedColor;

    private Color challengeBackgroundBaseColor;

    public Color challengeTextHighlightedColor;

    private Color challengeTextBaseColor;


    [Header("Loan panel")]
    public Image loanBackground1;
    public Image loanBackground2;
    public Image loanBackground3;

    public TextMeshProUGUI LoanText1;
    public TextMeshProUGUI LoanText2;
    public TextMeshProUGUI LoanText3;

    public Color loanBackgroundHighlightedColor;

    public Color loanTextHighlightedColor;




    public static int selectedFight = -1;

    // Start is called before the first frame update
    void Start()
    {
        // Setup for main panel
        moneyButtonBaseColor = moneyButton.color;
        robotButtonBaseColor = robotButton.color;
        characterButtonBaseColor = characterButton.color;
        MoneyButtonClick();

        // Setup for stats panel
        robotStatsButtonBaseColor = robotStatsButton.color;
        characterStatsButtonBaseColor = characterStatsButton.color;
        RobotStatsButtonClick();

        // Setup for fight selection
        challengeBackgroundBaseColor = challengeBackground1.color;

        challengeTextBaseColor = challengeText1.color;

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Main panel functions
    public void MoneyButtonClick()
    {
        moneyButton.color = moneyButtonHighlightedColor;
        robotButton.color = robotButtonBaseColor;
        characterButton.color = characterButtonBaseColor;

        moneyButtonText.color = Color.white;
        robotButtonText.color = Color.black;
        characterButtonText.color = Color.black;
    }
    public void RobotButtonClick()
    {
        moneyButton.color = moneyButtonBaseColor;
        robotButton.color = robotButtonHighlightedColor;
        characterButton.color = characterButtonBaseColor;

        moneyButtonText.color = Color.black;
        robotButtonText.color = Color.white;
        characterButtonText.color = Color.black;
    }
    public void CharacterButtonClick()
    {
        moneyButton.color = moneyButtonBaseColor;
        robotButton.color = robotButtonBaseColor;
        characterButton.color = characterButtonHighlightedColor;

        moneyButtonText.color = Color.black;
        robotButtonText.color = Color.black;
        characterButtonText.color = Color.white;
    }

    public void FightPanelButtonClick()
    {
        moneyButton.color = moneyButtonBaseColor;
        robotButton.color = robotButtonBaseColor;
        characterButton.color = characterButtonBaseColor;

        moneyButtonText.color = Color.black;
        robotButtonText.color = Color.black;
        characterButtonText.color = Color.black;
    }


    // Stats panel functions
    public void RobotStatsButtonClick()
    {
        robotStatsButton.color = robotStatsButtonHighlightedColor;
        characterStatsButton.color = characterStatsButtonBaseColor;

        robotStatsButtonText.color = Color.white;
        characterStatsButtonText.color = Color.black;
    }
    public void CharacterStatsButtonClicked()
    {
        robotStatsButton.color = robotStatsButtonBaseColor;
        characterStatsButton.color = characterStatsButtonHighlightedColor;

        robotStatsButtonText.color = Color.black;
        characterStatsButtonText.color = Color.white;
    }


    // Fight selection functions
    public void Fight1Click()
    {
        challengeBackground1.color = challengeBackgroundHighlightedColor;
        challengeBackground2.color = challengeBackgroundBaseColor;
        challengeBackground3.color = challengeBackgroundBaseColor;

        challengeText1.color = challengeTextHighlightedColor;
        challengeText2.color = challengeTextBaseColor;
        challengeText3.color = challengeTextBaseColor;

        selectedFight = 0;
    }
    public void Fight2Click()
    {
        challengeBackground1.color = challengeBackgroundBaseColor;
        challengeBackground2.color = challengeBackgroundHighlightedColor;
        challengeBackground3.color = challengeBackgroundBaseColor;

        challengeText1.color = challengeTextBaseColor;
        challengeText2.color = challengeTextHighlightedColor;
        challengeText3.color = challengeTextBaseColor;

        selectedFight = 1;
    }
    public void Fight3Click()
    {
        challengeBackground1.color = challengeBackgroundBaseColor;
        challengeBackground2.color = challengeBackgroundBaseColor;
        challengeBackground3.color = challengeBackgroundHighlightedColor;

        challengeText1.color = challengeTextBaseColor;
        challengeText2.color = challengeTextBaseColor;
        challengeText3.color = challengeTextHighlightedColor;

        selectedFight = 2;
    }

    // Loan click functions
    public void Loan1Click()
    {
        loanBackground1.color = loanBackgroundHighlightedColor;
        LoanText1.color = loanTextHighlightedColor;
    }
    public void Loan2Click()
    {
        loanBackground2.color = loanBackgroundHighlightedColor;
        LoanText2.color = loanTextHighlightedColor;
    }
    public void Loan3Click()
    {
        loanBackground3.color = loanBackgroundHighlightedColor;
        LoanText3.color = loanTextHighlightedColor;
    }

}
