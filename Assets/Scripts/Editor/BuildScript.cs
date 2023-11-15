using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEditor;
using UnityEngine;
using System.IO;
using System;




public class BuildScript : MonoBehaviour
{


    

    static string getTimeSinceEpoch()
    {
        return (Math.Floor((DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds)).ToString();
    }

    static void PerformBuild()
    {
        System.Random rand = new System.Random();

        String[] arguments = System.Environment.GetCommandLineArgs();

        Debug.Log("ARGUMENTOS :" + arguments.ToString());

        string directoryPath = "C:/DEVOPS/Builds/Logs";
        string logPath = directoryPath + "/log" + getTimeSinceEpoch() +".txt";


        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        
        File.Create(logPath);

        string[] scenes = { "Assets/escenita.unity" };

        BuildPlayerOptions AndroidOptions = new BuildPlayerOptions();
        AndroidOptions.scenes = scenes;
        AndroidOptions.target = BuildTarget.Android;
        AndroidOptions.options = BuildOptions.CompressWithLz4;
        AndroidOptions.locationPathName = "C:/DEVOPS/Builds/Android/prueba.apk";

        BuildPlayerOptions iOSOptions = new BuildPlayerOptions();
        AndroidOptions.scenes = scenes;
        AndroidOptions.target = BuildTarget.iOS;
        AndroidOptions.options = BuildOptions.None;
        AndroidOptions.locationPathName = "C:/DEVOPS/Builds/iOS";


        try
        {
            Debug.Log("prueba");

            BuildReport report = BuildPipeline.BuildPlayer(
                AndroidOptions
             );

           /* BuildPipeline.BuildPlayer(
            defaultScene,
            "C:/DEVOPS/Builds/iOS",
            BuildTarget.iOS,
            BuildOptions.None

            );*/

            StreamWriter sw = File.AppendText(logPath);

            foreach (string arg in arguments) {
                sw.WriteLine("Params : " + arg);
            }

            sw.WriteLine(report.summary.totalTime);


            sw.Close();
        }
        catch(Exception e)
        {

        }




    }
}
