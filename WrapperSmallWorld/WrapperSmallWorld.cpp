// Il s'agit du fichier DLL principal.

#include "WrapperSmallWorld.h"

List<int> ^ WrapperSmallWorld::WrapperCarte::genererCarte(int size)
{
	List<int> ^ list = gcnew List<int>();
	int * buffer = create_map(size);

	for(int i = 0 ; i < size * size ; i++)
		list->Add(buffer[i]);

	return list;
}