using System.Collections;
using UnityEngine;

public class SoldierDef : MonoBehaviour
{
    public Globals Globals;
    public GameManager GameManager;
    public Soldier Soldier;
    public GameObject DetectionArea;

    public SoldierAtk target;
    public bool targetCaught;

    bool returning = false;

    void Start()
    {
        GameManager = Soldier.GameManager;
        StartCoroutine(ActivateDetection());
    }

    IEnumerator ActivateDetection()
    {
        if (Globals.gamePaused) yield return null;
        yield return new WaitForSeconds(Params.spawnTimeDef);
        DetectionArea.SetActive(true);
    }

    IEnumerator Reactivate()
    {
        if (Globals.gamePaused) yield return null;
        yield return new WaitForSeconds(Params.reactivateTimeDef);
        if (!GameManager.matchOver)
        {
            returning = true;
            targetCaught = false;
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
            if (target != null)
            {
                Soldier.animator.SetBool("Run", true);
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Params.speedNormalMultiplierDef * Time.deltaTime);

                DetectionArea.SetActive(false);

                if (targetCaught)
                {
                    Soldier.animator.SetBool("Run", false);
                    Soldier.SetStatus(false);
                    Soldier.SetKinematic(true);
                    StartCoroutine(Reactivate());
                }
                else
                {
                    if (target.isCaught)
                    {
                        Soldier.SetStatus(false);
                        Soldier.SetKinematic(true);
                        returning = true;
                    }
                }
            }

        }
        if (returning)
        {
            Soldier.animator.SetBool("Run", true);
            transform.position = Vector3.MoveTowards(transform.position, Soldier.originPos, Params.speedReturnMultiplier * Time.deltaTime);
            if (transform.position == Soldier.originPos)
            {
                Soldier.animator.SetBool("Run", false);
                returning = false;
                target = null;
                Soldier.SetStatus(true);
                Soldier.SetKinematic(false);
                DetectionArea.SetActive(true);
            }
        }
    }
}
