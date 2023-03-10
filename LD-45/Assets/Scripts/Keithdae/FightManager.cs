using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FightManager : MonoBehaviour
{
    // Singleton
    public static FightManager _instance;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);
    }

    public enum MoveType // The three types of attack : Attack > Projectile > Guard > Attack
    {
        Attack,
        Guard,
        Projectile
    };


    public class Fight
    {
        public int rewardMoney;
        public int rewardReputation;

        public Robot opponent;

        public Fight(int rm, int rr, Robot opp)
        {
            rewardMoney = rm;
            rewardReputation = rr;
            opponent = opp;
        }
    }

    public const int fightOfferNb = 3;   // Number of fights you are offered each day

    public Fight[] fightOffers = new Fight[fightOfferNb];
    public Robot[] opponents = new Robot[fightOfferNb];


    public Fight currentFight;
    public Robot opponent;
    private Robot player;

    [Header("UI")]
    public HealthBar playerHealth;
    public HealthBar opponentHealth;
    public Animate playerAnimate;
    public Animate opponentAnimate;
    public GameObject actionPanel;
    public TextMeshProUGUI textAnnonce;


    // UI
    [Header("Fight offers")]
    public TextMeshProUGUI fight1RewardMoneyText;
    public TextMeshProUGUI fight1RewardReputationText;
    public TextMeshProUGUI fight1PowerLevelText;
    public TextMeshProUGUI fight1BehaviourTypeText;
    public Image fight1RobotImage;

    public TextMeshProUGUI fight2RewardMoneyText;
    public TextMeshProUGUI fight2RewardReputationText;
    public TextMeshProUGUI fight2PowerLevelText;
    public TextMeshProUGUI fight2BehaviourTypeText;
    public Image fight2RobotImage;

    public TextMeshProUGUI fight3RewardMoneyText;
    public TextMeshProUGUI fight3RewardReputationText;
    public TextMeshProUGUI fight3PowerLevelText;
    public TextMeshProUGUI fight3BehaviourTypeText;
    public Image fight3RobotImage;


    [Header("Fight UI proba")]
    public Sprite atkSprite;
    public Sprite guardSprite;
    public Sprite projSprite;

    public Image probaImage;

    public Text probaText;


    // Start is called before the first frame update
    void Start()
    {
        UpdatePlayer();
        CreateFightOffers();
    }

    private void Update()
    {
        if (!playerAnimate.isDone || !opponentAnimate.isDone)
        {
            actionPanel.SetActive(false);
        }
        else if (playerAnimate.isDone && opponentAnimate.isDone)
        {
            actionPanel.SetActive(true);
        }
    }

    public void UpdatePlayer()
    {
        player = GameManager._instance.playerRobot;
    }


    public void UseAttack()
    {
        playerAnimate.attackAnimation(1);
        CombatPhase(MoveType.Attack);
    }
    public void UseGuard()
    {
        playerAnimate.ShieldAnimation();
        CombatPhase(MoveType.Guard);
    }
    public void UseProjectile()
    {
        playerAnimate.LaserAnimation(1);
        CombatPhase(MoveType.Projectile);
    }

    public void CombatPhase(MoveType playerMove)
    {
        MoveType opponentMove = opponent.ChoseMove();
        MoveType actualPlayerMove = playerMove;

        float rpsMultiplier = 1f; // The damage bonus from RPS
        float opponentRpsMult = 1f;

        if (UIManager.selectedSpecialUpgrade == 3) // Raging horns
        {
            int coinFlip = Random.Range(0, 2);
            Debug.Log("CoinFlip : " + coinFlip);
            if (coinFlip == 1)
            {
                coinFlip = Random.Range(0, 3);
                Debug.Log("CoinFlip2 : " + coinFlip);
                actualPlayerMove = coinFlip == 0 ? MoveType.Attack : (coinFlip == 1 ? MoveType.Guard : MoveType.Projectile);
            }
        }

        switch (opponentMove)
        {
            case MoveType.Attack:
                opponentAnimate.attackAnimation(-1);
                break;

            case MoveType.Guard:
                opponentAnimate.ShieldAnimation();
                break;

            case MoveType.Projectile:
                opponentAnimate.LaserAnimation(-1);
                break;
            default:
                print("Allo ???");
                break;
        }

        // Rock, Paper, Scissors
        if (actualPlayerMove == MoveType.Attack)
        {
            switch (opponentMove)
            {
                case MoveType.Attack:   // Neutral result => multiplier is still 1f
                    break;
                case MoveType.Guard:    // Bad matchup
                    rpsMultiplier = UIManager.selectedSpecialUpgrade == 0 ? 1f : 0.5f; // Brute force
                    opponentRpsMult = UIManager.selectedSpecialUpgrade == 0 ? 1f : 1.5f; // Brute force
                    break;
                case MoveType.Projectile:   // Good matchup
                    rpsMultiplier = 1.5f;
                    opponentRpsMult = 0.5f;
                    break;
                default:
                    Debug.LogError("Wrong moveType : " + opponentMove);
                    break;
            }
        }
        else if (actualPlayerMove == MoveType.Guard)
        {
            switch (opponentMove)
            {
                case MoveType.Guard:   // Neutral result => multiplier is still 1f
                    break;
                case MoveType.Projectile:    // Bad matchup
                    rpsMultiplier = UIManager.selectedSpecialUpgrade == 1 ? 1f : 0.5f;  // Absolute defence
                    opponentRpsMult = UIManager.selectedSpecialUpgrade == 1 ? 1f : 1.5f; // Absolute defence
                    break;
                case MoveType.Attack:   // Good matchup
                    rpsMultiplier = UIManager.selectedSpecialUpgrade == 0 ? 1f : 1.5f; // Absolute defence
                    opponentRpsMult = UIManager.selectedSpecialUpgrade == 0 ? 1f : 0.5f; // Absolute defence
                    break;
                default:
                    Debug.LogError("Wrong moveType : " + opponentMove);
                    break;
            }
        }
        else // playerMove == MoveType.Projectile
        {
            switch (opponentMove)
            {
                case MoveType.Projectile:   // Neutral result => multiplier is still 1f
                    rpsMultiplier = UIManager.selectedSpecialUpgrade == 2 ? 1.5f : 1f; // Glass cannon
                    break;
                case MoveType.Attack:    // Bad matchup
                    rpsMultiplier = UIManager.selectedSpecialUpgrade == 2 ? 1.5f : 0.5f; // Glass cannon
                    opponentRpsMult = 1.5f;
                    break;
                case MoveType.Guard:   // Good matchup
                    rpsMultiplier = 1.5f;
                    opponentRpsMult = 0.5f;
                    break;
                default:
                    Debug.LogError("Wrong moveType : " + opponentMove);
                    break;
            }
        }

        // Rps multiplier correction
        if (rpsMultiplier != 1f)
            rpsMultiplier *= 1 + (((player.skill * UIManager.selectedSpecialUpgrade == 0 ? 0.5f : 1f) - opponent.skill) / 100); // Brute force
        if (opponentRpsMult != 1f)
            opponentRpsMult *= 1 + ((opponent.skill - (player.skill * UIManager.selectedSpecialUpgrade == 0 ? 0.5f : 1f)) / 100); // Brute force

        // Damage calculation
        float randFactor = Random.Range(0.8f, 1.2f);
        int playerDamage = Mathf.FloorToInt(player.dmg * (UIManager.selectedSpecialUpgrade == 3 ? 1.5f : 1f) * (UIManager.selectedSpecialUpgrade == 3 ? 1.5f : 1f) * (100f / (100f + opponent.def)) * rpsMultiplier * randFactor); // Raging horns
        int opponentDamage = Mathf.FloorToInt(opponent.dmg * (100f / (100f + (player.def * UIManager.selectedSpecialUpgrade == 2 ? 0.5f : 1f))) * opponentRpsMult * randFactor); // Glass cannon

        Debug.Log("Opponent move : " + opponentMove);
        Debug.Log("Player damage = " + playerDamage + ", opponent damage = " + opponentDamage);

        // Deal the damage
        int playerHP = player.hp - opponentDamage;
        int opponentHP = opponent.hp - playerDamage;

        if (playerHP <= 0 && opponentHP <= 0)
        {
            // If both robots reach 0 hp at the same time, the one who took the most damage is the loser
            if (playerHP < opponentHP)
            {
                // Lose
                player.hp = 0;
                opponent.hp -= playerDamage;
                GameManager._instance.EndFight(false);
            }
            else
            {
                // Win
                player.hp -= opponentDamage;
                opponent.hp = 0;
                GameManager._instance.EndFight(true);
            }
        }
        else if (playerHP <= 0)
        {
            // Lose
            player.hp = 0;
            opponent.hp -= playerDamage;
            GameManager._instance.EndFight(false);
        }
        else if (opponentHP <= 0)
        {
            // Win
            player.hp -= opponentDamage;
            opponent.hp = 0;
            GameManager._instance.EndFight(true);
        }
        else
        {
            // The fight keeps going, both are still alive
            player.hp -= opponentDamage;
            opponent.hp -= playerDamage;
            UpdateHealthBars();
        }
    }

    public void ResetHealthBars()
    {
        playerAnimate.ResetAnimations();
        opponentAnimate.ResetAnimations();
        playerHealth.ResetHealthBar();
        opponentHealth.ResetHealthBar();
    }

    public void UpdateHealthBars()
    {
        playerHealth.UpdateHealthBar((float)player.hp / player.maxHp);
        opponentHealth.UpdateHealthBar((float)opponent.hp / opponent.maxHp);
    }

    public void ResetAction()
    {
        actionPanel.SetActive(true);
    }

    public void CreateFightOffers()
    {
        // Fight are from easiest to hardest
        int maxHp;
        int dmg;
        int def;
        int skl;
        int rewardMoney;
        int rewardRep;
        int playerPowerLevel = player.GetPowerLevel();
        int opponentPowerLevel;

        // Random stats spread
        float spreadHp = Random.Range(20f, 50f) * Robot.hpPowerMult;
        float spreadDmg = Random.Range(6f, 20f) * Robot.dmgPowerMult;
        float spreadDef = Random.Range(6f, 20f) * Robot.defPowerMult;
        float spreadSkl = Random.Range(7f, 18f) * Robot.sklPowerMult;

        float repRewardBoost = 1f + (Economy._instance.reputation / 100f);

        // Fight 1 50 + (50 - 70% of player's power level)
        opponentPowerLevel = 50 + Mathf.FloorToInt(playerPowerLevel * Random.Range(0.5f, 0.7f));
        rewardMoney = Mathf.FloorToInt(200 * repRewardBoost);
        rewardRep = 7;


        float sum = spreadHp + spreadDmg + spreadDef + spreadSkl;
        // Normalize values
        maxHp = Mathf.FloorToInt(spreadHp * opponentPowerLevel / sum / Robot.hpPowerMult);
        dmg = Mathf.FloorToInt(spreadDmg * opponentPowerLevel / sum / Robot.dmgPowerMult);
        def = Mathf.FloorToInt(spreadDef * opponentPowerLevel / sum / Robot.defPowerMult);
        skl = Mathf.FloorToInt(spreadSkl * opponentPowerLevel / sum / Robot.sklPowerMult);

        opponents[0].SetRobot(maxHp, dmg, def, skl, (Robot.RobotBehaviourType)Random.Range(0, 4));

        //Debug.Log("FightOffer1 : Desired power level = " + opponentPowerLevel + ", actual power level = " + opponents[0].GetPowerLevel() + '\n' + opponents[0].ToString());

        fightOffers[0] = new Fight(rewardMoney, rewardRep, opponents[0]);

        // Fight 2 50 + (80 - 100% of player's power level)
        opponentPowerLevel = 50 + Mathf.FloorToInt(playerPowerLevel * Random.Range(0.8f, 1.0f));
        rewardMoney = Mathf.FloorToInt(300 * repRewardBoost);
        rewardRep = 20;


        spreadHp = Random.Range(20f, 50f) * Robot.hpPowerMult;
        spreadDmg = Random.Range(6f, 20f) * Robot.dmgPowerMult;
        spreadDef = Random.Range(6f, 20f) * Robot.defPowerMult;
        spreadSkl = Random.Range(7f, 18f) * Robot.sklPowerMult;
        sum = spreadHp + spreadDmg + spreadDef + spreadSkl;
        // Normalize values
        maxHp = Mathf.FloorToInt(spreadHp * opponentPowerLevel / sum / Robot.hpPowerMult);
        dmg = Mathf.FloorToInt(spreadDmg * opponentPowerLevel / sum / Robot.dmgPowerMult);
        def = Mathf.FloorToInt(spreadDef * opponentPowerLevel / sum / Robot.defPowerMult);
        skl = Mathf.FloorToInt(spreadSkl * opponentPowerLevel / sum / Robot.sklPowerMult);

        opponents[1].SetRobot(maxHp, dmg, def, skl, (Robot.RobotBehaviourType)Random.Range(0, 4));

        //Debug.Log("FightOffer2 : Desired power level = " + opponentPowerLevel + ", actual power level = " + opponents[1].GetPowerLevel() + '\n' + opponents[1].ToString());

        fightOffers[1] = new Fight(rewardMoney, rewardRep, opponents[1]);

        // Fight 3 50 + (110 - 130% of player's power level)
        opponentPowerLevel = 50 + Mathf.FloorToInt(playerPowerLevel * Random.Range(1.1f, 1.3f));
        rewardMoney = Mathf.FloorToInt(450 * repRewardBoost);
        rewardRep = 50;

        spreadHp = Random.Range(20f, 50f) * Robot.hpPowerMult;
        spreadDmg = Random.Range(6f, 20f) * Robot.dmgPowerMult;
        spreadDef = Random.Range(6f, 20f) * Robot.defPowerMult;
        spreadSkl = Random.Range(7f, 18f) * Robot.sklPowerMult;
        sum = spreadHp + spreadDmg + spreadDef + spreadSkl;
        // Normalize values
        maxHp = Mathf.FloorToInt(spreadHp * opponentPowerLevel / sum / Robot.hpPowerMult);
        dmg = Mathf.FloorToInt(spreadDmg * opponentPowerLevel / sum / Robot.dmgPowerMult);
        def = Mathf.FloorToInt(spreadDef * opponentPowerLevel / sum / Robot.defPowerMult);
        skl = Mathf.FloorToInt(spreadSkl * opponentPowerLevel / sum / Robot.sklPowerMult);

        opponents[2].SetRobot(maxHp, dmg, def, skl, (Robot.RobotBehaviourType)Random.Range(1, 4)); // We don't allow balanced type for the hardest fight (would be impossible)

        //Debug.Log("FightOffer3 : Desired power level = " + opponentPowerLevel + ", actual power level = " + opponents[2].GetPowerLevel() + '\n' + opponents[2].ToString());

        fightOffers[2] = new Fight(rewardMoney, rewardRep, opponents[2]);

        UpdateFightOffersTexts();
    }


    // UI
    // Fight offers
    private string GetBehaviourDesc(Robot.RobotBehaviourType rbt)
    {
        string desc = "";

        switch (rbt)
        {
            case Robot.RobotBehaviourType.Balanced:
                desc = "Balanced fighter";
                break;
            case Robot.RobotBehaviourType.AttackHeavy:
                desc = "Extremely aggressive";
                break;
            case Robot.RobotBehaviourType.GuardHeavy:
                desc = "Very cautious";
                break;
            case Robot.RobotBehaviourType.ProjectileHeavy:
                desc = "Long range specialist";
                break;
            default:
                Debug.LogError("Wrong behaviour type : " + rbt);
                break;
        }

        return desc;
    }

    public void UpdateFightOffersTexts()
    {
        fight1RewardMoneyText.text = "+" + fightOffers[0].rewardMoney;
        fight2RewardMoneyText.text = "+" + fightOffers[1].rewardMoney;
        fight3RewardMoneyText.text = "+" + fightOffers[2].rewardMoney;

        fight1RewardReputationText.text = fightOffers[0].rewardReputation + " reputation";
        fight2RewardReputationText.text = fightOffers[1].rewardReputation + " reputation";
        fight3RewardReputationText.text = fightOffers[2].rewardReputation + " reputation";


        fight1PowerLevelText.text = "PWR : " + fightOffers[0].opponent.GetPowerLevel();
        fight2PowerLevelText.text = "PWR : " + fightOffers[1].opponent.GetPowerLevel();
        fight3PowerLevelText.text = "PWR : " + fightOffers[2].opponent.GetPowerLevel();

        fight1BehaviourTypeText.text = GetBehaviourDesc(fightOffers[0].opponent.behaviourType);
        fight2BehaviourTypeText.text = GetBehaviourDesc(fightOffers[1].opponent.behaviourType);
        fight3BehaviourTypeText.text = GetBehaviourDesc(fightOffers[2].opponent.behaviourType);

        /*fight1RobotImage.sprite = AllRobotSprites._instance.GetSpriteByModel(fightOffers[0].opponent.model);
        fight2RobotImage.sprite = AllRobotSprites._instance.GetSpriteByModel(fightOffers[1].opponent.model);
        fight3RobotImage.sprite = AllRobotSprites._instance.GetSpriteByModel(fightOffers[2].opponent.model);*/
    }

    public void UpdateProbaUI()
    {
        int hpm = opponent.GetHighestProbaMove();
        float highestProba = opponent.GetHighestPercent();

        probaImage.sprite = hpm == 0 ? atkSprite : (hpm == 1 ? guardSprite : projSprite);

        float obsMult = 1 + (GameManager._instance.observation * 5f / 100);
        float randDecal = Random.Range(-5f / obsMult, 5f / obsMult);
        float lowBoundary = highestProba + randDecal + Random.Range(-20f / obsMult, -10f / obsMult);
        float highBoundary = highestProba + randDecal + Random.Range(10f / obsMult, 20f / obsMult);

        probaText.text = lowBoundary.ToString("F1") + "%-" + highBoundary.ToString("F1") + "%";
    }

    public void ShowAnnonce(string text, float time, System.Action callback = null)
    {
        StartCoroutine(doShowAnnonce(text, time, callback));
    }

    IEnumerator doShowAnnonce(string text, float time, System.Action callback = null)
    {

        textAnnonce.enabled = true;
        yield return new WaitForEndOfFrame();

        textAnnonce.text = text;
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        textAnnonce.enabled = false;
        if (callback != null)
            callback();
    }
}
