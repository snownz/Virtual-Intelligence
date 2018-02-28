#include <stdio.h>
#include <math.h>

void C_V_mult_C(float* cache, float* v, float c)
{
	int i = 0;
	int len = sizeof(cache) / sizeof(float);

	for (i = 0; i < len; i++)
	{
		cache[i] = v[i] * c;
	}
}