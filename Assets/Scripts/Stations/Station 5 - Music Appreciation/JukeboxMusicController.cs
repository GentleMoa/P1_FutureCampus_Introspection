using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JukeboxMusicController : MonoBehaviour
{
    //Public Variables
    public int playhead;
    public List<AudioClip> trackList = new List<AudioClip>();

    //Private Variables
    private bool _pause = true;
    private AudioSource _audioSource;

    void Start()
    {
        //Grab a reference to the audio source
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayOrPause()
    {
        if (_pause == false)
        {
            //Pause the current track
            _audioSource.Pause();

            //Toggle the pause boolean
            _pause = true;
        }
        else if (_pause == true)
        {
            //Play/continue the current track
            PlayCurrentTrack();

            //Toggle the pause boolean
            _pause = false;
        }
    }

    public void NextTrack()
    {
        //Increment playhead by +1
        playhead += 1;

        //Make sure the playhead does not go out of bounds of the tracklist
        LoopPlayhead();

        //Play/continue the current track
        PlayCurrentTrack();
    }

    public void PreviousTrack()
    {
        //Increment playhead by -1
        playhead -= 1;

        //Make sure the playhead does not go out of bounds of the tracklist
        LoopPlayhead();

        //Play/continue the current track
        PlayCurrentTrack();
    }

    private void PlayCurrentTrack()
    {
        //Play/continue the current track
        _audioSource.clip = trackList[playhead];
        _audioSource.Play();
    }

    private void LoopPlayhead()
    {
        if (playhead < 0)
        {
            playhead = trackList.Count - 1;
        }

        if (playhead > trackList.Count - 1)
        {
            playhead = 0;
        }
    }
}
