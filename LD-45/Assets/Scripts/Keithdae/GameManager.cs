﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        economy = Economy._instance;
        fightManager = FightManager._instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void EndFight(bool win)
    {
        if(win)
        {
            // You earn the money and reputation from the fight
            economy.money += fightManager.currentFight.rewardMoney;
            economy.reputation += fightManager.currentFight.rewardReputation;
            economy.UpdateInfo();
        }
        else
        {
            // You lose reputation because you lost AND YOU ARE TRASH
            economy.reputation -= fightManager.currentFight.rewardReputation;
        }
    }
}