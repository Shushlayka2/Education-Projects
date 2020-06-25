using System;
using System.IO;

namespace ForYandex
{
    class Order
    {
        internal int Id { get; private set; }
        internal string Dt { get; private set; }
        internal string Amount { get; private set; }
        int product_id;
        internal int Product_id
        {
            get
            {
                return product_id;
            }
            private set
            {
                if ((value > 0) && (value < 8))
                    product_id = value;
                else
                    throw new Exception("Параметр 'product_id' вне границ возможных значений.");
            }
        }


        static string Path;
        static StreamReader sr;
        static string[] Struct;

        internal Order(string filePath)
        {
            Path = filePath;
            sr = new StreamReader(Path);
            StructIdentify();
        }

        protected Order() { }

        static void StructIdentify()
        {
            string line = sr.ReadLine();
            Struct = line.Split('\t');
            if (!IsStructRight())
                throw new Exception("Неправильно введены названия столбцов!");
        }

        static bool IsStructRight()
        {
            if (Struct.Length != 4)
                return false;

            foreach (string field in Struct)
                if ((field != "id") && (field != "dt") && (field != "product_id") && (field != "amount"))
                    return false;

            for (int i = 0; i < 4; i++)
                for (int j = i + 1; j < 4; j++)
                    if (Struct[i] == Struct[j])
                        return false;

            return true;
        }

        static internal Order GiveNextOrder(int currentRowNumber, out bool isComplete)
        {
            string row = sr.ReadLine();
            if (row != null)
                isComplete = true;
            else
            {
                isComplete = false;
                return null;
            }

            try
            {
                string[] elems = row.Split('\t');
                if (elems.Length != 4)
                    throw new Exception("Несоответствующее количество элементов.");
                Order nextOrder = new Order();
                for (int i = 0; i < 4; i++)
                {
                    int numform;
                    switch (Struct[i])
                    {
                        case "id":
                            if (int.TryParse(elems[i], out numform))
                                nextOrder.Id = numform;
                            else
                                throw new Exception("Неверный тип параметра 'id'.");
                            break;
                        case "dt":
                            DateTime datatimeform;
                            if (DateTime.TryParse(elems[i], out datatimeform))
                                nextOrder.Dt = elems[i];
                            else
                                throw new Exception("Неверный тип параметра 'dt'.");
                            break;
                        case "amount":
                            double doubform;
                            elems[i] = elems[i].Replace('.', ',');
                            if (double.TryParse(elems[i], out doubform))
                            {
                                elems[i] = elems[i].Replace(',', '.');
                                nextOrder.Amount = elems[i];
                            }
                            else
                                throw new Exception("Неверный тип параметра 'amount'.");
                            break;
                        case "product_id":
                            if (int.TryParse(elems[i], out numform))
                                nextOrder.Product_id = numform;
                            else
                                throw new Exception("Неверный тип параметра 'product_id'.");
                            break;
                    }
                }

                return nextOrder;
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}: " + ex.Message, currentRowNumber);
                return null;
            }
        }
    }
}
