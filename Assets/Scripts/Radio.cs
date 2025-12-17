using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Radio : MonoBehaviour
{
    [System.Serializable]
    public class Station
    {
        public float frequency;        // Fake-Frequenz
        public string stationName;
        public string streamURL;
    }

    public Station[] stations;
    public float tuningTolerance = 0.15f;
    public FrequencyController frequencyController;

    private AudioSource audioSource;
    private Coroutine playRoutine;
    private Station currentStation;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = true;
    }

    public void UpdateFrequency(float frequency)
    {
        Station station = FindStation(frequency);

        if (station == currentStation)
            return;

        currentStation = station;

        if (playRoutine != null)
            StopCoroutine(playRoutine);

        if (station == null)
        {
            audioSource.Stop(); // später: Rauschen
        }
        else
        {
            playRoutine = StartCoroutine(PlayStream(station.streamURL));
        }
    }

    Station FindStation(float frequency)
    {
        foreach (Station s in stations)
        {
            if (Mathf.Abs(frequency - s.frequency) <= tuningTolerance)
                return s;
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
                Debug.LogError("Radio error: " + www.error);
                yield break;
            }

            audioSource.clip = DownloadHandlerAudioClip.GetContent(www);
            audioSource.Play();
        }
    }

    private void Update()
    {
        if(frequencyController.frequency!=currentStation.frequency||frequencyController.startButton)
            {
            UpdateFrequency(frequencyController.frequency);
            PlayStream(currentStation.streamURL);
            }
    }
}

