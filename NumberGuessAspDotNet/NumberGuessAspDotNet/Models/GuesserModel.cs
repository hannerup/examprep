namespace NumberGuessAspDotNet.Models;

public class GuesserModel
{
    public int? Number { get; set; }
    public bool Under { get; set; }
    public bool Over { get; set; }
    public bool CorrectGuess { get; set; }
}