using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    // Singleton
    public static UIManager _instance;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);
    }


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

    public Button startFightButton;


    [Header("Loan panel")]
    public Image loanBackground1;
    public Image loanBackground2;
    public Image loanBackground3;

    public TextMeshProUGUI loanText1;
    public TextMeshProUGUI loanText2;
    public TextMeshProUGUI loanText3;

    public Color loanBackgroundHighlightedColor;
    private Color loanBackgroundBaseColor;

    public Color loanTextHighlightedColor;
    private Color loanTextBaseColor;

    [Header("Special upgrades")]
    public Image selectButtonBackground1;
    public Image selectButtonBackground2;
    public Image selectButtonBackground3;
    public Image selectButtonBackground4;
    public Image selectButtonBackground5;

    public TextMeshProUGUI selectButtonText1;
    public TextMeshProUGUI selectButtonText2;
    public TextMeshProUGUI selectButtonText3;
    public TextMeshProUGUI selectButtonText4;
    public TextMeshProUGUI selectButtonText5;

    public Color selectButtonBackgroundHighlightedColor;
    public Color selectButtonTextHighlightedColor;

    private Color selectButtonBackgroundBaseColor;
    private Color selectButtonTextBaseColor;

    [Header("Things for reset")]
    public GameObject fightPanel;
    public GameObject moneyPanel;

    public GameObject selectedFightBackground1;
    public GameObject selectedFightBackground2;
    public GameObject selectedFightBackground3;

    public RectTransform moneyPanelButton;

    public Button loanButton1;
    public Button loanButton2;
    public Button loanButton3;
    public GameObject selectedLoanBackground1;
    public GameObject selectedLoanBackground2;
    public GameObject selectedLoanBackground3;
    public GameObject selectedLoanSignStamp1;
    public GameObject selectedLoanSignStamp2;
    public GameObject selectedLoanSignStamp3;

    public static int selectedFight = -1;

    public static int selectedSpecialUpgrade = -1;

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

        // Setup for loan selection
        loanBackgroundBaseColor = loanBackground1.color;
        loanTextBaseColor = loanText1.color;

        // Setup for special upgrades
        selectButtonBackgroundBaseColor = selectButtonBackground1.color;
        selectButtonTextBaseColor = selectButtonText1.color;
    }

    // Update is called once per frame
    void Update()
    {

    }


    // Reset the UI of preparation phase
    public void ResetPreparationUI()
    {
        MoneyButtonClick();
        RobotStatsButtonClick();
        ResetFightUI();
        ResetLoanUI();
        fightPanel.SetActive(false);
        moneyPanel.SetActive(true);

        moneyPanelButton.SetAsLastSibling();
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
        startFightButton.interactable = true;
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
        startFightButton.interactable = true;
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
        startFightButton.interactable = true;
    }

    public void ResetFightUI()
    {
        challengeBackground1.color = challengeBackgroundBaseColor;
        challengeBackground2.color = challengeBackgroundBaseColor;
        challengeBackground3.color = challengeBackgroundBaseColor;

        challengeText1.color = challengeTextBaseColor;
        challengeText2.color = challengeTextBaseColor;
        challengeText3.color = challengeTextBaseColor;

        selectedFight = -1;
        startFightButton.interactable = false;
        startFightButton.transform.parent.gameObject.SetActive(false);

        selectedFightBackground1.SetActive(false);
        selectedFightBackground2.SetActive(false);
        selectedFightBackground3.SetActive(false);
    }

    // Loan click functions
    public void Loan1Click()
    {
        loanBackground1.color = loanBackgroundHighlightedColor;
        loanText1.color = loanTextHighlightedColor;
        loanText1.text = "Great deal !";

        Economy._instance.TakeLoanOffer(0);
    }
    public void Loan2Click()
    {
        loanBackground2.color = loanBackgroundHighlightedColor;
        loanText2.color = loanTextHighlightedColor;
        loanText2.text = "Great deal !";

        Economy._instance.TakeLoanOffer(1);
    }
    public void Loan3Click()
    {
        loanBackground3.color = loanBackgroundHighlightedColor;
        loanText3.color = loanTextHighlightedColor;
        loanText3.text = "Great deal !";

        Economy._instance.TakeLoanOffer(2);
    }

    public void ResetLoanUI()
    {
        loanBackground1.color = loanBackgroundBaseColor;
        loanText1.color = loanTextBaseColor;
        loanText1.text = "Take";
        loanBackground2.color = loanBackgroundBaseColor;
        loanText2.color = loanTextBaseColor;
        loanText2.text = "Take";
        loanBackground3.color = loanBackgroundBaseColor;
        loanText3.color = loanTextBaseColor;
        loanText3.text = "Take";

        selectedLoanBackground1.SetActive(false);
        selectedLoanBackground2.SetActive(false);
        selectedLoanBackground3.SetActive(false);

        selectedLoanSignStamp1.SetActive(false);
        selectedLoanSignStamp2.SetActive(false);
        selectedLoanSignStamp3.SetActive(false);

        loanButton1.interactable = true;
        loanButton2.interactable = true;
        loanButton3.interactable = true;
    }


    // Special upgrades functions
    public void SpecialUpgrade1Click()
    {
        selectButtonBackground1.color = selectButtonBackgroundHighlightedColor;
        selectButtonBackground2.color = selectButtonBackgroundBaseColor;
        selectButtonBackground3.color = selectButtonBackgroundBaseColor;
        selectButtonBackground4.color = selectButtonBackgroundBaseColor;
        selectButtonBackground5.color = selectButtonBackgroundBaseColor;

        selectButtonText1.color = selectButtonTextHighlightedColor;
        selectButtonText2.color = selectButtonTextBaseColor;
        selectButtonText3.color = selectButtonTextBaseColor;
        selectButtonText4.color = selectButtonTextBaseColor;
        selectButtonText5.color = selectButtonTextBaseColor;

        selectButtonText1.text = "Selected";

        selectedSpecialUpgrade = 0;
    }
    public void SpecialUpgrade2Click()
    {
        selectButtonBackground1.color = selectButtonBackgroundBaseColor;
        selectButtonBackground2.color = selectButtonBackgroundHighlightedColor;
        selectButtonBackground3.color = selectButtonBackgroundBaseColor;
        selectButtonBackground4.color = selectButtonBackgroundBaseColor;
        selectButtonBackground5.color = selectButtonBackgroundBaseColor;

        selectButtonText1.color = selectButtonTextBaseColor;
        selectButtonText2.color = selectButtonTextHighlightedColor;
        selectButtonText3.color = selectButtonTextBaseColor;
        selectButtonText4.color = selectButtonTextBaseColor;
        selectButtonText5.color = selectButtonTextBaseColor;

        selectButtonText2.text = "Selected";

        selectedSpecialUpgrade = 1;
    }
    public void SpecialUpgrade3Click()
    {
        selectButtonBackground1.color = selectButtonBackgroundBaseColor;
        selectButtonBackground2.color = selectButtonBackgroundBaseColor;
        selectButtonBackground3.color = selectButtonBackgroundHighlightedColor;
        selectButtonBackground4.color = selectButtonBackgroundBaseColor;
        selectButtonBackground5.color = selectButtonBackgroundBaseColor;

        selectButtonText1.color = selectButtonTextBaseColor;
        selectButtonText2.color = selectButtonTextBaseColor;
        selectButtonText3.color = selectButtonTextHighlightedColor;
        selectButtonText4.color = selectButtonTextBaseColor;
        selectButtonText5.color = selectButtonTextBaseColor;

        selectButtonText3.text = "Selected";

        selectedSpecialUpgrade = 2;
    }
    public void SpecialUpgrade4Click()
    {
        selectButtonBackground1.color = selectButtonBackgroundBaseColor;
        selectButtonBackground2.color = selectButtonBackgroundBaseColor;
        selectButtonBackground3.color = selectButtonBackgroundBaseColor;
        selectButtonBackground4.color = selectButtonBackgroundHighlightedColor;
        selectButtonBackground5.color = selectButtonBackgroundBaseColor;

        selectButtonText1.color = selectButtonTextBaseColor;
        selectButtonText2.color = selectButtonTextBaseColor;
        selectButtonText3.color = selectButtonTextBaseColor;
        selectButtonText4.color = selectButtonTextHighlightedColor;
        selectButtonText5.color = selectButtonTextBaseColor;

        selectButtonText4.text = "Selected";

        selectedSpecialUpgrade = 3;
    }
    public void SpecialUpgrade5Click()
    {
        selectButtonBackground1.color = selectButtonBackgroundBaseColor;
        selectButtonBackground2.color = selectButtonBackgroundBaseColor;
        selectButtonBackground3.color = selectButtonBackgroundBaseColor;
        selectButtonBackground4.color = selectButtonBackgroundBaseColor;
        selectButtonBackground5.color = selectButtonBackgroundHighlightedColor;

        selectButtonText1.color = selectButtonTextBaseColor;
        selectButtonText2.color = selectButtonTextBaseColor;
        selectButtonText3.color = selectButtonTextBaseColor;
        selectButtonText4.color = selectButtonTextBaseColor;
        selectButtonText5.color = selectButtonTextHighlightedColor;

        selectButtonText5.text = "Selected";

        selectedSpecialUpgrade = 4;
    }


    public void SpecialUpgradeTextUpdate()
    {
        selectButtonText1.text = "Select";
        selectButtonText2.text = "Select";
        selectButtonText3.text = "Select";
        selectButtonText4.text = "Select";
        selectButtonText5.text = "Select";
    }
}
