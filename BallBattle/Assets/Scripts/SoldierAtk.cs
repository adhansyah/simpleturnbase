using System.Collections;
using UnityEngine;

public class SoldierAtk : MonoBehaviour
{
    public Globals Globals;
    public GameManager GameManager;
    public Soldier Soldier;
    public GameObject ball;
    public GameObject CircleIndicator;

    public bool isCaught;

    Vector3 gatePos;

    void Start()
    {
        GameManager = Soldier.GameManager;
        if (Soldier.isPlayer)
        {
             gatePos = GameManager.enemyGate.transform.position;
        }
        else
        {
            gatePos = GameManager.playerGate.transform.position;
        }
    }

    SoldierAtk GetNearestAtker()
    {
        float minDistance = float.PositiveInfinity;
        SoldierAtk potentialAtker = null;
        foreach (SoldierAtk soldierAtk in GameManager.AtkSoldiers)
        {
            if (!soldierAtk.Soldier.isActive || soldierAtk == this) continue;
            float distance = Vector3.Distance(soldierAtk.transform.position, transform.position);
            if (distance < minDistance)
            {
                potentialAtker = soldierAtk;
                minDistance = distance;
            }
        }
        return potentialAtker;
    }

    IEnumerator Reactivate()
    {
        if (Globals.gamePaused) yield return null;
        yield return new WaitForSeconds(Params.reactivateTimeAtk);
        if (!GameManager.matchOver)
        {
            Soldier.SetStatus(true);
            Soldier.SetKinematic(true);
            isCaught = false;
        }
    }

    void Update()
    {
        if (Globals.gamePaused)
        {
            Soldier.animator.SetBool("Run", false);
            return;
        }
        if (Soldier.isActive)
        {
            Soldier.animator.SetBool("Run", true);

            if (GameManager.ballHolder == null)
            {
                Soldier.animator.speed = 1;

                CircleIndicator.SetActive(false);
                Soldier.SetKinematic(true);
                transform.position = Vector3.MoveTowards(transform.position, ball.transform.position, Params.speedNormalMultiplierAtk * Time.deltaTime);

                if (Vector3.Distance(transform.position, ball.transform.position) <= Params.spacingBallAtk)
                {
                    GameManager.ballHolder = Soldier;
                    ball.transform.parent = Soldier.transform.parent;
                }
            }

            else if (GameManager.ballHolder == Soldier)
            {
                Soldier.animator.speed = Params.speedCarryMultiplier / Params.speedNormalMultiplierAtk;

                CircleIndicator.SetActive(true);
                if (isCaught)
                {
                    CircleIndicator.SetActive(false);
                    SoldierAtk nearestAtker = GetNearestAtker();
                    if (nearestAtker == null)
                    {
                        GameManager.MatchOver(false);
                        Soldier.SetStatus(false);
                        return;
                    }
                    
                    Vector3 targetDirection = nearestAtker.transform.position - transform.position;
                    Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 360, 0f);
                    transform.rotation = Quaternion.LookRotation(newDirection);

                    Soldier.animator.SetBool("Pass", true);
                    Soldier.animator.SetBool("Run", false);
                    Soldier.animator.SetBool("Pass", false);

                    GameManager.ballHolder = null;
                    Soldier.SetStatus(false);
                    Soldier.SetKinematic(true);

                    ball.transform.parent = null;
                    ball.GetComponent<Ball>().passedTo = nearestAtker;
                    
                    StartCoroutine(Reactivate());
                }
                else
                {
                    Soldier.animator.speed = 1;
                    Soldier.SetKinematic(false);
                    transform.position = Vector3.MoveTowards(transform.position, gatePos, Params.speedCarryMultiplier * Time.deltaTime);
                }
            }
            else
            {
                Soldier.animator.speed = 1;

                CircleIndicator.SetActive(false);
                Soldier.SetKinematic(true);
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, gatePos.y, gatePos.z), Params.speedNormalMultiplierAtk * Time.deltaTime);

                if (transform.position.z == gatePos.z)
                {
                    GameManager.AtkSoldiers.Remove(this);

                    Soldier.animator.SetBool("Down", true);
                    Soldier.SetStatus(false);
                    Destroy(gameObject, 1.2f);
                }
            }
        }
    }
}
