namespace WebMediaController
{
    public class MediaController
    {
        public static void NextTrack()
        {
            KeyboardHelper.SendNextTrack();
        }
        public static void PreviousTrack()
        {
            KeyboardHelper.SendPreviousTrack();
        }
        public static void PlayPause()
        {
            KeyboardHelper.SendPlayPause();
        }
    }
}
