using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDetect : MonoBehaviour
{
    public SoldierDef soldierDef;
    GameManager GameManager;

    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SoldierAtk")
        {
            SoldierAtk soldier = other.transform.parent.GetComponent<SoldierAtk>();
            if (GameManager.ballHolder == soldier.Soldier)
            {
                soldierDef.target = soldier;
            }
        }
    }
}
