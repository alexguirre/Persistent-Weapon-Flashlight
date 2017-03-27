using System.Runtime.InteropServices;

namespace PersistentWeaponFlashlight.Memory
{
	[StructLayout(LayoutKind.Explicit)]
	internal struct CPedWeaponManager
	{
		[FieldOffset(16)]
		public unsafe CPed* PedOwner;

		[FieldOffset(24)]
		public uint CurrentWeaponNameHash;

		[FieldOffset(120)]
		public unsafe CObject* CurrentWeaponObject;
	}
}
