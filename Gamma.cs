
using System;
using System.Runtime.InteropServices;

public static class GammaController
{
    [DllImport("user32.dll")]
    public static extern IntPtr GetDC(IntPtr hWnd);

    [DllImport("gdi32.dll")]
    public static extern bool SetDeviceGammaRamp(IntPtr hDC, ref RAMP lpRamp);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct RAMP
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public UInt16[] Red;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public UInt16[] Green;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public UInt16[] Blue;
    }

    private static RAMP originalGamma;
    private static bool isOriginalGammaSaved = false;

    public static void ApplyFilter(int strength) // strength from 2000 (none) to 6500 (max)
    {
        if (!isOriginalGammaSaved)
        {
            originalGamma = GetCurrentGamma();
            isOriginalGammaSaved = true;
        }

        // Invert the strength value so that a higher slider value means a stronger filter
        // Map [2000, 6500] to a percentage [0.0, 1.0]
        double filterStrength = (strength - 2000.0) / (6500.0 - 2000.0);

        IntPtr screenDC = GetDC(IntPtr.Zero);

        RAMP ramp = new RAMP();
        ramp.Red = new ushort[256];
        ramp.Green = new ushort[256];
        ramp.Blue = new ushort[256];

        for (int i = 0; i < 256; i++)
        {
            double value = i / 255.0;
            
            // Red channel remains the same
            ramp.Red[i] = (ushort)(value * 65535);

            // Reduce green and blue channels based on filter strength
            // At max strength, green is at ~60% and blue is at ~20%
            double greenValue = value * (1.0 - 0.4 * filterStrength);
            double blueValue = value * (1.0 - 0.8 * filterStrength);

            ramp.Green[i] = (ushort)(greenValue * 65535);
            ramp.Blue[i] = (ushort)(blueValue * 65535);
        }

        SetDeviceGammaRamp(screenDC, ref ramp);
    }

    public static void Reset()
    {
        if (isOriginalGammaSaved)
        {
            IntPtr screenDC = GetDC(IntPtr.Zero);
            SetDeviceGammaRamp(screenDC, ref originalGamma);
        }
    }

    private static RAMP GetCurrentGamma()
    {
        // This is a placeholder. In a real application, you might want to use GetDeviceGammaRamp.
        // However, GetDeviceGammaRamp is not available on all systems.
        // For simplicity, we'll just create a default ramp.
        RAMP ramp = new RAMP();
        ramp.Red = new ushort[256];
        ramp.Green = new ushort[256];
        ramp.Blue = new ushort[256];
        for (int i = 0; i < 256; i++)
        {
            ramp.Red[i] = (ushort)(i * 257);
            ramp.Green[i] = (ushort)(i * 257);
            ramp.Blue[i] = (ushort)(i * 257);
        }
        return ramp;
    }
}
