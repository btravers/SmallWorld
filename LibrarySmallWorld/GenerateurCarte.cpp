#include "GenerateurCarte.h"

#define NB_TYPE 5


GenerateurCarte::GenerateurCarte(int size, std::string PeupleA, std::string PeupleB)
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

	if(PeupleA == "vikings")
	{
		_posJA = 0;
	}
	else
	{
		int i = 0;
		bool positionne = false;
		while (! positionne)
		{
			if(_carte[i]!=3)
			{
				_posJA = i;
				positionne = true;
			}
			i++;
		}
	}

	if(PeupleB == "vikings")
	{
		_posJB = (size*size)-1;
	}
	else
	{
		int i = (size*size)-1;
		bool positionne = false;
		while (! positionne)
		{
			if(_carte[i]!=3)
			{
				_posJB = i;
				positionne = true;
			}
			i--;
		}
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

EXTERNC DLL GenerateurCarte* GenerateurCarte_New(int size, std::string PeupleA, std::string PeupleB)
{
	return new GenerateurCarte(size, PeupleA, PeupleB);
}

EXTERNC DLL void GenerateurCarte_Delete(GenerateurCarte* gc)
{
	delete gc;
}

EXTERNC DLL int* Cases_Destinations(std::string peuple, int rg, int * carte, int taille, int pm)
{
	std::vector<int> positions = std::vector<int>();
		int x = rg/taille;
		int y = rg%taille;

		for(int i=0 ; i<taille*taille ; i++)
		{
			if((pm - std::abs((i/taille)-x) - std::abs((i%taille)-y))>-1 && i!=rg)
			{
				if( (carte[i] != 3) || ((carte[i] == 3) && (peuple == "vikings")))
				{
					positions.push_back(i);
				}
			}
		}

		if(peuple == "nains")
		{
			//TODO ajouter les cases montagnes vides
		}

		int nb = positions.size();
		int * res = new int[nb];

		for(int i=0 ; i<nb ; i++)
		{
			res[i] = positions[i];
		}

		return res;
}