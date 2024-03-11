using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(LevelEditor))]
public class ELevelEditor : Editor
{
    private LevelEditor _levelEditor;

    public override void OnInspectorGUI()
    {
        //
        DrawDefaultInspector();
        //Set current position to animation position
        if (GUILayout.Button("SpawnRow"))
        {
            _levelEditor.SetGrid();
            SceneView.RepaintAll();
        }
        if (GUILayout.Button("RemoveRow"))
        {
            _levelEditor.RemoveGrid();
            SceneView.RepaintAll();
        }




    }
    private void OnSceneGUI()
    {
        LevelEditor levelEditor = (LevelEditor)target;

        //Handles.DrawWireDisc
        //Handles.DrawGizmos(Gizmos.DrawCube(levelEditor.transform.p));
        
    }

    private void OnEnable()
    {
        if (!_levelEditor)
        {
            //target is object
            _levelEditor = target as LevelEditor;
        }
    }
}
