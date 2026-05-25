using System; // подключение библиотеки
using System.Collections.Generic; // подключение коллекции

// абстрактный базовый класс(шаблон для наследования)
abstract class Animal
{
    public string Name { get; set; } // свойство: кличка жиотного
    public int Age { get; set; } // свойство: возраст животноного
    public string Habitat { get; set; } // свойство: среда обитания
    public string FoodType { get; set; } // свойство: тип питания
    // коструктор базового класса
    protected Animal(string name, int age, string habitat, string foodType)
    {
        Name = name; // сохраняем имя
        Age = age; // сохраняем возраст
        Habitat = habitat; // сохраняем среду
        FoodType = foodType; // сохраняем тип питания
    }
    //виртуалный метод для вывода информации
    public virtual string GetInfo()
    {
        return $"кличка: {Name}, возраст: {Age}, среда: {Habitat}, питание: {FoodType}"; // возращаем общую информацию
    }
}

// класс mammal наследуется от animal
class Mammal : Animal
{
    public bool HasFur {  get; set; } // свойство: наличие шерсти
    public Mammal (string name, int age, string habitat, string foodType, bool hasFur) : base(name, age, habitat, foodType) // вызываем конструктор animal
                                                                                                                           {
        HasFur = hasFur; // сохраняем шерсть

}
    // переопределение метода
    public override string GetInfo()
    {
        return base.GetInfo() + $", тип: млекопитающее, шерсть: {(HasFur ? "есть" : "нет")}"; // добавление уникальной информации
    }

}
// класс bird наследуется от animal
class Bird : Animal
{
    public double WingSpan { get; set; } // свойство: размах крыльев
    public Bird(string name, int age, string habitat, string foodType, double wingSpan) : base(name, age, habitat, foodType) // коструктор птицы
    {
        WingSpan = wingSpan; // сохраняем размах крыльев
    }
    public override string GetInfo()
    {
        return base.GetInfo()+$", тип: птица, размах крыльев: {WingSpan}"; // возращаем информацию о птице
    }
}
// класс fish наследует от animal
class Fish : Animal
{
    public string WaterType { get; set; } // свойство: тип воды
    public Fish(string name, int age, string habitat, string foodType, string waterType):base (name, age, habitat, foodType) // конструктор рыбы
    {
        WaterType = waterType; // сохранение типа воды
    }
    public override string GetInfo()
    {
        return base.GetInfo()+$", тип: рыба, вода: {WaterType}"; //возращение информации о рыбе
    }
}
//класс reptile наследуется от animal
class Reptile : Animal
{
    public bool IsVenomous { get; set; } // свойство: ядовитость

    public Reptile(string name, int age, string habitat, string foodType, bool isVenomous)
        : base(name, age, habitat, foodType)
    {
        IsVenomous = isVenomous; // cохраняем ядовитость
    }
    public override string GetInfo()
    {
        return base.GetInfo() + $", тип: пресмыкающиеся, ядовитые: {(IsVenomous ? "да" : "нет")}"; // возвращаем информацию
    }
}
// класс amphibian наследуется от animal
class Amphibian : Animal
{
    
    public string SkinMoisture { get; set; }// свойство: влажность кожи

    
    public Amphibian(string name, int age, string habitat, string foodType, string skinMoisture)
        : base(name, age, habitat, foodType)// конструктор земноводного
    {
        SkinMoisture = skinMoisture; // сохраняем влажность кожи
    }

    
    public override string GetInfo()
    {
        
        return base.GetInfo() +
               $", тип: земноводное, влажность кожи: {SkinMoisture}";// возвращаем информацию
    }
}
// singleton-класс для управления животными
class AnimalManager
{
   
    private static readonly AnimalManager instance = new AnimalManager(); // создаем единственный объект класса

    // свойство доступа к объекту
    public static AnimalManager Instance
    {
        get { return instance; }
    }

   
    private readonly List<Animal> animals = new List<Animal>(); // список животных

    // закрытый конструктор
    private AnimalManager() { }

    // метод добавления животного
    public void AddAnimal(Animal animal)
    {
        animals.Add(animal); // добавляем животное в список
        Console.WriteLine("животное добавлено.");
    }

    // метод вывода всех животных
    public void ShowAllAnimals()
    {
        // проверка: список пуст?
        if (animals.Count == 0)
        {
            Console.WriteLine("животных пока нет.");
            return; // выход из метода
        }

        // цикл для вывода списка
        for (int i = 0; i < animals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {animals[i].GetInfo()}");
        }
    }

    // метод поиска по имени
    public void FindAnimalByName(string name)
    {
        // перебираем животных
        foreach (Animal animal in animals)
        {
            // проверяем совпадение имени
            if (animal.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(animal.GetInfo());
                return;
            }
        }

        // если животное не найдено
        Console.WriteLine("животное не найдено.");
    }

    // метод меню
    public void Menu()
    {
        // бесконечный цикл меню
        while (true)
        {
            Console.WriteLine("\n--- МЕНЮ ЗООПАРКА ---");
            Console.WriteLine("1. показать всех животных");
            Console.WriteLine("2. найти животное по кличке");
            Console.WriteLine("3. добавить животное");
            Console.WriteLine("4. выход");

            Console.Write("выберите пункт: ");

            // считываем выбор пользователя
            string choice = Console.ReadLine();

            // проверяем выбор
            switch (choice)
            {
                case "1":
                    ShowAllAnimals(); // показать животных
                    break;

                case "2":
                    Console.Write("введите кличку: ");
                    FindAnimalByName(Console.ReadLine()); // поиск
                    break;

                case "3":
                    AddAnimalFromConsole(); // добавление
                    break;

                case "4":
                    Console.WriteLine("программа завершена.");
                    return; // выход из программы

                default:
                    Console.WriteLine("ошибка: выберите пункт от 1 до 4.");
                    break;
            }
        }
    }

    // метод добавления животного через консоль
    private void AddAnimalFromConsole()
    {
        Console.WriteLine("\nвыберите тип животного:");
        Console.WriteLine("1. млекопитающее");
        Console.WriteLine("2. птица");
        Console.WriteLine("3. рыба");
        Console.WriteLine("4. пресмыкающееся");
        Console.WriteLine("5. земноводное");

        Console.Write("тип: ");
        string type = Console.ReadLine();

        Console.Write("кличка: ");
        string name = Console.ReadLine();

        Console.Write("возраст: ");

        int age;

        // проверка правильности ввода
        while (!int.TryParse(Console.ReadLine(), out age) || age < 0)
        {
            Console.Write("ошибка. введите число: ");
        }

        Console.Write("среда обитания: ");
        string habitat = Console.ReadLine();

        Console.Write("тип питания: ");
        string foodType = Console.ReadLine();

        // проверяем тип животного
        switch (type)
        {
            case "1":

                Console.Write("есть шерсть? да/нет: ");

                bool hasFur = Console.ReadLine().ToLower() == "да";

                AddAnimal(new Mammal(name, age, habitat, foodType, hasFur));

                break;

            case "2":

                Console.Write("размах крыльев: ");

                double wingSpan;

                while (!double.TryParse(Console.ReadLine(), out wingSpan))
                {
                    Console.Write("ошибка. Введите число: ");
                }

                AddAnimal(new Bird(name, age, habitat, foodType, wingSpan));

                break;

            case "3":

                Console.Write("тип воды: ");

                string waterType = Console.ReadLine();

                AddAnimal(new Fish(name, age, habitat, foodType, waterType));

                break;

            case "4":

                Console.Write("ядовитое? да/нет: ");

                bool isVenomous = Console.ReadLine().ToLower() == "да";

                AddAnimal(new Reptile(name, age, habitat, foodType, isVenomous));

                break;

            case "5":

                Console.Write("влажность кожи: ");

                string skinMoisture = Console.ReadLine();

                AddAnimal(new Amphibian(name, age, habitat, foodType, skinMoisture));

                break;

            default:

                Console.WriteLine("такого типа животного нет.");

                break;
        }
    }
}
// Главный класс программы
class Program
{
    // главный метод
    static void Main()
    {
        // получаем Singleton-объект менеджера
        AnimalManager manager = AnimalManager.Instance;

        // добавляем тестовых животных
        manager.AddAnimal(new Mammal("Барсик", 5, "лес", "хищник", true));

        manager.AddAnimal(new Bird("Кеша", 2, "джунгли", "всеядное", 0.35));

        manager.AddAnimal(new Fish("Немо", 1, "водоём", "всеядное", "морская"));

        manager.AddAnimal(new Reptile("Каа", 8, "джунгли", "хищник", false));

        manager.AddAnimal(new Amphibian("Квакша", 3, "болото", "насекомоядное", "влажная"));

        // запускаем меню
        manager.Menu();
    }//да
}
