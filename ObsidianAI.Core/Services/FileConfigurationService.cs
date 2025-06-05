using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ObsidianAI.Core.Models;

namespace ObsidianAI.Core.Services
{
    public class FileConfigurationService
    {
        private readonly string _settingsFilePath;
        private readonly JsonSerializerOptions _jsonOptions;
        private AppSettings? _cachedSettings;

        public FileConfigurationService()
        {
            var appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appFolder = Path.Combine(appdataPath, "ObsidianAIClient");

            // 폴더가 없으면 생성
            Directory.CreateDirectory(appFolder);

            _settingsFilePath = Path.Combine(appFolder, "settings.json");

            _jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public async Task SaveSettingsAsync(AppSettings settings)
        {
            try
            {
                var json = JsonSerializer.Serialize(settings, _jsonOptions);

                await File.WriteAllTextAsync(_settingsFilePath, json);
                _cachedSettings = settings;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"설정 저장 실패: {ex.Message}", ex);
            }
        }

        public async Task<string?> GetApiKeyAsync(string provider)
        {
            var settings = await LoadSettingsAsync();
            return provider.ToLower() switch
            {
                "claude" => settings.ApiKeys.Claude,
                "openai" => settings.ApiKeys.OpenAI,
                "gemini" => settings.ApiKeys.Gemini,
                _ => null
            };
        }

        public async Task SetApiKeyAsync(string provider, string apiKey)
        {
            var settings = await LoadSettingsAsync();

            switch (provider.ToUpper())
            {
                case "claude":
                    settings.ApiKeys.Claude = apiKey;
                    break;
                case "openai":
                    settings.ApiKeys.OpenAI = apiKey;
                    break;
                case "gemini":
                    settings.ApiKeys.Gemini = apiKey;
                    break;
                default:
                    throw new ArgumentException($"지원하지 않는 AI 제공자: {provider}");
            }

            await SaveSettingsAsync(settings);
        }

        public async Task<T> GetSettingAsync<T>(string key, T defaultValue = default!)
        {
            var settings = await LoadSettingsAsync();

            // 리플렉션을 사용해서 설정값 조회
            // 예: "obsidian.vaultPath", "ui.theme" 등
            var parts = key.Split('.');
            object current = settings;

            foreach (var part in parts)
            {
                var property = current.GetType().GetProperty(part,
                    System.Reflection.BindingFlags.IgnoreCase |
                    System.Reflection.BindingFlags.Public |
                    System.Reflection.BindingFlags.Instance);

                if (property == null)
                {
                    return defaultValue;
                }

                current = property.GetValue(current) ?? defaultValue;

                if (current.Equals(defaultValue))
                {
                    return defaultValue;
                }
            }

            return current is T result ? result : defaultValue;
        }

        public void ClearCache()
        {
            _cachedSettings = null;
        }


        public async Task<AppSettings> LoadSettingsAsync()
        {
            // 캐시된 파일 있으면 리턴
            if (_cachedSettings != null)
            {
                return _cachedSettings;
            }

            // 설정 파일이 없으면 기본값으로 생성
            if (!File.Exists(_settingsFilePath))
            {
                _cachedSettings = new AppSettings();
                await SaveSettingsAsync(_cachedSettings);
                return _cachedSettings;
            }

            try
            {
                // JSON 파일 읽어오기
                var json = await File.ReadAllTextAsync(_settingsFilePath);
                // json 파싱. 실패시 새로 생성
                _cachedSettings = JsonSerializer.Deserialize<AppSettings>(json, _jsonOptions) ?? new AppSettings();
                return _cachedSettings;
            }
            catch (Exception ex)
            {
                // JSON 파싱 실패 시 기본값으로 fallback
                Console.WriteLine($"설정 파일 로드 실패: {ex.Message}");
                _cachedSettings = new AppSettings();
                return _cachedSettings;
            }
        }
    }
}
