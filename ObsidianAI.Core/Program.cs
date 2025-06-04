using System.Threading.Tasks;

namespace ObsidianAI.Core
{
    internal class Program
    {
        private static async Task ProcessCommand(string[] args)
        {
            var command = args[0].ToLower();

            switch (command)
            {
                case "test":
                    await TestCommand();
                    break;
                case "help":
                    ShowHelp();
                    break;
                default:
                    Console.WriteLine($"Unknown command: {command}");
                    ShowHelp();
                    break;
            }
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine("  test    - Test basic functionality");
            Console.WriteLine("  help    - Show this help message");
        }

        private static async Task TestCommand()
        {
            Console.WriteLine("Testing basic functionality...");
            Console.WriteLine("✅ CLI is working!");

            // TODO: 여기에 설정 서비스 테스트 추가
            await Task.CompletedTask;
        }

        static async Task Main(string[] args)
        {
            Console.WriteLine("ObsidianAI Core CLI v1.0");
            Console.WriteLine("======================");

            // 입력값 없을때
            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }

            try
            {
                // 커멘드 처리
                await ProcessCommand(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Environment.Exit(1);
            }
        }
    }
}
