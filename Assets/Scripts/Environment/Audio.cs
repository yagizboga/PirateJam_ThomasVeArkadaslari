using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField] AudioSource audioSource1;
    [SerializeField] AudioSource audioSource2;
    [SerializeField] AudioSource audioSource3;
    [SerializeField] AudioClip a1;

    void Start(){
        audioSource1.clip = a1;
        audioSource1.Play();
        audioSource2.clip = a1;
        audioSource2.Play();
        audioSource3.clip = a1;
        audioSource3.Play();

    }
}
