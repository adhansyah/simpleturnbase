using UnityEngine;

public class Ball : MonoBehaviour
{
    public Globals Globals;
    public float moveSpeed = Params.speedBallMultiplier;
    public SoldierAtk passedTo;

    void Update()
    {
        if (Globals.gamePaused) return;
        if (transform.parent == null && passedTo != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, passedTo.transform.position, moveSpeed * Time.deltaTime);
            if (transform.position == passedTo.transform.position)
            {
                passedTo = null;
            }
        }
    }

    
}
