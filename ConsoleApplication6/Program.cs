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
                Console.WriteLine("\t'view' - View your current squad\n\t'full view' - View your squad in it's entirety!\n\t'export' - Displays your squad code.");
                Console.WriteLine("\t'import' - Import a squad from a chain of text");
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
                else if (reply.Equals("full view"))
                {
                    for (int x = 0; x < main.squad.Length; x++)
                    {
                        Console.WriteLine(main.squad[x].ToString());
                    }
                }
                else if (reply.Equals("export"))
                {
                    Console.WriteLine(main.export());
                }
                else if (reply.Equals("import"))
                {
                    Console.WriteLine("Copy your Code Here:");
                    reply = Console.ReadLine();
                    string manip;
                    bool importing = true;
                    int count = 0;
                    Soldier s;
                    while (importing)
                    {
                        if (reply.Length < 3)
                        {
                            importing = false;
                            break;
                        }
                        s = new Soldier();
                        manip = reply.Substring(0,reply.IndexOf("|"));
                        if (manip.Substring(0, 1).Equals("0"))
                        {
                            s.setSpecies("Derse");
                        }
                        else if (manip.Substring(0,1).Equals("1"))
                        {
                            s.setSpecies("Prospit");
                        }
                        else
                        {
                            Console.WriteLine("ERR IN SPECIES SET - IMPORT");
                        }

                        manip = manip.Substring(1, manip.Length - 1);

                        if (manip.Substring(0, 1).Equals("0"))
                        {
                            s.setType("Conscript");
                        }
                        else if (manip.Substring(0, 1).Equals("1"))
                        {
                            s.setType("Basic Infantry");
                        }
                        else
                        {
                            Console.WriteLine("ERR IN TYPE SET - IMPORT");
                        }

                        manip = manip.Substring(1, manip.Length - 1);
                        //Console.WriteLine(manip);
                        //Console.WriteLine($"-{manip.IndexOf("-")}-\t-{manip.Length - manip.IndexOf("-") - 1}-");
                        manip = manip.Substring(manip.IndexOf("-")+1, manip.Length - manip.IndexOf("-")-2);
                        Console.WriteLine($"Manip: {manip}))");
                        Console.WriteLine($"Name: {manip.Substring(0, manip.IndexOf("-"))}))");
                        s.setName(manip.Substring(0, manip.IndexOf("-")));
                        
                        manip = manip.Substring(manip.IndexOf("-")+1);
                        Console.WriteLine($"Manip: {manip}))");
                        s.setDesc(manip);

                        reply = reply.Substring(reply.IndexOf("|")+1, reply.Length-reply.IndexOf("|")-1);
                        s.update();
                        main.addSoldier(s);
                        count++;
                    }
                    Console.WriteLine("Import Complete!");
                }
                else
                {
                    Console.WriteLine("Sorry, I didn't understand that.");
                }
            }


        }


        //Type - 0 is Derse, 1 is Prospit
        //Class - 0 is Conscript, 1 is Basic Infantry
        //after the first two numbers, then name and description
        public string export()
        {
            string s = "";
            for (int x = 0; x < squad.Length; x++)
            {
                if (squad[x].getSpecies().Equals("Derse"))
                {
                    s += "0";
                }
                if (squad[x].getSpecies().Equals("Prospit"))
                {
                    s += "1";
                }
                if (squad[x].getType().Equals("Conscript"))
                {
                    s += "0";
                }
                if (squad[x].getType().Equals("Basic Infantry"))
                {
                    s += "1";
                }
                s += $"-{squad[x].getName()}-{squad[x].getDesc()}";
                s += "|";
            }
            return s;
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
                    desc = reply.Substring(reply.IndexOf(" ")+1, reply.Length - reply.IndexOf(" ")-1);
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

            s += ($"{species} {type} {name}\n\t{description}\n\tHP (wounded): {HP}, {wounded}\n\tDMG: {DMG}");

            return s;
        }

        public void update()
        {
            string s = type;
            //conscript
            if (s.Equals("conscript") || s.Equals("Conscript") || s.Equals("con") || s.Equals("c") || s.Equals("Con"))
            {
                DMG = 0.5f;
                HP = 0.5f;
                wounded = false;
                type = "Conscript";
            }
            //basic infantry
            if (s.Equals("basic") || s.Equals("Basic Infantry") || s.Equals("bi") || s.Equals("basic infantry") || s.Equals("infantry"))
            {
                DMG = 1;
                HP = 1;
                wounded = false;
                type = "Basic Infantry";
            }

            s = prospit ? "Prospit" : "Derse";
            setSpecies(s);
        }

        public float getDMG()
        {
            return DMG;
        }

        public void setDMG(float i)
        {
            DMG = i;
        }

        public string getName()
        {
            return name;
        }

        public void setName(string s)
        {
            name = s;
        }

        public string getType()
        {
            return type;
        }

        public void setType(string s)
        {
            type = s;
        }

        public bool getWounded()
        {
            return wounded;
        }

        public void setWounded (bool b)
        {
            wounded = b;
        }

        public string getSpecies()
        {
            String species = prospit ? "Prospit" : "Derse";
            return species;
        }

        public void setSpecies(string s)
        {
            if (s.Equals("Prospit"))
            {
                prospit = true;
            }
            else if (s.Equals("Derse"))
            {
                prospit = false;
            }
            else
            {
                Console.WriteLine("ERR IN SET SPECIES - SOLDIER");
            }
        }

        public string getDesc()
        {
            return description;
        }

        public void setDesc(string s)
        {
            description = s;
        }
    }

}
