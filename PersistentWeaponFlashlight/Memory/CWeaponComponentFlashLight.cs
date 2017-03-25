namespace PersistentWeaponFlashlight.Memory
{
    // System
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct CWeaponComponentFlashLight
    {
        //[FieldOffset(0x0008)] public CWeaponComponentFlashLightInfo* WeaponComponentFlashLightInfo;
        [FieldOffset(0x0010)] public CWeapon* OwnerWeapon;
        [FieldOffset(0x0018)] public CObject* OwnerObject;

        [FieldOffset(0x0049)] public byte FlashLightState;

        public CWeaponComponentFlashLightState State
        {
            get { return (CWeaponComponentFlashLightState)FlashLightState; }
            set { FlashLightState = (byte)value; }
        }
    }

    internal enum CWeaponComponentFlashLightState : byte
    {
        None                    = 0b00000000,
        On                      = 0b00000001,
        Unknown2                = 0b00000010,
        Unknown4                = 0b00000100,
        Unknown8                = 0b00001000,
        Unknown16               = 0b00010000,
        Unknown32               = 0b00100000,
        Unknown64               = 0b01000000,
        Unknown128              = 0b10000000,
    }
}
