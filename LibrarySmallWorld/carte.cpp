#include "carte.h"
#define NB_TYPE 5

int * Carte::generateMap(int size)
{
	srand (time(NULL));
	const int s = size*size;
	
	int nbType [NB_TYPE] = {0,0,0,0,0};

	int * carte;
	carte = new int[s];

	for(int i = 0 ; i < s ; i++) {
		do {
			carte[i] = rand() % 5;
		} while((s/NB_TYPE+2) > nbType[carte[i]]);
		nbType[carte[i]]++;
	}
	return carte;
}