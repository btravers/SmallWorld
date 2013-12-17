#include "WrapperSmallWorld.h"
using namespace WrapperSmallWorld;
using namespace msclr::interop;

WrapperCarte::WrapperCarte(int size, String^ PeupleA, String^ PeupleB)
{
	_generateur = GenerateurCarte_New(size, marshal_as<std::string>(PeupleA), marshal_as<std::string>(PeupleB));
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