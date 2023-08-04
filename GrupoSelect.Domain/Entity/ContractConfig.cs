namespace GrupoSelect.Domain.Entity
{
    public class ContractConfig
    {
        public int Id { get; set; }
        public string ContractNum { get; set; }
        public string DayWeekUpdate { get; set; }
        public bool HasWeekUpdate { get; set; }
    }
}
