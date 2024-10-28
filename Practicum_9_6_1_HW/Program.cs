namespace Practicum_9_6_1_HW
{
    internal class Program
    {
        public class InvalidinputException : Exception
        { 
            public InvalidinputException(string message) : base(message) { }
        } //новый класс для создания собственных исключений

        public delegate void SortEventHandler(List<string> surnames); // определяем делегат для события сортировки
        public static event SortEventHandler OnSort; //ивент

        static void Main(string[] args)
        {
            List<string> surnames = new List<string>
            { "Пушкин" , "Чичиков" , "Ломоносов" , "Гоголь" , "Малевич"};

            Console.WriteLine("Фамилии: ");
            DisplaySurnames(surnames);


            while (true)
            {
                Console.WriteLine("");

                string input = Console.ReadLine();

                try
                {
                    // Проверяем ввод пользователя
                    if (input != "1" && input != "2")
                    {
                        throw new InvalidinputException("Неверный ввод. Пожалуйста, введите 1 или 2.");
                    }

                    OnSort += (surnames) => //запускаем ивент с применением делегата
                    {
                        if (input == "1") { surnames.Sort(); }
                        else if (input == "2") { surnames.Sort((x, y) => string.Compare(y, x)); }
                    };

                    OnSort?.Invoke(surnames);
                    DisplaySurnames(surnames);
                    break;
                }
                catch (InvalidinputException ex)
                { Console.WriteLine(ex.Message); }
            }
            static void DisplaySurnames(List<string> surnames)// метода сортировки имен
            {
                foreach (var surname in surnames)
                { Console.WriteLine(surname); }
            }

             //блок с собственными типами исключений task1 
            try //проверка
            {
                Console.WriteLine("блок try начал свою  работу ");
                throw new Exception("ошибка введенного значения");
            }


            catch (Exception ex) when (ex.Message == "ошибка введенного значения") //собственное исключение
            {
                Console.WriteLine("необходимо ввести правильное значение");
                Console.WriteLine(ex.Message);
            }


            catch (Exception ex) when (ex is ArgumentNullException) // базовое исключение vs
            {
                Console.WriteLine("Аргумент равен нулю");
            }


            catch (Exception ex) when (ex is FileNotFoundException) //исключение - не найден файл
            {
                Console.WriteLine("файл не найден");
            }


            catch (Exception ex) when (ex is AccessViolationException) // исключение - недостаток прав доступа
            {
                Console.WriteLine("Ошибка доступа");
            }


            catch (Exception ex) when (ex is DivideByZeroException) // исключение при делении на ноль 
            {
                Console.WriteLine("Аргумент равен нулю");
            }
            Console.ReadKey();
        }
    }
}
