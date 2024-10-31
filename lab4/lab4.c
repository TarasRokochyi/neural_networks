#include <stdlib.h>
#include <stdio.h>
#include <time.h>
#include <math.h>
#include "letters.h"

#define AMNT_LTTR 26
#define AMNT_INPT 25
#define MULT 0.5

double evaluate(int *letter, double *weights, int amountOfInputs);
void clear();

int main (){
    srand(time(NULL));


    //int theta = rand() % 10;

    //initializing neurons with random numbers
    double neurons[AMNT_LTTR];
    double weights[AMNT_LTTR][AMNT_INPT];
    for (int i = 0; i < AMNT_LTTR; i++){
        for (int j = 0; j < AMNT_INPT; j++){
            weights[i][j] = (rand() % 10) * 0.01;
        }
    }


    int numb_of_iter = 0;
    for (int o = 0; o < AMNT_LTTR; o++){
        int all_right = 0;
        double eps;

        while(!all_right){
            all_right = 1;
            for(int i = 0; i < AMNT_LTTR; i++){
                // evaluating Y - output of one neuron
                neurons[o] = evaluate(letters[i], weights[o], AMNT_INPT);
                //printf("%f\n", neurons[o]);

                // evaluating epsilon 
                eps = desire_response[i][o] - neurons[o];
                // if epilon is <= than break the cycle
                if (fabs(eps) <= 0.15)
                    //printf("right\n");
                    continue;

                //printf("false\n");

                all_right = 0;

                // changin the weights of the neuron
                //             Yi(1-Yi)(Di-Yi)                  (Di-Yi) - epsilon
                //printf("%f %f\n", neurons[o], eps);
                double delta = neurons[o] * (1.0 - neurons[o]) * eps;
                for (int t = 0; t < AMNT_INPT; t++){
                    //              Wij(t) + del(Wij)      del(Wij) = n*delta*Xi
                    //printf("%f %f %f %f \n", weights[o][t], MULT, delta, (double)letters[i][t]);
                    //printf("%f\n", weights[o][t]);
                    weights[o][t] = weights[o][t] + MULT * delta * (double)letters[i][t];
                    //printf("%f\n", weights[o][t]);
                }
            }
            //return 0;
            numb_of_iter++;
        }
    }
    printf("%d\n", numb_of_iter);



    //while(!all_right){

    //    all_right = 1;

    //    numb_of_iter++;
    //    for (int i = 0; i < AMNT_LTTR; i++){
    //        for (int o = 0; o < AMNT_LTTR; o++){
    //            // evaluating Y - output of one neuron
    //            neurons[o] = evaluate(letters[i], weights[o], AMNT_INPT, theta);
    //            //printf("%d\n", neurons[o]);
    //            // evaluating epsilon 
    //            eps = desire_response[i][o] - neurons[o];
    //            if (fabs(eps) >= 0.15)
    //                break;

    //            all_right = 0;

    //            for (int t = 0; t < AMNT_INPT; t++){
    //                weights[o][t] = weights[o][t] + (letters[i][t] * MULT * eps);
    //            }


    //        }
    //        //printf("\n");
    //    }
    //}

    //printf("%d\n", numb_of_iter);

    for (int i = 0; i < 25; i++){
        printf("%f ", weights[0][i]);
    }
    printf("\n");

    while(1){
        printf("enter a letter: ");
        int c;
        c = getchar();
        if (c == EOF)
            break; 
        getchar();

        //printf("%d\n",c);
        for (int i = 0; i < AMNT_LTTR; i++){
            printf("%f - %c\n", evaluate(check_letters[c-97], weights[i], AMNT_INPT), i+97);
            //if(evaluate(letters[c-97], weights[i], AMNT_INPT) > 0.85){
            //    printf("its %c\n", i+65);
            //    break;
            //}
        }
    }

}


double evaluate(int* letter, double* weights, int amountOfInputs){
    float sum = 0;
    for(int i = 0; i < amountOfInputs; i++){
        sum += weights[i] * letter[i];
    }

    //printf("%f\n", 1.0 / (1.0 + exp(-sum)));
    return 1.0 / (1.0 + exp(-sum));
}

void clear (void)
{
    while ( getchar() != '\n' );
}

