using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuestCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 1 || args.Length == 1 && args[KeyNumber.Help].ToLower() == ConsoleKeys.Help)
                ShowHelp();
            else
            {
                string folder, internalName, externalName, mprName = null;
                int pathIdx = Array.IndexOf(args, ConsoleKeys.PathQuest);
                if (pathIdx == -1)
                {
                    ShowHelp(HelpType.Path);
                    return;
                }

                int nameIdx = Array.IndexOf(args, ConsoleKeys.NameQuest);
                if(nameIdx == -1)
                {
                    ShowHelp(HelpType.Name);
                    return;
                }

                if(args.Length <= ++pathIdx && !Directory.Exists(args[pathIdx]))
                {
                    Console.WriteLine(String.Format("Не указано значение аргумента {0} или путь указанный в нём не существует", ConsoleKeys.PathQuest));
                    return;
                }
                folder = args[pathIdx];

                if(args.Length <= ++nameIdx)
                {
                    Console.WriteLine(String.Format("Не указано значение аргумента {0}", ConsoleKeys.NameQuest));
                    return;
                }
                internalName = args[nameIdx];

                var fc = new FilesCreator(folder, internalName);

                int labelIdx = Array.IndexOf(args, ConsoleKeys.LabelQuest);
                if (labelIdx != -1)
                {
                    if (args.Length > ++labelIdx)
                    {
                        externalName = args[labelIdx];
                        fc.ExternalName = externalName;
                    }
                }

                int mprNameIdx = Array.IndexOf(args, ConsoleKeys.MprName);
                if (mprNameIdx != -1)
                {
                    if (args.Length > ++mprNameIdx)
                    {
                        mprName = args[mprNameIdx];
                        fc.MprName = mprName;
                    }
                }

                var res = fc.CreateFiles();
                switch(res)
                {
                    case CreateExitCode.Success:
                        Console.WriteLine(String.Format("Шаблон квеста {0} успешно создан", internalName));
                        break;
                    case CreateExitCode.Exception:
                        Console.WriteLine("Произошла ошибка при создании шаблона. Детали см. выше");
                        break;
                    case CreateExitCode.Canceled:
                        Console.WriteLine("Создание шаблона отменено");
                        break;
                }
            }
        }

        static void ShowHelp(int helpType = HelpType.Main)
        {
            switch(helpType)
            {
                case HelpType.Main:
                    Console.WriteLine(String.Format("Аргументы:\n{0}: Путь, где будет создан квест.\n{1}: Внутренее имя квеста.\n" + 
                        "{2}(необязательный): внешнее название квеста, которое будет видно при взятии диалога.\n" + 
                        "Пример: {0} С:\\EvilIslands\\Maps {1} z3q4 {2} Свинья убийца", 
                        ConsoleKeys.PathQuest, ConsoleKeys.NameQuest, ConsoleKeys.LabelQuest));
                    break;
                case HelpType.Path:
                    Console.WriteLine(String.Format("Необходимо указать папку, где будет создан квест: {0} <Папка, где создаём квест>", ConsoleKeys.PathQuest));
                    break;
                case HelpType.Name:
                    Console.WriteLine(String.Format("Необходимо указать имя квеста: {0} <Имя квеста>", ConsoleKeys.NameQuest));
                    break;
            }
        }
    }
}
