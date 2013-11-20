#include "api.h"
#include "carte.h"

EXTERNC DLL int * create_map(int size)
{ 
	return Carte::generateMap(size); 
}