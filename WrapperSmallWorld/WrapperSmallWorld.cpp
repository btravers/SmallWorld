#include "WrapperSmallWorld.h"
using namespace WrapperSmallWorld;
using namespace msclr::interop;

WrapperCarte::WrapperCarte(int size, String^ peupleA, String^ peupleB)
{
	_generateur = GenerateurCarte_New(size, marshal_as<std::string>(peupleA), marshal_as<std::string>(peupleB));
}

WrapperCarte::~WrapperCarte()
{
	GenerateurCarte_Delete(_generateur);
}

List<int> ^ WrapperCarte::getCarte()
{
	List<int> ^ list = gcnew List<int>();
	int * buffer = _generateur->getCarte();

	for(int i = 0 ; i < (_generateur->getSize())*(_generateur->getSize()) ; i++)
		list->Add(buffer[i]);

	return list;
}
			
int WrapperCarte::getPosJA()
{
	return _generateur->getPosJA();
}
			
int WrapperCarte::getPosJB()
{
	return _generateur->getPosJB();
}

List<int> ^ WrapperSmallWorld::Destinations::destinations(String^ peuple, int rg, List<int>^ carte, int taille, int pm)
{
	int * c = new int[taille*taille];
	for(int i=0 ; i<taille*taille ; i++)
	{
		c[i] = carte[i];
	}

	int * buffer = Cases_Destinations(marshal_as<std::string>(peuple), rg, c, taille, pm);
	delete[] c;
	List<int> ^ res = gcnew List<int>();
	int nb = sizeof(*buffer)/sizeof(int);

	for(int i=0 ; i<nb ; i++)
	{
		res->Add(buffer[i]);
	}

	return res;
}