using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUI : UI<PlayUI>
{
    public Button buttonSettings;
    public Button buttonAttack;
	public Joystick joystick;

	protected override void Awake()
	{
		base.Awake();
		buttonSettings.onClick.AddListener(() => { SettingsUI.Open();});
	}
}
