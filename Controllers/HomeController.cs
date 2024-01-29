using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BibliotecaWebb.Models;
using Microsoft.EntityFrameworkCore;
using BibliotecaWebb.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;





namespace BibliotecaWebb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AutorRepository _autorRepository;

    public HomeController(ILogger<HomeController> logger, AutorRepository autorRepository)
    {
        _logger = logger;
        _autorRepository = autorRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public IActionResult AgregarAutor()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AgregarAutor(string nombre)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(nombre))
            {
                _autorRepository.AgregarNuevo(nombre);
                return RedirectToAction("Lista");
            }
            else
            {
                // Manejar el caso cuando el nombre es nulo o vacío
                ModelState.AddModelError("Nombre", "El nombre del autor es requerido.");
                return View();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al agregar autor: {ex.Message}");
            return RedirectToAction("Error");
        }
    }

    public IActionResult AgregarLibro()
    {
        var listaAutores = _autorRepository.ObtenerTodos();
        ViewBag.ListaAutores = new SelectList(listaAutores, "AutorID", "Nombre");
        return View();
    }

[HttpPost]
public IActionResult AgregarLibro(string titulo, int autorId)
{
    try
    {
        _autorRepository.AgregarLibro(titulo, autorId);
        return RedirectToAction("Lista"); // Redirige a la página de lista después de agregar el libro
    }
    catch (Exception ex)
    {
        _logger.LogError($"Error al agregar libro: {ex.Message}");
        // Puedes redirigir a una página de error o manejar de alguna otra manera
        return RedirectToAction("Error");
    }
}

    public IActionResult Lista()
    {
        try
        {
            var listaAutores = _autorRepository.ObtenerTodos();
            return View(listaAutores);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error al obtener la lista de autores: {ex.Message}");
            // Puedes redirigir a una página de error o manejar de alguna otra manera
            return RedirectToAction("Error");
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}