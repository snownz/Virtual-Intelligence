#include <stdio.h>
#include <math.h>
#include <limits.h>
#include <time.h>

void loop(int timing)
{
	int i = 0;
	for (i = 0; i < timing; i++)
	{
		double a = sqrt(100);
	}
}

int main()
{
    clock_t start, end;
    int diff;
    int i =0;
    
    start = clock();

    loop(100000000);

    end = clock();

    diff = (((double)(end - start)) / CLOCKS_PER_SEC) * 1000;

    printf("\n Operation took %u milisenconds\n", diff);

    return 0;
}