using UnityEngine;
using UnityEngine.Events;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;

public class DetectVideos : MonoBehaviour {
    
    public float detectionTime = 1.0f;
    
    public List<string> videoStages;
    [SerializeField]
    private string[] createdVideos;
    public string path;
    void Awake () {
    videoStages = new List<string>();

        path = Application.dataPath;
        string directoryName = "Videos";

#if UNITY_EDITOR
        path += "/../" + directoryName;
#else
        if (Application.platform == RuntimePlatform.OSXPlayer) {
            path += "/../../";
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer) {
            path += "/../";
        }
#endif

        Directory.CreateDirectory(path);

        InvokeRepeating("detectNewVideo", 0f, detectionTime);
    }

    private void detectNewVideo()
    {
        createdVideos = Directory.GetFiles(path);
        if (createdVideos.Length > 0)
        {
            for (int i = 0; i < createdVideos.Length; i++)
            {
                string file = createdVideos[i];
                if (!videoStages.Contains(file))
                {
                    videoStages.Add(createdVideos[i]);
                }
            }
        }
        else
        {
            print("No files at : " + path);
        }
    }
    // Update is called once per frame
    void Update () {
	}
}
