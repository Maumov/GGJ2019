using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateTiles : EditorWindow
{
    //Variables
    public static int xAmountTiles;
    public static int zAmountTiles;
    public static int maxHeight;
    public static object Tile;

    public static GameObject terrain;
    List<MonoScript> scripts = new List<MonoScript>();

    [MenuItem("Tools/Tiles Managment")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(CreateTiles));
    }

    private void OnEnable()
    {
        terrain = Resources.Load<GameObject>("Prefab/Tile");
    }

    public void OnGUI()
    {
        GUILayout.Label("Tiles Settings", EditorStyles.boldLabel);
        terrain = (GameObject)EditorGUILayout.ObjectField("Terrain Prefab", terrain, typeof(GameObject), true);
        TerrainSettingsContextMenu();

        EditorGUILayout.Separator();
        AddScriptsContextMenu();
        EditorGUILayout.Separator();

    }

    public static void CreateTilesInScene()
    {
        ClearInSceneTiles();

        GameObject Map = new GameObject();
        Map.name = "Terrain";
        Map.tag = "Terrain";

        for (int i = 0; i < zAmountTiles; i++)
        {
            GameObject row = new GameObject();
            row.name = "Row " + (i + 1);
            row.transform.parent = Map.transform;

            for (int j = 0; j < xAmountTiles; j++)
            {
                // int noise = Random.Range(0, 2);
                GameObject.Instantiate(terrain, new Vector3(j, 0f, i), Quaternion.identity, row.transform).name = "Tile";
            }
        }
    }

    public static void ClearInSceneTiles()
    {
        GameObject[] maps = GameObject.FindGameObjectsWithTag("Terrain");
        foreach(GameObject map in maps)
        {
            GameObject.DestroyImmediate(map);
        }
    }

    public void TerrainSettingsContextMenu()
    {
        GUILayout.Label("Terrain Settings");
        EditorGUILayout.BeginVertical();
        xAmountTiles = (int)EditorGUILayout.IntSlider("Rows", xAmountTiles, 1, 50);
        zAmountTiles = (int)EditorGUILayout.IntSlider("Columns", zAmountTiles, 1, 50);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Generate Terrain"))
        {
            CreateTilesInScene();
        }
        if (GUILayout.Button("Delete Terrain"))
        {
            ClearInSceneTiles();
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    public void AddScriptsContextMenu()
    {
        EditorGUILayout.Separator();
        GUILayout.Label("Add Scripts to Gameobjects", EditorStyles.boldLabel);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Add Components");
        if (GUILayout.Button("+"))
        {
            scripts.Add(null);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginVertical();
        EditorGUILayout.Space();
        for (int i = 0; i < scripts.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            scripts[i] = (MonoScript)EditorGUILayout.ObjectField("Scripts", scripts[i], typeof(MonoScript), true);

            if (GUILayout.Button("Remove"))
            {
                scripts.Remove(scripts[i]);
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add now"))
        {
            for (int i = 0; i < scripts.Count; i++)
            {
                Debug.Log(terrain.GetComponent(scripts[i].GetClass()));
                if(scripts[i] == null)
                {
                    return;
                }

                if (terrain.gameObject.GetComponent(scripts[i].GetClass()) == null)
                {
                    terrain.AddComponent(scripts[i].GetClass());
                }
            }
        }

        if (GUILayout.Button("Remove All"))
        {
            MonoBehaviour[] ScriptToDelete = terrain.GetComponents<MonoBehaviour>();
            foreach(MonoBehaviour script in ScriptToDelete)
            {
                DestroyImmediate(script);
            }
        }
        EditorGUILayout.EndHorizontal();
    }
}