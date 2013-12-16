#ifndef CARTE_H
#define CARTE_H

#include <stdio.h>
#include <stdlib.h>
#include <time.h>

class Carte
{
	private:
		int * carte;
		int posJA;
		int posJB;
	public:
		int * generateMap(int size);
		int getPosJA();
		int getPosJB();
};

#endif