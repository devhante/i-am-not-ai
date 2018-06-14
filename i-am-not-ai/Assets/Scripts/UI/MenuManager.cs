using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	public static MenuManager Instance { get; private set; }

	//Play
	public PlayMenu PlayMenuPrefab;
	public SettingsMenu SettingsMenuPrefab;

	public Stack<Menu> menuStack = new Stack<Menu>();

	private void Awake()
	{
		Instance = this;
		OpenMenu<PlayMenu>();
	}

	private void OnDestroy()
	{
		Instance = null;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && menuStack.Count > 0)
		{
			menuStack.Peek().OnBackPressed();
		}
	}

	public void OpenMenu<T>() where T : Menu
	{
		var prefab = GetPreFab<T>();
		var instance = Instantiate<Menu>(prefab, transform);

		//top 메뉴를 deactivate한다.
		if (menuStack.Count > 0)
			menuStack.Peek().gameObject.SetActive(false);

		menuStack.Push(instance);
	}

	public void CloseMenu()
	{
		var instance = menuStack.Pop();
		Destroy(instance.gameObject);

		//top 메뉴를 Activate한다.
		if (menuStack.Count > 0)
			menuStack.Peek().gameObject.SetActive(true);
	}

	public T GetPreFab<T>() where T : Menu
	{
		//Game
		if (typeof(T) == typeof(PlayMenu))
			return PlayMenuPrefab as T;

		if (typeof(T) == typeof(SettingsMenu))
			return SettingsMenuPrefab as T;

		//타입이 prefeb에 없는거면 Excpetion 반환
		throw new MissingReferenceException();
	}
}
