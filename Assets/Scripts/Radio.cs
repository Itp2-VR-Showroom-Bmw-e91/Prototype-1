using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Radio : MonoBehaviour
{
    [System.Serializable]
    public class RadioStation
    {
        public float minFrequency;
        public float maxFrequency;
        public string streamURL;
    }

    public RadioStation[] stations;

    private AudioSource audioSource;
    private Coroutine streamCoroutine;
    private string currentURL = "";

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = true;
    }

    public void UpdateFrequency(float frequency)
    {
        string newURL = GetStationURL(frequency);

        if (newURL == currentURL)
            return;

        currentURL = newURL;

        if (streamCoroutine != null)
            StopCoroutine(streamCoroutine);

        if (string.IsNullOrEmpty(newURL))
        {
            audioSource.Stop(); // kein Sender → Stille/Rauschen
        }
        else
        {
            streamCoroutine = StartCoroutine(PlayStream(newURL));
        }
    }

    string GetStationURL(float frequency)
    {
        foreach (RadioStation station in stations)
        {
            if (frequency >= station.minFrequency &&
                frequency <= station.maxFrequency)
            {
                return station.streamURL;
            }
        }
        return null;
    }

    IEnumerator PlayStream(string url)
    {
        audioSource.Stop();

        using (UnityWebRequest www =
            UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Radio stream error: " + www.error);
                yield break;
            }

            AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
