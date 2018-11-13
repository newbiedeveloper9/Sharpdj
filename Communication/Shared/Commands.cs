using System;
using System.Collections.Generic;

namespace Communication.Shared
{
    public sealed class Commands
    {
        private static readonly Lazy<Commands> lazy =
            new Lazy<Commands>(() => new Commands());

        public static Commands Instance => lazy.Value;

        private Commands()
        {
        }
        
        

        public readonly Dictionary<string, string>
            CommandsDictionary = new Dictionary<string, string>()
            {
                {"Success", "0x0"},
                {"Error", "0x1"},

                {"Register", "0x2"},
                {"Login", "0x3"},
                {"Disconnect", "0x4"},
                {"GetPeoples", "0x5"},
                {"ChangePassword", "0x6"},
                {"ChangeUsername", "0x7"},
                {"ChangeLogin", "0x8"},
                {"ChangeRank", "0x9"},
                {"JoinRoom", "0xA"},
                {"CreateRoom", "0xB"},
                {"AfterLogin", "0xC"},
                {"JoinQueue", "0xD"},
                {"UpdateDj", "0xE"},
                {"SendMessage", "0xF"},
                {"AddUserToRoom", "0x10"}, //16
                {"RemoveUserFromRoom", "0x11"},
                {"ChangeTrack", "0x12"},
            };

        public List<string> GetMessageParameters(string message)
        {
            var list = new List<string>();
            message = message.Substring(message.IndexOf(' ') + 1);
            list.AddRange(message.Split('$'));
            return list;
        }

        public string GetMessageCommand(string message) =>
            message.Remove(message.IndexOf(' '));
    }
}