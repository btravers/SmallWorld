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
 * La classe GenerateurCarte est utilisé pour la création de la carte et le positionnement des unités
 * @author Mickaël Olivier, Benoit Travers
 */
class DLL GenerateurCarte
{
private:
	/** _size est la largeur de la carte : 5, 10 ou 15 cases */
	int _size;
	/** _carte stocke le type des cases sur la carte */
	int * _carte;
	/** _posJA est la position de départ des unités du Joueur A */
	int _posJA;
	/** _posJB est la position de départ des unités du Joueur B */
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

	/** getPosJA retourne la position initiale des unités du Joueur A
	 *  @return int la position initiale des unités du Joueur A
     */
	int getPosJA();

	/** getPosJB retourne la position initiale des unités du Joueur B
	 *  @return int la position initiale des unités du Joueur B
     */
	int getPosJB();

	/** getCarte retourne la carte générée sous la forme d'un pointeur sur un tableau d'entier
	 *  @return int* la carte générée
	 */
	int * getCarte();

	/** getSize retourne la largeur de la carte
	 *  @return int la largeur de la carte
	 */
	int getSize();
};

/** GenerateurCarte_New méthode exportée et appelée depuis le wrapper permettant de retournant un pointeur sur une entité GenerateurCarte
 *  @param size la largeur de la carte
 *  @param PeupleA le peuple chosit par le Joueur A : Vikings, Nains ou Gaulois
 *  @param PeupleB le peuple chosit par le Joueur B : Vikings, Nains ou Gaulois
 *  @return GenerateurCarte* un pointeur sur le GenerateurCarte construit à partir des paramêtres
 */
EXTERNC DLL GenerateurCarte* GenerateurCarte_New(int size, std::string PeupleA, std::string PeupleB);

/** GenerateurCarte_Delete méthode exportée et appelée depuis le wrapper permettant de détruire le GenerateurCarte passé en paramêtre 
 *  @param gc pointeur sur le GenerateurCarte à détruire
 */
EXTERNC DLL void GenerateurCarte_Delete(GenerateurCarte* gc);

/** Cases_Destinations méthode exportée et appelée depuis le wrapper permettant de rendre la liste des cases qu'une unité peut cibler
 *  @param peuple le peuple de l'unité voulant connaître ses destinations possibles
 *  @param rg la case où se situe l'unité voulant connaître ses destinations possibles
 *  @param carte un pointeur sur la carte
 *  @param taille la largeur de la carte
 *  @param pm les points de mouvement restant à l'unité voulant connaître ses destinations possibles
 *  @param posAdversaire un pointeur sur un tableau contenant les positions des unités de l'adversaire
 *  @param nbAdversaires le nombre de position contenu dans le tableau contenant les positions des unités de l'adversaire
 *  @return int* un pointeur sur le tableau des destinations possibles
 */
EXTERNC DLL int* Cases_Destinations(std::string peuple, int rg, int * carte, int taille, double pm, int * posAdversaire, int nbAdversaires);

/**
 * La classe OperationSurCarte contient des méthodes statiques utilisées par les méthodes précédentes
 * @author Mickaël Olivier, Benoit Travers
 */
class OperationSurCarte
{
public:
	/** adversairePresent permet de savoir si un adversaire est présent sur une case donnée
	 *  @param rg position de la case où l'on souhaite savoir si il y a un adversaire
	 *  @param adversaire pointeur sur le tableau des positions des unités de l'adversaire
	 *  @param taille la taille du tableau contenant les positions des unités de l'adversaire
	 *  @return bool vrai si une unité de l'adversaire est présente sur une case donnée
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