using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.Build;

public class Builder :MonoBehaviour
{
    static string[] SCENES = FindEnabledEditorScenes();

    static string APP_NAME = PlayerSettings.productName;
    static string TARGET_DIR = "Builds";

    public static BuildTarget target=BuildTarget.StandaloneWindows64;


    static void Build()
    {
        target = BuildTarget.iOS;
        string target_dir = "iOS" + APP_NAME + ".ipa";
        GenericBuild(SCENES, TARGET_DIR + "/" + target_dir, target, BuildOptions.None);
    }

    [MenuItem("Builds/Standalone/MacOS")]
    static void PerformMacOSXBuild()
    {
        string target_dir = "MacOSX"+APP_NAME + ".app";
        GenericBuild(SCENES, TARGET_DIR + "/" + target_dir, BuildTarget.StandaloneOSX, BuildOptions.None);
    }

    [MenuItem("Builds/Standalone/Windows64")]
    static void PerformWin64Build()
    {
        string target_dir = "Win64"+APP_NAME+"/"+ APP_NAME+".exe";
        GenericBuild(SCENES, TARGET_DIR + "/" + target_dir, BuildTarget.StandaloneWindows64, BuildOptions.None);
    }

    [MenuItem("Builds/Android/Unsigned")]
    static void PerformAndroidBuild()
    {
        string target_dir = "Android" + APP_NAME + "/" + APP_NAME + ".apk";
        GenericBuild(SCENES, TARGET_DIR + "/" + target_dir, BuildTarget.Android, BuildOptions.None);
    }


    #region internal functions
    public static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();
        foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }
        return EditorScenes.ToArray();
    }

    static void GenericBuild(string[] scenes, string target_dir, BuildTarget build_Target, BuildOptions build_options)
    {
        EditorUserBuildSettings.SwitchActiveBuildTarget(build_Target);
       /* UnityEditor.Build.Reporting.BuildReport rep = */BuildPipeline.BuildPlayer(scenes, target_dir, build_Target, build_options);
      /*  if (rep.summary.totalErrors > 0)
        {
            throw new Exception("BuildPlayer failure:" + rep.summary.result);
        }*/

    }
    #endregion
}
