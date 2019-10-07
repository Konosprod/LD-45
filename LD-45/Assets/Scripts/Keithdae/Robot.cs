using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public static float hpPowerMult = 1f;
    public static float dmgPowerMult = 3f;
    public static float defPowerMult = 3f;
    public static float sklPowerMult = 2f;


    public enum RobotBehaviourType
    {
        Balanced,           // ~33% attack / ~33% guard / ~33% projectile
        AttackHeavy,        // ~60% attack / ~20% guard / ~20% projectile
        GuardHeavy,         // ~20% attack / ~60% guard / ~20% projectile
        ProjectileHeavy     // ~20% attack / ~20% guard / ~60% projectile
    };

    public int maxHp;
    [HideInInspector()]
    public int hp;
    public int dmg;
    public int def;
    public int skill;

    public RobotBehaviourType behaviourType = 0;
    private float thresholdAttack;
    private float thresholdGuard;
    private float thresholdProjectile;

    public void SetRobot(int mHp, int dm, int df, int skl, RobotBehaviourType rbt)
    {
        maxHp = mHp;
        hp = maxHp;
        dmg = dm;
        def = df;
        skill = skl;
        behaviourType = rbt;

        float randomRand = Random.Range(0f, 6f);

        switch (behaviourType)
        {
            case RobotBehaviourType.Balanced:
                thresholdAttack = 33.3f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                thresholdGuard = 33.3f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                thresholdProjectile = 33.3f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                break;
            case RobotBehaviourType.AttackHeavy:
                thresholdAttack = 70f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                thresholdGuard = 15f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                thresholdProjectile = 15f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                break;
            case RobotBehaviourType.GuardHeavy:
                thresholdAttack = 15f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                thresholdGuard = 70f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                thresholdProjectile = 15f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                break;
            case RobotBehaviourType.ProjectileHeavy:
                thresholdAttack = 15f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                thresholdGuard = 15f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                thresholdProjectile = 70f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                break;

            default:
                Debug.LogError("Non.");
                break;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetHighestPercent()
    {
        float highestThreshold = thresholdAttack;
        if (thresholdGuard > highestThreshold)
            highestThreshold = thresholdGuard;
        if (thresholdProjectile > highestThreshold)
            highestThreshold = thresholdProjectile;
        return highestThreshold * 100 / (thresholdAttack + thresholdGuard + thresholdProjectile);
    }

    // 0 => atk / 1 => guard / 2 => proj
    public int GetHighestProbaMove()
    {
        int hpm = 0;
        float highestThreshold = thresholdAttack;
        if (thresholdGuard > highestThreshold)
        {
            highestThreshold = thresholdGuard;
            hpm = 1;
        }
        if (thresholdProjectile > highestThreshold)
        {
            highestThreshold = thresholdProjectile;
            hpm = 2;
        }
        return hpm;
    }

    public FightManager.MoveType ChoseMove()
    {
        FightManager.MoveType move;

        float rand = Random.Range(0f, thresholdAttack + thresholdGuard + thresholdProjectile);

        //Debug.Log("%Atk=" + thresholdAttack + ",%Grd" + thresholdGuard + ",%Prj" + thresholdProjectile + " / Rand=" + rand);

        move = rand < thresholdAttack ? FightManager.MoveType.Attack : (rand < thresholdAttack + thresholdGuard ? FightManager.MoveType.Guard : FightManager.MoveType.Projectile);

        //Debug.Log("Move picked : " + move);

        return move;
    }

    public int GetPowerLevel()
    {
        // Power is HP + dmg * 3 + def * 3 + skill * 2
        return Mathf.FloorToInt(maxHp * hpPowerMult + dmg * dmgPowerMult + def * defPowerMult + skill * sklPowerMult);
    }

    public override string ToString()
    {
        return "MaxHp = " + maxHp + '\n' + "Dmg = " + dmg + '\n' + "Def = " + def + '\n' + "Skl = " + skill;
    }
}
