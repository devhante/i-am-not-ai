using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUI : UI<PlayUI>
{
    public Button buttonSettings;
    public Button buttonKick;
    public Button buttonRun;
    public Joystick moveJoystick;
    public Joystick watchJoystick;

    protected override void Awake()
    {
        base.Awake();
        buttonSettings.onClick.AddListener(() => { SettingsUI.Open(); });
        buttonKick.onClick.AddListener(() => { OnClickButtonKick(); });
        buttonRun.onClick.AddListener(() => { OnClickButtonRun(); });
    }

    private void OnClickButtonKick()
    {
        Debug.Log("OnClickButtonKick");
    }

    private void OnClickButtonRun()
    {
        Debug.Log("OnClickButtonKick");
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