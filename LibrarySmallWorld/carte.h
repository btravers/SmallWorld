#ifndef CARTE_H
#define CARTE_H

#include <stdio.h>
#include <stdlib.h>
#include <time.h>

class Carte
{
	private:
		int * _carte;
		int _posJA;
		int _posJB;
	public:
		int * generateMap(int size);
		int getPosJA();
		int getPosJB();
};

#endif