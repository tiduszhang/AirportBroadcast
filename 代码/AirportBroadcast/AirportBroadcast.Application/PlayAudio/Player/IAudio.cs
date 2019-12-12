using System;
namespace AirportBroadcast.PlayAudio
{
    interface IAudio
    {
        byte DeviceId { get; set; }
        string DeviceList { get; }
        int Duration { get; }
        event Audio.EndofPlayEventHandle EndofPlayEvent;
        int Left { get; }
        void Pause();
        int play(string path, int port);
        int PlayState { get; }
        int Position { get; }
        int Right { get; }
        event Audio.StatusofPlayEventHandle StatusPlayEvent;
        void stop();
    }
}
