#include "carte.h"
#define NB_TYPE 5

int * Carte::generateMap(int size)
{
	srand (time(NULL));
	const int s = size*size;
	
	int nbType [NB_TYPE] = {0,0,0,0,0};

	int * carte;
	carte = new int[s];
	bool ok = false;

	for(int i = 0 ; i < s ; i++) {
		do {
			carte[i] = rand() % 5;
			if(nbType[carte[i]]-1 < s/NB_TYPE)
			{
				ok = true;
			}
		} while (!ok);

		nbType[carte[i]]++;
		ok = false;
	}

	return carte;
}
