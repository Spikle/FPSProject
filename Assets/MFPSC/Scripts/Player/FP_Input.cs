using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Scripts.Managers;

public class FP_Input : MonoBehaviour 
{
    [SerializeField] private bool useMobileInput = true;
    private Inputs mobileInputs = new Inputs();

    public bool UseMobileInput => useMobileInput;

    private void OnEnable()
    {
        SetInputs(EventManager.OnUpdateInputs?.Invoke());
    }

    private void SetInputs(Inputs inputs)
    {
        mobileInputs = inputs;
    }

    public Vector3 MoveInput()
    {
        return mobileInputs.moveJoystick != null ? mobileInputs.moveJoystick.MoveInput() : Vector3.zero;
    }

    public Vector2 LookInput()
    {
        return mobileInputs.lookPad != null ? mobileInputs.lookPad.LookInput() : Vector2.zero;
    }

    public Vector2 ShotInput()
    {
        return mobileInputs.shotButton != null ? mobileInputs.shotButton.MoveInput() : Vector2.zero;
    }

    public bool Shoot()
    {
        return mobileInputs.shotButton != null ? mobileInputs.shotButton.IsPressed() : false;
    }

    public bool Reload()
    {
        return mobileInputs.reloadButton != null ? mobileInputs.reloadButton.OnRelease() : false;
    }

    public bool Run()
    {
        return mobileInputs.runButton != null ? mobileInputs.runButton.IsPressed() : false;
    }

    public bool Jump()
    {
        return mobileInputs.jumpButton != null ? mobileInputs.jumpButton.IsPressed() : false;
    }

    public bool Crouch()
    {
        return mobileInputs.crouchButton != null ? mobileInputs.crouchButton.Toggle() : false;
    }
}

[System.Serializable]
public class Inputs
{
    public FP_Joystick moveJoystick;
    public FP_Lookpad lookPad;
    public FP_Button runButton, jumpButton, crouchButton, shotButton, reloadButton;
    public Button leftTurn, rightTurn;
}