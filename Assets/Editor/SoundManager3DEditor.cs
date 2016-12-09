//using UnityEngine;
//using System.Collections;
//using UnityEditor;
//[CustomEditor(typeof(SoundManager3D))]
//public class SoundManager3DEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        SoundManager3D soundManager3D = target as SoundManager3D;

//        soundManager3D.globalVolumeControl = EditorGUILayout.Toggle("GlobalVolumeControl", soundManager3D.globalVolumeControl);
//        if (soundManager3D.globalVolumeControl)
//        {
//            soundManager3D.volume = EditorGUILayout.Slider("Volume", soundManager3D.volume, 0.0f, 1.0f);
//        }
//        base.OnInspectorGUI();
//    }
//}