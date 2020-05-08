using System;

public static int PopCount(ulong value)
{
    value = (value & 0x5555555555555555) + ((value >> 1) & 0x5555555555555555);
    value = (value & 0x3333333333333333) + ((value >> 2) & 0x3333333333333333);
    value = (value & 0x0f0f0f0f0f0f0f0f) + ((value >> 4) & 0x0f0f0f0f0f0f0f0f);
    value = (value & 0x00ff00ff00ff00ff) + ((value >> 8) & 0x00ff00ff00ff00ff);
    value = (value & 0x0000ffff0000ffff) + ((value >> 16) & 0x0000ffff0000ffff);
    value = (value & 0x00000000ffffffff) + ((value >> 32) & 0x00000000ffffffff);
    return (int)value;
}