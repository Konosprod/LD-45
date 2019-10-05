using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public enum RobotBehaviourType
    {
        Balanced,           // 33% attack / 33% guard / 33% projectile
        AttackHeavy,        // 60% attack / 20% guard / 20% projectile
        GuardHeavy,         // 20% attack / 60% guard / 20% projectile
        ProjectileHeavy,    // 20% attack / 20% guard / 60% projectile
        AttackOnly,         // 100% attack
        GuardOnly,          // 100% guard
        ProjectileOnly,     // 100% projectile
        AlternateAG,        // attack puis guard en alternance
        AlternateAP,        // attack puis projectile en alternance
        AlternateGP         // guard puis projectile en alternance
    };

    public int hp;
    public int dmg;
    public int def;
    public int skill;

    public RobotBehaviourType behaviourType = 0;
    private bool alternate = true; // true => on fait le premier type de move / false => on fait le deuxième type de move

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public FightManager.MoveType ChoseMove()
    {
        FightManager.MoveType move = FightManager.MoveType.Attack;
        int rand = 0;

        switch (behaviourType)
        {
            case RobotBehaviourType.Balanced:
                move = (FightManager.MoveType)Random.Range(0, 3);
                break;
            case RobotBehaviourType.AttackHeavy:
                rand = Random.Range(0, 10);
                move = rand < 6 ? FightManager.MoveType.Attack : rand < 8 ? FightManager.MoveType.Guard : FightManager.MoveType.Projectile;
                break;
            case RobotBehaviourType.GuardHeavy:
                rand = Random.Range(0, 10);
                move = rand < 6 ? FightManager.MoveType.Guard : rand < 8 ? FightManager.MoveType.Attack : FightManager.MoveType.Projectile;
                break;
            case RobotBehaviourType.ProjectileHeavy:
                rand = Random.Range(0, 10);
                move = rand < 6 ? FightManager.MoveType.Projectile : rand < 8 ? FightManager.MoveType.Guard : FightManager.MoveType.Attack;
                break;
            case RobotBehaviourType.AttackOnly:
                move = FightManager.MoveType.Attack;
                break;
            case RobotBehaviourType.GuardOnly:
                move = FightManager.MoveType.Guard;
                break;
            case RobotBehaviourType.ProjectileOnly:
                move = FightManager.MoveType.Projectile;
                break;
            case RobotBehaviourType.AlternateAG:
                if (alternate)
                    move = FightManager.MoveType.Attack;
                else
                    move = FightManager.MoveType.Guard;
                alternate = !alternate;
                break;
            case RobotBehaviourType.AlternateAP:
                if (alternate)
                    move = FightManager.MoveType.Attack;
                else
                    move = FightManager.MoveType.Projectile;
                alternate = !alternate;
                break;
            case RobotBehaviourType.AlternateGP:
                if (alternate)
                    move = FightManager.MoveType.Guard;
                else
                    move = FightManager.MoveType.Projectile;
                alternate = !alternate;
                break;

            default:
                Debug.LogError("Non.");
                break;
        }

        return move;
    }
}
