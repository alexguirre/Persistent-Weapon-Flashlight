namespace PersistentWeaponFlashlight
{
    using System;

#if RPH
    using Rage;
#elif SHVDN
    using GTA;
#endif

    internal static class Util
    {
#if RPH
        public static void Log(string text) => Game.LogTrivial(text);

        public static bool IsWeaponSpecialTwoJustPressed() => Game.IsControlJustPressed(0, GameControl.WeaponSpecialTwo);

        public static IntPtr GetPlayerPedAddress()
        {
            var ped = Game.LocalPlayer.Character;

            return ped ? ped.MemoryAddress : IntPtr.Zero;
        }

        public static void UnloadActivePlugin() => Game.UnloadActivePlugin();
#elif SHVDN
        public static void Log(string text)
        {
            using (System.IO.StreamWriter writer = new System.IO.StreamWriter("persistent-weapon-flashlight.log", true))
            {
                writer.WriteLine(text);
            }
        }

        public static bool IsWeaponSpecialTwoJustPressed() => Game.IsControlJustPressed(Control.WeaponSpecial2);

        public static IntPtr GetPlayerPedAddress()
        {
            var ped = Game.Player.Character;

            return ped.Exists() ? ped.MemoryAddress : IntPtr.Zero;
        }

        public static void UnloadActivePlugin() => throw new Exception("There was an error");
#endif
    }
}
