using System;
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

        public static Sprite GetSprite(string key, SystemLanguage language)
        {
            if (IsActive)
            {
                return instance.spriteTranslator.GetTranslation(key, language);
            }

            throw new Exception("Localization Manager doesn't exist");
        }

        public static AudioClip GetAudioClip(string key, SystemLanguage language)
        {
            if (IsActive)
            {
                return instance.audioClipTranslator.GetTranslation(key, language);
            }

            throw new Exception("Localization Manager doesn't exist");
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