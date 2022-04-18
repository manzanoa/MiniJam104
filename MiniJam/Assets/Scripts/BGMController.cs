using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    public float t0 = 0, t1 = 0;
    public AudioClip audioClip;
    public AudioSource[] musicSources = new AudioSource[2];
    public float loopPoint; // Time in the audio file to loop to (s)
    public float loopLength; // Duration of the loop (s)
    private double time;
    private int nextSource;
    private double nextEventTime;
    private bool play;

    // Start is called before the first frame update
    void Start()
    {
        play = false;
        loopLength = audioClip.length - loopPoint;

        // create two audiosources
        for (int i = 0; i < 2; i++)
        {
            musicSources[i] = gameObject.AddComponent<AudioSource>();
            musicSources[i].clip = audioClip;
            musicSources[i].Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        time = AudioSettings.dspTime;
        t0 = musicSources[0].time;
        t1 = musicSources[1].time;

        // Check 2 seconds ahead, just to be safe if loading/frame issues occur
        if (play && time + 2f > nextEventTime)
        {
            // Schedule the audio to play 
            musicSources[nextSource].time = loopPoint;
            musicSources[nextSource].PlayScheduled(nextEventTime);
            Debug.Log("Scheduled audio source " + nextSource + " to play in 2 seconds");

            // move the event time ahead
            nextEventTime += loopLength;
            nextSource = 1 - nextSource; //Switch to other AudioSource
        }
    }

    public void StartLoop()
    {
        play = true;
        musicSources[0].Play();
        nextEventTime = AudioSettings.dspTime + loopPoint + loopLength;
        nextSource = 1;
    }
}
