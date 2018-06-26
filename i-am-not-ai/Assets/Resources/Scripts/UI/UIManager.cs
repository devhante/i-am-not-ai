using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance { get; private set; }

	public PlayUI PlayUIPrefab;
	public SettingsUI SettingsUIPrefab;

	public Stack<UI> UIStack = new Stack<UI>();

	private void Awake()
	{
		Instance = this;
		OpenUI<PlayUI>();
	}

	private void OnDestroy()
	{
		Instance = null;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && UIStack.Count > 0)
		{
			UIStack.Peek().OnBackPressed();
		}
	}

	public void OpenUI<T>() where T : UI
	{
		var prefab = GetPreFab<T>();
		var instance = Instantiate<UI>(prefab, transform);

		UIStack.Push(instance);
	}

	public void CloseMenu()
	{
		var instance = UIStack.Pop();
		Destroy(instance.gameObject);
	}

	public T GetPreFab<T>() where T : UI
	{
		//Game
		if (typeof(T) == typeof(PlayUI))
			return PlayUIPrefab as T;

		if (typeof(T) == typeof(SettingsUI))
			return SettingsUIPrefab as T;

		throw new MissingReferenceException();
	}
}
