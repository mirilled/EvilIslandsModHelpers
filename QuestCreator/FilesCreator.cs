using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace QuestCreator
{
    public enum CreateExitCode
    {
        Success = 0,
        Exception = 1,
        Canceled = 2
    }

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

        public CreateExitCode CreateFiles()
        {
            try
            {
                var dir = Path.Combine(Folder, String.Format(Templates.MqFolder, InternalName));
                var mobFile = Path.Combine(Folder, InternalName + ".mob");
                if (Directory.Exists(dir))
                {
                    Console.WriteLine(String.Format("Папка для квеста с названием {0} уже существует", InternalName));
                    Console.Write("Удалить её и создать заново?(y/n;д/н)");
                    var key = Console.ReadKey().KeyChar;
                    Console.WriteLine();
                    var prettyKey = key.ToString().ToLower();
                    if (prettyKey == "y" || prettyKey == "д")
                    {
                        Directory.Delete(dir, true);
                        File.Delete(mobFile);
                    }
                    else
                    {
                        //Console.WriteLine("Отменено пользователем");
                        return CreateExitCode.Canceled;
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
                //Файл настроек карты и миникарты
                var mapsetfile = Path.Combine(settingsFolder, Templates.SettingsMapFileName);
                File.WriteAllText(mapsetfile, String.Join('\n', String.Format(String.Join('\n', Templates.SettingsMap), InternalName)));
                //Файл настроек переменных квеста
                var questsetfile = Path.Combine(settingsFolder, Templates.SettingsQuestFileName);
                File.WriteAllText(questsetfile, String.Join('\n', String.Format(String.Join('\n', Templates.SettingsQuest), InternalName)));
                //Пустой моб-файл
                using var fs = new FileStream(mobFile, FileMode.Create);
                fs.Write(Templates.MobEmpty, 0, Templates.MobEmpty.Length);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return CreateExitCode.Exception;
            }
            return CreateExitCode.Success;
        }
    }
}
