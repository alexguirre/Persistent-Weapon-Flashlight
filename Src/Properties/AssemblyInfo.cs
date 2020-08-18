using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Persistent Weapon Flashlight")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("Persistent Weapon Flashlight")]
[assembly: AssemblyCopyright("Copyright ©  2017-2020 alexguirre")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("ef0339fc-04b8-4e7a-aadf-5e692fd0081a")]
[assembly: AssemblyVersion("1.4.0.*")]
[assembly: AssemblyFileVersion("1.4.0.0")]
#if RPH
[assembly: Rage.Attributes.Plugin("Persistent Weapon Flashlight",
                                  Author = "alexguirre",
                                  Description = "Makes the flashlight on your weapon stay on when you're not aiming.",
                                  SupportUrl = "https://github.com/alexguirre/Persistent-Weapon-Flashlight",
                                  PrefersSingleInstance = true)]
#endif