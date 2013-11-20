#include "api.h"

using namespace System;
using namespace System::Collections::Generic;

namespace WrapperSmallWorld {

	public ref class WrapperCarte
	{
		public:
			static List<int> ^ genererCarte(int size);
	};
}
