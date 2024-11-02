using AppCalculadora;
using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Write("Ingrese una expresión en notación infija:(o escriba 'salir' para terminar): ");
            string expresion = Console.ReadLine();

            // Verifica si el usuario desea salir
            if (expresion.ToLower() == "salir")
            {
                Console.WriteLine("Finalizando el programa...");
                break;
            }

            // Instancia de la calculadora con la expresión ingresada
            CCalculadora calculadora = new CCalculadora(expresion);

            try
            {
                // Convertir la expresión a notación posfija
                string notacionPosfija = calculadora.ConvertirAPosFijo();
                Console.WriteLine("Notación posfija: " + notacionPosfija);

                // Evaluar solo si la expresión contiene solo números
                if (calculadora.ContieneSoloNumeros())
                {
                    float resultado = calculadora.Evaluar();
                    Console.WriteLine("El resultado es: " + resultado);
                }
                else
                {
                    Console.WriteLine("No es posible calcular el resultado ya que la expresión contiene variables no numéricas.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error en la evaluación: " + e.Message);
            }

            Console.WriteLine(); // Línea en blanco para separar las operaciones
        }
    }
}
