using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager _instance;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);
    }


    public Robot playerRobot;
    public string RobotModel;
    private Economy economy;
    private FightManager fightManager;

    public int observation = 0; // Character's observation stat

    // UI
    [Header("Robot stats")]
    public List<Sprite> sprites;
    public Image RobotPreview;
    public TextMeshProUGUI hpRobotStatText;
    public TextMeshProUGUI dmgRobotStatText;
    public TextMeshProUGUI defRobotStatText;
    public TextMeshProUGUI sklRobotStatText;
    public TextMeshProUGUI pwrRobotStatText;

    [Header("Character stats")]
    public TextMeshProUGUI obsCharacterStatText;

    [Header("Phase change")]
    public GameObject preparationPhaseUI;
    public GameObject preparationBackground;
    public GameObject fightPhaseUI;
    public GameObject fightThings; // Background, robots, grid
    public GameObject actionPanel;



    // Start is called before the first frame update
    void Start()
    {
        economy = Economy._instance;
        fightManager = FightManager._instance;

        UpdateRobotStats();
        UpdateObsCharacterStatText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void EndFight(bool win)
    {
        UIManager._instance.ResetPreparationUI();
        if(win)
        {
            fightManager.ShowAnnonce("You win !", 3f, ReturnToPrep);
            // You earn the money and reputation from the fight
            economy.EarnMoney(fightManager.currentFight.rewardMoney);
            economy.GainReputation(fightManager.currentFight.rewardReputation * (UIManager.selectedSpecialUpgrade == 4? 2 : 1));    // Reputation gains are doubled with top hat
        }
        else
        {
            fightManager.ShowAnnonce("You lose !", 3f, ReturnToPrep);
            // You lose reputation because you lost AND YOU ARE TRASH
            economy.LoseReputation(fightManager.currentFight.rewardReputation);
        }
    }

    private void ReturnToPrep()
    {
        playerRobot.hp = playerRobot.maxHp;

        preparationPhaseUI.SetActive(true);
        UIManager._instance.ResetPreparationUI();
        preparationBackground.SetActive(true);
        fightPhaseUI.SetActive(false);
        fightThings.SetActive(false);

        fightManager.CreateFightOffers();
        economy.NextDay();
    }

    public void StartFight()
    {
        SoundManager._instance.PlayMusic(SoundType.Fight);

        fightManager.opponent = fightManager.fightOffers[UIManager.selectedFight].opponent;
        fightManager.currentFight = fightManager.fightOffers[UIManager.selectedFight];

        playerRobot.hp = playerRobot.maxHp;
        fightManager.opponent.hp = fightManager.opponent.maxHp;

        fightManager.ResetHealthBars();
        fightManager.UpdateProbaUI();
        fightManager.ResetAction();
        fightManager.ShowAnnonce("Fight !", 1f);

        preparationPhaseUI.SetActive(false);
        preparationBackground.SetActive(false);
        fightPhaseUI.SetActive(true);
        fightThings.SetActive(true);
    }


    // UI
    // Robot stats
    public void UpdateRobotStats()
    {
        UpdateHpRobotStatText();
        UpdateDmgRobotStatText();
        UpdateDefRobotStatText();
        UpdateSklRobotStatText();
        UpdatePwrRobotStatText();
    }

    public void SetRobotModel(string s)
    {
        RobotModel = s;
        UpdateRobotPreview();
        string model = RobotModel.Substring(RobotModel.Length - 2);
        SpriteRenderer r = playerRobot.GetComponent<SpriteRenderer>();
        r.sprite = Resources.Load<Sprite>(RobotModel);
        Animator a = playerRobot.GetComponent<Animator>();
        print(model + "/" + RobotModel + "-idle");
        a.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(model + "/" + RobotModel + "-idle");
    }

    public void UpdateRobotPreview()
    {
        RobotPreview.sprite =  sprites[int.Parse(RobotModel.Substring(RobotModel.Length - 2))-1];
        //RobotPreview.sprite = RobotModel.Split(['-'])
    }

    public void UpdateHpRobotStatText()
    {
        hpRobotStatText.text = " HP : " + playerRobot.hp + '/' + playerRobot.maxHp;
    }
    public void UpdateDmgRobotStatText()
    {
        dmgRobotStatText.text = "DMG : " + playerRobot.dmg;
    }
    public void UpdateDefRobotStatText()
    {
        defRobotStatText.text = "DEF : " + playerRobot.def;
    }
    public void UpdateSklRobotStatText()
    {
        sklRobotStatText.text = "SKL : " + playerRobot.skill;
    }
    public void UpdatePwrRobotStatText()
    {
        pwrRobotStatText.text = "PWR : " + playerRobot.GetPowerLevel();
    }

    // Character stats
    public void UpdateObsCharacterStatText()
    {
        obsCharacterStatText.text = "OBS : " + observation;
    }
}
