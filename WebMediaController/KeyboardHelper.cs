using System.Runtime.InteropServices;
namespace WebMediaController
{
    // Определяем вспомогательный класс для вызова функции keybd_event из Windows API
    public class KeyboardHelper
    {
        // Константы для кодов клавиш
        private const byte VK_MEDIA_PLAY_PAUSE = 0xB3;
        private const byte VK_MEDIA_STOP = 0xB2;
        private const byte VK_MEDIA_PREV_TRACK = 0xB1;
        private const byte VK_MEDIA_NEXT_TRACK = 0xB0;

        // Имитируем нажатие медиа-клавиши
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        // Отправляем событие нажатия медиа-клавиши
        private static void SendMediaKey(byte keyCode)
        {
            keybd_event(keyCode, 0, 0, 0);
            keybd_event(keyCode, 0, 2, 0);
        }

        // Отправляем команду Play/Pause
        public static void SendPlayPause()
        {
            SendMediaKey(VK_MEDIA_PLAY_PAUSE);
        }

        // Отправляем команду Stop
        public static void SendStop()
        {
            SendMediaKey(VK_MEDIA_STOP);
        }

        // Отправляем команду Previous Track
        public static void SendPreviousTrack()
        {
            SendMediaKey(VK_MEDIA_PREV_TRACK);
        }

        // Отправляем команду Next Track
        public static void SendNextTrack()
        {
            SendMediaKey(VK_MEDIA_NEXT_TRACK);
        }
    }
}
