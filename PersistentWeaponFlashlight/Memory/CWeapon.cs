namespace PersistentWeaponFlashlight.Memory
{
    // System
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct CWeapon
    {
        [FieldOffset(0x0140)] public CWeaponComponentFlashLight* WeaponComponentFlashLight;
    }
}
