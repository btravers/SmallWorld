#pragma once

#ifdef WANTDESTEXP
#define DLL __declspec( dllexport )
#define EXTERNC extern "C"
#else
#define DLL __declspec( dllimport )
#define EXTERNC extern "C"
#endif

#include <vector> 
#include <iostream> 
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <string>
#include <cmath>


class DLL Destinations
{
public:
	static int* destinations(std::string peuple, int rg, int ** carte, int taille)
	{
		std::vector<int> positions = std::vector<int>();
		int x = rg/taille;
		int y = rg%taille;

		for(int i=x-1 ; i<x+1 ; i++)
		{
			for(int j ; j<y+1 ; j++)
			{
				if(!(peuple != "vikings" && carte[i][j] == 3 && i > -1 && j > -1 && i < taille && j < taille && ((std::abs(x-i)+std::abs(y-j)-1)>0)))
				{
					positions.push_back(i*taille+j);
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
};
EXTERNC DLL int* destinations(std::string peuple, int rg, int ** carte, int taille)
{
	return Destinations::destinations(peuple,rg,carte,taille);
}