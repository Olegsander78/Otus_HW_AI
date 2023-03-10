using System;
using Game.App;
using LocalizationModule;
using UnityEngine;

namespace Game.Localization
{
    public sealed class LocalizationManager : MonoBehaviour
    {
        public static bool IsActive
        {
            get { return instance != null; }
        }

        private static LocalizationManager instance;

        [SerializeField]
        private LocalizationTextConfig textConfig;

        [SerializeField]
        private LocalizationSpriteConfig spriteConfig;

        [SerializeField]
        private LocalizationAudioClipConfig audioConfig;

        private ITranslator<string> textTranslator;

        private ITranslator<Sprite> spriteTranslator;

        private ITranslator<AudioClip> audioClipTranslator;

        public static string GetText(string key, SystemLanguage language)
        {
            if (IsActive)
            {
                return instance.textTranslator.GetTranslation(key, language);
            }

            throw new Exception("Localization Manager doesn't exist");
        }

        public static string GetCurrentText(string key)
        {
            return GetText(key, LanguageManager.CurrentLanguage);
        }

        public static Sprite GetSprite(string key, SystemLanguage language)
        {
            if (IsActive)
            {
                return instance.spriteTranslator.GetTranslation(key, language);
            }

            throw new Exception("Localization Manager doesn't exist");
        }

        public static Sprite GetCurrentSprite(string key)
        {
            return GetSprite(key, LanguageManager.CurrentLanguage);
        }

        public static AudioClip GetAudioClip(string key, SystemLanguage language)
        {
            if (IsActive)
            {
                return instance.audioClipTranslator.GetTranslation(key, language);
            }

            throw new Exception("Localization Manager doesn't exist");
        }

        public static AudioClip GetCurrentAudioClip(string key)
        {
            return GetAudioClip(key, LanguageManager.CurrentLanguage);
        }

        private void Awake()
        {
            if (IsActive)
            {
                throw new Exception("Localization Manager already exists!");
            }

            instance = this;

            this.textTranslator = new LocalizationTextTranslator(this.textConfig);
            this.spriteTranslator = new SpriteTranslator(this.spriteConfig.entities);
            this.audioClipTranslator = new AudioClipTranslator(this.audioConfig.entities);
        }

        private void OnDestroy()
        {
            instance = null;
        }
    }
}