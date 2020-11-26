using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pendu
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] pendu = new string[11, 20];
            Initialiser_Pendu(pendu);
            Initialisation_Partie(pendu);
            
            
            

        }


        public static string[] InitialiserTabMots(string NomDico) // renvoi un tableau qui contient tous les mots du dico
        {
            

            string[] tabMots =System.IO.File.ReadAllLines(NomDico);//lit chaque ligne du fichier dico_fr.txt et le stocke dans un tableau
            
            return tabMots;
        }
        
        public static void Initialiser_Pendu(string[,] pendu)
        {
            for (int i = 0; i < pendu.GetLength(0); i++) // initialisation d'une matrice(11x20) pleine d'espace vide
            {
                for (int j = 0; j < pendu.GetLength(1); j++)
                {
                    pendu[i, j] = " ";
                }
            }

            for (int i = 1; i < pendu.GetLength(0); i++) // on créé les frontières verticales du cadre de gauche et de droite
            {
                pendu[i, 0] = "|";
                pendu[i, 19] = "|";
            }
            for (int j = 1; j < pendu.GetLength(1); j++)  // on créé les frontières horizontales du cadre du haut et du bas
            {
                pendu[0, j] = "-";
                pendu[10, j] = "-";
            }
            pendu[0, 0] = "+";  //coin supérieur gauche
            pendu[0, 19] = "+"; //etc
            pendu[10, 0] = "+";
            pendu[10, 19] = "+";

        }



        public static void Potence(string[,] pendu)
        {
            for (int j = 3; j < 15; j++) // dessin du sol de la potence
            {
                pendu[8, j] = "_";
            }

            for (int i = 2; i < 9; i++)  // dessin de la barre verticale de la potence
            {
                pendu[i, 4] = "|";
            }
            for (int j = 5; j < 16; j++)  // dessin du reste de la potence (barre horizontale + corde + poutre de soutien)
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



        public static void Initialisation_Partie(string[,]pendu)
        {
            Console.WriteLine("********************");
            Console.WriteLine("*** JEU DU PENDU ***");
            Console.WriteLine("********************");
            Console.WriteLine();

            Console.WriteLine("Nom du Joueur 1 ?");
            string Joueur1 = Console.ReadLine();
            Console.WriteLine("Nom du Joueur 2 ?");
            string Joueur2 = Console.ReadLine();
            Console.WriteLine();

            int test1 = 1;
            string role1 = " ";
            string role2 = " ";
            string niveau = "";
            while (test1 != 0)  // on s'assure avec cette boucle que le joueur choisisse bien une des reponses proposées (1 ou 2)
            {
                Console.WriteLine("Quel rôle souhaites-tu jouer {0} ? Tapes 1 pour 'faire deviner le mot' ou 2 pour 'chercher le mot' ", Joueur1);
                try
                {
                    int reponse = int.Parse(Console.ReadLine());
                    if (reponse == 1)
                    {
                        role1 = "Choisisseur_Du_Mot";
                        role2 = "Chercheur_D'un_Mot";
                        test1 = 0;
                    }
                    else if (reponse == 2)
                    {
                        role1 = "Chercheur_Du_Mot";
                        role2 = "Choisisseur_D'un_Mot";
                        test1 = 0;
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
            }

            Console.WriteLine();
            int test_difficulte = 1;
            //string niveau = " ";
            while (test_difficulte != 0)
            {
                Console.WriteLine("CHOIX DE LA DIFFCIULTE ? Entrez 'facile'(le mot fera moins de 6 lettres) ou 'difficile' (le mot fera 10 lettres ou plus)");
                string reponse = Console.ReadLine();
                if (reponse == "facile")
                {
                    niveau = "facile";
                    test_difficulte = 0;
                }
                else if (reponse == "difficile")
                {
                    niveau = "difficile";
                    test_difficulte = 0;
                }
            }
            Console.WriteLine("La Partie va commencer ! {0} a le rôle du {1} , {2} a le rôle du {3}.", Joueur1, role1, Joueur2, role2);
            bool Recommencer = true;
            while (Recommencer )
            {
                int test2 = 1;
                JoueurVsJoueur(Joueur1, Joueur2, role1, role2, pendu,niveau);
                while (test2 != 0)
                {
                    Console.WriteLine("Voulez-vous recommencer la Partie ? Tapes 1 pour 'oui' ou 2 pour 'non' ");
                    int reponse = int.Parse(Console.ReadLine());

                    if (reponse == 1)
                    {
                        test2 = 0;
                        int test3 = 1;
                        Initialiser_Pendu(pendu);
                        while (test3 != 0)
                        {
                            Console.WriteLine("Souhaitez-vous échanger les rôles ? Tapes 1 pour 'oui' ou 2 pour 'non'");
                            int reponse2 = int.Parse(Console.ReadLine());
                            if (reponse2 == 1)
                            {
                                string copie_role1 = role1;
                                role1 = role2;
                                role2 = copie_role1;
                                test3 = 0;
                            }
                            else if (reponse2 == 2)
                            {
                                test3 = 0;
                            }
                        }
                    }
                    else if (reponse == 2)
                    {
                        test2 = 0;
                        Recommencer = false;
                    }
                }

            }



        }

        public static void JoueurVsJoueur(string Joueur1,string Joueur2,string role1,string role2,string[,] pendu, string niveau) // fonction qui s'occuppe de l'interaction du jeu et du déroulement de la partie
        {
            if (role1 == "Choisisseur_Du_Mot")
            {
                Console.WriteLine("{0} doit choisir un mot à faire deviner", Joueur1);
            }
            else
            {
                Console.WriteLine("{0} doit choisir un mot à faire deviner", Joueur2);
            }

            
            string motDevine="";
            bool motValide = false;
            bool present_dico = false;
            while (!motValide) // Vérifie que le mot saisie est bien un mot existant
                while (!motValide) // Vérifie que le mot saisie est bien un mot existant
                {
                    present_dico = false;
                    motDevine = Console.ReadLine();
                    foreach (string mots in InitialiserTabMots("dico_fr.txt"))
                    {
                        string motMinuscule = mots.ToLower();
                        if (motDevine == motMinuscule)
                        {
                            present_dico = true;
                            if (niveau == "facile")
                            {
                                if (motDevine.Length < 6)
                                {
                                    Console.WriteLine("le mot est valide");
                                    motValide = true;
                                }
                            }
                            else
                            {
                                if (motDevine.Length >= 6)
                                {
                                    Console.WriteLine("le mot est valide");
                                    motValide = true;
                                }
                            }

                        }
                    }
                    if (!motValide)
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
            Console.Clear();

            if (role1 == "Chercheur_Du_Mot")
            {
                Console.WriteLine("{0} doit trouver une lettre ou donner le mot directement", Joueur1);
            }
            else
            {
                Console.WriteLine("{0} doit trouver une lettre ou donner le mot directement", Joueur2);
            }

            AfficheMot(motDevine, pendu);

        }


        public static void AfficheMot(string motDevine,string[,] pendu)
        {


            char[] lettrePropose = new char[8];
                char[] motCherche = new char[motDevine.Length];
                int nombreErreur = 0;
                bool partieGagne = false;
                bool partiePerdue = false;

                //string motTrouve = "";


                for (int i = 0; i < motCherche.Length; i++)// initialisation du tableau de lettres déjà trouvées du mot à deviner
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
                    
                    char lettreDevine = char.Parse(Console.ReadLine());
                    
                    for (int i = 0; i < motDevine.Length; i++)
                    {
                        if (lettreDevine == motDevine[i])
                        {
                            motCherche[i] = lettreDevine;
                            trouve = true;
                        }
                        else
                        {
                            lettrePropose.SetValue(lettreDevine, i);
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
                    //Console.WriteLine("motTrouvé = {0}    motDevine= {1}", motTrouve, motDevine);
                    
                    if (motTrouve==""+motDevine && nombreErreur < 7)
                    {
                        
                        
                        partieGagne = true;
                        Console.WriteLine("FELICITATION VOUS AVEZ TROUVE LE MOT");
                    }

                    if (nombreErreur >= 7)
                    {
                        partiePerdue = true;
                        Console.WriteLine("DOMMAGE LE CHERCHEUR DE MOT A PERDU");
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                }
            }
           
            




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

            for (int i = 0; i < pendu.GetLength(0); i++)    // affichage du pendu
            {
                for (int j = 0; j < pendu.GetLength(1); j++)
                {
                    Console.Write(pendu[i, j]);
                }
                Console.WriteLine();
            }

        }

        public static void Menu()
        {
            Console.WriteLine("Quel mode de jeu voulez vous faire? \n Tapez 1 pour jouer contre une IA et 2 pour jouer contre quelqu'un");
            int modeJeu = int.Parse(Console.ReadLine());

            if (modeJeu == 2)
            {


            }

            if (modeJeu == 1)
            {


            }
            

        }

        public static void Aide()
        {

        }



    
    }
}
