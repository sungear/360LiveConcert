using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PlayMyPath : MonoBehaviour {

    public MediaPlayerCtrl _moviePlayer;
    public string _pathToCall;
    public List<string> playList;
    private int selectedVideo = 1;

    private float duration = 0;

	// Use this for initialization
	void Start () {
        playList = GetComponent<DetectVideos>().videoStages;
    }
	
	// Update is called once per frame
	void Update ()
    {
        print("PLAYLIST_SIZE : " + playList.Count);
        if (duration <= 0)
        {
            PlayVideo();
        }
        else
        {
            duration -= 1000 * Time.deltaTime;
            //print("DURATION_DECREMENTATION");
        }
        print("DURATION_UPDATE : " + duration);
    }

    private void PlayVideo()
    {
        playList = GetComponent<DetectVideos>().videoStages;
        print("PLAYLIST_SIZE : " + playList.Count);
        if (playList.Count > 0)
        {       if (selectedVideo < playList.Count)
                {
                    print("NEXT : " + playList[selectedVideo]);
                    _pathToCall = playList[selectedVideo].Replace("C:/", "C://");
                    _moviePlayer.m_strFileName = _pathToCall;
                    _moviePlayer.Play();
                    duration = _moviePlayer.GetDuration();
                    if (duration != 0)
                    {
                        selectedVideo++;
                    }
                }
            else
                {
                    print("FINISHED");
                    _pathToCall = playList[0].Replace("C:/", "C://");
                    _moviePlayer.m_strFileName = _pathToCall;
                    _moviePlayer.Play();
                }
        }
    }
}
