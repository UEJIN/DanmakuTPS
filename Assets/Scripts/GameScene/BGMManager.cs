using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    private AudioSource audioSource;

    // ������BGM���󂯎���Ă���
    [SerializeField]
    private AudioClip[] clips;

    void Start()
    {
        // AudioSource �R���|�[�l���g�̎擾
        audioSource = GetComponent<AudioSource>();

        // �Ȃ̕ύX(clips���烉���_���ɑI�΂��)
        audioSource.clip = clips[Random.Range(0, clips.Length)];

        // �Đ�
        audioSource.Play();
    }
}
