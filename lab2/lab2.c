#include <stdio.h>
#include <stdlib.h>
#define LIM 1


int evaluate(int *number, int *weights, int amountOfInputs, int theta);

int getNumber();

void printArray();


int main(){

    int numbers[9][12] = {{0,0,1,
                           0,1,1,
                           0,0,1,
                           0,0,1},
                                 
                          {0,1,1,
                           0,0,1,
                           0,1,0,
                           1,1,1},

                          {0,1,1,
                           0,1,1,
                           0,0,1,
                           0,1,1},

                          {0,0,1,
                           0,1,1,
                           1,1,1,
                           0,0,1},

                          {0,1,1,
                           0,1,0,
                           1,0,1,
                           0,1,0},
                          
                          {0,1,1,
                           1,0,0,
                           1,1,1,
                           0,1,0},

                          {1,1,1,
                           0,0,1,
                           0,1,0,
                           1,0,0},

                          {0,1,0,
                           1,1,1,
                           1,0,1,
                           0,1,0},

                          {0,1,1,
                           0,1,1,
                           0,0,1,
                           0,1,0}};
    
    int weights[] = {0,2,1,
                    1,3,1,
                    0,1,0,
                    1,2,0};

    int theta = 3;

    for(int o = 0; o < 10; o++)
        for(int i = 1; i <= 9; i++){
            int *number = numbers[i-1];
            int output = evaluate(number, weights, 12, theta);
            //printf("%d\n", output); 
            if(output == i % 2){
                if (output == 0)
                    for(int j = 0; j < 12; j++){
                        if(number[j] == 1)
                            weights[j] += number[j];
                    }
                else if(output == 1)
                    for(int j = 0; j < 12; j++){
                        if(number[j] == 1)
                            weights[j] -= number[j];
                    }

                //printArray(weights);
            }
        }

    int input = getNumber();

    if (evaluate(numbers[input-1], weights, 12, theta) == 1)
        printf("the digit is even\n");
    else
        printf("the digit is odd\n");

    printArray(weights);

}


int evaluate(int* number, int* weights, int amountOfInputs, int theta){
    int sum = 0;
    for(int i = 0; i < amountOfInputs; i++){
        sum += weights[i] * number[i];
    }

    return (sum >= theta);
}


int getNumber(){

    char c;
    char s[LIM];
    for(int i = 0; i < LIM+1 && (c = getchar()) != '\n'; i++){
        s[i] = c;
    }

    return atoi(s);
}

void printArray(int *arr){
    for(int i = 0; i < 12; i++){
        printf("%d ", arr[i]);
    }
    printf("\n");
}

