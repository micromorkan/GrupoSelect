namespace GrupoSelect.Domain.Entity
{
    public class Contract
    {
        public int Id { get; set; }
        public int ProposalId { get; set; }
        public string ContractNum { get; set; }
        public string Status { get; set; }
        public string? ReprovedReason { get; set; }
        public string? ReprovedExplain { get; set; }
        public byte[]? ContractConsultancy { get; set; }
        public byte[]? ContractFinancialAdmin { get; set; }
        public byte[]? VideoAgree { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateStatus { get; set; }
        public DateTime? DateAproved { get; set; }
        public int? UserIdAproved { get; set; }
        public virtual Proposal Proposal { get; set; }
    }
}
