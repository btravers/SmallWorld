#include "WrapperSmallWorld.h"
using namespace WrapperSmallWorld;

WrapperCarte::WrapperCarte(int size)
{
	_generateur = GenerateurCarte_New(size);
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