using Utils;

var solver = new Solver();
var file = await solver.client.GetPuzzle("2");

var input = file.Select(x => {
    var line = x.Split(" ");
    return new Tuple<string, int>(line[0], int.Parse(line[1]));
}).ToArray();

var day1 = new Submarine();
foreach (var (direction, velocity) in input) {
    day1 = direction switch {
        "up" => day1 with {Y = day1.Y - velocity},
        "down" => day1 with {Y = day1.Y + velocity},
        "forward" => day1 with {X = day1.X + velocity},
        _ => throw new Exception("Unrecognized command")
    };
}

Console.WriteLine($"First answer is: {day1.X * day1.Y}");

var day2 = new Submarine();
foreach (var (direction, velocity) in input) {
    day2 = direction switch {
        "up" => day2 with {Aim = day2.Aim - velocity},
        "down" => day2 with {Aim = day2.Aim + velocity},
        "forward" => day2 with {X = day2.X + velocity, Y = day2.Y + (day2.Aim * velocity)},
        _ => throw new Exception("Unrecognized command")
    };
}

Console.WriteLine($"Second answer is: {day2.X * day2.Y}");

record Submarine {
    public int X { get; set; }
    public int Y { get; set; }
    public int Aim { get; set; }
};