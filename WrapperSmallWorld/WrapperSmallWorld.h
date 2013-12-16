#pragma once

#include "GenerateurCarte.h"

using namespace System;
using namespace System::Collections::Generic;

namespace WrapperSmallWorld {

	public ref class WrapperCarte
	{
		private:
			GenerateurCarte* _generateur;
		public:
			WrapperCarte(int size);
			~WrapperCarte();
			List<int> ^ getCarte();
			int getPosJA();
			int getPosJB();
	};
}
