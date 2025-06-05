using System.Runtime;
using System.Text.Json.Serialization;

namespace ObsidianAI.Core.Models
{
    /// <summary>
    /// API 키 설정
    /// </summary>
    public class ApiKeys
    {
        [JsonPropertyName("claude")]
        public string? Claude { get; set; }

        [JsonPropertyName("openai")]
        public string? OpenAI { get; set; }

        [JsonPropertyName("gemini")]
        public string? Gemini { get; set; }
    }

    /// <summary>
    /// 옵시디언 설정 
    /// </summary>
    public class ObsidianSettings
    {
        [JsonPropertyName("vaultPath")]
        public string? VaultPath { get; set; }

        [JsonPropertyName("mcpPort")]
        public int McpPort { get; set; } = 3000;

        [JsonPropertyName("autoConnect")]
        public bool AutoConnect { get; set; } = true;
    }

    /// <summary>
    /// UI 설정
    /// </summary>
    public class UiSettings
    {
        [JsonPropertyName("theme")]
        public string Theme { get; set; } = "dark";

        [JsonPropertyName("windowWidth")]
        public int WindowWidth { get; set; } = 1200;

        [JsonPropertyName("windowHeight")]
        public int WindowHeight { get; set; } = 800;

        [JsonPropertyName("fontSize")]
        public int FontSize { get; set; } = 14;
    }

    /// <summary>
    /// 고급 설정
    /// </summary>
    public class AdvancedSettings
    {
        [JsonPropertyName("maxChatHistory")]
        public int MaxChatHistoryDays { get; set; } = 90;

        [JsonPropertyName("enableCache")]
        public bool EnableCache { get; set; } = true;

        [JsonPropertyName("logLevel")]
        public string LogLevel { get; set; } = "Information";

        [JsonPropertyName("defaultAiProvider")]
        public string DefaultAiProvider { get; set; } = "claude";
    }

    public class AppSettings
    {
        /// <summary>
        /// AI 제공자별 API 키 설정
        /// </summary>
        [JsonPropertyName("apiKeys")]
        public ApiKeys ApiKeys { get; set; } = new();

        /// <summary>
        /// Obsidian 연동 설정
        /// </summary>
        [JsonPropertyName("obsidian")]
        public ObsidianSettings Obsidian { get; set; } = new();

        /// <summary>
        /// UI 관련 설정
        /// </summary>
        [JsonPropertyName("ui")]
        public UiSettings UI { get; set; } = new();

        /// <summary>
        /// 고급 설정
        /// </summary>
        [JsonPropertyName("advanced")]
        public AdvancedSettings Advanced { get; set; } = new();
    }
}
