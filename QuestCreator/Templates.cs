using System;
using System.Collections.Generic;
using System.Text;

namespace QuestCreator
{
    public static class Templates
    {
        #region Имена файлов
        public const string MqFolder = "{0}_mq";
        public const string TakeFileName = "briefing {0}_1";
        public const string CancelFileName = "briefing {0}_2";
        public const string CompleteFileName = "briefing {0}_3";
        public const string QuestDescFileName = "quest {0}";

        public const string SettingsMapFileName = "map.txt";
        public const string SettingsQuestFileName = "quest.ini";
        #endregion

        #region Шаблоны
        public static string[] TakeBrief 
        {
            get
            {
                return new string[]
                {
                    MQConstants.ExternalNameQuest,
                    "#show Hero",
                    "#show <npc>",
                    "#CAMERA <1-7>",
                    "#phrase Hero  1",
                    "<Фраза Hero>",
                    "#CAMERA <1-7>",
                    "#phrase <npc>  2",
                    "<Фраза <npc>>",
                };
            }
        }

        public static string[] CompleteBrief
        {
            get
            {
                return new string[]
                {
                    MQConstants.CompleteNameBrief,
                    "#show Hero",
                    "#show <npc>",
                    "#CAMERA <1-7>",
                    "#phrase Hero  1",
                    "<Фраза Hero>",
                    "#CAMERA <1-7>",
                    "#phrase <npc>  2",
                    "<Фраза <npc>>",
                };
            }
        }

        public static string[] CanelBrief
        {
            get
            {
                return new string[]
                {
                    MQConstants.CancelNameBrief,
                    "#show Hero",
                    "#show <npc>",
                    "#CAMERA <1-7>",
                    "#phrase Hero  1",
                    "<Фраза Hero>",
                    "#CAMERA <1-7>",
                    "#phrase <npc>  2",
                    "<Фраза <npc>>",
                };
            }
        }

        public static string[] QuestDesc
        {
            get
            {
                return new string[]
                {
                    MQConstants.ExternalNameQuest,
                    "<Короткое описание>",
                    "#subobj  1",
                    "<Название подзадания>",
                    "<Описание подзадания>",
                };
            }
        }

        public static string[] SettingsMap
        {
            get
            {
                return new string[]
                {
                    "#zone {0} <Аллод> game",
                    "#res",
                    "<mpr> <mob>",
                    "#maps",
                    "<ширина> <высота>",
                    "<текстура миникары> <текстура карты в табе>",
                    "#weather",
                    "<погода>",
                    "#sky",
                    "<небо>",
                    "",
                    "#exit 1",
                    "<имя базы>",
                    "#deploy",
                    "<нижний левый край> <верхний правый край>",
                    "#remove",
                    "<нижний левый край> <верхний правый край>",
                    "#view",
                    "<поворот камеры>"
                };
            }
        }

        public static string[] SettingsQuest
        {
            get
            {
                return new string[]
                {
                    "[quest]",
                    "name={0}",
                    "exp=0",
                    "money=0",
                    "questplaces={0;0}",
                    "[briefing receive]",
                    "name={0}_1",
                    "give items=",
                    "give quests=q.{0}.{0}",
                    "give quests=q.{0}.{0}.1",
                    "[briefing reject]",
                    "name={0}_2",
                    "[briefing complete]",
                    "name={0}_3",
                    "exp=0",
                    "money=0",
                    "give items="
                };
            }
        }
        #endregion
    }
}
