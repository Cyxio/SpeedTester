using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioScript : MonoBehaviour
{
    int BopH;
    int SoundID;
    int BopM;
    int SoundID1;
    int BopL;
    int SoundID2;

    // Start is called before the first frame update
    void Start()
    {
        AndroidNativeAudio.makePool();
        BopH = AndroidNativeAudio.load("Sounds/BopH.wav");
        BopM = AndroidNativeAudio.load("Sounds/BopM.wav");
        BopL = AndroidNativeAudio.load("Sounds/BopL.wav");
        GameObject.Find("Sounds").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVol", 1f);
    }

    public void PlaySound(int num)
    {
        switch (num)
        {
            case 0: SoundID = AndroidNativeAudio.play(BopH, PlayerPrefs.GetFloat("SoundsVol", 1f)); break;
            case 1: SoundID1 = AndroidNativeAudio.play(BopM, PlayerPrefs.GetFloat("SoundsVol", 1f)); break;
            case 2: SoundID2 = AndroidNativeAudio.play(BopL, PlayerPrefs.GetFloat("SoundsVol", 1f)); break;
        }
    }

    public void SetSliders()
    {
        Slider slider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat("MusicVol");
        slider = GameObject.Find("SoundsSlider").GetComponent<Slider>();
        slider.value = PlayerPrefs.GetFloat("SoundsVol");
    }

    public void SoundsUpdate(Slider slider)
    {
        PlayerPrefs.SetFloat("SoundsVol", slider.value);
    }

    public void MusicUpdate(Slider slider)
    {
        PlayerPrefs.SetFloat("MusicVol", slider.value);
        GameObject.Find("Sounds").GetComponent<AudioSource>().volume = slider.value;
    }


    void OnApplicationQuit()
    {
        AndroidNativeAudio.unload(BopH);
        AndroidNativeAudio.unload(BopM);
        AndroidNativeAudio.unload(BopL);
        AndroidNativeAudio.releasePool();
    }
}
