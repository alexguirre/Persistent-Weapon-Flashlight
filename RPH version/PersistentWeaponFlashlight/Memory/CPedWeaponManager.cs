namespace PersistentWeaponFlashlight.Memory
{
    // System
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Explicit)]
    internal unsafe struct CPedWeaponManager
    {
        [FieldOffset(0x0010)] public CPed* PedOwner;
        [FieldOffset(0x0018)] public uint CurrentWeaponNameHash;
        //[FieldOffset(0x0020)] public CWeaponInfo* CurrentWeaponInfo;
        [FieldOffset(0x0078)] public CObject* CurrentWeaponObject;
    }
}
