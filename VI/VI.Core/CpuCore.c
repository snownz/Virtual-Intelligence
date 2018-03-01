#include <stdio.h>
#include <math.h>

void C_V_mult_V(int len, float* cache, float* v0, float* v1)
{
	int i = 0;
	for (i = 0; i < len; i++) cache[i] = v0[i] * v1[i];
}
void C_V_div_V(int len, float* cache, float* v0, float* v1)
{
	int i = 0;
	for (i = 0; i < len; i++) cache[i] = v0[i] / v1[i];
}
void C_V_sub_V(int len, float* cache, float* v0, float* v1)
{
	int i = 0;
	for (i = 0; i < len; i++) cache[i] = v0[i] - v1[i];
}
void C_V_add_V(int len, float* cache, float* v0, float* v1)
{
	int i = 0;
	for (i = 0; i < len; i++) cache[i] = v0[i] + v1[i];
}

void C_V_add_C(int len, float* cache, float* v, float c)
{
	int i = 0;
	for (i = 0; i < len; i++) cache[i] = v[i] + c;
}
void C_V_mult_C(int len, float* cache, float* v, float c)
{
	int i = 0;
	for (i = 0; i < len; i++) cache[i] = v[i] * c;
}
void C_V_sub_C(int len, float* cache, float* v, float c)
{
	int i = 0;
	for (int i = 0; i < len; i++) cache[i] = v[i] - c;
}
void C_V_sub_C(int len, float* cache, float c, float* v)
{
	int i = 0;
	for (int i = 0; i < len; i++) cache[i] = c - v[i];
}
void C_V_div_C(int len, float* cache, float* v, float c)
{
	int i = 0;
	for (int i = 0; i < len; i++) cache[i] = v[i] / c;
}
void C_V_div_C(int len, float* cache, float c, float* v)
{
	int i = 0;
	for (int i = 0; i < len; i++) cache[i] = c / v[i];
}

void C_Tanh(int len, float* cache, float* arr)
{
	int i;
	for (i = 0; i < len; i++) cache[i] = tanhf(arr[i]);
}
void C_Sin(int len, float* cache, float* arr)
{
	int i;
	for (i = 0; i < len; i++) cache[i] = sinf(arr[i]);
}
void C_Cos(int len, float* cache, float* arr)
{
	int i;
	for (i = 0; i < len; i++) cache[i] = cosf(arr[i]);
}
void C_Pow(int len, float* cache, float* arr, float exp)
{
	int i;
	for (i = 0; i < len; i++) cache[i] = powf(arr[i], exp);
}
void C_Exp(int len, float* cache, float* arr)
{
	int i;
	for (i = 0; i < len; i++) cache[i] = expf(arr[i]);
}
void C_Log(int len, float* cache, float* arr)
{
	int i;
	for (i = 0; i < len; i++) cache[i] = logf(arr[i]);
}
void C_Sqrt(int len, float* cache, float* arr)
{
	int i;
	for (i = 0; i < len; i++) cache[i] = sqrtf(arr[i]);
}

void C_VT_mult_M(int w, int h, float** cache, float* vt, float** m)
{
	int x, y;
	for (x = 0; x < w; x++)	for (y = 0; y < h; y++)	cache[x][y] = vt[y] * m[x][y];
}
void C_M_mult_M(int w, int h, float** cache, float** m0, float** m1)
{
	int x, y;
	for (x = 0; x < w; x++)	for (y = 0; y < h; y++)	cache[x][y] = m0[x][y] * m1[x][y];
}
void C_M_div_M(int w, int h, float** cache, float** m0, float** m1)
{
	int x, y;
	for (x = 0; x < w; x++)	for (y = 0; y < h; y++)	cache[x][y] = m0[x][y] / m1[x][y];
}
void C_M_sub_M(int w, int h, float** cache, float** m0, float** m1)
{
	int x, y;
	for (x = 0; x < w; x++)	for (y = 0; y < h; y++)	cache[x][y] = m0[x][y] - m1[x][y];
}
void C_M_add_M(int w, int h, float** cache, float** m0, float** m1)
{
	int x, y;
	for (x = 0; x < w; x++)	for (y = 0; y < h; y++)	cache[x][y] = m0[x][y] + m1[x][y];
}
void C_M_mult_V(int w, int h, float** cache, float** m, float* v)
{
	int x, y;
	for (x = 0; x < w; x++)	for (y = 0; y < h; y++)	cache[x][y] = m[x][y] * v[x];
}
void C_M_mult_C(int w, int h, float** cache, float** m, float c)
{
	int x, y;
	for (x = 0; x < w; x++)	for (y = 0; y < h; y++)	cache[x][y] = m[x][y] * c;
}
void C_M_add_C(int w, int h, float** cache, float** m, float c)
{
	int x, y;
	for (x = 0; x < w; x++)	for (y = 0; y < h; y++)	cache[x][y] = m[x][y] + c;
}
void C_M_div_C(int w, int h, float** cache, float** m, float c)
{
	int x, y;
	for (x = 0; x < w; x++)	for (y = 0; y < h; y++)	cache[x][y] = m[x][y] / c;
}
void C_C_div_M(int w, int h, float** cache, float** m, float c)
{
	int x, y;
	for (x = 0; x < w; x++)	for (y = 0; y < h; y++)	cache[x][y] = c / m[x][y];
}
void C_C_div_M_int(int w, int h, float** cache, float** m, int c)
{
	int x, y;
	for (x = 0; x < w; x++)	for (y = 0; y < h; y++)	cache[x][y] = c / m[x][y];
}

void C_Sqrt(int w, int h, float** cache, float** arr)
{
	int x, y;
	for (x = 0; x < w; x++)	for (y = 0; y < h; y++)	cache[x][y] = sqrtf(arr[x][y]);
}

float** C_VT_mult_V(int w, int h, float** cache, float* vt, float* v)
{
	int x, y;
	for (x = 0; x < w; x++)	for (y = 0; y < h; y++)	cache[x][y] = vt[y] * v[x];
}
float** C_M_mult_MT(int w, int h, float** cache, float** mt, float** m)
{
	int x, y;
	for (x = 0; x < w; x++)	for (y = 0; y < h; y++)	cache[x][y] = m[x][y] * mt[y][x];
}

void C_SumLine(int w, int h, float* cache, float** arr)
{
	int x, y;
	for (x = 0; x <w; x++)
	{
		float sum = 0;
		for (y = 0; y <h; y++) sum += arr[x][y];

		cache[x] = sum;
	}
}
void C_SumColumn(int w, int h, float* cache, float** arr)
{
	int x, y;
	for (y = 0; y < h; y++)
	{
		float sum = 0;
		for (x = 0; x < w; x++) sum += arr[x][y];

		cache[y] = sum;
	}
}