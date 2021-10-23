using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundController : MonoBehaviour
{
    public AudioClip jump;
    public AudioClip scoreHighlight;
    private AudioSource audioplay;

    // Start is called before the first frame update
    private void Start()
    {
        audioplay = GetComponent<AudioSource>();
    }

    public void PlayScoreH()
    {
        audioplay.PlayOneShot(scoreHighlight);
    }
   
    public void PlayJump()
    {
        audioplay.PlayOneShot(jump);
    }
}
