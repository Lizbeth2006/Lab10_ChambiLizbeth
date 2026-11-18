namespace Lab10_ChambiLizbeth.Application.Constants;

public static class TicketStatus
{
    public const string Abierto = "abierto";
    public const string EnProceso = "en_proceso";
    public const string Cerrado = "cerrado";

    public static readonly string[] All = { Abierto, EnProceso, Cerrado };
}