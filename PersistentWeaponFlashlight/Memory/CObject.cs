namespace PersistentWeaponFlashlight.Memory
{
    // System
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct CObject
    {
        [FieldOffset(0x0340)] public CWeapon* Weapon;
    }
}
