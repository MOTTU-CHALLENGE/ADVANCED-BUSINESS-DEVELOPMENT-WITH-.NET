namespace CM_API_MVC.Models
{
    public enum Frequencia
    {
        UHF, HF, LF
    }

    public enum TipoMoto
    {
        SPORT, POP, E
    }

    public enum StatusMoto
    {
        Ativo = 'A',
        Inativo = 'I',
        Manutencao = 'M'
    }

    public enum StatusRfid
    {
        Ativo = 'A',
        Inativo = 'I'
    }
    public enum StatusWifi
    {
        Ativo = 'A',
        Inativo = 'I'
    }

}
