using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProDerSquads
{


    class Program
    {
        Soldier[] squad;

        public Program()
        {
            squad = new Soldier[0];
        }

        static void Main(string[] args)
        {

            Console.WriteLine("Hi!");
            string reply;
            bool playing = false;
            Program main = new Program();

            //Console.WriteLine("What would you like to do?\n\t'dmg' - Calculate damage dealt of a Squad\n\t'buildsquad' - Builds your Squad");
            while (!playing)
            {
                Console.WriteLine("What would you like to do?\n\t'dmg' - Calculate damage dealt of a Squad\n\t'buildsquad' - Builds your Squad");
                Console.WriteLine("\t'view' - View your current squad");
                reply = Console.ReadLine();
                if (reply.Equals("dmg"))
                {
                    Console.WriteLine(main.dmg());
                }
                else if (reply.Equals("buildsquad"))
                {
                    main.buildSquad();
                }
                else if (reply.Equals("view"))
                {
                    Console.WriteLine(main.view());
                }
                else
                {
                    Console.WriteLine("Sorry, I didn't understand that.");
                }
            }


        }

        public string view()
        {
            String s = "Your Squad:\n";
            for (int x = 0; x < squad.Length; x++)
            {
                s += ($"{squad[x].getSpecies()} {squad[x].getType()} - {squad[x].getName()}, {squad[x].getWounded()}\n");
            }
            return s;
        }

        public int dmg()
        {
            if (squad.Length == 0)
            {
                Console.WriteLine("You must build a squad first!");
                buildSquad();
                return -1;
            }
            else
            {
                float count = 0;
                for (int x = 0; x < squad.Length; x++)
                {
                    count += squad[x].getDMG();
                }
                int countish = Convert.ToInt32(Math.Floor(count));
                return countish;
            }
        }

        public void buildSquad()
        {
            Console.WriteLine("You should type out the full name of the unit, like 'Prospit Conscript' or 'derse con' or 'p c'.\n\tConsripts\n\tBasic Infantry");
            bool decided = false;
            bool prospit;
            bool good = false;
            string reply, soldier, type, name, desc;
            Soldier s = null;
            while (!decided)
            {
                Console.WriteLine("What soldiers would you like to add?\n\tType 'done' when you're finished.");
                reply = Console.ReadLine();
                //break loop
                if (reply.Equals("exit") || reply.Equals("back") || reply.Equals("done") || reply.Equals("Done"))
                {
                    break;
                }
                type = reply.Substring(0, reply.IndexOf(" "));
                soldier = reply.Substring(reply.IndexOf(" "), reply.Length - reply.IndexOf(" "));
                soldier = soldier.Substring(1, soldier.Length - 1);
                //
                Console.WriteLine("{0} -> {1}", soldier, type);
                //

                //make sure to substring reply
                //Prospit/Derse Determinate
                if (type.Contains("Prospit") || type.Contains("prospit") || type.Contains("p"))
                {
                    prospit = true;
                    good = true;
                }
                else if (type.Contains("Derse") || type.Contains("derse") || type.Contains("d"))
                {
                    prospit = false;
                    good = true;
                }
                else
                {
                    Console.WriteLine("Err; didn't specify Prospit or Derse");
                    good = false;
                    prospit = false;
                }

                //prompt
                s = new Soldier(soldier, prospit);
                //decide soldier's name and description ONLY IF they are already designed
                if (good && s.getDMG() != 0)
                {
                    
                    Console.WriteLine("What is the soldier's name? What is the soldier's unique trait?\nExample, 'Name Description', 'Carl He likes cats'");
                    reply = Console.ReadLine();

                    //get parts
                    name = reply.Substring(0, reply.IndexOf(" "));
                    desc = reply.Substring(reply.IndexOf(" "), reply.Length - reply.IndexOf(" "));
                    //assign parts
                    s.nameSoldier(name, desc);
                    addSoldier(s);
                }
                else
                {
                    Console.WriteLine("That soldier doesn't exist!");
                }

                if (s != null)
                {
                    Console.WriteLine(s.ToString());
                }
                

            }
            
        }

        public void addSoldier(Soldier s)
        {
            Soldier[] temp = new Soldier[squad.Length + 1];
            for (int x = 0; x < squad.Length; x++)
            {
                temp[x] = squad[x];
            }
            temp[squad.Length] = s;
            squad = temp;
        }
    }


    class Soldier
    {
        float DMG, HP;
        string name, description, type;
        bool wounded;
        bool prospit;

        public Soldier()
        {
            DMG = 0;
            HP = 0;
            wounded = false;
            name = "";
            description = "";
        }

        public Soldier(string s, bool b)
        {
            //conscript
            if (s.Equals("conscript") || s.Equals("Conscript") || s.Equals("con") || s.Equals("c") || s.Equals("Con"))
            {
                DMG = 0.5f;
                HP = 0.5f;
                wounded = false;
                type = "Conscript";
            }
            //basic infantry
            if (s.Equals("basic")||s.Equals("Basic Infantry")||s.Equals("bi")||s.Equals("basic infantry")||s.Equals("infantry"))
            {
                DMG = 1;
                HP = 1;
                wounded = false;
                type = "Basic Infantry";
            }

            prospit = b;
        }

        public void nameSoldier(string n, string d)
        {
            name = n;
            description = d;
        }

        public void nameSoldier(string n)
        {
            name = n;
        }

        public override string ToString()
        {
            String s = "";
            String species = prospit ? "Prospit" : "Derse";

            s += ($"{species} {name}\n\t{description}\n\t{HP}, {wounded}\n\t{DMG}");

            return s;
        }

        public float getDMG()
        {
            return DMG;
        }

        public string getName()
        {
            return name;
        }

        public string getType()
        {
            return type;
        }

        public bool getWounded()
        {
            return wounded;
        }

        public string getSpecies()
        {
            String species = prospit ? "Prospit" : "Derse";
            return species;
        }
    }

}
