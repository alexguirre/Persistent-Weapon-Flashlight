namespace PersistentWeaponFlashlight.Memory
{
    // System
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct CPed
    {
        [FieldOffset(0x10C8)] public CPedWeaponManager* WeaponManager;
    }
}
