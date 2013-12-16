#include "GenerateurCarte.h"

#define NB_TYPE 5


GenerateurCarte::GenerateurCarte(int size)
{
	srand (time(NULL));

	_size = size;

	const int s = size*size;
	
	int nbType [NB_TYPE] = {0,0,0,0,0};
	_carte = new int[s];
	bool ok = false;

	for(int i = 0 ; i < s ; i++) {
		do {
			_carte[i] = rand() % 5;
			if(nbType[_carte[i]]-1 < s/NB_TYPE)
			{
				ok = true;
			}
		} while (!ok);

		nbType[_carte[i]]++;
		ok = false;
	}
}

int GenerateurCarte::getPosJA()
{
	return _posJA;
}

int GenerateurCarte::getPosJB()
{
	return _posJB;
}


int * GenerateurCarte::getCarte()
{
	return _carte;
}

int  GenerateurCarte::getSize()
{
	return _size;
}

EXTERNC DLL GenerateurCarte* GenerateurCarte_New(int size)
{
	return new GenerateurCarte(size);
}

EXTERNC DLL void GenerateurCarte_Delete(GenerateurCarte* gc)
{
	delete gc;
}