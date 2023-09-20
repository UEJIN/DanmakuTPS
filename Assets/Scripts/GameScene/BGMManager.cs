using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private AudioSource audioSource;

    // 複数のBGMを受け取っておく
    [SerializeField]
    private AudioClip[] clips;

    void Start()
    {
        // AudioSource コンポーネントの取得
        audioSource = GetComponent<AudioSource>();

        // 曲の変更(clipsからランダムに選ばれる)
        audioSource.clip = clips[Random.Range(0, clips.Length)];

        // 再生
        audioSource.Play();
    }
}
