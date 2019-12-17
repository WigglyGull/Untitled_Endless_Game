using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class ExpBarAnimator {
    PlayerStats playerStats;
    bool isAnimating;
    float updateTimer;
    float updateTimerMax;

    public event EventHandler OnExprienceChanged;
    public event EventHandler OnLevelChanged;

    int level;
    int experience;

    public ExpBarAnimator(PlayerStats playerStats){
        SetPlayerStats(playerStats);
        updateTimerMax = .016f;

        FunctionUpdater.Create(() => Update());
    }

    public void SetPlayerStats(PlayerStats playerStats){
        this.playerStats = playerStats;

        level = playerStats.GetLevelNumber();
        experience = playerStats.GetExperience();

        playerStats.OnExprienceChanged += PlayerStats_OnExperienceChanged;
        playerStats.OnLevelChanged += PlayerStats_OnLevelChanged;
    }

    void PlayerStats_OnLevelChanged(object sender, System.EventArgs e){
        isAnimating = true;
    }

    void PlayerStats_OnExperienceChanged(object sender, System.EventArgs e){
        isAnimating = true;
    }

    void Update(){
        if(isAnimating){
            updateTimer += Time.deltaTime;
            while(updateTimer > updateTimerMax){
                updateTimer -= updateTimerMax;
                UpdateAddExperience();
            }
        }
    }

    void UpdateAddExperience(){
        if(level < playerStats.GetLevelNumber()){
            AddExperience();
        }else{
            if(experience < playerStats.GetExperience()){
                AddExperience();
            }else{
                isAnimating = false;
            }
        }
    }

    void AddExperience(){
        experience++;
        if(experience >= playerStats.GetExperienceToNextLevel(level)){
            level++;
            experience = 0;
            if(OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }
        if(OnExprienceChanged != null) OnExprienceChanged(this, EventArgs.Empty);
    }

    public int GetLevelNumber(){
        return level;
    }

    public int GetExperience(){
        return experience;
    }

    public float GetExperienceNormalized(){
        if(playerStats.IsMaxLevel(level)) return 1f;
        return (float)experience / playerStats.GetExperienceToNextLevel(level);
    }
}
