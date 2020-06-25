using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public static class AuthorEntity
{
    static string Path = @"..\..\..\BooksStructure\Authors";
    static List<Author> AuthorsList = new List<Author>();
    static int CurrentID;

    static int IdentifyCurrentID()
    {
        int result = 0;
        string temptPath = Path + @"\CurrentID.txt";
        using (StreamReader sr = new StreamReader(temptPath, Encoding.Default))
            result = int.Parse(sr.ReadLine());
        return result;
    }

    static public void Download()
    {
        CurrentID = IdentifyCurrentID();
        DirectoryInfo dir = new DirectoryInfo(Path);
        foreach (FileInfo f in dir.GetFiles())
        {
            if ((f.Name != "Structure.txt") && (f.Name != "CurrentID.txt"))
            {
                Author aut = new Author();
                aut.Parse(f);
                AuthorsList.Add(aut);
            }
        }
    }

    static public void AddAuthor(string Name)
    {
        CurrentID++;
        using (StreamWriter sw = new StreamWriter(Path + @"\CurrentID.txt", false, Encoding.Default))
            sw.WriteLine(CurrentID);
        Author aut = new Author(CurrentID, Name);
        AuthorsList.Add(aut);
        string temptPath = Path + @"\" + CurrentID + ".txt";
        File.Create(temptPath).Close();
        using (StreamWriter sw = new StreamWriter(temptPath, true, Encoding.Default))
            sw.WriteLine(Name);
    }

    static public void DeleteAuthors(int Id, string Name)
    {
        bool IsBroken = true;
        while (IsBroken)
        {
            IsBroken = false;
            foreach (Author aut in AuthorsList)
                if (((Id == -1) || (aut.Id == Id)) && ((Name == "") || (aut.Name == Name)))
                {
                    File.Delete(Path + @"\" + aut.Id + ".txt");
                    AuthorsList.Remove(aut);
                    CurrentID--;
                    IsBroken = true;
                    break;
                }
        }
        using (StreamWriter sw = new StreamWriter(Path + @"\CurrentID.txt", false, Encoding.Default))
            sw.WriteLine(CurrentID);
    }

    static public void UpdateAutors(int Id, string Name, string NewName)
    {
        foreach (Author aut in AuthorsList)
            if (((Id == -1) || (aut.Id == Id)) && ((Name == "") || (aut.Name == Name)))
            {
                if (NewName != "")
                    aut.Name = NewName;
                using (StreamWriter sw = new StreamWriter(Path + @"\" + aut.Id + ".txt", false, Encoding.Default))
                    sw.WriteLine(aut.Name);
            }
    }

    static public void SelectAuthors(int Id, string Name, string param)
    {
        foreach (Author aut in AuthorsList)
            if (((Id == -1) || (aut.Id == Id)) && ((Name == "") || (aut.Name == Name)))
            {
                string[] par = param.Split(',');
                for (int i = 0; i < par.Length; i++)
                {
                    switch (par[i].Trim().ToLower())
                    {
                        case "*":
                            Console.Write("{0}\t{1}", aut.Id, aut.Name);
                            break;
                        case "id":
                            Console.Write("{0}\t", aut.Id);
                            break;
                        case "name":
                            Console.Write("{0}", aut.Name);
                            break;
                        default:
                            throw new Exception("Задан неизвестный параметр!");
                    }
                }
                Console.WriteLine();
            }
    }
}

public class Author
{
    internal int Id;
    internal string Name;

    internal Author() { }

    internal Author(int Id, string Name)
    {
        this.Id = Id;
        this.Name = Name;
    }

    internal void Parse(FileInfo f)
    {
        Id = int.Parse(f.Name.Substring(0, f.Name.IndexOf('.')));
        using (StreamReader sr = new StreamReader(f.DirectoryName + @"\" + f.Name, Encoding.Default))
            Name = sr.ReadLine();
    }
}

public static class BookEntity
{
    static string Path = @"..\..\..\BooksStructure\Books";
    static List<Book> BooksList = new List<Book>();
    static int CurrentID;

    static int IdentifyCurrentID()
    {
        int result = 0;
        string temptPath = Path + @"\CurrentID.txt";
        using (StreamReader sr = new StreamReader(temptPath, Encoding.Default))
            result = int.Parse(sr.ReadLine());
        return result;
    }

    static public void Download()
    {
        CurrentID = IdentifyCurrentID();
        DirectoryInfo dir = new DirectoryInfo(Path);
        foreach (FileInfo f in dir.GetFiles())
        {
            if ((f.Name != "Structure.txt") && (f.Name != "CurrentID.txt"))
            {
                Book book = new Book();
                book.Parse(f);
                BooksList.Add(book);
            }
        }
    }

    static public void AddBook(string Name, string Language, int Author, string Genres, int Quantity)
    {
        CurrentID++;
        using (StreamWriter sw = new StreamWriter(Path + @"\CurrentID.txt", false, Encoding.Default))
            sw.WriteLine(CurrentID);
        Book book = new Book(CurrentID, Name, Language, Author, ToIntMas(Genres), Quantity);
        BooksList.Add(book);
        string temptPath = Path + @"\" + CurrentID + ".txt";
        File.Create(temptPath).Close();
        using (StreamWriter sw = new StreamWriter(temptPath, true, Encoding.Default))
            sw.WriteLine(Name + "/" + Language + "/" + Author + "/" + Genres + "/" + Quantity);
    }

    static public void DeleteBooks(int Id, string Name, string Language, int Author, string Genres, int Quantity)
    {
        int[] Gen = ToIntMas(Genres);
        bool IsBroken = true;
        while (IsBroken)
        {
            IsBroken = false;
            foreach (Book book in BooksList)
                if (((Id == -1) || (book.Id == Id)) && ((Name == "") || (book.Name == Name)) && ((Language == "") || (book.Language == Language)) && ((Author == -1) || (book.Author == Author)) && (SubsetSearch(Gen, book.Genres)) && ((Quantity == -1) || (book.Quantity == Quantity)))
                {
                    File.Delete(Path + @"\" + book.Id + ".txt");
                    BooksList.Remove(book);
                    CurrentID--;
                    IsBroken = true;
                    break;
                }
        }
        using (StreamWriter sw = new StreamWriter(Path + @"\CurrentID.txt", false, Encoding.Default))
            sw.WriteLine(CurrentID);
    }

    static public void UpdateBooks(int Id, string Name, string Language, int Author, string Genres, int Quantity, string NewName, string NewLanguage, int NewAuthor, string NewGenres, int NewQuantity)
    {
        int[] Gen = ToIntMas(Genres);
        int[] NewGen = ToIntMas(NewGenres);
        if (NewGenres != "")
            Genres = NewGenres;
        foreach (Book book in BooksList)
            if (((Id == -1) || (book.Id == Id)) && ((Name == "") || (book.Name == Name)) && ((Language == "") || (book.Language == Language)) && ((Author == -1) || (book.Author == Author)) && (SubsetSearch(Gen, book.Genres)) && ((Quantity == -1) || (book.Quantity == Quantity)))
            {
                if (NewName != "")
                    book.Name = NewName;
                if (NewLanguage != "")
                    book.Language = NewLanguage;
                if (NewAuthor != -1)
                    book.Author = NewAuthor;
                if (NewGenres != "")
                    book.Genres = NewGen;
                if (NewQuantity != -1)
                    book.Quantity = NewQuantity;
                using (StreamWriter sw = new StreamWriter(Path + @"\" + book.Id + ".txt", false, Encoding.Default))
                    sw.WriteLine(book.Name + "/" + book.Language + "/" + book.Author + "/" + Genres + "/" + book.Quantity);
            }
    }

    static public void SelectBooks(int Id, string Name, string Language, int Author, string Genres, int Quantity, string param)
    {
        int[] Gen = ToIntMas(Genres);
        foreach (Book book in BooksList)
            if (((Id == -1) || (book.Id == Id)) && ((Name == "") || (book.Name == Name)) && ((Language == "") || (book.Language == Language)) && ((Author == -1) || (book.Author == Author)) && (SubsetSearch(Gen, book.Genres)) && ((Quantity == -1) || (book.Quantity == Quantity)))
            {
                string[] par = param.Split(',');
                for (int i = 0; i < par.Length; i++)
                {
                    switch (par[i].Trim().ToLower())
                    {
                        case "*":
                            Console.Write("{0}\t{1}\t{2}\t{3}\t{4}\t{5}", book.Id, book.Name, book.Language, book.Author, ConvertToStr(book.Genres), book.Quantity);
                            break;
                        case "id":
                            Console.Write("{0}\t", book.Id);
                            break;
                        case "name":
                            Console.Write("{0}\t", book.Name);
                            break;
                        case "language":
                            Console.Write("{0}\t", book.Language);
                            break;
                        case "author":
                            Console.Write("{0}\t", book.Author);
                            break;
                        case "genres":
                            Console.Write("{0}\t", ConvertToStr(book.Genres));
                            break;
                        case "quantity":
                            Console.Write("{0}\t", book.Quantity);
                            break;
                        default:
                            throw new Exception("Задан неизвестный параметр!");
                    }
                }
                Console.WriteLine();
            }
    }

    static int[] ToIntMas(string s)
    {
        string[] str = s.Split(',');
        int[] result = new int[100];
        for (int i = 0; i < str.Length; i++)
            result[i] = int.Parse(str[i]);
        return result;
    }

    static bool SubsetSearch(int[] subset, int[] bunch)
    {
        bool answer = true;
        int i = 0;
        while (subset[i] != 0)
        {
            int j = 0;
            bool find = false;
            while (bunch[j] != 0)
            {
                if (subset[i] == bunch[j])
                {
                    find = true;
                    break;
                }
                j++;
            }
            if (!find)
            {
                answer = false;
                break;
            }
            i++;
        }
        return answer;
    }

    static string ConvertToStr(int[] param)
    {
        string result = "";
        int i = 0;
        while (param[i] != 0)
        {
            result += param[i] + ", ";
            i++;
        }
        result = result.Remove(result.Length - 2, 2);
        return result;
    }
}

public class Book
{
    internal int Id;
    internal string Name;
    internal string Language;
    internal int Author;
    internal int[] Genres = new int[100];
    internal int Quantity;

    internal Book() { }

    internal Book(int Id, string Name, string Language, int Author, int[] Genres, int Quantity)
    {
        this.Id = Id;
        this.Name = Name;
        this.Language = Language;
        this.Author = Author;
        this.Genres = Genres;
        this.Quantity = Quantity;
    }

    internal void Parse(FileInfo f)
    {
        string line;
        using (StreamReader sr = new StreamReader(f.DirectoryName + @"\" + f.Name, Encoding.Default))
            line = sr.ReadLine();
        Id = int.Parse(f.Name.Substring(0, f.Name.IndexOf('.')));
        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0:
                    Name = line.Substring(0, line.IndexOf("/"));
                    break;
                case 1:
                    Language = line.Substring(0, line.IndexOf("/"));
                    break;
                case 2:
                    Author = int.Parse(line.Substring(0, line.IndexOf("/")));
                    break;
                case 3:
                    string s = line.Substring(0, line.IndexOf("/"));
                    int j = 0;
                    int index = s.IndexOf(",");
                    while (index != -1)
                    {
                        Genres[j] = int.Parse(s.Substring(0, index));
                        s = s.Remove(0, index + 1).Trim();
                        index = s.IndexOf(",");
                        j++;
                    }
                    Genres[j] = int.Parse(s);
                    break;

            }
            line = line.Remove(0, line.IndexOf("/") + 1);
        }
        line = line.Trim();
        Quantity = int.Parse(line);
    }
}

public class ExtraditionEntity
{
    static string Path = @"..\..\..\BooksStructure\Extraditions";
    static List<Extradition> ExtraditionsList = new List<Extradition>();
    static int CurrentID;

    static int IdentifyCurrentID()
    {
        int result = 0;
        string temptPath = Path + @"\CurrentID.txt";
        using (StreamReader sr = new StreamReader(temptPath, Encoding.Default))
            result = int.Parse(sr.ReadLine());
        return result;
    }

    static public void Download()
    {
        CurrentID = IdentifyCurrentID();
        DirectoryInfo dir = new DirectoryInfo(Path);
        foreach (FileInfo f in dir.GetFiles())
        {
            if ((f.Name != "Structure.txt") && (f.Name != "CurrentID.txt"))
            {
                Extradition ext = new Extradition();
                ext.Parse(f);
                ExtraditionsList.Add(ext);
            }
        }
    }

    static public void AddExtradition(int Id_book, int Id_human)
    {
        CurrentID++;
        using (StreamWriter sw = new StreamWriter(Path + @"\CurrentID.txt", false, Encoding.Default))
            sw.WriteLine(CurrentID);
        Extradition ext = new Extradition(CurrentID, Id_book, Id_human);
        ExtraditionsList.Add(ext);
        string temptPath = Path + @"\" + CurrentID + ".txt";
        File.Create(temptPath).Close();
        using (StreamWriter sw = new StreamWriter(temptPath, true, Encoding.Default))
            sw.WriteLine(Id_book + "/" + Id_human);
    }

    static public void DeleteExtraditions(int Id, int Id_book, int Id_human)
    {
        bool IsBroken = true;
        while (IsBroken)
        {
            IsBroken = false;
            foreach (Extradition ext in ExtraditionsList)
                if (((Id == -1) || (ext.Id == Id)) && ((Id_book == -1) || (ext.Id_book == Id_book)) && ((Id_human == -1) || (ext.Id_human == Id_human)))
                {
                    File.Delete(Path + @"\" + ext.Id + ".txt");
                    ExtraditionsList.Remove(ext);
                    CurrentID--;
                    IsBroken = true;
                    break;
                }
        }
        using (StreamWriter sw = new StreamWriter(Path + @"\CurrentID.txt", false, Encoding.Default))
            sw.WriteLine(CurrentID);
    }

    static public void UpdateExtraditions(int Id, int Id_book, int Id_human, int NewId_book, int NewId_human)
    {
        foreach (Extradition ext in ExtraditionsList)
            if (((Id == -1) || (ext.Id == Id)) && ((Id_book == -1) || (ext.Id_book == Id_book)) && ((Id_human == -1) || (ext.Id_human == Id_human)))
            {
                if (NewId_book != -1)
                    ext.Id_book = NewId_book;
                if (NewId_human != -1)
                    ext.Id_human = NewId_human;
                using (StreamWriter sw = new StreamWriter(Path + @"\" + ext.Id + ".txt", false, Encoding.Default))
                    sw.WriteLine(ext.Id_book + "/" + ext.Id_human);
            }
    }

    static public void SelectExtraditions(int Id, int Id_book, int Id_human, string param)
    {
        foreach (Extradition ext in ExtraditionsList)
            if (((Id == -1) || (ext.Id == Id)) && ((Id_book == -1) || (ext.Id_book == Id_book)) && ((Id_human == -1) || (ext.Id_human == Id_human)))
            {
                string[] par = param.Split(',');
                for (int i = 0; i < par.Length; i++)
                {
                    switch (par[i].Trim().ToLower())
                    {
                        case "*":
                            Console.Write("{0}\t{1}\t{2}", ext.Id, ext.Id_book, ext.Id_human);
                            break;
                        case "id":
                            Console.Write("{0}\t", ext.Id);
                            break;
                        case "id_book":
                            Console.Write("{0}\t", ext.Id_book);
                            break;
                        case "id_human":
                            Console.Write("{0}", ext.Id_human);
                            break;
                        default:
                            throw new Exception("Задан неизвестный параметр!");
                    }
                }
                Console.WriteLine();
            }
    }
}

public class Extradition
{
    internal int Id;
    internal int Id_book;
    internal int Id_human;

    internal Extradition() { }

    internal Extradition(int Id, int Id_book, int Id_human)
    {
        this.Id = Id;
        this.Id_book = Id_book;
        this.Id_human = Id_human;
    }

    internal void Parse(FileInfo f)
    {
        Id = int.Parse(f.Name.Substring(0, f.Name.IndexOf('.')));
        string line;
        using (StreamReader sr = new StreamReader(f.DirectoryName + @"\" + f.Name, Encoding.Default))
            line = sr.ReadLine();
        Id_book = int.Parse(line.Substring(0, line.IndexOf("/")));
        line = line.Remove(0, line.IndexOf("/") + 1).Trim();
        Id_human = int.Parse(line);
    }
}

public class GenreEntity
{
    static string Path = @"..\..\..\BooksStructure\Genres";
    static List<Genre> GenresList = new List<Genre>();
    static int CurrentID;

    static int IdentifyCurrentID()
    {
        int result = 0;
        string temptPath = Path + @"\CurrentID.txt";
        using (StreamReader sr = new StreamReader(temptPath, Encoding.Default))
            result = int.Parse(sr.ReadLine());
        return result;
    }

    static public void Download()
    {
        CurrentID = IdentifyCurrentID();
        DirectoryInfo dir = new DirectoryInfo(Path);
        foreach (FileInfo f in dir.GetFiles())
        {
            if ((f.Name != "Structure.txt") && (f.Name != "CurrentID.txt"))
            {
                Genre gen = new Genre();
                gen.Parse(f);
                GenresList.Add(gen);
            }
        }
    }

    static public void AddGenre(string Name)
    {
        CurrentID++;
        using (StreamWriter sw = new StreamWriter(Path + @"\CurrentID.txt", false, Encoding.Default))
            sw.WriteLine(CurrentID);
        Genre gen = new Genre(CurrentID, Name);
        GenresList.Add(gen);
        string temptPath = Path + @"\" + CurrentID + ".txt";
        File.Create(temptPath).Close();
        using (StreamWriter sw = new StreamWriter(temptPath, true, Encoding.Default))
            sw.WriteLine(Name);
    }

    static public void DeleteGenres(int Id, string Name)
    {
        bool IsBroken = true;
        while (IsBroken)
        {
            IsBroken = false;
            foreach (Genre gen in GenresList)
                if (((Id == -1) || (gen.Id == Id)) && ((Name == "") || (gen.Name == Name)))
                {
                    File.Delete(Path + @"\" + gen.Id + ".txt");
                    GenresList.Remove(gen);
                    CurrentID--;
                    IsBroken = true;
                    break;
                }
        }
        using (StreamWriter sw = new StreamWriter(Path + @"\CurrentID.txt", false, Encoding.Default))
            sw.WriteLine(CurrentID);
    }

    static public void UpdateGenres(int Id, string Name, string NewName)
    {
        foreach (Genre gen in GenresList)
            if (((Id == -1) || (gen.Id == Id)) && ((Name == "") || (gen.Name == Name)))
            {
                if (NewName != "")
                    gen.Name = NewName;
                using (StreamWriter sw = new StreamWriter(Path + @"\" + gen.Id + ".txt", false, Encoding.Default))
                    sw.WriteLine(gen.Name);
            }
    }

    static public void SelectGenres(int Id, string Name, string param)
    {
        foreach (Genre gen in GenresList)
            if (((Id == -1) || (gen.Id == Id)) && ((Name == "") || (gen.Name == Name)))
            {
                string[] par = param.Split(',');
                for (int i = 0; i < par.Length; i++)
                {
                    switch (par[i].Trim().ToLower())
                    {
                        case "*":
                            Console.Write("{0}\t{1}", gen.Id, gen.Name);
                            break;
                        case "id":
                            Console.Write("{0}\t", gen.Id);
                            break;
                        case "name":
                            Console.Write("{0}", gen.Name);
                            break;
                        default:
                            throw new Exception("Задан неизвестный параметр!");
                    }
                }
                Console.WriteLine();
            }
    }
}

public class Genre
{
    internal int Id;
    internal string Name;

    internal Genre() { }

    internal Genre(int Id, string Name)
    {
        this.Id = Id;
        this.Name = Name;
    }

    internal void Parse(FileInfo f)
    {
        Id = int.Parse(f.Name.Substring(0, f.Name.IndexOf('.')));
        using (StreamReader sr = new StreamReader(f.DirectoryName + @"\" + f.Name, Encoding.Default))
            Name = sr.ReadLine();
    }
}

public class HumanEntity
{
    static string Path = @"..\..\..\BooksStructure\People";
    static List<Human> PeopleList = new List<Human>();
    static int CurrentID;

    static int IdentifyCurrentID()
    {
        int result = 0;
        string temptPath = Path + @"\CurrentID.txt";
        using (StreamReader sr = new StreamReader(temptPath, Encoding.Default))
            result = int.Parse(sr.ReadLine());
        return result;
    }

    static public void Download()
    {
        CurrentID = IdentifyCurrentID();
        DirectoryInfo dir = new DirectoryInfo(Path);
        foreach (FileInfo f in dir.GetFiles())
        {
            if ((f.Name != "Structure.txt") && (f.Name != "CurrentID.txt"))
            {
                Human hum = new Human();
                hum.Parse(f);
                PeopleList.Add(hum);
            }
        }
    }

    static public void AddHuman(string Name, string Contacts)
    {
        CurrentID++;
        using (StreamWriter sw = new StreamWriter(Path + @"\CurrentID.txt", false, Encoding.Default))
            sw.WriteLine(CurrentID);
        Human hum = new Human(CurrentID, Name, Contacts);
        PeopleList.Add(hum);
        string temptPath = Path + @"\" + CurrentID + ".txt";
        File.Create(temptPath).Close();
        using (StreamWriter sw = new StreamWriter(temptPath, true, Encoding.Default))
            sw.WriteLine(Name + "/" + Contacts);
    }

    static public void DeletePeople(int Id, string Name, string Contacts)
    {
        bool IsBroken = true;
        while (IsBroken)
        {
            IsBroken = false;
            foreach (Human hum in PeopleList)
                if (((Id == -1) || (hum.Id == Id)) && ((Name == "") || (hum.Name == Name)) && ((Contacts == "") || (hum.Contacts == Contacts)))
                {
                    File.Delete(Path + @"\" + hum.Id + ".txt");
                    PeopleList.Remove(hum);
                    CurrentID--;
                    IsBroken = true;
                    break;
                }
        }
        using (StreamWriter sw = new StreamWriter(Path + @"\CurrentID.txt", false, Encoding.Default))
            sw.WriteLine(CurrentID);
    }

    static public void UpdatePeople(int Id, string Name, string Contacts, string NewName, string NewContacts)
    {
        foreach (Human hum in PeopleList)
            if (((Id == -1) || (hum.Id == Id)) && ((Name == "") || (hum.Name == Name)) && ((Contacts == "") || (hum.Contacts == Contacts)))
            {
                if (NewName != "")
                    hum.Name = NewName;
                if (NewContacts != "")
                    hum.Contacts = NewContacts;
                using (StreamWriter sw = new StreamWriter(Path + @"\" + hum.Id + ".txt", false, Encoding.Default))
                    sw.WriteLine(Name + "/" + Contacts);
            }
    }

    static public void SelectPeople(int Id, string Name, string Contacts, string param)
    {
        foreach (Human hum in PeopleList)
            if (((Id == -1) || (hum.Id == Id)) && ((Name == "") || (hum.Name == Name)) && ((Contacts == "") || (hum.Contacts == Contacts)))
            {
                string[] par = param.Split(',');
                for (int i = 0; i < par.Length; i++)
                {
                    switch (par[i].Trim().ToLower())
                    {
                        case "*":
                            Console.Write("{0}\t{1}\t{2}", hum.Id, hum.Name, hum.Contacts);
                            break;
                        case "id":
                            Console.Write("{0}\t", hum.Id);
                            break;
                        case "name":
                            Console.Write("{0}\t", hum.Name);
                            break;
                        case "contacts":
                            Console.Write("{0}\t", hum.Contacts);
                            break;
                        default:
                            throw new Exception("Задан неизвестный параметр!");
                    }
                }
                Console.WriteLine();
            }
    }
}

public class Human
{
    internal int Id;
    internal string Name;
    internal string Contacts;

    internal Human() { }

    internal Human(int Id, string Name, string Contacts)
    {
        this.Id = Id;
        this.Name = Name;
        this.Contacts = Contacts;
    }

    internal void Parse(FileInfo f)
    {
        Id = int.Parse(f.Name.Substring(0, f.Name.IndexOf('.')));
        string line;
        using (StreamReader sr = new StreamReader(f.DirectoryName + @"\" + f.Name, Encoding.Default))
            line = sr.ReadLine();
        Name = line.Substring(0, line.IndexOf("/"));
        line = line.Remove(0, line.IndexOf("/") + 1);
        line = line.Trim();
        Contacts = line;
    }
}