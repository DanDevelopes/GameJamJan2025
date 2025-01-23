using Godot;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITower.GlobalSetting
{
    public static class GlobalSettings
    {
        private static int _MasterVolume = 10;
        private static bool _MasterVolumeAltered = false;
        private static object VolumeLock = new object();
        private static object KeyBindLock = new object();

        
        public async static Task SetMasterVolume(int setVolume) 
        {
            lock (VolumeLock)
            {
                _MasterVolume = setVolume;
                _MasterVolumeAltered = true;
            }
        }
        
        public async static Task<int> GetMasterVolume()
        {
            lock (VolumeLock)
            {
                return _MasterVolume;
            }
        }

        public async static Task<bool> IsMasterVolumeAltered()
        {
            lock (VolumeLock)
            {
                return _MasterVolumeAltered;
            }
        }
        
        public static class KeyBindings
        {
            public enum Actions
            {
                scrollup,
                scrolldown,
                scrollleft,
                scrollright,
                zoomin,
                zoomout
            };
            static private Dictionary<Actions, uint> keyBinds;  
            static KeyBindings()
            {
                lock (KeyBindLock)
                {
                    // set defaults
                    keyBinds = new Dictionary<Actions, uint>();
                    keyBinds[Actions.scrollup] = (uint)KeyList.W;
                    keyBinds[Actions.scrolldown] = (uint)KeyList.S;
                    keyBinds[Actions.scrollleft] = (uint)KeyList.A;
                    keyBinds[Actions.scrollright] = (uint)KeyList.D;
                    keyBinds[Actions.zoomin] = (uint)KeyList.R;
                    keyBinds[Actions.zoomout] = (uint)KeyList.F;
                }
                
            }
            public static Dictionary<Actions, uint> GetKeyBindings() 
            {
                lock (KeyBindLock)
                {
                    return keyBinds;
                }
                
            }
            public static void SetKey(uint scancode, Actions action)
            {
                lock (KeyBindLock)
                {
                    keyBinds[action] = scancode;
                }
            }
        }
    }
}
