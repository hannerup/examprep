using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NumberGuessAspDotNet.Models;

namespace NumberGuessAspDotNet.Controllers;

public class HomeController : Controller
{
    private static int _secretNumber;
    private static bool _isInitialized = false;

    public IActionResult Index()
    {
        if (!_isInitialized)
        {
            var random = new Random();
            _secretNumber = random.Next(1, 11);
            _isInitialized = true;
            Console.WriteLine(_secretNumber);
        }

        ViewBag.Message = "Indsæt et tal";
        return View();
    }
    public IActionResult Guess(GuesserModel model)
    {
        if (_isInitialized)
        {
            ViewBag.Message = "The game is not initialized.";
            return View("Index");
        }

        // Determine feedback for the guess
        if (model.Number < _secretNumber)
        {
            ViewBag.Message = "The number is higher.";
        }
        else if (model.Number > _secretNumber)
        {
            ViewBag.Message = "The number is lower.";
        }
        else
        {
            model.CorrectGuess = true;
            return View("Win", model); // Redirect to Win view for correct guess
        }

        return View("Index", model); // Reload Index with feedback
    }

}