using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public int experience;
    public int currentLevel;
    public Action OnLevelUp;

    public int maxExp;
    public int _maxLevel = 25;

    public Level(int level, Action OnLvlup)
    {
        maxExp = GetXpForLevel(_maxLevel);
        currentLevel = level;
        experience = GetXpForLevel(level);
        OnLevelUp = OnLvlup;
    }

    public int GetXpForLevel(int level) 
    {
        if (level > _maxLevel) //todo: Throw an exception dependant of game desing
            return 0;

        int firstPass = 0;
        int secondpass = 0;

        for (int levelCycle = 1; levelCycle < level; levelCycle++)
        {
            firstPass += (int)Mathf.Floor(levelCycle + (300.0f * Mathf.Pow(2.0f, levelCycle / 7.0f)));
            secondpass = firstPass / 4;
        }

        if (secondpass > maxExp && maxExp != 0) //todo: Throw an exception dependant of game desing
            return maxExp;

        if (secondpass < 0) //todo: Throw an exception dependant of game desing
            return maxExp;

        return secondpass;
    }

    public int GetLevelForXp(int xp)
    {
        if (xp > maxExp)
            return maxExp;

        int firstPass = 0;
        int secondpass = 0;

        for (int levelCycle = 1; levelCycle < _maxLevel; levelCycle++)
        {
            firstPass += (int)Mathf.Floor(levelCycle + (300.0f * Mathf.Pow(2.0f, levelCycle / 7.0f)));
            secondpass = firstPass / 4;
            if (secondpass > xp)
                return levelCycle;       
        }

        if (xp > secondpass) //todo: Throw an exception dependant of game desing
            return _maxLevel;

        return 0; //todo: Throw an exception dependant of game desing
    }

    public bool AddXp(int amount)
    {
        if (amount + experience < 0 /*would mean error*/ || experience > maxExp) /* or already reached maxExp*/
        {
            if (experience > maxExp)
                experience = maxExp; // so set exp to maxExp
            return false; // returning false means we haven't levelup
        }

        int oldLevel = GetLevelForXp(experience); // hold the lvl we currently are before adding the xp
        experience += amount; // add the xp
        if (oldLevel < GetLevelForXp(experience)) // recheck the lvl with the added xp, if it returns true, it means we lvl up
        { // means if(stuff == true) so we lvled up
            if (currentLevel < GetLevelForXp(experience))
            {
                currentLevel = GetLevelForXp(experience);
                if (OnLevelUp != null) //meaning we did pass in a function that we do wanna fire when we do lvl up
                    OnLevelUp.Invoke(); // fires the function that we inputed
                return true; // we did lvl up
            }
        }
        return false; // that amount of xp did not make us lvl up
    }
}
