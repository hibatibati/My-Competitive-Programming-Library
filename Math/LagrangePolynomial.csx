using System;

public ModInt LagrangePolynomial(ModInt[] Y, int T, int a = 0, int d = 1)
{
    if (T < Y.Length) return Y[T];
    ModInt[] left = new ModInt[Y.Length], right = new ModInt[Y.Length];
    left[0] = right[Y.Length - 1] = 1;
    for (int i = 0; i < Y.Length - 1; i++)
    {
        left[i + 1] = left[i] * (T - a - d * i);
    }
    for (int i = Y.Length - 2; i >= 0; i--)
    {
        right[i] = right[i + 1] * (T - a - d * (i + 1));
    }
    ModInt res = 0;
    for (int i = 0; i < Y.Length; i++)
    {
        ModInt r = Y[i] * left[i] * right[i] * ModInt.FacInv(i) * ModInt.FacInv(Y.Length - i - 1);
        if ((Y.Length - i - 1) % 2 == 0) res += r;
        else res -= r;
    }
    return res;
}