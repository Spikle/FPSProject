using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Scripts.Managers;

public class FP_FastTurn : MonoBehaviour
{
    public float turnSpeed = 5.5F;
    public float turnAngle = 180;
    private Transform thisT;
    public static bool turn;
    private Quaternion targetRotation;

    private void OnEnable()
    {
        SetInputs(EventManager.OnUpdateInputs?.Invoke());
    }

    private void SetInputs(Inputs inputs)
    {
        inputs.leftTurn.onClick.AddListener(LeftTurn);
        inputs.rightTurn.onClick.AddListener(RightTurn);
    }

    void Start () 
    {
        thisT = transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            LeftTurn();
        else if (Input.GetKeyDown(KeyCode.E))
            RightTurn();

        if (thisT.rotation != targetRotation)
        {
            if (turn == true)
                thisT.rotation = Quaternion.RotateTowards(thisT.rotation, targetRotation, turnSpeed * 100 * Time.deltaTime);
        }
        else
            turn = false;
	}


    void LeftTurn()
    {
        targetRotation = Quaternion.AngleAxis(turnAngle, transform.up) * thisT.rotation;
        turn = true;
    }

    void RightTurn()
    {
        targetRotation = Quaternion.AngleAxis(-turnAngle, transform.up) * thisT.rotation;
        turn = true;
    }
}