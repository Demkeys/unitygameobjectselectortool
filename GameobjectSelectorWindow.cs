/// <summary>
/// -----------------------------
/// Gameobject selector window
/// -----------------------------
/// Make sure to place this script in the Editor folder. To open the window click 'MyTools/GameobjectSelector'.
/// This window lets you assign a gameobject to the ObjectToSelect field. Once a gameobject is assigned, if the gameobject
/// has any child gameobjects, four buttons will show up, giving you the following options. 
/// 1.Select Parent gameobject
/// 2.Select First child gameobject
/// 3.Select Last child gameobject
/// 4.Select all child gameobjects
/// This is useful in situations where there are many child gameobjects and you don't wanna have to scroll through the hierarchy
/// every time you wanna select the first, last or all child gameobject.
/// Hope this helps.
/// -----------------------------
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEditor;

public class GameobjectSelectorWindow : EditorWindow {

	static GameObject ObjectToSelect;
	static GameObject OTSFirstChild;
	static GameObject OTSLastChild;

	// Called when you click 'My Tools/GameobjectSelector' from the Menu.
	[MenuItem("My Tools/Gameobject Selector")]
	static void CreateWindow()
	{
		GameobjectSelectorWindow window = (GameobjectSelectorWindow)EditorWindow.
			GetWindow<GameobjectSelectorWindow>();
		window.Show();
		ObjectToSelect = null;
	}

	void OnGUI()
	{
		ObjectToSelect = (GameObject)EditorGUILayout.ObjectField(
			"ObjectToSelect",
			ObjectToSelect,
			typeof(Object),
			true);

		// If ObjectToSelect field was not left empty
		if(ObjectToSelect != null)
		{
			// If parent gameobject actually has any children
			if(ObjectToSelect.transform.childCount > 0)
			{
				OTSLastChild = ObjectToSelect.transform.
					GetChild(ObjectToSelect.transform.childCount - 1).gameObject;
				OTSFirstChild = ObjectToSelect.transform.GetChild(0).gameObject;

				if(GUILayout.Button("Select " + ObjectToSelect.name))
				{
					Selection.activeGameObject = ObjectToSelect;
					EditorApplication.ExecuteMenuItem("Window/Hierarchy");
				}
				if(GUILayout.Button("Select " + ObjectToSelect.name + "'s First Child"))
				{
					Selection.activeGameObject = OTSFirstChild;
					EditorApplication.ExecuteMenuItem("Window/Hierarchy");
				}
				if(GUILayout.Button("Select " + ObjectToSelect.name + "'s Last Child"))
				{
					Selection.activeGameObject = OTSLastChild;
					EditorApplication.ExecuteMenuItem("Window/Hierarchy");
				}
				if(GUILayout.Button("Select all " + ObjectToSelect.name + "'s Children"))
				{
					Object[] temp = new Object[ObjectToSelect.transform.childCount];
					for(int i = 0; i < temp.Length; i++)
					{
						temp[i] = ObjectToSelect.transform.GetChild(i).gameObject;
					}
					Selection.objects = temp;
					EditorApplication.ExecuteMenuItem("Window/Hierarchy");
				}
			}
			// If parent gameobject doesn't have any children
			else
			{
				EditorGUILayout.HelpBox("Gameobject does not have child gameobjects.", MessageType.Info);
			}
		}
		// If ObjectToSelect field was left empty
		else if(ObjectToSelect == null)
		{
			EditorGUILayout.HelpBox("Please add a gameobject to the ObjectToSelect field.", MessageType.Info);
		}
	}
}
