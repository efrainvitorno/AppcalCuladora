using System;

class Program
{
    static void Main()
    {
        Console.Write("Ingrese una expresión en notación infija: ");
        string expresion = Console.ReadLine();

        CCalculadora calculadora = new CCalculadora(expresion);

        try
        {
            // Convertir la expresión a notación postfija
            string notacionPosfija = calculadora.ConvertirAPosFijo();
            Console.WriteLine("Notación postfija: " + notacionPosfija);

            // Intentar evaluar la expresión solo si contiene solo números
            if (calculadora.ContieneSoloNumeros())
            {
                float resultado = calculadora.Evaluar();
                Console.WriteLine("El resultado es: " + (int)resultado); // Convertir el resultado a entero
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

        Console.WriteLine("Presione cualquier tecla para salir...");
        Console.ReadKey();
    }
}
