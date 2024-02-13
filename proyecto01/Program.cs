using System;
using System.Collections.Generic;

class PagoServicio
{
    public int NumeroPago { get; set; }
    public DateTime Fecha { get; set; }
    public string Hora { get; set; }
    public string Cedula { get; set; }
    public string Nombre { get; set; }
    public string Apellido1 { get; set; }
    public string Apellido2 { get; set; }
    public int NumeroCaja { get; set; }
    public int TipoServicio { get; set; }
    public string NumeroFactura { get; set; }
    public double MontoPagar { get; set; }
    public double MontoComision { get; set; }
    public double MontoDeducido { get; set; }
    public double MontoPagaCliente { get; set; }
    public double Vuelto { get; set; }
}

class Program
{
    static List<PagoServicio> pagos = new List<PagoServicio>();

    static void Main(string[] args)
    {
        int opcion;
        do
        {
            Console.WriteLine("Menu Principal");
            Console.WriteLine("1. Inicializar Vectores");
            Console.WriteLine("2. Realizar Pagos");
            Console.WriteLine("3. Consultar Pagos");
            Console.WriteLine("4. Modificar Pagos");
            Console.WriteLine("5. Eliminar Pagos");
            Console.WriteLine("6. Submenú Reportes");
            Console.WriteLine("7. Salir");
            Console.Write("Seleccione una opción: \n ");
            opcion = LeerEntero();

            switch (opcion)
            {
                case 1:
                    InicializarVectores();
                    break;
                case 2:
                    RealizarPagos();
                    break;
                case 3:
                    ConsultarPagos();
                    break;
                case 4:
                    ModificarPagos();
                    break;
                case 5:
                    EliminarPagos();
                    break;
                case 6:
                    SubmenuReportes();
                    break;
                case 7:
                    Console.WriteLine("Saliendo del programa...");
                    break;
                default:
                    Console.WriteLine("La opción que indicas es inválida. Por favor, seleccione una opción válida.");
                    break;
            }
        } while (opcion != 7);
    }

    static void InicializarVectores()
    {
        Console.WriteLine("---------------------------------------");
        Console.WriteLine("Vectores inicializados correctamente.");
        Console.WriteLine("---------------------------------------\n");
    }

    static void RealizarPagos()
    {
        if (pagos.Count == 10)
        {
            Console.WriteLine("Vectores Llenos\n");
            return;
        }

        PagoServicio nuevoPago = new PagoServicio();

        Console.WriteLine("Ingrese la información que se requiere para realizar el pago:\n");

        Console.Write("Fecha (DD/MM/AAAA): \n");
        nuevoPago.Fecha = LeerFecha();

        Console.Write("Cédula:\n ");
        nuevoPago.Cedula = Console.ReadLine();

        Console.Write("Nombre:\n ");
        nuevoPago.Nombre = Console.ReadLine();

        Console.Write("Apellido1:\n ");
        nuevoPago.Apellido1 = Console.ReadLine();

        Console.Write("Apellido2:\n ");
        nuevoPago.Apellido2 = Console.ReadLine();

        Console.Write("Tipo de Servicio (1=Recibo de Luz, 2=Recibo Teléfono, 3=Recibo de Agua):\n ");
        nuevoPago.TipoServicio = LeerEntero();

        Console.Write("Número de Factura:\n ");
        nuevoPago.NumeroFactura = Console.ReadLine();

        Console.Write("Monto a Pagar:\n ");
        nuevoPago.MontoPagar = LeerDouble();
        
        // Determinar la comisión
        switch (nuevoPago.TipoServicio)
        {
            case 1:
                nuevoPago.MontoComision = nuevoPago.MontoPagar * 0.04;
                break;
            case 2:
                nuevoPago.MontoComision = nuevoPago.MontoPagar * 0.055;
                break;
            case 3:
                nuevoPago.MontoComision = nuevoPago.MontoPagar * 0.065;
                break;
            default:
                break;
        }

        // Calcular el monto deducido
        nuevoPago.MontoDeducido = nuevoPago.MontoPagar - nuevoPago.MontoComision;

        // Solicitar el monto pagado por el cliente
        do
        {
            Console.Write("Monto Pagado por el Cliente:\n");
            nuevoPago.MontoPagaCliente = LeerDouble();

            if (nuevoPago.MontoPagaCliente < nuevoPago.MontoPagar)
            {
                Console.WriteLine("El monto indicado por el cliente es menor al monto necesario para realizar el pago. Por favor intente nuevamente.\n");
            }
        } while (nuevoPago.MontoPagaCliente < nuevoPago.MontoPagar);

        // Calcular el vuelto
        nuevoPago.Vuelto = nuevoPago.MontoPagaCliente - nuevoPago.MontoPagar;

        // Imprimir el vuelto si lo hay
        if (nuevoPago.Vuelto > 0)
        {
            Console.WriteLine($"Vuelto a pagar: {nuevoPago.Vuelto}\n");
        }

        // Asignar número de pago y hora actual
        nuevoPago.NumeroPago = pagos.Count + 1;
        nuevoPago.Hora = DateTime.Now.ToString("HH:mm\n");

        // Asignar número de caja aleatorio
        Random rnd = new Random();
        nuevoPago.NumeroCaja = rnd.Next(1, 4);

        // Agregar el nuevo pago a la lista de pagos
        pagos.Add(nuevoPago);

        Console.WriteLine("------------------------------");
        Console.WriteLine("Pago realizado exitosamente.");
        Console.WriteLine("------------------------------\n");
    }

    static void ConsultarPagos()
    {
        Console.Write("Ingrese el número de pago que desea consultar:\n ");
        int numeroPago = LeerEntero();

        PagoServicio pago = pagos.Find(p => p.NumeroPago == numeroPago);

        if (pago != null)
        {
            Console.WriteLine("Información del Pago:");
            Console.WriteLine($"Número de Pago: {pago.NumeroPago}");
            Console.WriteLine($"Fecha: {pago.Fecha}");
            Console.WriteLine($"Hora: {pago.Hora}");
            Console.WriteLine($"Cédula: {pago.Cedula}");
            Console.WriteLine($"Nombre: {pago.Nombre}");
            Console.WriteLine($"Apellido1: {pago.Apellido1}");
            Console.WriteLine($"Apellido2: {pago.Apellido2}");
            Console.WriteLine($"Número de Caja: {pago.NumeroCaja}");
            Console.WriteLine($"Tipo de Servicio: {pago.TipoServicio}");
            Console.WriteLine($"Número de Factura: {pago.NumeroFactura}");
            Console.WriteLine($"Monto a Pagar: {pago.MontoPagar}");
            Console.WriteLine($"Monto Comisión: {pago.MontoComision}");
            Console.WriteLine($"Monto Deducido: {pago.MontoDeducido}");
            Console.WriteLine($"Monto Pagado por el Cliente: {pago.MontoPagaCliente}");
            Console.WriteLine($"Vuelto: {pago.Vuelto}\n");
        }
        else
        {
            Console.WriteLine("El pago no se encuentra en los registros.\n");
        }
    }

    static void ModificarPagos()
    {

        Console.WriteLine("Ingrese el número de pago que desea modificar: ");
        Console.WriteLine("----------------------------------------------\n");
        int numeroPago = LeerEntero();

        PagoServicio pago = pagos.Find(p => p.NumeroPago == numeroPago);

        if (pago != null)
        {
            Console.WriteLine("Información actual del Pago:");
            Console.WriteLine("-----------------------------\n");
            Console.WriteLine($"Número de Pago: {pago.NumeroPago}");
            Console.WriteLine($"Fecha: {pago.Fecha}");
            Console.WriteLine($"Hora: {pago.Hora}");
            Console.WriteLine($"Cédula: {pago.Cedula}");
            Console.WriteLine($"Nombre: {pago.Nombre}");
            Console.WriteLine($"Apellido1: {pago.Apellido1}");
            Console.WriteLine($"Apellido2: {pago.Apellido2}");
            Console.WriteLine($"Número de Caja: {pago.NumeroCaja}");
            Console.WriteLine($"Tipo de Servicio: {pago.TipoServicio}");
            Console.WriteLine($"Número de Factura: {pago.NumeroFactura}");
            Console.WriteLine($"Monto a Pagar: {pago.MontoPagar}");
            Console.WriteLine($"Monto Comisión: {pago.MontoComision}");
            Console.WriteLine($"Monto Deducido: {pago.MontoDeducido}");
            Console.WriteLine($"Monto Pagado por el Cliente: {pago.MontoPagaCliente}");
            Console.WriteLine($"Vuelto: {pago.Vuelto}\n");

            // Aquí permitimos al usuario modificar los datos del pago
            Console.WriteLine("Ingrese información a modificar del pago:\n");

            Console.Write("Fecha (DD/MM/AAAA): ");
            pago.Fecha = LeerFecha();

            Console.Write("Cédula: ");
            pago.Cedula = Console.ReadLine();

            Console.Write("Nombre: ");
            pago.Nombre = Console.ReadLine();

            Console.Write("Apellido1: ");
            pago.Apellido1 = Console.ReadLine();

            Console.Write("Apellido2: ");
            pago.Apellido2 = Console.ReadLine();

            Console.Write("Tipo de Servicio (1=Recibo de Luz, 2=Recibo Teléfono, 3=Recibo de Agua): ");
            pago.TipoServicio = LeerEntero();

            Console.Write("Número de Factura: ");
            pago.NumeroFactura = Console.ReadLine();

            Console.Write("Monto a Pagar: ");
            pago.MontoPagar = LeerDouble();

            
            // Determinar la comisión nuevamente
            switch (pago.TipoServicio)
            {
                case 1:
                    pago.MontoComision = pago.MontoPagar * 0.04;
                    break;
                case 2:
                    pago.MontoComision = pago.MontoPagar * 0.055;
                    break;
                case 3:
                    pago.MontoComision = pago.MontoPagar * 0.065;
                    break;
                default:
                    break;
            }

            // Calcular el monto deducido nuevamente
            pago.MontoDeducido = pago.MontoPagar - pago.MontoComision;



            Console.WriteLine("-------------------------------");
            Console.WriteLine("Cambios en el pago realizados exitosamente!.");
            Console.WriteLine("-------------------------------\n");
        }
        else
        {
            Console.WriteLine("Pago no se encuentra registrado.\n");
        }
    }

    static void EliminarPagos()
    {
        Console.Write("Ingrese el número de pago a eliminar:\n ");
        int numeroPago = LeerEntero();

        PagoServicio pago = pagos.Find(p => p.NumeroPago == numeroPago);

        if (pago != null)
        {
            Console.WriteLine("Información del Pago a Eliminar:");
            Console.WriteLine($"Número de Pago: {pago.NumeroPago}");
            Console.WriteLine($"Fecha: {pago.Fecha}");
            Console.WriteLine($"Hora: {pago.Hora}");
            Console.WriteLine($"Cédula: {pago.Cedula}");
            Console.WriteLine($"Nombre: {pago.Nombre}");
            Console.WriteLine($"Apellido1: {pago.Apellido1}");
            Console.WriteLine($"Apellido2: {pago.Apellido2}");
            Console.WriteLine($"Número de Caja: {pago.NumeroCaja}");
            Console.WriteLine($"Tipo de Servicio: {pago.TipoServicio}");
            Console.WriteLine($"Número de Factura: {pago.NumeroFactura}");
            Console.WriteLine($"Monto a Pagar: {pago.MontoPagar}");
            Console.WriteLine($"Monto Comisión: {pago.MontoComision}");
            Console.WriteLine($"Monto Deducido: {pago.MontoDeducido}");
            Console.WriteLine($"Monto Pagado por el Cliente: {pago.MontoPagaCliente}");
            Console.WriteLine($"Vuelto: {pago.Vuelto} \n");

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("¿Está seguro de eliminar este pago? (S/N):");
            Console.WriteLine("-------------------------------------------\n");
            string respuesta = Console.ReadLine();

            if (respuesta.ToUpper() == "S")
            {
                pagos.Remove(pago);
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("Transacción eliminada correctamente.");
                Console.WriteLine("-------------------------------------------------\n");
            }
            else
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Error, información no fue eliminada.\n");
                Console.WriteLine("----------------------------------------");
            }
        }
        else
        {
            Console.WriteLine("Pago no se encuentra registrado.\n");
        }
    }

    static void SubmenuReportes()
    {
        int opcionReporte;
        do
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("Submenú Reportes");
            Console.WriteLine("--------------------\n");
            Console.WriteLine("1. Ver todos los Pagos");
            Console.WriteLine("2. Ver Pagos por tipo de Servicio");
            Console.WriteLine("3. Ver Pagos por código de caja");
            Console.WriteLine("4. Ver Dinero Comisionado por servicios");
            Console.WriteLine("5. Regresar Menú Principal");
            Console.Write("Seleccione una opción:\n ");
            opcionReporte = LeerEntero();

            switch (opcionReporte)
            {
                case 1:
                    ReporteTodosLosPagos();
                    break;
                case 2:
                    ReportePagosPorTipoServicio();
                    break;
                case 3:
                    ReportePagosPorCodigoCaja();
                    break;
                case 4:
                    ReporteDineroComisionado();
                    break;
                case 5:
                    Console.WriteLine("-------------------------------------");
                    Console.WriteLine("Regresando al Menú Principal...");
                    Console.WriteLine("-------------------------------------\n");
                    break;
                default:
                    Console.WriteLine("Opción inválida. Por favor, seleccione una opción válida.\n");
                    break;
            }
        } while (opcionReporte != 5);
    }

    static void ReporteTodosLosPagos()
    {
        if (pagos.Count > 0)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Reporte de Todos los Pagos:");
            Console.WriteLine("---------------------------------\n");
            foreach (var pago in pagos)
            {
                Console.WriteLine($"Número de Pago: {pago.NumeroPago}");
                Console.WriteLine($"Fecha: {pago.Fecha}");
                Console.WriteLine($"Hora: {pago.Hora}");
                Console.WriteLine($"Cédula: {pago.Cedula}");
                Console.WriteLine($"Nombre: {pago.Nombre}");
                Console.WriteLine($"Apellido1: {pago.Apellido1}");
                Console.WriteLine($"Apellido2: {pago.Apellido2}");
                Console.WriteLine($"Número de Caja: {pago.NumeroCaja}");
                Console.WriteLine($"Tipo de Servicio: {pago.TipoServicio}");
                Console.WriteLine($"Número de Factura: {pago.NumeroFactura}");
                Console.WriteLine($"Monto a Pagar: {pago.MontoPagar}");
                Console.WriteLine($"Monto Comisión: {pago.MontoComision}");
                Console.WriteLine($"Monto Deducido: {pago.MontoDeducido}");
                Console.WriteLine($"Monto Pagado por el Cliente: {pago.MontoPagaCliente}");
                Console.WriteLine($"Vuelto: {pago.Vuelto} \n");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("No hay pagos registrados.");
            Console.WriteLine("-------------------------------\n");
        }
    }

    static void ReportePagosPorTipoServicio()
    {
        Console.WriteLine("-----------------------------------------------------------");
        Console.WriteLine("Ingrese el tipo de servicio (1=Luz, 2=Teléfono, 3=Agua):");
        Console.WriteLine("-----------------------------------------------------------\n");
        int tipoServicio = LeerEntero();

        List<PagoServicio> pagosPorTipo = pagos.FindAll(p => p.TipoServicio == tipoServicio);

        if (pagosPorTipo.Count > 0)
        {
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine($"Reporte de Pagos para el Tipo de Servicio {tipoServicio}:");
            Console.WriteLine("----------------------------------------------------------------\n");
            foreach (var pago in pagosPorTipo)
            {
                Console.WriteLine($"Número de Pago: {pago.NumeroPago}");
                Console.WriteLine($"Fecha: {pago.Fecha}");
                Console.WriteLine($"Hora: {pago.Hora}");
                Console.WriteLine($"Cédula: {pago.Cedula}");
                Console.WriteLine($"Nombre: {pago.Nombre}");
                Console.WriteLine($"Apellido1: {pago.Apellido1}");
                Console.WriteLine($"Apellido2: {pago.Apellido2}");
                Console.WriteLine($"Número de Caja: {pago.NumeroCaja}");
                Console.WriteLine($"Número de Factura: {pago.NumeroFactura}");
                Console.WriteLine($"Monto a Pagar: {pago.MontoPagar}");
                Console.WriteLine($"Monto Comisión: {pago.MontoComision}");
                Console.WriteLine($"Monto Deducido: {pago.MontoDeducido}");
                Console.WriteLine($"Monto Pagado por el Cliente: {pago.MontoPagaCliente}");
                Console.WriteLine($"Vuelto: {pago.Vuelto}  \n");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine($"No hay transacciones registradas para este tipo de servicio {tipoServicio}.");
            Console.WriteLine("------------------------------------------------------------------------\n");
        }
    }

    static void ReportePagosPorCodigoCaja()
    {
        Console.Write("Ingrese el código de caja:\n ");
        int codigoCaja = LeerEntero();

        List<PagoServicio> pagosPorCaja = pagos.FindAll(p => p.NumeroCaja == codigoCaja);

        if (pagosPorCaja.Count > 0)
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine($"Reporte de Pagos para el Código de Caja {codigoCaja}:");
            Console.WriteLine("------------------------------------------------------------\n");
            foreach (var pago in pagosPorCaja)
            {
                Console.WriteLine($"Número de Pago: {pago.NumeroPago}");
                Console.WriteLine($"Fecha: {pago.Fecha}");
                Console.WriteLine($"Hora: {pago.Hora}");
                Console.WriteLine($"Cédula: {pago.Cedula}");
                Console.WriteLine($"Nombre: {pago.Nombre}");
                Console.WriteLine($"Apellido1: {pago.Apellido1}");
                Console.WriteLine($"Apellido2: {pago.Apellido2}");
                Console.WriteLine($"Tipo de Servicio: {pago.TipoServicio}");
                Console.WriteLine($"Número de Factura: {pago.NumeroFactura}");
                Console.WriteLine($"Monto a Pagar: {pago.MontoPagar}");
                Console.WriteLine($"Monto Comisión: {pago.MontoComision}");
                Console.WriteLine($"Monto Deducido: {pago.MontoDeducido}");
                Console.WriteLine($"Monto Pagado por el Cliente: {pago.MontoPagaCliente}");
                Console.WriteLine($"Vuelto: {pago.Vuelto}\n");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("--------------------------------------------------------------------");
            Console.WriteLine($"No hay pagos registrados para este código de caja {codigoCaja}.");
            Console.WriteLine("--------------------------------------------------------------------\n");
        }
    }

    static void ReporteDineroComisionado()
    {
        Dictionary<int, double> comisionesPorServicio = new Dictionary<int, double>();

        foreach (var pago in pagos)
        {
            if (!comisionesPorServicio.ContainsKey(pago.TipoServicio))
            {
                comisionesPorServicio[pago.TipoServicio] = 0;
            }
            comisionesPorServicio[pago.TipoServicio] += pago.MontoComision;
        }

        Console.WriteLine("Dinero Comisionado por Servicios:");
        foreach (var kvp in comisionesPorServicio)
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine($"Tipo de Servicio: {kvp.Key}, Dinero Comisionado: {kvp.Value}");
            Console.WriteLine("-------------------------------------------------------------------\n");
        }
    }

    static int LeerEntero()
    {
        int numero;
        while (!int.TryParse(Console.ReadLine(), out numero))
        {
            Console.WriteLine("-------------------------------------------------------------------");
            Console.WriteLine("Entrada inválida. Por favor, ingrese un número entero válido.");
            Console.WriteLine("-------------------------------------------------------------------\n");
        }
        return numero;
    }

    static double LeerDouble()
    {
        double numero;
        while (!double.TryParse(Console.ReadLine(), out numero))
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Entrada inválida. Por favor, ingrese un número válido.");
            Console.WriteLine("------------------------------------------------------------\n");
        }
        return numero;
    }

    static DateTime LeerFecha()
    {
        DateTime fecha;
        while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out fecha))
        {
            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine("Entrada inválida. Por favor, ingrese una fecha válida en formato DD/MM/AAAA.");
            Console.WriteLine("----------------------------------------------------------------------------------\n");
        }
        return fecha;
    }
}
