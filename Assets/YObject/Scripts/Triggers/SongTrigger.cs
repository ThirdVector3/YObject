using GeometryDashAPI.Data;
using UnityEngine;

public class SongTrigger : YTrigger
{
    private int id;
    private float volume;
    private int channel;
    private bool loop;
    private int startMs;
    private int endMs;
    private int fadeInMs;
    private int fadeOutMs;



    public SongTrigger(int id, int channel, float volume, bool loop, int startMs, int endMs, int fadeInMs, int fadeOutMs) : base()
    {
        this.id = id;
        this.channel = channel;
        this.volume = volume;
        this.loop = loop;
        this.startMs = startMs;
        this.endMs = endMs;
        this.fadeInMs = fadeInMs;
        this.fadeOutMs = fadeOutMs;
    }

    public override void Activate()
    {
        YSoundManager.Instance.PlaySong(channel, id, volume, loop, startMs, endMs);
    }

    public override string GetString(Vector2? pos, int[] groups = null, int[] groupsParent = null)
    {
        return $"1,1934,2,{pos.Value.x},3,{pos.Value.y}{GetGroupsString(groups, groupsParent)},155,1,62,1,87,1,13,1,36,1,392,{id},406,{volume},413,{(loop ? 1 : 0)},408,{startMs},409,{fadeInMs},410,{endMs},411,{fadeOutMs},421,1,422,0.5,10,0.5,432,{channel};";
    }
}
