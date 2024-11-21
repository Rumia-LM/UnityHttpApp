using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class MusicController : MonoBehaviour
{
    AudioSource myAudio;
    public Slider volSlider;
    public TMP_Dropdown musicSelect;
    Dictionary<int, string> dict = new Dictionary<int,string>();
    // Start is called before the first frame update
    void Start()
    {
        dict.Add(0, "https://joytas.net/php/futta-fly3t.wav");
        dict.Add(1, "https://joytas.net/php/futta-rainbow3t.wav");
        dict.Add(2, "https://joytas.net/php/futta-snowman3t.wav");

        myAudio = GetComponent<AudioSource>();
        myAudio.volume=volSlider.value;
        StartCoroutine(GetAudioClip(dict[0]));
    }

    IEnumerator GetAudioClip(string musicUrl){
        using(var uwr = UnityWebRequestMultimedia.GetAudioClip(musicUrl, AudioType.WAV)){
            yield return uwr.SendWebRequest();
            if(uwr.result != UnityWebRequest.Result.Success){
                Debug.LogError(uwr.error);
                yield break;
            }
            myAudio.clip = DownloadHandlerAudioClip.GetContent(uwr);
        }
    }

    public void PlayBt(){
        myAudio.Play();
    }
    public void PauseBt(){
        myAudio.Pause();
    }
    public void StopBt(){
        myAudio.Stop();
    }
    public void OnVolChange(){
        myAudio.volume=volSlider.value;
    }
    public void ChangeMusic(){
        StartCoroutine(GetAudioClip(dict[musicSelect.value]));
    }
}
