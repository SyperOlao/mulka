using System;
using System.Collections.Generic;
using System.IO;
using Palmmedia.ReportGenerator.Core.Common;
using System.Text.Json;
using Newtonsoft.Json;
using UnityEngine;


namespace Dialogs.Data.Character
{
    public class LoadedCharacterService
    {
        private readonly Dictionary<string, DialogCharacterDto> _dialogCharacterList;

        public LoadedCharacterService()
        {
            const string filePath = "Assets/Scripts/Dialogs/Data/Character/Info/CharacterInfo.json";
            if (!File.Exists(filePath))
            {
                throw new Exception("JSON file not found at path: " + filePath + "in LoadedCharacterService");
            }

            var json = File.ReadAllText(filePath);
            var characters = JsonConvert.DeserializeObject<Dictionary<string, DialogCharacterDto>>(json);
            _dialogCharacterList = characters;
        }

        public DialogCharacterDto GetCharacterByName(string name)
        {
            if (_dialogCharacterList.TryGetValue(name, out var character))
            {
                return character;
            }

            Debug.LogWarning($"Character with name '{name}' not found!");
            return null;
        }
    }
}