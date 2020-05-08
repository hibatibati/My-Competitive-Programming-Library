using System;

int MSB(int v)
{
    var rt = 0;
    if ((v >> 16) > 0) { rt |= 16; v >>= 16; }
    if ((v >> 8) > 0) { rt |= 8; v >>= 8; }
    if ((v >> 4) > 0) { rt |= 4; v >>= 4; }
    if ((v >> 2) > 0) { rt |= 2; v >>= 2; }
    return rt | (v >> 1);
}