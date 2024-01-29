
namespace BibliotecaWebb.Models
{
    public class Autor
    {
        public int AutorID { get; set; }
        public string Nombre { get; set; }

        // Relación uno a muchos con Libros
        public List<Libro> Libros { get; set; }
    }

    public class Libro
    {
        public int ID { get; set; }
        public string Titulo { get; set; }

        // Clave foránea
        public int AutorID { get; set; }
    
        // Propiedad de navegación
        public Autor Autor { get; set; }
    }
}
