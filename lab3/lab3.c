#include <stdlib.h>
#include <stdio.h>
#include <time.h>
#include "letters.h"

#define AMNT_LTTR 26
#define AMNT_INPT 25
#define MULT 0.5

int evaluate(int *letter, float *weights, int amountOfInputs, int theta);
void clear();

int main (){

    int neurons[AMNT_LTTR];

    srand(time(NULL));

    int theta = rand() % 10;

    float weights[AMNT_LTTR][AMNT_INPT];
    for (int i = 0; i < AMNT_LTTR; i++){
        for (int j = 0; j < AMNT_INPT; j++){
            weights[i][j] = rand() % 10;
        }
    }


    int numb_of_iter = 0;
    int flag = 1;
    int eps;
    while(1){
        if (flag == 0)
            break;

        flag = 0;
        numb_of_iter++;
        for (int i = 0; i < AMNT_LTTR; i++){
            for (int o = 0; o < AMNT_LTTR; o++){
                neurons[o] = evaluate(letters[i], weights[o], AMNT_INPT, theta);
                //printf("%d\n", neurons[o]);
                eps = desire_response[i][o] - neurons[o];
                if (eps == 0)
                    continue;

                flag++;

                for (int t = 0; t < AMNT_INPT; t++){
                    weights[o][t] = weights[o][t] + (letters[i][t] * MULT * eps);
                }


            }
            //printf("\n");
        }
    }

    printf("%d\n", numb_of_iter);

    for (int i = 0; i < 25; i++){
        printf("%f", weights[0][i]);
    }
    printf("\n");

    while(1){
        printf("enter a letter: ");
        int c;
        c = getchar();
        if (c == EOF)
            break; 
        getchar();

        printf("%d\n",c);
        for (int i = 0; i < AMNT_LTTR; i++){
            if(evaluate(letters[c-97], weights[i], AMNT_INPT, theta)){
                printf("its %c\n", i+65);
                break;
            }
        }
    }

}


int evaluate(int* letter, float* weights, int amountOfInputs, int theta){
    float sum = 0;
    for(int i = 0; i < amountOfInputs; i++){
        sum += weights[i] * letter[i];
    }

    return (sum >= theta);
}

void clear (void)
{
    while ( getchar() != '\n' );
}

