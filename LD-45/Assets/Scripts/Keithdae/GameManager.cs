﻿using System.Collections;
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
    }
}
