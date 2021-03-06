#pragma once

#include "GenerateurCarte.h"
#include <stdlib.h>
#include <string.h>
#include <msclr\marshal_cppstd.h>

using namespace System;
using namespace System::Collections::Generic;

namespace WrapperSmallWorld {

	/**
	 * La classe WrapperSmallWorld permet de faire l'interm�diaire entre le code c# et la librairie c++ pour la cr�ation de la carte
	 * @author Micka�l Olivier, Benoit Travers
	 */
	public ref class WrapperCarte
	{
	private:
		/** _generateur le GenerateurCarte */
		GenerateurCarte* _generateur;
	public:
		/** Le constructeur de WrapperCarte
		 *  @param size la largeur de la carte
		 *  @param PeupleA le peuple chosit par le Joueur A : Vikings, Nains ou Gaulois
		 *  @param PeupleB le peuple chosit par le Joueur B : Vikings, Nains ou Gaulois
		 */
		WrapperCarte(int size, String^ PeupleA, String^ PeupleB);

		/** Le destructeur de WrapperCarte
		 */
		~WrapperCarte();

		/** getCarte retourne la carte g�n�r�e sous la forme d'une liste d'entier
		 *  @return List<int> ^ la carte g�n�r�e
		 */
		List<int> ^ getCarte();

		/** getPosJA retourne la position initiale des unit�s du Joueur A
		 *  @return int la position initiale des unit�s du Joueur A
		 */
		int getPosJA();

		/** getPosJB retourne la position initiale des unit�s du Joueur B
		 *  @return int la position initiale des unit�s du Joueur B
		 */
		int getPosJB();
	};


	/**
	 * La classe Destinations permet de faire l'interm�diaire entre le code c# et la librairie c++ pour obtenir les destinations possibles d'une unit�
	 * @author Micka�l Olivier, Benoit Travers
	 */
	public ref class Destinations
	{
	public:
		/** Cases_Destinations m�thode export�e et appel�e depuis le wrapper permettant de rendre la liste des cases qu'une unit� peut cibler
		 *  @param peuple le peuple de l'unit� voulant conna�tre ses destinations possibles
		 *  @param rg la case o� se situe l'unit� voulant conna�tre ses destinations possibles
		 *  @param carte la carte
		 *  @param taille la largeur de la carte
		 *  @param pm les points de mouvement restant � l'unit� voulant conna�tre ses destinations possibles
		 *  @param posAdversaire une liste contenant les positions des unit�s de l'adversaire
		 *  @return List<int> ^ la liste des destinations possibles
		 */
		static List<int> ^ destinations(String^ peuple, int rg, List<int>^ carte, int taille, double pm, List<int>^ posAdversaire);
	};
}
