#include "api.h"
#include "carte.h"

EXTERNC DLL int * create_map(int size)
{ 
	Carte carte = Carte();
	return carte.generateMap(size); 
}