using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
[ExecuteInEditMode]
public class EditorQOLImprovement : MonoBehaviour
{
    // [MenuItem("janTrstek/Clear Game Saves")]
    // private static void ClearGameSaves()
    // {
    // }

    [MenuItem("janTrstek/Group Selected %g")]
    private static void GroupSelected()
    {
        if (!Selection.activeTransform) return;
        var go = new GameObject(Selection.activeTransform.name + " Group");
        Undo.RegisterCreatedObjectUndo(go, "Group Selected");
        go.transform.SetParent(Selection.activeTransform.parent, false);
        foreach (var transform in Selection.transforms) Undo.SetTransformParent(transform, go.transform, "Group Selected");
        Selection.activeGameObject = go;
    }

    [MenuItem("janTrstek/Select Transform Parent %p", false, int.MaxValue)]
    static void SelectParentMenuitem()
    {
        if (Selection.activeTransform.parent != null)
        {
            Selection.activeTransform = Selection.activeTransform.parent;
        }
    }



    [MenuItem("janTrstek/Move Player to Cam Front (linux) %p")]
    private static void MoveToCamView2()
    {
        MoveToCamView();
    }

    [MenuItem("janTrstek/Move Player to Cam Front %F1")]
    private static void MoveToCamView()
    {
        SceneView sceneView = SceneView.lastActiveSceneView;
        Camera cam = sceneView.camera;
        Vector3 vecEulerRot = new Vector3(0, cam.transform.eulerAngles.y, 0);

        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        Player.transform.position = cam.transform.position;
        Player.transform.eulerAngles = vecEulerRot;

        sceneView.Repaint();
    }

    [MenuItem("janTrstek/Look Through Cam %F2")]
    private static void ObjectLookThroughCam()
    {

        SceneView sceneView = SceneView.lastActiveSceneView;
        Camera cam = sceneView.camera;

        foreach (GameObject _go in Selection.objects)
        {
            _go.transform.position = cam.transform.position;
            _go.transform.eulerAngles = cam.transform.eulerAngles;
        }

        sceneView.Repaint();
    }


    [MenuItem("janTrstek/Parent to Last Selection %t")]
    private static void ParentToLastSelection()
    {
        // Can't transfrom with just one or zero objects selected
        if (Selection.objects.Length <= 1) { return; }

        SceneView sceneView = SceneView.lastActiveSceneView;
        GameObject lastSelection = Selection.objects[Selection.gameObjects.Length - 1] as GameObject;

        foreach (GameObject _go in Selection.objects)
        {
            if (_go != lastSelection)
            {
                _go.transform.parent = lastSelection.transform;
                EditorUtility.SetDirty(_go);
                sceneView.Repaint();
            }
        }
    }


    [MenuItem("janTrstek/Reset Editor Camera Rotation O_o")]
    private static void ResetEditorCameraRotation()
    {
        SceneView sceneView = SceneView.lastActiveSceneView;
        sceneView.rotation = Quaternion.identity;
    }




}
