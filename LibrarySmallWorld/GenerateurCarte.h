#pragma once

#ifdef WANTDLLEXP
#define DLL __declspec( dllexport )
#define EXTERNC extern "C"
#else
#define DLL __declspec( dllimport )
#define EXTERNC extern "C"
#endif

#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <string>

class DLL GenerateurCarte
{
private:
	int _size;
	int * _carte;
	int _posJA;
	int _posJB;
public:
	GenerateurCarte(int size, std::string PeupleA, std::string PeupleB);
	int getPosJA();
	int getPosJB();
	int * getCarte();
	int getSize();
};

EXTERNC DLL GenerateurCarte* GenerateurCarte_New(int size, std::string PeupleA, std::string PeupleB);
EXTERNC DLL void GenerateurCarte_Delete(GenerateurCarte* gc);
