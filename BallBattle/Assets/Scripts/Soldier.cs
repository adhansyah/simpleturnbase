using System;
using System.Collections;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public GameManager GameManager;
    public SkinnedMeshRenderer mesh;
    public Material playerMat;
    public Material enemyMat;
    public Material spawningMat;
    public GameObject directionIndicator;
    public Animator animator;

    public bool isActive = false;
    public bool isPlayer = true;

    Material originMat;
    public Vector3 originPos;

    Vector3 lastPos;

    void Awake()
    {
        originPos = transform.parent.position;
        lastPos = transform.position;
        isActive = false;
        mesh.material = spawningMat;
    }

    void Start()
    {
        if (isPlayer)
        {
            StartCoroutine(ChangeColor(GameManager.player.spawnTime));
        }
        else
        {
            StartCoroutine(ChangeColor(GameManager.enemy.spawnTime));
        }
        animator.Play("Idle");
    }

    public void SetAttr(bool _isPlayer)
    {
        if (_isPlayer)
        {
            isPlayer = true;
            originMat = playerMat;
        }
        else
        {
            isPlayer = false;
            originMat = enemyMat;
        }
    }

    IEnumerator ChangeColor(float time)
    {
        mesh.material = spawningMat;
        yield return new WaitForSeconds(time);
        mesh.material = originMat;
        isActive = true;
    }

    public void SetStatus(bool active)
    {
        if (active)
        {
            isActive = true;
            mesh.material = originMat;
        }
        else
        {
            isActive = false;
            mesh.material = spawningMat;
        }
    }
    
    public void SetKinematic(bool kinematic)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (kinematic)
        {
            rigidbody.isKinematic = true;
            rigidbody.detectCollisions = false;
        }
        else
        {
            rigidbody.isKinematic = false;
            rigidbody.detectCollisions = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (gameObject.tag == "SoldierDef" && collision.gameObject.tag == "SoldierAtk")
        {
            SoldierDef soldierDef = transform.parent.GetComponent<SoldierDef>();
            SoldierAtk soldierAtk = collision.transform.parent.GetComponent<SoldierAtk>();

            if (transform.parent.GetComponent<SoldierDef>().target == soldierAtk)
            {
                soldierDef.targetCaught = true;
                soldierAtk.isCaught = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gate")
        {
            if (GameManager.ballHolder == this)
            {
                GameManager.MatchOver(true);
                return;
            }
        }
    }

    void Update()
    {
        if (transform.position != lastPos)
        {
            Vector3 targetDirection = transform.position - lastPos;
            lastPos = transform.position;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, Params.speedSoldierRotate, 0f);

            transform.rotation = Quaternion.LookRotation(newDirection);

            directionIndicator.SetActive(true);
        }
        else
        {
            directionIndicator.SetActive(false);
        }
    }
}
