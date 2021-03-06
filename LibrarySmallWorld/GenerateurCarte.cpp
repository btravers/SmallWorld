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

EXTERNC DLL int* Cases_Destinations(std::string peuple, int rg, int * carte, int taille, double pm, int * posAdversaire, int nbAdversaires)
{
	std::vector<int> positions = std::vector<int>();
	int x = rg/taille;
	int y = rg%taille;

	for(int i=0 ; i<taille ; i++)
	{
		for(int j=0; j<taille ; j++)
		{
			if(pm == 1 && (std::abs(i-x) + std::abs(j-y)) == 1 && (i*taille+j)!=rg)
			{
				if( (carte[i*taille+j] != 3) || ((carte[i*taille+j] == 3) && (peuple == "vikings")) || OperationSurCarte::adversairePresent(i*taille+j, posAdversaire, nbAdversaires))
				{
					positions.push_back(i*taille+j);
				}
			}
			else
			{
				if(peuple == "nains" && (i*taille+j)!=rg && pm == 1)
				{
					if(carte[i*taille+j] == 0 && carte[rg] == 0 && ! OperationSurCarte::adversairePresent(i*taille+j, posAdversaire, nbAdversaires))
					{
						positions.push_back(i*taille+j);
					}
				}
				else if(peuple == "gaulois" && (i*taille+j)!=rg)
				{
					if((std::abs(i-x) + std::abs(j-y)) == 2 && carte[i*taille+j] == 1 && pm ==1)
					{
						if((i!=x && j!=y && (carte[x*taille+j] == 1 || carte[i*taille+y] == 1)) || (i == x && carte[i*taille+((y+j)/2)] == 1) || (j == y && carte[((i+x)/2)*taille+j] == 1))
						{
							positions.push_back(i*taille+j);
						}
					}
					else if ((std::abs(i-x) + std::abs(j-y)) == 1 && carte[i*taille+j] == 1 && pm == 0.5)
					{
						positions.push_back(i*taille+j);
					}
				}
			}
		}
	}

	int nb = positions.size();
	int * res = new int[nb+1];
	res[0] = nb;

	for(int i=0 ; i<nb ; i++)
	{
		res[i+1] = positions[i];
	}

	return res;
}