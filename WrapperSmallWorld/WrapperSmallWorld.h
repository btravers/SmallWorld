#pragma once

#include "GenerateurCarte.h"
#include <stdlib.h>
#include <string.h>
#include <msclr\marshal_cppstd.h>

using namespace System;
using namespace System::Collections::Generic;

namespace WrapperSmallWorld {

	public ref class WrapperCarte
	{
		private:
			GenerateurCarte* _generateur;
		public:
			WrapperCarte(int size, String^ PeupleA, String^ PeupleB);
			~WrapperCarte();
			List<int> ^ getCarte();
			int getPosJA();
			int getPosJB();
	};
}
