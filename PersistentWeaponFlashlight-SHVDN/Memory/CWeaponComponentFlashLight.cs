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

        [FieldOffset(0x0049)] public byte State;

        public bool IsOn { get { return (State & 1) != 0; } }
    }
}
