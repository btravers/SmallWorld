#ifdef WANTDLLEXP
	#define DLL __declspec(dllexport)
	#define EXTERNC extern "C"
#else
	#define DLL __declspec(dllimport)
	#define EXTERNC extern "C"
#endif

EXTERNC DLL int * create_map(int size);