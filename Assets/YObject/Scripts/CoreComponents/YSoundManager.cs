using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class YSoundManager
{
    private List<AudioSource> songSources = new List<AudioSource>();
    private UnityEngine.Coroutine[] songCoroutines = new UnityEngine.Coroutine[5];
    public void Init()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject gameObject = new GameObject("SongChannel" + i, typeof(AudioSource));
            gameObject.transform.SetParent(YGameManager.Instance.transform);
            songSources.Add(gameObject.GetComponent<AudioSource>());
        }
    }
    public void PlaySong(int channel, int id, float volume, bool loop, int startMs, int endMs)
    {
        var songs = Resources.Load<YProjectSettings>("YProjectSettings").songs;
        if (!songs.ContainsKey(id) && id != 0)
            return;
        if (channel > 4 || channel < 0)
            return;
        if (volume < 0 || volume > 2)
            return;

        if (id == 0)
        {
            if (songCoroutines[channel] != null)
            {
                YGameManager.Instance.StopCoroutine(songCoroutines[channel]);
                songSources[channel].Stop();
            }
            return;
        }

        songSources[channel].volume = volume / 2;
        songSources[channel].clip = songs[id];
        var coroutine = YGameManager.Instance.StartCoroutine(IEPlaySong(channel, id, loop, startMs, endMs));
        songCoroutines[channel] = coroutine;
    }
    private IEnumerator IEPlaySong(int channel, int id, bool loop, int startMs, int endMs)
    {
        songSources[channel].Stop();
        songSources[channel].loop = loop;
        songSources[channel].time = startMs / 1000f;
        songSources[channel].Play();
        yield return new WaitForSeconds(endMs > 0 ? (endMs - startMs) / 1000f : songSources[channel].clip.length);
        if (loop)
        {
            var coroutine = YGameManager.Instance.StartCoroutine(IEPlaySong(channel, id, loop, startMs, endMs));
            songCoroutines[channel] = coroutine;
        }
    }
}
