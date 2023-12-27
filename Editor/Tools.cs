using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace com.davidhopetech.tools.Run_Time.Editor
{
	public class Tools : MonoBehaviour
	{
		static public bool release = true;
#if UNITY_EDITOR
		[SerializeField] private GameObject srcGO;


		[MenuItem("Tools/Setup/Create Default Folders")]
		public static void CreateDefaultFolders()
		{
			CreateFolders("_Project", "Art", "Code", "Scenes");
			AssetDatabase.Refresh();
		}


		public static void CreateFolders(string root, params string[] folders)
		{
			var fullPath = Path.Combine(Application.dataPath, root);

			foreach (var folder in folders)
			{
				Directory.CreateDirectory(Path.Combine(fullPath, folder));
			}
		}
	
		[ContextMenu("Align Transform")]
		private void AlignTransform()
		{
			transform.position = srcGO.transform.position;
			transform.rotation = srcGO.transform.rotation;
		}


		[MenuItem("GameObject/Davids Tools/Move Offset to Parent", false, 10)]
		static void MoveOffsetToParent(MenuCommand menuCommand)
		{
			foreach(GameObject go in Selection.gameObjects)
			{
				go.transform.parent.localPosition += go.transform.localPosition;
				go.transform.localPosition        =  Vector3.zero;
			}
		}


		[MenuItem("GameObject/Davids Tools/Select Game Object Cameras", false, 10)]
		static void SelectGameObjectCameras(MenuCommand menuCommand)
		{
			SelectGameObjectTypes<Camera>();
		}


		[MenuItem("GameObject/Davids Tools/Select Game Object Configurable Joints", false, 10)]
		static void SelectGameObjectConfigurableJoints(MenuCommand menuCommand)
		{
			SelectGameObjectTypes<ConfigurableJoint>();
		}


		[MenuItem("GameObject/Davids Tools/Select Game Object Box Colliders", false, 10)]
		static void SelectGameObjectBoxColliders(MenuCommand menuCommand)
		{
			SelectGameObjectTypes<BoxCollider>();
		}


		[MenuItem("GameObject/Davids Tools/Select Game Object Colliders", false, 10)]
		static void SelectGameObjectColliders(MenuCommand menuCommand)
		{
			SelectGameObjectTypes<Collider>();
		}


		[MenuItem("GameObject/Davids Tools/Select Game Object Lights", false, 10)]
		static void SelectGameObjectLights(MenuCommand menuCommand)
		{
			SelectGameObjectTypes<Light>();
		}


		static void SelectGameObjectTypes<T>() where T : Component
		{
			// get root objects in scene
			var typedGOs = new List<GameObject>();

			// iterate root objects and do something
			for(int i = 0; i < Selection.gameObjects.Length; ++i)
			{
				GameObject go = Selection.gameObjects[i];

				var components = go.GetComponentsInChildren<T>(true);
				foreach(T component in components)
				{
					typedGOs.Add(component.gameObject);
					Debug.Log("\tRigid Body:" + go.name + " - " + component.name);
				}
			}

			Selection.objects = typedGOs.ToArray();

		}


		[MenuItem("GameObject/Davids Tools/Select All Box Colliders", false, 10)]
		static void SelectAllBoxColliders(MenuCommand menuCommand)
		{
			SelectAllGameObjectTypes<BoxCollider>();
		}


		[MenuItem("GameObject/Davids Tools/Select All Lights", false, 10)]
		static void SelectAllLightsBodies(MenuCommand menuCommand)
		{
			SelectAllGameObjectTypes<Light>();
		}


		[MenuItem("GameObject/Davids Tools/Select All Rigid Bodies", false, 10)]
		static void SelectAllRigideBodies(MenuCommand menuCommand)
		{
			SelectAllGameObjectTypes<Rigidbody>();
		}


		[MenuItem("GameObject/Davids Tools/Select All Mesh Colliders", false, 10)]
		static void SelectAllMeshColliders(MenuCommand menuCommand)
		{
			SelectAllGameObjectTypes<MeshCollider>();
		}


		[MenuItem("GameObject/Davids Tools/Select All Colliders", false, 10)]
		static void SelectColliders(MenuCommand menuCommand)
		{
			SelectAllGameObjectTypes<Collider>();
		}


		static void SelectAllGameObjectTypes<T>() where T : Component
		{
			// get root objects in scene
			List<GameObject> rootObjects = new List<GameObject>();
			SceneManager.GetActiveScene().GetRootGameObjects(rootObjects);

			var typedGOs = new List<GameObject>();

			// iterate root objects and do something
			for (int i = 0; i < rootObjects.Count; ++i)
			{
				GameObject go = rootObjects[i];

				var components = go.GetComponentsInChildren<T>(true);
				foreach (T component in components)
				{
					typedGOs.Add(component.gameObject);
					Debug.Log("\tRigid Body:" + go.name + " - " + component.name);
				}
			}

			Selection.objects = typedGOs.ToArray();

		}


		static void RemoveComponents<T>() where T: Component
		{
			GameObject[] gos = Selection.gameObjects;

			foreach (var go in gos)
			{
				Debug.Log("Game Object: " + go.name);
				T[] meshColiders = go.GetComponentsInChildren<T>(true);
				foreach (T mc in meshColiders)
				{
					Debug.Log("\tMesh Collider:" + mc.name);
					DestroyImmediate(mc);
				}
			}
		}


		[MenuItem("GameObject/Davids Tools/Remove Mesh Colliders", false, 10)]
		static void RemoveMeshColliders(MenuCommand menuCommand)
		{
			RemoveComponents<MeshCollider>();
		}


		[MenuItem("GameObject/Davids Tools/Remove Colliders", false, 10)]
		static void RemoveColliders(MenuCommand menuCommand)
		{
			RemoveComponents<Collider>();
		}
#endif
	}
}

