using UnityEngine;

public class PlayerParams
{
    public bool isAtk;
    public float energyRegen;
    public float energyCost;
    public float spawnTime;
}

public static class Params
{
    public static int matchPerGame = 5;
    public static float timeLimit = 140f;
    public static int energyBarCount = 6;

    public static float energyRegenAtk = .5f; // per sec
    public static float energyRegenDef = .5f; // per sec
    
    public static float energyCostAtk = 2f; // points
    public static float energyCostDef = 2f; // points
    
    public static float spawnTimeAtk = .5f; // sec
    public static float spawnTimeDef = .5f; // sec

    public static float reactivateTimeAtk = 2.5f; // sec
    public static float reactivateTimeDef = 4f; // sec

    public static float speedNormalMultiplierAtk = 1.5f;
    public static float speedNormalMultiplierDef = 1.0f;

    public static float speedCarryMultiplier = .75f; // ATK only
    public static float speedBallMultiplier = 1.5f; // ATK only

    public static float speedReturnMultiplier = 2f; // DEF only
    public static float detectionRangeMultiplier = .35f; // DEF only

    public static Color playerColor = Color.cyan;
    public static Color enemyColor = Color.red;

    public static float spacingBallAtk = 0f; //
    public static float speedSoldierRotate = 2; // rad/s
    public static float ARInitialScale = 200f;

    static PlayerParams setAtkParams()
    {
        PlayerParams p = new PlayerParams();
        p.isAtk = true;
        p.energyRegen = energyRegenAtk;
        p.energyCost = energyCostAtk;
        p.spawnTime = spawnTimeAtk;
        return p;
    }

    static PlayerParams setDefParams()
    {
        PlayerParams p = new PlayerParams();
        p.isAtk = false;
        p.energyRegen = energyRegenDef;
        p.energyCost = energyCostDef;
        p.spawnTime = spawnTimeDef;
        return p;
    }

    public static PlayerParams GetPlayerParams(bool isPlayer)
    {
        if (isPlayer)
        {
            if (GameManager.matchOrd % 2 == 1)
            {
                return setAtkParams();   
            }
            else
            {
                return setDefParams();
            }
        }
        else
        {
            if (GameManager.matchOrd % 2 == 1)
            {
                return setDefParams();
            }
            else
            {
                return setAtkParams();
            }
        }
    }
}
