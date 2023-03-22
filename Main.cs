using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace librerymain
{
    public class user
    {
        static void Main()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Välkommen till byblitek terminal för login: 1, för registrera: 2. ");
                var vadVillDu = Console.ReadLine();

                string fullPathLogin = "C:\\Users\\theo.hellstrompeter\\Documents\\programering 2\\c#\\Isak och dominik 09-09-22\\libreryprodjekt\\login.txt";
                var lins = File.ReadAllLines(fullPathLogin).Where(arg => !string.IsNullOrWhiteSpace(arg));
                File.WriteAllLines(fullPathLogin, lins);

                if (vadVillDu == "1")
                {
                    login();
                } else if (vadVillDu == "2")
                {
                    skapandeKonto();
                }
            }
        }

        static void login()
        {
            bool wrongPass = false;
            string personNum = "";
            string password = "";
            string namn = "";
            string efternamn = "";

            while(!LoginSheck(personNum, password))
            {
                Console.Clear();

                if (wrongPass)
                {
                    Console.WriteLine("Fel Lösenord! ");
                } else
                {
                    Console.WriteLine("Välkomen! ");
                }

                Console.WriteLine("För att logga in behöver du ange ditt personnummer och lösenord. ");
                Console.WriteLine("");

                Console.WriteLine("Personnummer: ");
                personNum = Console.ReadLine();

                Console.WriteLine("Namn: ");
                namn = Console.ReadLine();

                Console.WriteLine("Efternamn: ");
                efternamn = Console.ReadLine();

                Console.WriteLine("Password: ");
                password = Console.ReadLine();

                Console.WriteLine();

                wrongPass = true;
            }

            Sidan(WhoIs(namn, efternamn, personNum, password));
        }

        static void skapandeKonto()
        {
            Console.WriteLine("För att registrera dig ange Personnummer och lösenord");
            Console.WriteLine("");

            Console.WriteLine("Personnummer");
            var personNumber = Console.ReadLine();

            Console.WriteLine("Namn");
            var name = Console.ReadLine().ToLower();

            Console.WriteLine("efternamn");
            var efternamn = Console.ReadLine().ToLower();

            Console.WriteLine("Lösenord");
            var password = Console.ReadLine();

            Console.WriteLine("");

            if (isItHere(personNumber, password, name, efternamn))
            {
                Console.Clear();
                Console.WriteLine("Ge riktiga uppgifter");
                Console.WriteLine("");
                skapandeKonto();
                return;
            } else if (personNumFinns(personNumber)){
                Console.Clear();
                Console.WriteLine("Personnumbret finns redan. ");
                Console.WriteLine("");
                skapandeKonto();
                return;
            }

            var line = $"\n{name.ToLower()}!{efternamn.ToLower()}!{personNumber}!{password}";
            string[] lines = { line };

            File.AppendAllLines(@"C:\Users\theo.hellstrompeter\Documents\programering 2\c#\Isak och dominik 09-09-22\libreryprodjekt\login.txt", lines);
            
            Console.WriteLine("Du har registrerat ett konto :). ");

            Thread.Sleep(3000);

            Main();
        }

        static int WhoIs(string namn, string efternamn, string personnummer, string lösenord)
        {
            string[] person = System.IO.File.ReadAllLines(@"C:\Users\theo.hellstrompeter\Documents\programering 2\c#\Isak och dominik 09-09-22\libreryprodjekt\login.txt");

            int bajs = 0;

            for (int i = 0; i < person.Length; i++)
            {
                var line = person[i].Trim();
                string[] delar = line.Split("!");
                string namnFromDb = delar[0];
                string efternamnFromDb = delar[1];
                string personnummerFromDb = delar[2];
                string lösenordFromDb = delar[3];

                if (namnFromDb == namn && efternamnFromDb == efternamn && personnummerFromDb == personnummer && lösenordFromDb == lösenord)
                {
                    bajs = i;
                }
            }

            return bajs;
        }

        static bool LoginSheck(string persNum, string password) 
        {
            string[] person = System.IO.File.ReadAllLines(@"C:\Users\theo.hellstrompeter\Documents\programering 2\c#\Isak och dominik 09-09-22\libreryprodjekt\login.txt");

            for(int i = 0; i < person.Length; i++)
            {
                var line = person[i].Trim();
                string[] delar = line.Split("!");
                string personNumFormDb = delar[2];
                string passwordFromDb = delar[3];

                if (personNumFormDb == persNum && passwordFromDb == password)
                {
                    return true;    
                }
            }

            return false;
        }

        static bool personNumFinns(string personNumber)
        {
            string[] persssNum = System.IO.File.ReadAllLines(@"C:\Users\theo.hellstrompeter\Documents\programering 2\c#\Isak och dominik 09-09-22\libreryprodjekt\login.txt");

            for( int i = 0; i < persssNum.Length; i++)
            {
                var Linje = persssNum[i];
                
                if (Linje == personNumber)
                {
                    int theUser = i;
                    return true;
                }
            }

            return false;
        }

        static bool isItHere(string? personNumber, string? password, string? name, string? efternamn)
        {
            if (personNumber == null || personNumber == "") return true;
            if (password == null || password == "") return true;
            if (name == null || name == "") return true;
            if (efternamn == null || efternamn == "") return true;

            return false;
        }

        static void Sidan(int idd)
        {

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Profilsidan:");
                Console.WriteLine("Du är inloggad. ");
                Console.WriteLine("");

                if (!HarHyrt(idd))
                {
                    Console.WriteLine("Vill du ändra login:1, lämna tillbaka din bok:2 eller logga ut:3. ");
                    var ditVal = Console.ReadLine();

                    if (ditVal == "1")
                    {
                        DuByterSkit(idd);
                    }
                    else if (ditVal == "2")
                    {
                        VillInteHaLängre(idd);
                    }
                    else if (ditVal == "3")
                    {
                        Main();
                    }
                    else
                    {

                    }
                } else if (HarHyrt(idd))
                {
                    Console.WriteLine("Vill du ändra login:1, hyra en bok:2 eller logga ut:3. ");
                    var ditVal = Console.ReadLine();

                    if (ditVal == "1")
                    {
                        DuByterSkit(idd);
                    }
                    else if (ditVal == "2")
                    {
                        HyrDinBok(idd);
                    }
                    else if (ditVal == "3")
                    {
                        Main();
                    }
                    else
                    {

                    }
                }
            }
        }

        static void DuByterSkit (int vilken)
        {
            Console.Clear();
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\theo.hellstrompeter\Documents\programering 2\c#\Isak och dominik 09-09-22\libreryprodjekt\login.txt");

            for (int i = 0; i < lines.Length; i++)
            {
                if (i == vilken)
                {
                    var line = lines[i].Trim();
                    string[] deDel = line.Split('!');

                    Console.WriteLine("Skriv nytt lösenord. ");
                    var ny = Console.ReadLine();

                    var daSkit = $"{deDel[0]}!{deDel[1]}!{deDel[2]}!{ny}";

                    lines[i] = daSkit;

                    File.WriteAllLines(@"C:\Users\theo.hellstrompeter\Documents\programering 2\c#\Isak och dominik 09-09-22\libreryprodjekt\login.txt", lines);
                }
            }
        }

        static void HyrDinBok(int vilken)
        {
            string[] bok = System.IO.File.ReadAllLines(@"C:\Users\theo.hellstrompeter\Documents\programering 2\c#\Isak och dominik 09-09-22\libreryprodjekt\bok.txt");
            string[] person = System.IO.File.ReadAllLines(@"C:\Users\theo.hellstrompeter\Documents\programering 2\c#\Isak och dominik 09-09-22\libreryprodjekt\login.txt");
            int ja = 1;

            while (ja == 1)
            {
                Console.Clear();
                int num = 0;



                for (int i = 0; i < bok.Length; i++)
                {
                    var line = bok[i].Trim();
                    string[] lines = line.Split('!');
                    if (lines[3] == "0")
                    {
                        var bb = bok[i].Replace("!", " ").Trim(new Char[] { '1', '0' });
                        Console.WriteLine(bb);
                        num += 1;
                        Console.WriteLine($"boken är nummer {num}: ");
                        Console.WriteLine("");
                    }
                }

                Console.WriteLine("Vilken bok vill du hyra. ");
                string c = Console.ReadLine();
                int numm;
                Int32.TryParse(c, out numm);
                numm -= 1;

                for (int i = 0; i < person.Length; i++)
                {
                    if (i == vilken)
                    {
                        var line = person[i].Trim();
                        string[] lines = line.Split('!');

                        var del = $"{lines[0]}!{lines[1]}!{lines[2]}!{lines[3]}!{numm}";

                        person[i] = del;
                        File.WriteAllLines(@"C:\Users\theo.hellstrompeter\Documents\programering 2\c#\Isak och dominik 09-09-22\libreryprodjekt\login.txt", person);
                    }
                }

                for (int i = 0; i < bok.Length; i++)
                {
                    if (numm == i)
                    {
                        var line = bok[i].Trim();
                        string[] lines = line.Split('!');

                        var del = $"{lines[0]}!{lines[1]}!{lines[2]}!1";

                        bok[i] = del;
                        File.WriteAllLines(@"C:\Users\theo.hellstrompeter\Documents\programering 2\c#\Isak och dominik 09-09-22\libreryprodjekt\bok.txt", bok);

                        ja = 0;
                    }
                }
            }
        }

        static void VillInteHaLängre(int vilken)
        {
            string[] person = System.IO.File.ReadAllLines(@"C:\Users\theo.hellstrompeter\Documents\programering 2\c#\Isak och dominik 09-09-22\libreryprodjekt\login.txt");
            string[] bok = System.IO.File.ReadAllLines(@"C:\Users\theo.hellstrompeter\Documents\programering 2\c#\Isak och dominik 09-09-22\libreryprodjekt\bok.txt");

            Console.Clear();

            for (int i = 0; i < bok.Length; i++)
            {
                var line = bok[i].Trim();
                string[] lines = line.Split("!");

                if (lines[3] == "1")
                {
                    var del = $"{lines[0]}!{lines[1]}!{lines[2]}!0";
                    bok[i] = del;

                    File.WriteAllLines(@"C:\Users\theo.hellstrompeter\Documents\programering 2\c#\Isak och dominik 09-09-22\libreryprodjekt\bok.txt", bok);
                }
            }

            var bad = person[vilken].Trim();
            string[] bad1 = bad.Split("!");

            var dell = $"{bad1[0]}!{bad1[1]}!{bad1[2]}!{bad1[3]}";
            person[vilken] = dell;

            File.WriteAllLines(@"C:\Users\theo.hellstrompeter\Documents\programering 2\c#\Isak och dominik 09-09-22\libreryprodjekt\login.txt", person);

            Console.WriteLine("Du har lämnat tillbaka din bok. ");
            Thread.Sleep(3000);
        }

        static bool HarHyrt (int vilken)
        {
            string[] pers = System.IO.File.ReadAllLines(@"C:\Users\theo.hellstrompeter\Documents\programering 2\c#\Isak och dominik 09-09-22\libreryprodjekt\login.txt");

            var line = pers[vilken].Trim();
            string[] lines = line.Split("!");

            if (lines.Length == 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}