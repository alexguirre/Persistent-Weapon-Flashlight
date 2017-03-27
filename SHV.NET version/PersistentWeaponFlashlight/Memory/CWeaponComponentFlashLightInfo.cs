using System.Runtime.InteropServices;

namespace PersistentWeaponFlashlight.Memory
{
	[StructLayout(LayoutKind.Explicit)]
	internal struct CWeaponComponentFlashLightInfo
	{
		[FieldOffset(16)]
		public uint Name;

		[FieldOffset(20)]
		public uint Model;

		[FieldOffset(24)]
		public uint LocalizedName;

		[FieldOffset(28)]
		public uint LocalizedDescription;

		[FieldOffset(32)]
		public uint AttachBone;

		[FieldOffset(56)]
		public byte ShowOnWheel;

		[FieldOffset(57)]
		public byte CreateObject;

		[FieldOffset(64)]
		public float MainLightIntensity;

		[FieldOffset(72)]
		public float MainLightRange;

		[FieldOffset(76)]
		public float MainLightFalloffExponent;

		[FieldOffset(80)]
		public float MainLightInnerAngle;

		[FieldOffset(84)]
		public float MainLightOuterAngle;

		[FieldOffset(88)]
		public float MainLightCoronaIntensity;

		[FieldOffset(92)]
		public float MainLightCoronaSize;

		[FieldOffset(96)]
		public float MainLightVolumeIntensity;

		[FieldOffset(100)]
		public float MainLightVolumeSize;

		[FieldOffset(104)]
		public float MainLightVolumeExponent;

		[FieldOffset(112)]
		public float MainLightShadowFadeDistance;

		[FieldOffset(116)]
		public float MainLightSpecularFadeDistance;

		[FieldOffset(120)]
		public float SecondaryLightIntensity;

		[FieldOffset(128)]
		public float SecondaryLightRange;

		[FieldOffset(132)]
		public float SecondaryLightFalloffExponent;

		[FieldOffset(136)]
		public float SecondaryLightInnerAngle;

		[FieldOffset(140)]
		public float SecondaryLightOuterAngle;

		[FieldOffset(144)]
		public float SecondaryLightVolumeIntensity;

		[FieldOffset(148)]
		public float SecondaryLightVolumeSize;

		[FieldOffset(152)]
		public float SecondaryLightVolumeExponent;

		[FieldOffset(160)]
		public float SecondaryLightFadeDistance;

		[FieldOffset(164)]
		public float TargetDistalongAimCamera;

		[FieldOffset(168)]
		public ushort FlashLightBone;

		[FieldOffset(170)]
		public ushort FlashLightBoneBulbOn;

		[FieldOffset(172)]
		public ushort FlashLightBoneBulbOff;

		[FieldOffset(174)]
		public byte ToggleWhenAiming;
	}
}
