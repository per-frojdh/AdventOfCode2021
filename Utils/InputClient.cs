namespace Utils;

public class InputClient {
    private static readonly HttpClient HttpClient = new();

    public InputClient(AppSecrets config) {
        Config = config;
    }

    private AppSecrets Config { get; }

    private async Task<string[]> FetchPuzzleByDay(string dayNumber, bool splitContent = true, string year = "2021",
        string separator = "\n") {
        var url = $"https://adventofcode.com/{year}/day/{dayNumber}/input";
        HttpClient.DefaultRequestHeaders.Add("Cookie", $"session={Config.SessionValue}");
        var result = await HttpClient.GetAsync(url);
        if (result.IsSuccessStatusCode) {
            var content = await result.Content.ReadAsStringAsync();
            if (splitContent) return content.Split("\n").Where(x => !string.IsNullOrEmpty(x)).ToArray();

            return content.Split(separator);
        }

        return new string[] { };
    }

    public async Task<string[]> GetPuzzle(string dayNumber) {
        var filePath = BuildFilePath(dayNumber);
        var exists = File.Exists(filePath);

        if (exists) return await File.ReadAllLinesAsync(filePath);

        var content = await FetchPuzzleByDay(dayNumber);
        await WriteInputToFile(content, dayNumber);
        return content;
    }

    private string BuildFilePath(string dayNumber) {
        var projectPath = Path.GetDirectoryName(Environment.CurrentDirectory);
        if (string.IsNullOrEmpty(projectPath)) throw new Exception("Could not find directory");

        Directory.CreateDirectory(Path.Combine(projectPath, "data"));

        var dataDirectory = Path.Combine(projectPath, "data");
        var filePath = Path.Combine(dataDirectory, $"day{dayNumber}_puzzle.txt");
        return filePath;
    }

    private async Task WriteInputToFile(string[] content, string dayNumber) {
        var filePath = BuildFilePath(dayNumber);
        await File.WriteAllLinesAsync(filePath, content);
    }

    public int? TryParsePuzzleLine(string puzzleLine) {
        return int.TryParse(puzzleLine, out var line) ? line : null;
    }
}