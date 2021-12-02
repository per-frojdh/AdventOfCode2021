using Microsoft.Extensions.Configuration;

namespace Utils;

public class AppBuilder {
    public static AppSecrets Build() {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddUserSecrets<AppBuilder>()
            .Build();

        var config = new AppSecrets();
        configuration.Bind(config);
        return config;
    }
}

public class AppSecrets {
    public string? SessionValue { get; set; }
}

public class Solver {
    public readonly AppSecrets config;
    public InputClient client;

    public Solver() {
        config = AppBuilder.Build();
        client = new InputClient(config);
    }
}