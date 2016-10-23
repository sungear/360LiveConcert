using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PlayMyPath : MonoBehaviour {

    public MediaPlayerCtrl _moviePlayer;
    public string _pathToCall;
    public Queue<string> playList;

    private float duration;
    private float time = 0;

	// Use this for initialization
	void Start () {
        print("pathToCall : " + _pathToCall);
        playList = GetComponent<DetectVideos>().videoStages;
        PlayVideo();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (duration >= 0)
        {
            PlayVideo();
        }
        else
        {
            duration -= 1000 * Time.deltaTime;
        }
    }

    private void PlayVideo()
    {
        if (playList.Count > 0)
        {
            if (duration == 0)
            {
                print("NEXT : " + playList.Peek());
                _pathToCall = playList.Peek().Replace("C:/", "C://");
                _moviePlayer.m_strFileName = _pathToCall;
                _moviePlayer.Play();
                playList.Dequeue();
            }
            else
            {
                duration = _moviePlayer.GetDuration();
                playList = GetComponent<DetectVideos>().videoStages;
            }
            duration = _moviePlayer.GetDuration();
        }
    }
}
