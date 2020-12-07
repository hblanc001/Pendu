using System;
using System.Collections.Generic;

namespace pendu
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] pendu = new string[11, 20];
            Initialiser_Pendu(pendu);
            int modeJeu = Menu();
            Initialisation_Partie(pendu, modeJeu);

        }




        public static string[] InitialiserTabMots(string NomDico) // Renvoi un tableau qui contient tous les mots du dico
        {
            string[] tabMots = System.IO.File.ReadAllLines(NomDico);// Lit chaque ligne du fichier dico_fr.txt et le stocke dans un tableau
            return tabMots;
        }




        public static void Initialiser_Pendu(string[,] pendu)
        {
            for (int i = 0; i < pendu.GetLength(0); i++) // Initialisation d'une matrice(11x20) pleine d'espace vide
            {
                for (int j = 0; j < pendu.GetLength(1); j++)
                {
                    pendu[i, j] = " ";
                }
            }
            for (int i = 1; i < pendu.GetLength(0); i++) // On créé les frontières verticales du cadre de gauche et de droite
            {
                pendu[i, 0] = "|";
                pendu[i, 19] = "|";
            }
            for (int j = 1; j < pendu.GetLength(1); j++)  // On créé les frontières horizontales du cadre du haut et du bas
            {
                pendu[0, j] = "-";
                pendu[10, j] = "-";
            }
            pendu[0, 0] = "+";  //Coin supérieur gauche
            pendu[0, 19] = "+"; //etc
            pendu[10, 0] = "+";
            pendu[10, 19] = "+";
        }




        public static void Initialisation_Partie(string[,] pendu, int modeJeu)
        {

            string niveau = " ";
            string Joueur1 = " ";
            string Joueur2 = " ";
            string role1 = " ";
            string role2 = " ";

            if (modeJeu == 1)  //mode JoueurVsJoueur
            {
                Console.WriteLine("Nom du Joueur 1 ?");
                Joueur1 = Console.ReadLine();
                Console.WriteLine("Nom du Joueur 2 ?");
                Joueur2 = Console.ReadLine();
                Console.WriteLine();
            }
            else  //mode JoueurVs_IA
            {
                Console.WriteLine("Nom du Joueur ?");
                Joueur1 = Console.ReadLine();
                Console.WriteLine("Vous jouerez contre une IA");
                Joueur2 = "IA";
                Console.WriteLine();
            }

            bool test1 = false;
            while (!test1)  // On s'assure avec cette boucle que le joueur choisisse bien une des reponses proposées (1 ou 2)
            {
                Console.WriteLine("Quel rôle souhaites-tu jouer {0} ? Tapes 1 pour 'faire deviner le mot' ou 2 pour 'chercher le mot' ", Joueur1);
                try
                {
                    int reponse = int.Parse(Console.ReadLine());
                    if (reponse == 1)
                    {
                        role1 = "Choisisseur_Du_Mot";
                        role2 = "Chercheur_D'un_Mot";
                        test1 = true;
                    }
                    else if (reponse == 2)
                    {
                        role1 = "Chercheur_Du_Mot";
                        role2 = "Choisisseur_D'un_Mot";
                        test1 = true;
                    }
                }
                catch (Exception e) // Renvoie la nature de l'erreur commise
                {
                    Console.WriteLine(e.Message);
                }
            }


            Console.WriteLine();
            bool test_difficulte = false;
            while (!test_difficulte)
            {
                Console.WriteLine("CHOIX DE LA DIFFCIULTE ? Entrez 'facile' (le mot fera moins de 6 lettres) ou 'difficile' (le mot fera 6 lettres ou plus)");
                try
                {
                    string reponse = Console.ReadLine();


                    if (reponse == "facile")
                    {
                        niveau = "facile";
                        test_difficulte = true;
                    }
                    else if (reponse == "difficile")
                    {
                        niveau = "difficile";
                        test_difficulte = true;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.WriteLine("La Partie va commencer ! {0} a le rôle du {1} , {2} a le rôle du {3}.", Joueur1, role1, Joueur2, role2);

            bool Recommencer = true;
            int changement = 0;
            while (Recommencer == true)    // Boucle permettant de recommencer une partie si les joueurs le souhaitent
            {
                if (modeJeu == 1)
                {
                    if (changement % 2 == 1)   // La valeur pair ou impair permet d'inverser les rôles des Joueurs s'ils l'ont souhaités
                    {
                        JoueurVsJoueur(Joueur2, Joueur1, role2, role1, pendu, niveau, changement);  // Lancement d'une Partie JoueurVsJoueur en inversant les rôles
                    }
                    else
                    {
                        JoueurVsJoueur(Joueur1, Joueur2, role1, role2, pendu, niveau, changement);  // Lancement d'une Partie JoueurVsJoueur sans inverser les rôles
                    }
                }
                else
                {
                    if (changement % 2 == 1)
                    {
                        JoueurVs_IA(Joueur2, Joueur1, role2, role1, pendu, niveau, changement);  // Lancement d'une Partie JoueurVs_IA en inversant les rôles
                    }
                    else
                    {
                        JoueurVs_IA(Joueur1, Joueur2, role1, role2, pendu, niveau, changement);  // Lancement d'une Partie JoueurVs_IA sans inverser les rôles
                    }
                }

                bool test2 = false;
                while (!test2)
                {
                    Console.WriteLine("Voulez-vous recommencer la Partie ? Tapes 1 pour 'oui' ou 2 pour 'non' ");
                    int reponse = int.Parse(Console.ReadLine());
                    if (reponse == 1)
                    {
                        test2 = true;
                        bool test3 = false;
                        Initialiser_Pendu(pendu);  // Il faut réinitialiser l'état du Pendu en cas de nouvelle partie !
                        while (!test3)
                        {
                            Console.WriteLine("Souhaitez-vous échanger les rôles ? Tapes 1 pour 'oui' ou 2 pour 'non'");
                            int reponse2 = int.Parse(Console.ReadLine());
                            if (reponse2 == 1)
                            {
                                changement++;
                                test3 = true;
                            }
                            else if (reponse2 == 2)
                            {
                                test3 = true;
                            }
                        }
                    }
                    else if (reponse == 2)
                    {
                        test2 = true;
                        Recommencer = false;
                    }
                }
            }
        }





        public static void JoueurVsJoueur(string Joueur1, string Joueur2, string role1, string role2, string[,] pendu, string niveau, int changement) // fonction qui s'occuppe de l'interaction du jeu en mode 'user vs user' et du déroulement de la partie
        {
            Changement_Role_Eventuel(Joueur1, Joueur2, role1, role2, changement);
            string motDevine = ChoixMot(niveau, false); //  Permet de choisir un mot valide du dico, en accord avec le niveau choisi (facile ou difficile)         
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("La partie peut commencer ! Veuillez choisir une lettre ou donner le mot directement");

            Deroulement_Partie(motDevine, pendu, false, 1);   // "false" par défaut comme il n'y a pas d'IA dans ce mode de jeu
        }



        public static void JoueurVs_IA(string Joueur1, string Joueur2, string role1, string role2, string[,] pendu, string niveau, int changement)
        {
            Changement_Role_Eventuel(Joueur1, Joueur2, role1, role2, changement);
            bool isIAChoisi;
            if (role1 == "Choisisseur_Du_Mot")
            {
                isIAChoisi = false;
            }
            else
            {
                isIAChoisi = true;
            }

            string motDevine = ChoixMot(niveau, isIAChoisi); //  Permet de choisir un mot valide du dico, en accord avec le niveau choisi (facile ou difficile)         
            Console.Clear();
            Console.WriteLine("La partie peut commencer ! Veuillez choisir une lettre ou donner le mot directement");
            Deroulement_Partie(motDevine, pendu, isIAChoisi, 2);
        }



        public static string ChoixMot(string niveau, bool isIAChoisi)
        {
            string[] tabMots = InitialiserTabMots("dico_fr.txt");
            bool motValide = false;
            bool present_dico = false;
            string Test_Mot = "";
            bool bonneTaille = false;
            while (!motValide) // Vérifie que le mot saisie est bien un mot existant
            {

                present_dico = false;
                if (!isIAChoisi)
                {
                    Test_Mot = Console.ReadLine();
                }
                else
                {
                    while (!bonneTaille)
                    {

                        int randIndice = (new Random()).Next(tabMots.Length);
                        Test_Mot = tabMots[randIndice].ToLower();
                        if (niveau == "facile" && Test_Mot.Length < 6)
                        {
                            bonneTaille = true;
                        }
                        if (niveau == "difficile" && Test_Mot.Length >= 6)
                        {
                            bonneTaille = true;
                        }
                    }

                }

                foreach (string mots in tabMots)
                {
                    string motMinuscule = mots.ToLower();
                    if (Test_Mot == motMinuscule)
                    {
                        present_dico = true;
                        if (niveau == "facile")   // meme si le mot existe bien dans le dico, il doit etre en accord avec le niveau choisi !
                        {
                            if (Test_Mot.Length < 6)
                            {
                                Console.WriteLine("le mot est valide");
                                motValide = true;
                            }
                        }
                        else
                        {
                            if (Test_Mot.Length >= 6)
                            {
                                Console.WriteLine("le mot est valide");
                                motValide = true;
                            }
                        }
                    }
                }
                if (!motValide)  // Explique pourquoi le mot n'est pas valide
                {
                    if (present_dico == true & niveau == "facile")
                    {
                        Console.WriteLine("Ce mot est trop long , recommencez !");
                    }
                    else if (present_dico == true & niveau == "difficile")
                    {
                        Console.WriteLine("Ce mot est trop court , recommencez !");
                    }
                    else
                    {
                        Console.WriteLine("Ce mot n'est pas valide, veuillez recommencer!");
                    }
                }
            }
            return Test_Mot;  // Renvoie le mot que devra deviner l'autre joueur
        }





        


        public static void afficheLettrePropose(List <char> lettrePropose)
        {
            if (lettrePropose.Count > 0)
            {
                Console.WriteLine("Les lettres déjà proposées sont: ");
                foreach (char caractere in lettrePropose)
                {
                    Console.Write(caractere + " ");
                }
            }
            Console.WriteLine();
        }



        public static void Deroulement_Partie(string motDevine, string[,] pendu, bool isIAChoisi, int modeJeu)
        {
            List<char> lettrePropose = new List<char>();   // cette liste contiendra les lettres déjà proposées par le joueur
            char[] motCherche = new char[motDevine.Length];
            int nombreErreur = 0;
            string lettreDevine = "";
            bool partieGagne = false;
            bool partiePerdue = false;
            string[] TabFreqMot = { "e", "a", "i", "s", "n", "r", "t", "o", "l", "u", "d", "c", "m", "p", "g", "b", "v", "h", "f", "q", "y", "x", "j", "k", "w", "z" };
            int indice = 0;

            for (int i = 0; i < motCherche.Length; i++)// Initialisation du tableau de lettres déjà trouvées du mot à deviner
            {
                motCherche[i] = '_';
            }

            while (!partieGagne && !partiePerdue)
            {
                try
                {
                    foreach (char caractere in motCherche)
                    {
                        Console.Write(caractere + "  ");
                    }
                    bool trouve = false;
                    Console.WriteLine();


                    if (modeJeu == 2)   // bloc spécifique au bloc JoueurVs_IA
                    {
                        if (!isIAChoisi)
                        {
                            lettreDevine = TabFreqMot[indice];
                            indice++;
                        }
                        else
                        {
                            afficheLettrePropose(lettrePropose);   // Affiche les lettres déjà proposées
                            Console.WriteLine("Proposez une nouvelle lettre ou le mot directment");
                            lettreDevine = Console.ReadLine();
                        }

                        char lettre = lettreDevine[0];
                        // Aide(motDevine, motCherche, lettre);
                        for (int i = 0; i < motDevine.Length; i++)
                        {
                            if (lettre == motDevine[i])
                            {
                                motCherche[i] = lettre;
                                trouve = true;
                            }
                        }
                        lettrePropose.Add(lettre);
                    }


                    else   // bloc spécifique au bloc Joueur_VS_Joueur
                    {
                        afficheLettrePropose(lettrePropose);   // Affiche les lettres déjà proposées
                        Console.WriteLine("Proposez une nouvelle lettre ou le mot directment");
                        lettreDevine = Console.ReadLine();


                        if (lettreDevine.Length > 1)  // L'utilisateur a tapé un mot
                        {
                            string tentative = lettreDevine;
                            if (tentative == motDevine)
                            {
                                partieGagne = true;
                                Console.WriteLine("FELICITATION VOUS AVEZ TROUVE LE MOT");
                            }
                            else
                            {
                                partiePerdue = true;
                                Console.WriteLine("DOMMAGE VOUS AVEZ PERDU !  Le mot était " + motDevine);
                            }
                        }
                        else  // L'utilisateur a tapé une lettre
                        {
                            char lettre = lettreDevine[0];
                            // Aide(motDevine, motCherche, lettre);
                            for (int i = 0; i < motDevine.Length; i++)
                            {
                                if (lettre == motDevine[i])
                                {
                                    motCherche[i] = lettre;
                                    trouve = true;
                                }
                            }
                            lettrePropose.Add(lettre);  // on met à jour la liste des lettres proposées
                        }
                    }

                    if (!trouve)
                    {
                        nombreErreur++;

                    }
                    Console.WriteLine("\n");
                    Affiche_Pendu(pendu, nombreErreur);

                    string motTrouve = "";
                    for (int i = 0; i < motDevine.Length; i++)
                    {
                        motTrouve += motCherche[i];
                    }

                    if (motTrouve == "" + motDevine && nombreErreur < 7)
                    {
                        partieGagne = true;
                        Console.WriteLine("FELICITATION VOUS AVEZ TROUVE LE MOT !   " + motDevine);
                    }
                    if (nombreErreur >= 7)
                    {
                        partiePerdue = true;
                        Console.WriteLine("DOMMAGE VOUS AVEZ PERDU !   le mot était   " + motDevine);
                    }

                    if (modeJeu == 2)  //  bloc spécifique au bloc JoueurVs_IA
                    {
                        if (!isIAChoisi)
                        {
                            System.Threading.Thread.Sleep(3000);  // L'IA marque une pause de 3 sec avant de jouer
                        }
                    }


                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }





        public static void Potence(string[,] pendu)
        {
            for (int j = 3; j < 15; j++) // Dessin du sol de la potence
            {
                pendu[8, j] = "_";
            }
            for (int i = 2; i < 9; i++)  // Dessin de la barre verticale de la potence
            {
                pendu[i, 4] = "|";
            }
            for (int j = 5; j < 16; j++)  // Dessin du reste de la potence (barre horizontale + corde + poutre de soutien)
            {
                pendu[2, j] = "-";
            }
            pendu[2, 13] = "|";
            pendu[2, 6] = "/";
            pendu[3, 5] = "/";
        }




        public static void Tete(string[,] pendu)
        {
            pendu[3, 13] = "O";
        }



        public static void Corps(string[,] pendu)
        {
            pendu[4, 13] = "|";
            pendu[5, 13] = "|";
        }



        public static void Bras_Gauche(string[,] pendu)
        {
            pendu[4, 12] = "/";
        }



        public static void Bras_Droit(string[,] pendu)
        {
            pendu[4, 14] = "/";
        }



        public static void Jambe_Gauche(string[,] pendu)
        {
            pendu[6, 12] = "/";
        }



        public static void Jambe_Droite(string[,] pendu)
        {
            pendu[6, 14] = "/";
        }





        public static void Affiche_Pendu(string[,] pendu, int Nb_Erreur)
        {
            switch (Nb_Erreur)  // A chaque nouvelle erreur, une partie du pendu devient visible
            {
                case 1:
                    Potence(pendu);
                    break;
                case 2:
                    Potence(pendu);
                    Tete(pendu);
                    break;
                case 3:
                    Potence(pendu);
                    Tete(pendu);
                    Corps(pendu);
                    break;
                case 4:
                    Potence(pendu);
                    Tete(pendu);
                    Corps(pendu);
                    Bras_Gauche(pendu);
                    break;
                case 5:
                    Potence(pendu);
                    Tete(pendu);
                    Corps(pendu);
                    Bras_Gauche(pendu);
                    Bras_Droit(pendu);
                    break;
                case 6:
                    Potence(pendu);
                    Tete(pendu);
                    Corps(pendu);
                    Bras_Gauche(pendu);
                    Bras_Droit(pendu);
                    Jambe_Gauche(pendu);
                    break;
                case 7:
                    Potence(pendu);
                    Tete(pendu);
                    Corps(pendu);
                    Bras_Gauche(pendu);
                    Bras_Droit(pendu);
                    Jambe_Gauche(pendu);
                    Jambe_Droite(pendu);
                    break;
            }

            for (int i = 0; i < pendu.GetLength(0); i++)    // Affichage du pendu
            {
                for (int j = 0; j < pendu.GetLength(1); j++)
                {
                    Console.Write(pendu[i, j]);
                }
                Console.WriteLine();
            }

        }




        public static int Menu()
        {
            Console.WriteLine("******************");
            Console.WriteLine("* JEU DU PENDU *");
            Console.WriteLine("******************");
            Console.WriteLine();

            int modeJeu = -99;
            while (modeJeu <= 0 || modeJeu >= 3)
            {
                Console.WriteLine("Quel mode de jeu voulez vous faire? \n 1: Joueur VS Joueur\n 2: Joueur VS IA");
                try
                {
                    modeJeu = int.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return modeJeu;


        }




        public static void Aide(string MotDevine, char[] Motcherche, char tentative)//Affiche une lettre si l'utilisateur 
        {
            Random rnd = new Random();
            bool aideEffectuee = false;


            int indexLettreCherche = rnd.Next(MotDevine.Length);
            if (tentative == '?' && aideEffectuee)
            {
                Motcherche[indexLettreCherche] = MotDevine[indexLettreCherche];
                Console.WriteLine("AIDE");
                aideEffectuee = true;
            }

        }


      

        public static void Changement_Role_Eventuel(string Joueur1, string Joueur2, string role1, string role2, int changement)
        {
            if (changement % 2 == 0)   // La valeur pair ou impair permet d'inverser les rôles des Joueurs s'ils l'ont souhaités
            {
                if (role1 == "Choisisseur_Du_Mot")
                {
                    Console.WriteLine("{0} doit choisir un mot à faire deviner", Joueur1);
                }
                else
                {
                    Console.WriteLine("{0} doit choisir un mot à faire deviner", Joueur2);
                }
            }
            else
            {
                if (role1 == "Choisisseur_Du_Mot")
                {
                    Console.WriteLine("{0} doit choisir un mot à faire deviner", Joueur2);
                }
                else
                {
                    Console.WriteLine("{0} doit choisir un mot à faire deviner", Joueur1);
                }
            }
        }



        public static void Minuteur()
        {
            int compteur = 10; // Attendre 10 secondes
            while (compteur != 0)
            {
                Console.WriteLine("Temps: " + compteur--);
                System.Threading.Thread.Sleep(1000);
            }
            Console.Read();
        }

       

    }
}

