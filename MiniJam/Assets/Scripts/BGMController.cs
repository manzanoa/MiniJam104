using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{
    public float t0 = 0, t1 = 0;
    public AudioSource[] musicSources;
    public const float loopPoint = 16.004f; // Time in the audio file to loop to (s)
    public const float loopLength = 211.249f; // Duration of the loop (s)
    private double time;
    private int nextSource;
    private double nextEventTime;

    // Start is called before the first frame update
    void Start()
    {
        // musicSources[0].time = 200f;
        musicSources[0].Play();
        nextEventTime = AudioSettings.dspTime + loopPoint + loopLength - musicSources[0].time;
        nextSource = 1;
    }

    // Update is called once per frame
    void Update()
    {
        time = AudioSettings.dspTime;
        t0 = musicSources[0].time;
        t1 = musicSources[1].time;

        // Check 2 seconds ahead, just to be safe if loading/frame issues occur
        if (time + 2f > nextEventTime)
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
}
