using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
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


    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;

        float randomRand = Random.Range(0f, 6f);

        switch (behaviourType)
        {
            case RobotBehaviourType.Balanced:
                thresholdAttack = 33.3f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                thresholdGuard = 33.3f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                thresholdProjectile = 33.3f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                break;
            case RobotBehaviourType.AttackHeavy:
                thresholdAttack = 60f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                thresholdGuard = 20f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                thresholdProjectile = 20f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                break;
            case RobotBehaviourType.GuardHeavy:
                thresholdAttack = 20f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                thresholdGuard = 60f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                thresholdProjectile = 20f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                break;
            case RobotBehaviourType.ProjectileHeavy:
                thresholdAttack = 20f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                thresholdGuard = 20f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                thresholdProjectile = 60f + Random.Range(0f, 11f + randomRand) - 5f - randomRand;
                break;

            default:
                Debug.LogError("Non.");
                break;
        }
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

    public FightManager.MoveType ChoseMove()
    {
        FightManager.MoveType move;

        float rand = Random.Range(0f, thresholdAttack + thresholdGuard + thresholdProjectile);
        move = rand < thresholdAttack ? FightManager.MoveType.Attack : rand < thresholdGuard ? FightManager.MoveType.Guard : FightManager.MoveType.Projectile;

        return move;
    }
}
