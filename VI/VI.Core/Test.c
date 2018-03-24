#include <stdio.h>
#include <math.h>
#include <limits.h>
#include <time.h>
#include <stdlib.h>


void VectorMulti(int size)
{
    float *a =(float*) malloc(size*sizeof(float));
    int i;
    for(i = 0; i < size; i++)
    {
        a[i] = a[i] * a[i];
    }
}
void VecotrLog(int size)
{
    float *a = (float*)malloc(size*sizeof(float));
    int i;
    for(i = 0; i < size; i++)
    {
        a[i] = logf(a[i]);
    }
}
void VecotrSqrt(int size)
{
    float *a = (float*)malloc(size*sizeof(float));
    int i;
    for(i = 0; i < size; i++)
    {
        a[i] = sqrtf(100);
    }
}
void VecotrPow(int size)
{
    float *a = (float*)malloc(size*sizeof(float));
    int i;
    for(i = 0; i < size; i++)
    {
        a[i] = powf(a[i], 1e+2);
    }
}
void VecotrExp(int size)
{
    float *a = (float*)malloc(size*sizeof(float));
    int i;
    for(i = 0; i < size; i++)
    {
        a[i] = expf(a[i]);
    }
}


int main()
{
    clock_t start, end;
    int diff;
    int size = 1000000000;
    
    start = clock();
    VectorMulti(size);
    end = clock();
    diff = (((double)(end - start)) / CLOCKS_PER_SEC) * 1000;
    printf("\n Execution Array Multi Time: %u ms\n", diff);

    start = clock();
    VecotrLog(size);
    end = clock();
    diff = (((double)(end - start)) / CLOCKS_PER_SEC) * 1000;
    printf("\n Execution Array Log Time: %u ms\n", diff);

    start = clock();
    VecotrSqrt(size);
    end = clock();
    diff = (((double)(end - start)) / CLOCKS_PER_SEC) * 1000;
    printf("\n Execution Array Sqrt Time: %u ms\n", diff);

    start = clock();
    VecotrPow(size);
    end = clock();
    diff = (((double)(end - start)) / CLOCKS_PER_SEC) * 1000;
    printf("\n Execution Array Pow Time: %u ms\n", diff);

    start = clock();
    VecotrExp(size);
    end = clock();
    diff = (((double)(end - start)) / CLOCKS_PER_SEC) * 1000;
    printf("\n Execution Array Exp Time: %u ms\n", diff);

    return 0;
}