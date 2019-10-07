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
    private Economy economy;
    private FightManager fightManager;

    public int observation = 0; // Character's observation stat

    // UI
    [Header("Robot stats")]
    public TextMeshProUGUI hpRobotStatText;
    public TextMeshProUGUI dmgRobotStatText;
    public TextMeshProUGUI defRobotStatText;
    public TextMeshProUGUI sklRobotStatText;

    [Header("Character stats")]
    public TextMeshProUGUI obsCharacterStatText;

    [Header("Phase change")]
    public GameObject preparationPhaseUI;
    public GameObject preparationBackground;
    public GameObject fightPhaseUI;
    public GameObject fightThings; // Background, robots, grid



    // Start is called before the first frame update
    void Start()
    {
        economy = Economy._instance;
        fightManager = FightManager._instance;

        UpdateHpRobotStatText();
        UpdateDmgRobotStatText();
        UpdateDefRobotStatText();
        UpdateSklRobotStatText();
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
            // You earn the money and reputation from the fight
            economy.EarnMoney(fightManager.currentFight.rewardMoney);
            economy.GainReputation(fightManager.currentFight.rewardReputation);
        }
        else
        {
            // You lose reputation because you lost AND YOU ARE TRASH
            economy.LoseReputation(fightManager.currentFight.rewardReputation);
        }

        preparationPhaseUI.SetActive(true);
        UIManager._instance.ResetPreparationUI();
        fightPhaseUI.SetActive(false);

        fightManager.CreateFightOffers();
        economy.NextDay();
    }

    public void StartFight()
    {
        fightManager.opponent = fightManager.fightOffers[UIManager.selectedFight].opponent;
        fightManager.currentFight = fightManager.fightOffers[UIManager.selectedFight];

        playerRobot.hp = playerRobot.maxHp;
        fightManager.opponent.hp = fightManager.opponent.maxHp;
        fightManager.UpdateHealthBars();

        preparationPhaseUI.SetActive(false);
        fightPhaseUI.SetActive(true);
    }


    // UI
    // Robot stats
    public void UpdateRobotStats()
    {
        UpdateHpRobotStatText();
        UpdateDmgRobotStatText();
        UpdateDefRobotStatText();
        UpdateSklRobotStatText();
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

    // Character stats
    public void UpdateObsCharacterStatText()
    {
        obsCharacterStatText.text = "OBS : " + observation;
    }
}
