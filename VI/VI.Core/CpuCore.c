#include <stdio.h>
#include <math.h>

void C_V_mult_C(int len, float* cache, float* v, float c)
{
	int i = 0;

	for (i = 0; i < len; i++)
	{
		cache[i] = v[i] * c;
	}
}