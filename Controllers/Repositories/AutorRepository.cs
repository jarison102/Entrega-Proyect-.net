using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using BibliotecaWebb.Models; // Agrega esta línea para importar el espacio de nombres de las clases de modelos
using BibliotecaWebb.Repositories;


namespace BibliotecaWebb.Repositories
{
    public class AutorRepository
    {
        private readonly string _connectionString;

        // Nuevo constructor que acepta la cadena de conexión directamente
        public AutorRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }
 public List<Autor> ObtenerTodos()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sql = "SELECT * FROM Autores";
                var autores = connection.Query<Autor>(sql);

                foreach (var autor in autores)
                {
                    autor.Libros = ObtenerLibrosPorAutor(autor.AutorID).ToList(); // Convertir el IEnumerable a List
                }

                return autores.ToList(); // Convertir el IEnumerable a List antes de devolverlo
            }
        }


        public void AgregarNuevo(string nombre)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    var sql = "INSERT INTO Autores (Nombre) VALUES (@Nombre)";
                    int rowsAffected = connection.Execute(sql, new { Nombre = nombre });

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Autor agregado correctamente");
                    }
                    else
                    {
                        Console.WriteLine("No se pudo agregar el autor");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al agregar el autor: {ex.Message}");
                }

            }
        }

        public void AgregarLibro(string titulo, int autorId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                try
                {
                    var sql = "INSERT INTO Libros (Titulo, AutorID) VALUES (@Titulo, @AutorID)";
                    int rowsAffected = connection.Execute(sql, new { Titulo = titulo, AutorID = autorId });

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine("Libro agregado correctamente");
                    }
                    else
                    {
                        Console.WriteLine("No se pudo agregar el libro");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al agregar el libro: {ex.Message}");
                }
            }
        }

        private IEnumerable<Libro> ObtenerLibrosPorAutor(int autorId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var sql = "SELECT * FROM Libros WHERE AutorID = @AutorID";
                return connection.Query<Libro>(sql, new { AutorID = autorId });
            }
        }
    }
}

