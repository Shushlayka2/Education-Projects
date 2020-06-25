using System;

namespace CustomDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeTables();
            string s = ""; // Строка введенная в консоль
            while (true)
            {
                s = Console.ReadLine();
                if (s != "")
                {
                    try
                    {
                        string command = Cut(ref s).ToLower();
                        string table = "";
                        string set = "";
                        string param = "";
                        if (Parser(command, ref s, ref table, ref set, ref param))
                            throw new Exception("Syntax error!");
                        switch (command)
                        {
                            case "insert":
                                AddItems(table, s);
                                break;
                            case "delete":
                                DeleteItems(table, s);
                                break;
                            case "update":
                                UpdateItems(table, set, s);
                                break;
                            case "select":
                                SelectItems(table, param, s);
                                break;
                            case "close":
                                Environment.Exit(0);
                                break;
                            case "clear":
                                Console.Clear();
                                break;
                            default:
                                throw new Exception("Command " + command.ToUpper() + " wasn't found!");
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }          
                }
                s = "";
            }
        }

        static void InitializeTables()
        {
            AuthorEntity.Download();
            BookEntity.Download();
            ExtraditionEntity.Download();
            GenreEntity.Download();
            HumanEntity.Download();
        }

        static string Cut(ref string str)
        {
            string result;
            if (str.IndexOf(' ') != -1)
            {
                result = str.Substring(0, str.IndexOf(' '));
                result = result.Trim();
                str = str.Remove(0, str.IndexOf(' ') + 1);
            }
            else
            {
                result = str;
                str = "";
            }
            return result;
        }

        static bool Parser(string com, ref string s, ref string table, ref string set, ref string param)
        {
            bool Isfind = false; // Найдена ли ошибка?
            string tempt = "";
            switch (com)
            {
                case "insert":
                    tempt = Cut(ref s).ToLower();
                    if (tempt != "into")
                        Isfind = true;
                    table = Cut(ref s);
                    tempt = Cut(ref s).ToLower();
                    if (tempt != "values")
                        Isfind = true;
                    break;
                case "delete":
                    tempt = Cut(ref s).ToLower();
                    if (tempt != "from")
                        Isfind = true;
                    table = Cut(ref s);
                    tempt = Cut(ref s).ToLower();
                    if ((tempt != "where") && (tempt != ""))
                        Isfind = true;
                    break;
                case "update":
                    table = Cut(ref s);
                    tempt = Cut(ref s).ToLower();
                    if (tempt != "set")
                        Isfind = true;
                    if (s.IndexOf("where") != -1)
                    {
                        set = s.Substring(0, s.IndexOf("where")).Trim();
                        s = s.Remove(0, s.IndexOf("where"));
                        tempt = Cut(ref s).ToLower();
                        if ((tempt != "where") && (tempt != ""))
                            Isfind = true;
                    }
                    else
                    {
                        set = s;
                        s = "";
                    }
                    break;
                case "select":
                    param = s.Substring(0, s.IndexOf("from")).Trim();
                    s = s.Remove(0, s.IndexOf("from"));
                    tempt = Cut(ref s);
                    if (tempt != "from")
                        Isfind = true;
                    table = Cut(ref s);
                    tempt = Cut(ref s).ToLower();
                    if ((tempt != "where") && (tempt != ""))
                        Isfind = true;
                    break;
            }
            return Isfind;
        }

        static void AddItems(string table, string values)
        {
            values = values.TrimStart('(');
            values =  values.TrimEnd(')');
            string[] val = values.Split(';');
            for (int i = 0; i < val.Length; i++)
                val[i] = val[i].Trim();
            switch (table)
            {
                case "Authors":
                    AuthorEntity.AddAuthor(val[0]);
                    Console.WriteLine("Done!");
                    break;
                case "Books":
                    BookEntity.AddBook(val[0], val[1], int.Parse(val[2]), val[3], int.Parse(val[4]));
                    Console.WriteLine("Done!");
                    break;
                case "Extraditions":
                    ExtraditionEntity.AddExtradition(int.Parse(val[0]), int.Parse(val[1]));
                    Console.WriteLine("Done!");
                    break;
                case "Genres":
                    GenreEntity.AddGenre(val[0]);
                    Console.WriteLine("Done!");
                    break;
                case "People":
                    HumanEntity.AddHuman(val[0], val[1]);
                    Console.WriteLine("Done!");
                    break;
                default:
                    throw new Exception("Table " + table.ToUpper() + " wasn't found!");
            }
        }

        static void DeleteItems(string table, string where)
        {
            int Id = -1, Author = -1, Quantity = -1, Id_book = -1, Id_human = -1;
            string Name = "", Language = "", Genres = "", Contacts = "";
            Splitting(where, ref Id, ref Name, ref Language, ref Author, ref Genres, ref Quantity, ref Id_book, ref Id_human, ref Contacts);

            switch (table)
            {
                case "Authors":                  
                    AuthorEntity.DeleteAuthors(Id, Name);
                    Console.WriteLine("Done!");
                    break;
                case "Books":
                    BookEntity.DeleteBooks(Id, Name, Language, Author, Genres, Quantity);
                    Console.WriteLine("Done!");
                    break;
                case "Extraditions":
                    ExtraditionEntity.DeleteExtraditions(Id, Id_book, Id_human);
                    break;
                case "Genres":
                    GenreEntity.DeleteGenres(Id, Name);
                    break;
                case "People":
                    HumanEntity.DeletePeople(Id, Name, Contacts);
                    break;
                default:
                    throw new Exception("Table " + table.ToUpper() + " wasn't found!");
            }
        }

        static void UpdateItems(string table, string set, string where)
        {
            int Id = -1, Author = -1, Quantity = -1, Id_book = -1, Id_human = -1;
            string Name = "", Language = "", Genres = "", Contacts = "";
            Splitting(where, ref Id, ref Name, ref Language, ref Author, ref Genres, ref Quantity, ref Id_book, ref Id_human, ref Contacts);

            int NewId = -1, NewAuthor = -1, NewQuantity = -1, NewId_book = -1, NewId_human = -1;
            string NewName = "", NewLanguage = "", NewGenres = "", NewContacts = "";
            Splitting(set, ref NewId, ref NewName, ref NewLanguage, ref NewAuthor, ref NewGenres, ref NewQuantity, ref NewId_book, ref NewId_human, ref NewContacts);

            switch (table)
            {
                case "Authors":
                    AuthorEntity.UpdateAutors(Id, Name, NewName);
                    Console.WriteLine("Done!");
                    break;
                case "Books":
                    BookEntity.UpdateBooks(Id, Name, Language, Author, Genres, Quantity, NewName, NewLanguage, NewAuthor, NewGenres, NewQuantity);
                    Console.WriteLine("Done!");
                    break;
                case "Extraditions":
                    ExtraditionEntity.UpdateExtraditions(Id, Id_book, Id_human, NewId_book, NewId_human);
                    break;
                case "Genres":
                    GenreEntity.UpdateGenres(Id, Name, NewName);
                    break;
                case "People":
                    HumanEntity.UpdatePeople(Id, Name, Contacts, NewName, NewContacts);
                    break;
                default:
                    throw new Exception("Table " + table.ToUpper() + " wasn't found!");
            }
        }

        static void SelectItems(string table, string param, string where)
        {
            int Id = -1, Author = -1, Quantity = -1, Id_book = -1, Id_human = -1;
            string Name = "", Language = "", Genres = "", Contacts = "";
            Splitting(where, ref Id, ref Name, ref Language, ref Author, ref Genres, ref Quantity, ref Id_book, ref Id_human, ref Contacts);

            switch (table)
            {
                case "Authors":
                    AuthorEntity.SelectAuthors(Id, Name, param);
                    Console.WriteLine("Done!");
                    break;
                case "Books":
                    BookEntity.SelectBooks(Id, Name, Language, Author, Genres, Quantity, param);
                    Console.WriteLine("Done!");
                    break;
                case "Extraditions":
                    ExtraditionEntity.SelectExtraditions(Id, Id_book, Id_human, param);
                    break;
                case "Genres":
                    GenreEntity.SelectGenres(Id, Name, param);
                    break;
                case "People":
                    HumanEntity.SelectPeople(Id, Name, Contacts, param);
                    break;
                default:
                    throw new Exception("Table " + table.ToUpper() + " wasn't found!");
            }
        }

        static void Splitting(string str, ref int Id, ref string Name, ref string Language, ref int Author, ref string Genres, ref int Quantity, ref int Id_book, ref int Id_human, ref string Contacts)
        {
            string[] s = str.Split(';');
            string[][] arguments = new string[10][];
            if (str != "")
            {
                for (int i = 0; i < s.Length; i++)
                {
                    arguments[i] = new string[2];
                    arguments[i] = s[i].Split('=');
                    arguments[i][0] = arguments[i][0].Trim().ToLower();
                    arguments[i][1] = arguments[i][1].Trim();
                }
            }
            int j = 0;
            while (arguments[j] != null)
            {
                switch (arguments[j][0])
                {
                    case "id":
                        Id = int.Parse(arguments[j][1]);
                        break;
                    case "name":
                        Name = arguments[j][1];
                        break;
                    case "language":
                        Language = arguments[j][1];
                        break;
                    case "author":
                        Author = int.Parse(arguments[j][1]);
                        break;
                    case "genres":
                        Genres = arguments[j][1];
                        break;
                    case "quantity":
                        Quantity = int.Parse(arguments[j][1]);
                        break;
                    case "id_book":
                        Id_book = int.Parse(arguments[j][1]);
                        break;
                    case "id_human":
                        Id_human = int.Parse(arguments[j][1]);
                        break;
                    case "contacts":
                        Contacts = arguments[j][1];
                        break;
                    default:
                        throw new Exception("Parameter " + arguments[j][0] + " wasn't found!");
                }
                j++;
            }
        }
    }
}
