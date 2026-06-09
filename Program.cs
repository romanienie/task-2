using System;
using System.Collections.Generic;

// abstrct class animal-common parent for all animals
abstract class Animal{
    
    public string Nickname { get; set; }
    public int Age { get; set; }
    public string Habitat { get; set; }
    public string FoodType { get; set; }

    // onstructor of the base class animal
    public Animal(string nickname, int age, string habitat, string foodType){
        
        Nickname = nickname;
        Age = age;
        Habitat = habitat;
        FoodType = foodType;
    }

    // `virtual` means that heirs can change this method
    public virtual string GetInfo(){

        return $"Кличка: {Nickname}\nВозраст: {Age} лет\nМесто обитания: {Habitat}\nТип питания: {FoodType}";
    }
}

class Mammal : Animal{

    public bool HasFur { get; set; }

    public Mammal(string nickname, int age, string habitat, string foodType, bool hasFur)
        : base(nickname, age, habitat, foodType){

        HasFur = hasFur;
    }

    public override string GetInfo(){

        string furText = HasFur ? "есть" : "нет";

        return base.GetInfo() + $"\nТип: Млекопитающее\nШерсть: {furText}";
    }
}

class Bird : Animal{

    public double WingSpan { get; set; }

    public Bird(string nickname, int age, string habitat, string foodType, double wingSpan)
        : base(nickname, age, habitat, foodType){

        WingSpan = wingSpan;
    }

    public override string GetInfo(){

        return base.GetInfo() + $"\nТип: Птица\nРазмах крыльев: {WingSpan} м";
    }
}

class Fish : Animal{

    public bool LivesInSaltWater { get; set; }

    public Fish(string nickname, int age, string habitat, string foodType, bool livesInSaltWater)
        : base(nickname, age, habitat, foodType){

        LivesInSaltWater = livesInSaltWater;
    }

    public override string GetInfo(){

        return base.GetInfo() + $"\nТип: Рыба\nОбитает в солёной воде: {(LivesInSaltWater ? "да" : "нет")}";
    }
}

class Reptile : Animal{

    public bool IsVenomous { get; set; }

    public Reptile(string nickname, int age, string habitat, string foodType, bool isVenomous)
        : base(nickname, age, habitat, foodType){

        IsVenomous = isVenomous;
    }

    public override string GetInfo(){

        return base.GetInfo() + $"\nТип: Пресмыкающееся\nЯдовитое: {(IsVenomous ? "да" : "нет")}";
    }
}

class Amphibian : Animal{

    public bool CanBreatheUnderwater { get; set; }

    public Amphibian(string nickname, int age, string habitat, string foodType, bool canBreatheUnderwater)
        : base(nickname, age, habitat, foodType){

        CanBreatheUnderwater = canBreatheUnderwater;
    }

    public override string GetInfo(){

        return base.GetInfo() + $"\nТип: Земноводное\nМожет дышать под водой: {(CanBreatheUnderwater ? "да" : "нет")}";
    }
}

class AnimalManager{

    private static AnimalManager instance;
    private List<Animal> animals;

    private AnimalManager(){

        animals = new List<Animal>();
    }

    public static AnimalManager Instance{

        get{

            if (instance == null){

                instance = new AnimalManager();
            }

            return instance;
        }
    }

    public void AddAnimal(Animal animal){

        animals.Add(animal);
    }

    public void ShowAllAnimals(){

        if (animals.Count == 0){

            Console.WriteLine("Список животных пуст.");
            return;
        }

        for (int i = 0; i < animals.Count; i++){

            Console.WriteLine($"\n{i + 1}. {animals[i].GetInfo()}");
        }
    }

    public void ShowAnimalByName(){

        Console.WriteLine("Введите кличку животного: ");
        string name = Console.ReadLine();

        foreach (Animal animal in animals){

            if (animal.Nickname.ToLower() == name.ToLower()){

                Console.WriteLine(animal.GetInfo());
                return;
            }
        }

        Console.WriteLine("Животное не найдено.");
    }

    public void ShowMenu(){

        while (true){

            Console.WriteLine("\n=== Меню зоопарка ===");
            Console.WriteLine("1. Показать всех животных");
            Console.WriteLine("2. Найти животное по кличке");
            Console.WriteLine("3. Добавить животное");
            Console.WriteLine("4. Выйти");
            Console.WriteLine("Выберите пункт: ");

            string choice = Console.ReadLine();

            switch (choice){

                case "1":
                    ShowAllAnimals();
                    break;

                case "2":
                    ShowAnimalByName();
                    break;

                case "3":
                    CreateAnimalFromConsole();
                    break;

                case "4":
                    Console.WriteLine("Программа завершена.");
                    return;

                default:
                    Console.WriteLine("Ошибка: выберите пункт от 1 до 4.");
                    break;
            }
        }
    }

    private void CreateAnimalFromConsole(){
        Console.WriteLine("Выберите тип животного:");
        Console.WriteLine("1. Млекопитающее");
        Console.WriteLine("2. Птица");
        Console.WriteLine("3. Рыба");
        Console.WriteLine("4. Пресмыкающееся");
        Console.WriteLine("5. Земноводное");

        string typeChoice = Console.ReadLine();

        Console.Write("Введите кличку: ");
        string nickname = Console.ReadLine();

        Console.Write("Введите возраст: ");
        int age;

        while (!int.TryParse(Console.ReadLine(), out age)){

            Console.Write("Ошибка. Введите возраст числом: ");
        }

        Console.Write("Введите место обитания: ");
        string habitat = Console.ReadLine();

        Console.Write("Введите тип питания: ");
        string foodType = Console.ReadLine();

        switch (typeChoice){

            case "1":
                Console.Write("Есть шерсть? да/нет: ");
                bool hasFur = Console.ReadLine().ToLower() == "да";
                AddAnimal(new Mammal(nickname, age, habitat, foodType, hasFur));
                break;

            case "2":
                Console.Write("Введите размах крыльев: ");
                double wingSpan;

                while (!double.TryParse(Console.ReadLine(), out wingSpan)){

                    Console.Write("Ошибка. Введите число: ");
                }

                AddAnimal(new Bird(nickname, age, habitat, foodType, wingSpan));
                break;

            case "3":
                Console.Write("Обитает в солёной воде? да/нет: ");
                bool livesInSaltWater = Console.ReadLine().ToLower() == "да";
                AddAnimal(new Fish(nickname, age, habitat, foodType, livesInSaltWater));
                break;

            case "4":
                Console.Write("Ядовитое? да/нет: ");
                bool isVenomous = Console.ReadLine().ToLower() == "да";
                AddAnimal(new Reptile(nickname, age, habitat, foodType, isVenomous));
                break;

            case "5":
                Console.Write("Может дышать под водой? да/нет: ");
                bool canBreatheUnderwater = Console.ReadLine().ToLower() == "да";
                AddAnimal(new Amphibian(nickname, age, habitat, foodType, canBreatheUnderwater));
                break;

            default:
                Console.WriteLine("Такого типа животного нет.");
                break;
        }
    }
}

class Program{

    static void Main(string[] args){

        AnimalManager manager = AnimalManager.Instance;

        manager.AddAnimal(new Mammal("Барсик", 5, "лес", "хищник", true));
        manager.AddAnimal(new Bird("Кеша", 2, "джунгли", "всеядное", 0.35));
        manager.AddAnimal(new Fish("Немо", 1, "водоём", "всеядное", true));
        manager.AddAnimal(new Reptile("Каа", 7, "джунгли", "хищник", false));
        manager.AddAnimal(new Amphibian("Квакша", 3, "болото", "хищник", true));

        manager.ShowMenu();
    }
}
