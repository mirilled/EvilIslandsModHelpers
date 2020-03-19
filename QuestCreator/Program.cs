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
                string folder, internalName, externalName = null;
                int pathKeyIdx = Array.IndexOf(args, ConsoleKeys.PathQuest);
                if (pathKeyIdx == -1)
                {
                    ShowHelp(HelpType.Path);
                    return;
                }

                int nameKeyIdx = Array.IndexOf(args, ConsoleKeys.NameQuest);
                if(nameKeyIdx == -1)
                {
                    ShowHelp(HelpType.Name);
                    return;
                }
                int pathIdx = pathKeyIdx + 1;
                if(args.Length <= pathIdx && !Directory.Exists(args[pathIdx]))
                {
                    Console.WriteLine(String.Format("Не указано значение аргумента {0} или путь указанный в нём не существует", ConsoleKeys.PathQuest));
                    return;
                }
                folder = args[pathIdx];

                int nameIdx = nameKeyIdx + 1;
                if(args.Length <= nameIdx)
                {
                    Console.WriteLine(String.Format("Не указано значение аргумента {0}", ConsoleKeys.NameQuest));
                    return;
                }
                internalName = args[nameIdx];

                int labelKeyIdx = Array.IndexOf(args, ConsoleKeys.LabelQuest);
                int labelIdx = -1;
                if (labelKeyIdx != -1)
                {
                    labelIdx = labelKeyIdx + 1;
                    externalName = args[labelIdx];
                }
                FilesCreator fc;

                if(labelKeyIdx == -1)
                    fc = new FilesCreator(folder, internalName);
                else
                    fc = new FilesCreator(folder, internalName, externalName);
                fc.CreateFiles();
            }

            //Console.WriteLine("Hello World!");
            Console.ReadKey();
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
