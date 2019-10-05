using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public Fight(int rm, int rr)
        {
            rewardMoney = rm;
            rewardReputation = rr;
        }
    }

    public Fight currentFight;
    public Robot opponent;
    public Robot player;

    // Start is called before the first frame update
    void Start()
    {
        UpdatePlayer();
    }

    public void UpdatePlayer()
    {
        player = GameManager._instance.playerRobot;
    }

    public void CombatPhase(MoveType playerMove)
    {
        MoveType opponentMove = opponent.ChoseMove();

        float rpsMultiplier = 1f; // The damage bonus from RPS
        float opponentRpsMult = 1f;

        // Rock, Paper, Scissors
        if (playerMove == MoveType.Attack)
        {
            switch (opponentMove)
            {
                case MoveType.Attack:   // Neutral result => multiplier is still 1f
                    break;
                case MoveType.Guard:    // Bad matchup
                    rpsMultiplier = 0.5f;
                    opponentRpsMult = 1.5f;
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
        else if (playerMove == MoveType.Guard)
        {
            switch (opponentMove)
            {
                case MoveType.Guard:   // Neutral result => multiplier is still 1f
                    break;
                case MoveType.Projectile:    // Bad matchup
                    rpsMultiplier = 0.5f;
                    opponentRpsMult = 1.5f;
                    break;
                case MoveType.Attack:   // Good matchup
                    rpsMultiplier = 1.5f;
                    opponentRpsMult = 0.5f;
                    break;
                default:
                    Debug.LogError("Wrong moveType : " + opponentMove);
                    break;
            }
        }
        else
        {
            switch (opponentMove)
            {
                case MoveType.Projectile:   // Neutral result => multiplier is still 1f
                    break;
                case MoveType.Attack:    // Bad matchup
                    rpsMultiplier = 0.5f;
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

        // Damage calculation
        int playerDamage = Mathf.FloorToInt(player.dmg * (100 / (100 + opponent.def)) * rpsMultiplier);
        int opponentDamage = Mathf.FloorToInt(opponent.dmg * (100 / (100 + player.def)) * opponentRpsMult);

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
        }
    }
}
