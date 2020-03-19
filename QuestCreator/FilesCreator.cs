using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QuestCreator
{
    public class FilesCreator
    {
        string Folder { get; set; }
        string InternalName { get; set; }
        string ExternalName { get; set; }

        public FilesCreator(string folder, string intName)
        {
            Folder = folder;
            InternalName = intName;
            ExternalName = intName;
        }

        public FilesCreator(string folder, string intName, string extName)
        {
            Folder = folder;
            InternalName = intName;
            ExternalName = extName;
        }

        public bool CreateFiles()
        {
            try
            {
                var dir = Path.Combine(Folder, String.Format(Templates.MqFolder, InternalName));
                if (Directory.Exists(dir))
                {
                    Console.WriteLine(String.Format("Папка для квеста с названием {0} уже существует", InternalName));
                    Console.Write("Удалить её и создать заново?(y/n;д/н)");
                    var key = Console.ReadKey().KeyChar;
                    var prettyKey = (key.ToString().ToLower());
                    if (prettyKey == "y" || prettyKey == "д")
                    {
                        Directory.Delete(dir);
                    }
                    else
                    {
                        Console.WriteLine("Отменено пользователем");
                    }
                }
                //Основная папка
                Directory.CreateDirectory(dir);
                //Папка с настройками
                var settingsFolder = Path.Combine(dir, InternalName);
                Directory.CreateDirectory(settingsFolder);
                //Файл взятия квеста
                var takefile = Path.Combine(dir, String.Format(Templates.TakeFileName, InternalName));
                File.WriteAllText(takefile, String.Join('\n', Templates.TakeBrief).Replace(MQConstants.ExternalNameQuest, ExternalName));
                //Файл отмены квеста
                var cancelfile = Path.Combine(dir, String.Format(Templates.CancelFileName, InternalName));
                File.WriteAllText(cancelfile, String.Join('\n', Templates.CanelBrief));
                //Файл завершения квеста
                var completefile = Path.Combine(dir, String.Format(Templates.CompleteFileName, InternalName));
                File.WriteAllText(completefile, String.Join('\n', Templates.CompleteBrief));
                //Файл описания квеста
                var questfile = Path.Combine(dir, String.Format(Templates.QuestDescFileName, InternalName));
                File.WriteAllText(questfile, String.Join('\n', Templates.QuestDesc));

                var mapsetfile = Path.Combine(settingsFolder, Templates.SettingsMapFileName);
                File.WriteAllText(mapsetfile, String.Join('\n', String.Format(String.Join('\n', Templates.SettingsMap), InternalName)));

                var questsetfile = Path.Combine(settingsFolder, Templates.SettingsQuestFileName);
                File.WriteAllText(questsetfile, String.Join('\n', String.Format(String.Join('\n', Templates.SettingsQuest), InternalName)));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            return true;
        }
    }
}
