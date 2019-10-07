using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    // Singleton
    public static UpgradeManager _instance;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);
    }

    [Header("Robot upgrades")]
    public int hpUpgradeLevel = 0;
    public int dmgUpgradeLevel = 0;
    public int defUpgradeLevel = 0;
    public int skillUpgradeLevel = 0;

    [Header("UI")]
    public Button upgradeHPButton;
    public Button upgradeDMGButton;
    public Button upgradeDEFButton;
    public Button upgradeSKLButton;

    public TextMeshProUGUI upgradeHPCostText;
    public TextMeshProUGUI upgradeDMGCostText;
    public TextMeshProUGUI upgradeDEFCostText;
    public TextMeshProUGUI upgradeSKLCostText;

    [Header("Character upgrades")]
    public int obsUpgradeLevel = 0;

    //UI



    // Start is called before the first frame update
    void Start()
    {
        hpUpgradeLevel = 0;
        dmgUpgradeLevel = 0;
        defUpgradeLevel = 0;
        skillUpgradeLevel = 0;

        obsUpgradeLevel = 0;

        CheckAffordableUpgrade();
    }

    public int GetUpgradeCost(int level) // 100 => 150 => 250 => 400 => 600 => 850 => 1150
    {
        return 100 + 50 * level * (level + 1) / 2;
    }

    public bool CanAffordUpgrade(int level)
    {
        return GetUpgradeCost(level) <= Economy._instance.money;
    }


    // Buy upgrade functions for stats
    public void BuyHpUpgrade()
    {
        if(CanAffordUpgrade(hpUpgradeLevel))
        {
            Economy._instance.SpendMoney(GetUpgradeCost(hpUpgradeLevel));
            hpUpgradeLevel++;
            GameManager._instance.playerRobot.maxHp += 10;
            GameManager._instance.playerRobot.hp = GameManager._instance.playerRobot.maxHp;
            GameManager._instance.UpdateRobotStats();
            CheckAffordableUpgrade();
        }
        else
        {
            Debug.LogError("Cannot afford upgrade");
        }
    }
    public void BuyDmgUpgrade()
    {
        if (CanAffordUpgrade(dmgUpgradeLevel))
        {
            Economy._instance.SpendMoney(GetUpgradeCost(dmgUpgradeLevel));
            dmgUpgradeLevel++;
            GameManager._instance.playerRobot.dmg += 3;
            GameManager._instance.UpdateRobotStats();
            CheckAffordableUpgrade();
        }
        else
        {
            Debug.LogError("Cannot afford upgrade");
        }
    }
    public void BuyDefUpgrade()
    {
        if (CanAffordUpgrade(defUpgradeLevel))
        {
            Economy._instance.SpendMoney(GetUpgradeCost(defUpgradeLevel));
            defUpgradeLevel++;
            GameManager._instance.playerRobot.def += 3;
            GameManager._instance.UpdateRobotStats();
            CheckAffordableUpgrade();
        }
        else
        {
            Debug.LogError("Cannot afford upgrade");
        }
    }
    public void BuySkillUpgrade()
    {
        if (CanAffordUpgrade(skillUpgradeLevel))
        {
            Economy._instance.SpendMoney(GetUpgradeCost(skillUpgradeLevel));
            skillUpgradeLevel++;
            GameManager._instance.playerRobot.skill += 3;
            GameManager._instance.UpdateRobotStats();
            CheckAffordableUpgrade();
        }
        else
        {
            Debug.LogError("Cannot afford upgrade");
        }
    }
    public void BuyObsUpgrade()
    {
        if (CanAffordUpgrade(obsUpgradeLevel))
        {
            Economy._instance.SpendMoney(GetUpgradeCost(obsUpgradeLevel));
            obsUpgradeLevel++;
            GameManager._instance.observation += 3;

            CheckAffordableUpgrade();
        }
        else
        {
            Debug.LogError("Cannot afford upgrade");
        }
    }


    public void CheckAffordableUpgrade()
    {
        upgradeHPButton.interactable = CanAffordUpgrade(hpUpgradeLevel);
        upgradeDMGButton.interactable = CanAffordUpgrade(dmgUpgradeLevel);
        upgradeDEFButton.interactable = CanAffordUpgrade(defUpgradeLevel);
        upgradeSKLButton.interactable = CanAffordUpgrade(skillUpgradeLevel);

        upgradeHPCostText.text = "Cost : " + GetUpgradeCost(hpUpgradeLevel) + " q";
        upgradeDMGCostText.text = "Cost : " + GetUpgradeCost(dmgUpgradeLevel) + " q";
        upgradeDEFCostText.text = "Cost : " + GetUpgradeCost(defUpgradeLevel) + " q";
        upgradeSKLCostText.text = "Cost : " + GetUpgradeCost(skillUpgradeLevel) + " q";
    }
}
