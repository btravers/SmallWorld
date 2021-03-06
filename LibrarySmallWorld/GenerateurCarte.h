#pragma once

#ifdef WANTDLLEXP
#define DLL __declspec( dllexport )
#define EXTERNC extern "C"
#else
#define DLL __declspec( dllimport )
#define EXTERNC extern "C"
#endif

#include <vector> 
#include <iostream> 
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <string>
#include <cmath>

/**
 * La classe GenerateurCarte est utilis� pour la cr�ation de la carte et le positionnement des unit�s
 * @author Micka�l Olivier, Benoit Travers
 */
class DLL GenerateurCarte
{
private:
	/** _size est la largeur de la carte : 5, 10 ou 15 cases */
	int _size;
	/** _carte stocke le type des cases sur la carte */
	int * _carte;
	/** _posJA est la position de d�part des unit�s du Joueur A */
	int _posJA;
	/** _posJB est la position de d�part des unit�s du Joueur B */
	int _posJB;
public:
	/** Le constructeur de GenerateurCarte
     *  @param size la largeur de la carte
	 *  @param PeupleA le peuple chosit par le Joueur A : Vikings, Nains ou Gaulois
	 *  @param PeupleB le peuple chosit par le Joueur B : Vikings, Nains ou Gaulois
     */
	GenerateurCarte(int size, std::string PeupleA, std::string PeupleB);

	/**
	 * Le destructeur de GenerateurCarte
	 */
	~GenerateurCarte()
	{
		delete _carte;
	}

	/** getPosJA retourne la position initiale des unit�s du Joueur A
	 *  @return int la position initiale des unit�s du Joueur A
     */
	int getPosJA();

	/** getPosJB retourne la position initiale des unit�s du Joueur B
	 *  @return int la position initiale des unit�s du Joueur B
     */
	int getPosJB();

	/** getCarte retourne la carte g�n�r�e sous la forme d'un pointeur sur un tableau d'entier
	 *  @return int* la carte g�n�r�e
	 */
	int * getCarte();

	/** getSize retourne la largeur de la carte
	 *  @return int la largeur de la carte
	 */
	int getSize();
};

/** GenerateurCarte_New m�thode export�e et appel�e depuis le wrapper permettant de retournant un pointeur sur une entit� GenerateurCarte
 *  @param size la largeur de la carte
 *  @param PeupleA le peuple chosit par le Joueur A : Vikings, Nains ou Gaulois
 *  @param PeupleB le peuple chosit par le Joueur B : Vikings, Nains ou Gaulois
 *  @return GenerateurCarte* un pointeur sur le GenerateurCarte construit � partir des param�tres
 */
EXTERNC DLL GenerateurCarte* GenerateurCarte_New(int size, std::string PeupleA, std::string PeupleB);

/** GenerateurCarte_Delete m�thode export�e et appel�e depuis le wrapper permettant de d�truire le GenerateurCarte pass� en param�tre 
 *  @param gc pointeur sur le GenerateurCarte � d�truire
 */
EXTERNC DLL void GenerateurCarte_Delete(GenerateurCarte* gc);

/** Cases_Destinations m�thode export�e et appel�e depuis le wrapper permettant de rendre la liste des cases qu'une unit� peut cibler
 *  @param peuple le peuple de l'unit� voulant conna�tre ses destinations possibles
 *  @param rg la case o� se situe l'unit� voulant conna�tre ses destinations possibles
 *  @param carte un pointeur sur la carte
 *  @param taille la largeur de la carte
 *  @param pm les points de mouvement restant � l'unit� voulant conna�tre ses destinations possibles
 *  @param posAdversaire un pointeur sur un tableau contenant les positions des unit�s de l'adversaire
 *  @param nbAdversaires le nombre de position contenu dans le tableau contenant les positions des unit�s de l'adversaire
 *  @return int* un pointeur sur le tableau des destinations possibles
 */
EXTERNC DLL int* Cases_Destinations(std::string peuple, int rg, int * carte, int taille, double pm, int * posAdversaire, int nbAdversaires);

/**
 * La classe OperationSurCarte contient des m�thodes statiques utilis�es par les m�thodes pr�c�dentes
 * @author Micka�l Olivier, Benoit Travers
 */
class OperationSurCarte
{
public:
	/** adversairePresent permet de savoir si un adversaire est pr�sent sur une case donn�e
	 *  @param rg position de la case o� l'on souhaite savoir si il y a un adversaire
	 *  @param adversaire pointeur sur le tableau des positions des unit�s de l'adversaire
	 *  @param taille la taille du tableau contenant les positions des unit�s de l'adversaire
	 *  @return bool vrai si une unit� de l'adversaire est pr�sente sur une case donn�e
	 */
	static bool adversairePresent(int rg, int * adversaires, int taille)
	{
		bool trouve = false;
		for(int i=0 ; i<taille && !trouve ; i++)
		{
			trouve = adversaires[i] == rg;
		}
		return trouve;
	}
};