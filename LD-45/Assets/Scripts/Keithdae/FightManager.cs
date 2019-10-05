﻿using System.Collections;
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

    public enum MoveType
    {
        Attack,
        Guard,
        Projectile
    };
    
    public Robot opponent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CombatPhase(MoveType playerMove)
    {
        //MoveType opponentMove = opponent.ChoseMove();
    }
}
