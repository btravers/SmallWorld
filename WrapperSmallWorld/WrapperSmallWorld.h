#pragma once

#include "GenerateurCarte.h"
#include <stdlib.h>
#include <string.h>
#include <msclr\marshal_cppstd.h>

using namespace System;
using namespace System::Collections::Generic;

namespace WrapperSmallWorld {

	/**
	 * La classe WrapperSmallWorld permet de faire l'intermédiaire entre le code c# et la librairie c++ pour la création de la carte
	 * @author Mickaël Olivier, Benoit Travers
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

		/** getCarte retourne la carte générée sous la forme d'une liste d'entier
		 *  @return List<int> ^ la carte générée
		 */
		List<int> ^ getCarte();

		/** getPosJA retourne la position initiale des unités du Joueur A
		 *  @return int la position initiale des unités du Joueur A
		 */
		int getPosJA();

		/** getPosJB retourne la position initiale des unités du Joueur B
		 *  @return int la position initiale des unités du Joueur B
		 */
		int getPosJB();
	};


	/**
	 * La classe Destinations permet de faire l'intermédiaire entre le code c# et la librairie c++ pour obtenir les destinations possibles d'une unité
	 * @author Mickaël Olivier, Benoit Travers
	 */
	public ref class Destinations
	{
	public:
		/** Cases_Destinations méthode exportée et appelée depuis le wrapper permettant de rendre la liste des cases qu'une unité peut cibler
		 *  @param peuple le peuple de l'unité voulant connaître ses destinations possibles
		 *  @param rg la case où se situe l'unité voulant connaître ses destinations possibles
		 *  @param carte la carte
		 *  @param taille la largeur de la carte
		 *  @param pm les points de mouvement restant à l'unité voulant connaître ses destinations possibles
		 *  @param posAdversaire une liste contenant les positions des unités de l'adversaire
		 *  @return List<int> ^ la liste des destinations possibles
		 */
		static List<int> ^ destinations(String^ peuple, int rg, List<int>^ carte, int taille, double pm, List<int>^ posAdversaire);
	};
}
