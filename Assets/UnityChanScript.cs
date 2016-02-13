using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

public class UnityChanScript : MonoBehaviour {

    private static Vector3 CENTER = new Vector3(0, 0, -6);
    private static List<Vector3> positionArray = new List<Vector3> ()
    {
        new Vector3(CENTER.x - 1.0f, 0, CENTER.z + 1.0f),
        new Vector3(CENTER.x - 0.0f, 0, CENTER.z + 1.0f),
        new Vector3(CENTER.x + 1.0f, 0, CENTER.z + 1.0f),
        new Vector3(CENTER.x - 1.0f, 0, CENTER.z - 0.0f),
        new Vector3(CENTER.x - 0.0f, 0, CENTER.z - 0.0f),
        new Vector3(CENTER.x + 1.0f, 0, CENTER.z - 0.0f),
        new Vector3(CENTER.x - 1.0f, 0, CENTER.z - 1.0f),
        new Vector3(CENTER.x - 0.0f, 0, CENTER.z - 1.0f),
        new Vector3(CENTER.x + 1.0f, 0, CENTER.z - 1.0f)
    };
    
    private Animator animator;
    
    private bool waitUsersInput; 
    private bool willMove;
    private bool willStop;
    private bool moved;
    
    private int currentPos;
    private int toPos;
    private int direction;
        
    void Start ()
    {
        waitUsersInput = true;
        willMove = false;
        willStop = false;
        moved = false;
        
        direction = 3;
        
        currentPos = 4;
        toPos = 4;

        animator = GetComponent<Animator>();
    }
    
    void Update ()
    {
        Debug.Log("toPos: " + toPos + ", " + "waitUsersInput: " + waitUsersInput.ToString() + ", " + willMove: " + willMove.ToString());

        if (waitUsersInput)
        {
            waitMoveToPos();
        }
        
        if (willMove)
        {
            Move(currentPos, toPos);
            willMove = false;
        }
        
        willStop = ShouldStopMoving(toPos);
        
        if (willStop)
        {
            animator.SetTrigger("runToIdle");
            willStop = false;
            waitUsersInput = true;
        }
    }
    
    private void waitMoveToPos() {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentPos % 3 != 2)
            {
                toPos = currentPos + 1;
                waitUsersInput = false;
                willMove = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentPos / 3 != 0)
            {
                toPos = currentPos - 3;
                waitUsersInput = false;
                willMove = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentPos % 3 != 0)
            {
                toPos = currentPos - 1;
                waitUsersInput = false;
                willMove = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentPos / 3 != 2)
            {
                toPos = currentPos + 3;
                waitUsersInput = false;
                willMove = true;
            }
        }
    }
        
    private void Move(int currentPos, int toPos)
    {
        Vector3 currentPosition = positionArray[currentPos];
        transform.DOLookAt(positionArray[toPos], 0.5f);
        transform.DOMove(positionArray[toPos], 1);
        animator.SetTrigger("idleToRun");
        this.currentPos = toPos;
    }
    
    private void MoveNorth()
    {
        Vector3 currentPosition = transform.localPosition;
        transform.DOLookAt(new Vector3(0, 0, 10), 0.5f);
        Vector3 moveNorthVector = new Vector3(0, 0, 1);
        transform.DOMove(currentPosition + moveNorthVector, 1);
        animator.SetTrigger("idleToRun");
    }
    
    private void MoveEast()
    {
        Vector3 currentPosition = transform.localPosition;
        transform.DOLookAt(new Vector3(10, 0, 0), 0.5f);
        Vector3 moveEastVector = new Vector3(1, 0, 0);
        transform.DOMove(currentPosition + moveEastVector, 1);
        animator.SetTrigger("idleToRun");
    }
    
    private void MoveSouth()
    {
        Vector3 currentPosition = transform.localPosition;
        transform.DOLookAt(new Vector3(0, 0, -10), 0.5f);
        Vector3 moveSouthVector = new Vector3(0, 0, -1);
        transform.DOMove(currentPosition + moveSouthVector, 1);
        animator.SetTrigger("idleToRun");
    }
    
    private void MoveWest()
    {
        Vector3 currentPosition = transform.localPosition;
        transform.DOLookAt(new Vector3(-10, 0, 0), 0.5f);
        Vector3 moveWestVector = new Vector3(-1, 0, 0);
        transform.DOMove(currentPosition + moveWestVector, 1);
        animator.SetTrigger("idleToRun");
    }
    
    private bool ShouldStopMoving(int toPos)
    {
        return Vector3.Distance(transform.localPosition, positionArray[toPos]) < 0.1f;
    }
    
}
