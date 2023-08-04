namespace GrupoSelect.Domain.Entity
{
    public class ContractHistoric
    {
        public Guid Id { get; set; }
        public int ProposalId { get; set; }
        public string ContractNum { get; set; }
        public string Status { get; set; }
        public string? ReprovedReason { get; set; }
        public string? ReprovedExplain { get; set; }
        public DateTime DateRegister { get; set; }
        public int UserIdRegister { get; set; }
    }
}
