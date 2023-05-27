using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : MonoBehaviour
{
    private AudioSource SoundtrackAudio;
    [SerializeField] public float TimeOffset;
    [SerializeField] public float RightTimeWindow;
    [SerializeField] public float DampingTime;
    [SerializeField] public float BPM = 120;
    public HashSet<Vector2> CapturedPositions = new HashSet<Vector2>();

    void Start()
    {
        SoundtrackAudio = GameObject.Find("Soundtrack").GetComponent<AudioSource>();
    }

    public float Time => SoundtrackAudio.time + TimeOffset;
}
