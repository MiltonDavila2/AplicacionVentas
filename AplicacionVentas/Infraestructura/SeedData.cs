using Microsoft.EntityFrameworkCore;
using AplicacionVentas.Models;


namespace AplicacionVentas.Infraestructura

{
    public class SeedData
    {
        public static void SeedDatabase(ContextoDatos context)
        {
            context.Database.Migrate();

            if(!context.Productos.Any())
            {

                Categoria Agarraderas = new Categoria { nombre = "Agarraderas", Slug = "Agarraderas" };
                Categoria Bisagra = new Categoria { nombre = "Bisagra", Slug = "Bisagra" };
                Categoria Broca = new Categoria { nombre = "broca", Slug = "broca" };

                context.Productos.AddRange(
                    new Producto
                    {
                        Nombre = "Agarradera con Toallero",
                        Slug = "agarradera-con-toallero",
                        Descripcion = "Agarradera con toallero 2 en 1 para puertas de vidrio",
                        Precio = 29.99M,
                        Categoria = Agarraderas,
                        Imagen = "Agarradera_Toallero.jpeg"



                    },
                    new Producto
                    {
                        Nombre = "Agarradera de barra",
                        Slug = "agarradera-de-barra",
                        Descripcion = "Agarradera de Acero Inoxidable Vaciada Tipo Barra",
                        Precio = 0.89M,
                        Categoria = Agarraderas,
                        Imagen = "agarradera_Barra.jpg"

                    },
                    new Producto
                    {
                        Nombre = "Bisagra 8 pulgadas",
                        Slug = "bisagra-8-pulgadas",
                        Descripcion = "Bisagra de 8 Pulgadas para Ventana Proyectable",
                        Precio = 4.49M,
                        Categoria = Bisagra,
                        Imagen = "Bisagra_8pulgadas.jpg"


                    },
                    new Producto
                    {
                        Nombre = "Bisagra para vidrio templado",
                        Slug = "bisagra-para-vidrio-templado",
                        Descripcion = "Apto para puerta de cristal de 8-10 mm",
                        Precio = 24.99M,
                        Categoria = Bisagra,
                        Imagen = "bisagra_vidrio_templado.jpg"
                    },
                    new Producto
                    {
                        Nombre = "Broca para vidrio",
                        Slug = "broca-para-vidrio",
                        Descripcion = "Para vidrio de 16 mm Broca metálica con punta de diamante, Mayor rendimiento y duración",
                        Precio = 7.99M,
                        Categoria = Broca,
                        Imagen = "Broca_vidrio.jpeg"


                    },
                    new Producto
                    {
                        Nombre = "Broca para concreto y ladrillo",
                        Slug = "broca-para-concreto-y-ladrillo",
                        Descripcion = "Broca estándar de metal duro para mampostería.Medidas: 5 mm D x 85 mm L. Para pared, concreto y ladrillo.",
                        Precio = 21.59M,
                        Categoria = Broca,
                        Imagen = "brocaconcretoladrillo.jpg"
                    }

                );

                context.SaveChanges();

            }
        }
    }
}
