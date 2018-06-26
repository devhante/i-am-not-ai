using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUI : UI<PlayUI>
{
    public Button buttonSettings;
    public Button buttonAttack;
    public Joystick moveJoystick;
    public Joystick watchJoystick;

    protected override void Awake()
    {
        base.Awake();
        buttonSettings.onClick.AddListener(() => { SettingsUI.Open(); });
        buttonAttack.onClick.AddListener(() => { OnClickButtonAttack(); });
    }

    private void OnClickButtonAttack()
    {
        Debug.Log("OnClickButtonAttack");
    }

    public Vector3 GetMoveJoystickValue()
    {
        return moveJoystick.Normal;
    }

    public Vector3 GetWatchJoystickValue()
    {
        return watchJoystick.Normal;
    }
}