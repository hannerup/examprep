using Microsoft.AspNetCore.Mvc;

using MVC.Models;

namespace MVC.Controllers;

public class HomeController : Controller {
    private static int _correctNumber;
    private static int _guessesLeft = 5;

    public ViewResult Index() {
        var model = new GuessModel();
        model.AllowGuesses = true;
        model.AnswersLeft = _guessesLeft;
        return View(model);
    }

    [HttpPost]
    public ViewResult Guess(GuessModel model) {
        model.AnswersLeft = _guessesLeft;
        Console.WriteLine($"{model.AnswersLeft}");
        if (model.Number == _correctNumber) {
            model.AllowGuesses = false;
            model.CorrectAnswer = true;
            Console.WriteLine($"RIGTIGT");
            return View("Correct", model);
        } else {
            if (model.Number > _correctNumber) {
                model.AllowGuesses = true;
                model.Below = true;
                _guessesLeft--;
                Console.WriteLine($"Forkert");
            } else {
                model.AllowGuesses = true;
                model.Above = true;
                _guessesLeft--;
                Console.WriteLine($"Forkert");
            }

        }
        return View("Index", model);
    }

    public static void GenerateNumber() {
        var rand = new Random();
        _correctNumber = rand.Next(1, 10);
    }
}
