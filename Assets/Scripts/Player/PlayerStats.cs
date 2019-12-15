using System;
using UnityEngine;

public class PlayerStats{
    public event EventHandler OnExprienceChanged;
    public event EventHandler OnLevelChanged;

    static readonly int[] experiencePerLevel = new[] {100, 120, 140, 160, 190, 200, 220, 240, 260, 290, 300, 320, 340, 360, 390, 400, 420, 440, 460, 490, 500, 520, 540, 560, 590, 600, 520, 540, 560, 590, 600};

    int level;
    int sparks;
    int itemNum;
    int experience;

    public PlayerStats(){
        level = 0;
        sparks = 0;
        itemNum = 0;
        experience = 0;
    }

    public void AddExperience(int amount){
        if(IsMaxLevel()) return;
        experience += amount;
        while (!IsMaxLevel() && experience >= GetExperienceToNextLevel(level)){
            level++;
            experience -= GetExperienceToNextLevel(level);
            if(OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }
        if(OnExprienceChanged != null) OnExprienceChanged(this, EventArgs.Empty);
    }

    public void AddSparks(){
        sparks++;
    }
    public void AddItem(){
        itemNum++;
    }
    public int GetSparks(){
        return sparks;
    }
    public int GetLevelNumber(){
        return level;
    }
    public int GetExperience(){
        return experience;
    }
    public int GetItemNum(){
        return itemNum;
    }

    public float GetExperienceNormalized(){
        if(IsMaxLevel()) return 1f;
        return (float)experience / GetExperienceToNextLevel(level);
    }

    public int GetExperienceToNextLevel(int level){
        if(level < experiencePerLevel.Length){
            return experiencePerLevel[level];
        }else{
            Debug.LogError("Level invaild " + level);
            return 100;
        }
    }

    public bool IsMaxLevel(){
        return IsMaxLevel(level);
    }

    public bool IsMaxLevel(int level){
        return level == experiencePerLevel.Length - 1;
    }
}
