using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

namespace JNE.CustomEditor
{
	public class CustomMenu : MonoBehaviour
	{
		private static string PrefabPath = "ReservedPrefabs/";

		[MenuItem("JNE/UI/Popup &_p")]
		public static void MakePopup()
		{
			MakeCopyOfPrefab ("Popup");
		}

		[MenuItem("JNE/UI/Button &_b")]
		public static void MakeButton()
		{
			MakeCopyOfPrefab ("Button");
		}

		[MenuItem("JNE/UI/TabButton &_t")]
		public static void MakeTabButton()
		{
			MakeCopyOfPrefab ("Tabbutton");
		}

		[MenuItem("JNE/UI/Scroll View &_s")]
		public static void MakeScrollView()
		{
			MakeCopyOfPrefab ("Scroll View");
		}

		private static void MakeCopyOfPrefab(string name)
		{
			GameObject selectedObj = Selection.activeGameObject;

			GameObject copy = GameObject.Instantiate (Resources.Load(PrefabPath + name), selectedObj.transform) as GameObject;

			if(selectedObj != null)
			{
				copy.transform.SetParent (selectedObj.transform);
			}

			copy.name = name;
		}
	}

}
#endif