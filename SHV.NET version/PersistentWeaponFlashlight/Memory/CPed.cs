using System.Runtime.InteropServices;

namespace PersistentWeaponFlashlight.Memory
{
	[StructLayout(LayoutKind.Explicit)]
	internal struct CPed
	{
		[FieldOffset(4296)]
		public unsafe CPedWeaponManager* WeaponManager;
	}
}
