namespace Main.Entities;

public class Employee
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Position { get; set; }
    public List<Performance> Performances { get; private set; } = new List<Performance>();
    public int AverageScore => Performances.Count == 0 ? 0 : Performances.Sum(p => p.Score) / Performances.Count;
}