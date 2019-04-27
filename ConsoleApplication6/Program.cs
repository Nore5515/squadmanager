using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//TO-DO
//1) Fix import/export with text documents FIXED
//2) Add scouts, sighhh
//3) have the build squad command be able to add, remove, reset
//4) be able to calculate total squad damage, damage to infantry and damage to vehicles
//5) implement "age" for each soldier
//6) Be able to deal damage randomly, in bulk and execution damage 
//6 a) Have a graveyard, as well as implement saving of wounds and such. damage dealt that isn't execution damage can only turn unwounded to wounded, or wounded to dead
//6 b) Be able to give a level of cover with the damage, to scale the total damage done.

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
                    //Console.WriteLine("Copy your Code Here:");
                    StreamReader sr = new StreamReader("C:\\Users\\Nore5515\\Documents\\Visual Studio 2015\\Projects\\ConsoleApplication6\\save.txt");
                    //reply = Console.ReadLine();
                    reply = sr.ReadLine();
                    sr.Close();
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
                        else if (manip.Substring(0, 1).Equals("2"))
                        {
                            s.setType("Sniper");
                        }
                        else if (manip.Substring(0, 1).Equals("3"))
                        {
                            s.setType("Engineer");
                        }
                        else if (manip.Substring(0, 1).Equals("4"))
                        {
                            s.setType("Machine Gunner");
                        }
                        else if (manip.Substring(0, 1).Equals("5"))
                        {
                            s.setType("AT Specialist");
                        }
                        else if (manip.Substring(0, 1).Equals("6"))
                        {
                            s.setType("Transport");
                        }
                        else if (manip.Substring(0, 1).Equals("7"))
                        {
                            s.setType("MRAP");
                        }
                        else
                        {
                            Console.WriteLine("ERR IN TYPE SET - IMPORT");
                        }

                        manip = manip.Substring(1, manip.Length - 1);
                        //Console.WriteLine(manip);
                        //Console.WriteLine($"-{manip.IndexOf("-")}-\t-{manip.Length - manip.IndexOf("-") - 1}-");
                        manip = manip.Substring(manip.IndexOf("-")+1, manip.Length - manip.IndexOf("-")-1);
                        //Console.WriteLine($"Manip: {manip}))");
                        //Console.WriteLine($"Name: {manip.Substring(0, manip.IndexOf("-"))}))");
                        s.setName(manip.Substring(0, manip.IndexOf("-")));

                        //Console.WriteLine(manip);
                        manip = manip.Substring(manip.IndexOf("-")+1);
                        //Console.WriteLine($"Manip: {manip}))");
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
        //Class - 0 is Conscript, 1 is Basic Infantry, 2 is Sniper, 3 is Engineer, 4 is Machine Gunner, 5 is AT Specialist, 6 is Transport, 7 is MRAP
        //after the first two numbers, then name and description
        //ISSUE; Cuts off one character from the end!!! I believe it to be the import...
        public string export()
        {
            StreamWriter sw = new StreamWriter("C:\\Users\\Nore5515\\Documents\\Visual Studio 2015\\Projects\\ConsoleApplication6\\save.txt");
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
                if (squad[x].getType().Equals("Sniper"))
                {
                    s += "2";
                }
                if (squad[x].getType().Equals("Engineer"))
                {
                    s += "3";
                }
                if (squad[x].getType().Equals("Machine Gunner"))
                {
                    s += "4";
                }
                if (squad[x].getType().Equals("AT Specialist"))
                {
                    s += "5";
                }
                if (squad[x].getType().Equals("Transport"))
                {
                    s += "6";
                }
                if (squad[x].getType().Equals("MRAP"))
                {
                    s += "7";
                }
                s += $"-{squad[x].getName()}-{squad[x].getDesc()}";
                s += "|";
            }
            //sw write all? s?
            sw.WriteLine(s);
            sw.Close();
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
                    if (!squad[x].noVehicle)
                    {
                        count += squad[x].getDMG();
                    }
                    
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
                if (good && s.getHP() != 0)
                {
                    
                    Console.WriteLine("What is the soldier's name? What is the soldier's unique trait?\nExample, 'Name Description', 'Carl He likes cats'");
                    reply = Console.ReadLine();

                    //get parts
                    try
                    {
                        name = reply.Substring(0, reply.IndexOf(" "));
                    }
                    catch (ArgumentOutOfRangeException) 
                    {
                        break;
                    }
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
        int setupTime;
        string name, description, type;
        bool wounded;
        bool setup;
        bool prospit;
        public bool noVehicle;
        public bool noInfantry;
        public bool vehicle;

        public Soldier()
        {
            DMG = 0;
            HP = 0;
            wounded = false;
            setupTime = 0;
            setup = true;
            name = "";
            description = "";
            noVehicle = false;
            noInfantry = false;
        }

        public Soldier(string s, bool b)
        {
            wounded = false;
            setupTime = 0;
            setup = true;
            noVehicle = false;
            noInfantry = false;
            type = s;
            updateType();

            prospit = b;
        }

        public void updateType()
        {
            string s = type;
            wounded = false;
            setupTime = 0;
            setup = true;
            noVehicle = false;
            noInfantry = false;
            //conscript
            if (s.Equals("conscript") || s.Equals("Conscript") || s.Equals("con") || s.Equals("c") || s.Equals("Con"))
            {
                DMG = 0.5f;
                HP = 0.5f;
                type = "Conscript";
            }
            //basic infantry
            if (s.Equals("basic") || s.Equals("Basic Infantry") || s.Equals("bi") || s.Equals("basic infantry") || s.Equals("infantry") || s.Equals("b"))
            {
                DMG = 1;
                HP = 1;
                type = "Basic Infantry";
            }
            //sniper
            if (s.Equals("sniper") || s.Equals("Sniper") || s.Equals("sn") || s.Equals("s"))
            {
                DMG = 16;
                HP = 1;
                type = "Sniper";
                setupTime = 2;
                setup = false;
                noVehicle = true;
            }
            //engineer
            if (s.Equals("engineer") || s.Equals("Engineer") || s.Equals("e") || s.Equals("eng") || s.Equals("engie") || s.Equals("Engie"))
            {
                DMG = 0.5f;
                HP = 1;
                type = "Engineer";
            }
            //machine gunner
            if (s.Equals("machine gunner") || s.Equals("Machine Gunner") || s.Equals("mg") || s.Equals("m") || s.Equals("Mach") || s.Equals("mach"))
            {
                DMG = 4;
                HP = 2;
                type = "Machine Gunner";
                setupTime = 1;
                setup = false;
            }
            //rocketeer
            if (s.Equals("AT Team") || s.Equals("Rocket Team") || s.Equals("rocket") || s.Equals("AT") || s.Equals("a") || s.Equals("r") || s.Equals("at"))
            {
                DMG = 16;
                HP = 1;
                type = "AT Specialist";
                setupTime = 1;
                setup = false;
                noInfantry = true;
            }
            //transport
            if (s.Equals("Transport") || s.Equals("transport") || s.Equals("tran") || s.Equals("t"))
            {
                DMG = 0;
                HP = 15;
                type = "Transport";
                vehicle = true;
            }
            //MRAP
            if (s.Equals("MRAP") || s.Equals("Jeep") || s.Equals("mrap") || s.Equals("mr") || s.Equals("j"))
            {
                DMG = 4;
                HP = 25;
                type = "MRAP";
                vehicle = true;
            }
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
            updateType();

            string s = getSpecies();
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

        public float getHP()
        {
            return HP;
        }

        public void setHP(float f)
        {
            HP = f;
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
            //Console.WriteLine(description);
            return description;
        }

        public void setDesc(string s)
        {
            description = s;
        }
    }

}
