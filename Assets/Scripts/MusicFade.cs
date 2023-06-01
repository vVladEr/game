using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFade : MonoBehaviour
{
    private AudioSource music;

    private void Start()
    {
        music = GameObject.Find("Soundtrack").GetComponent<AudioSource>();
    }

    public void MakeMusicFade() 
    {
        StartCoroutine(StartFade());
    
    }

    private IEnumerator StartFade() 
    {
        var curTime = 0f;
        var startVolume = music.volume;
        while (curTime < 1f)
        {
            curTime += Time.deltaTime;
            music.volume = Mathf.Lerp(startVolume, 0, curTime/1f);
            yield return null;
        }
        yield break;
    }
}
