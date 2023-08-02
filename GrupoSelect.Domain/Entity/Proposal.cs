namespace GrupoSelect.Domain.Entity
{
    public class Proposal
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int UserId { get; set; }
        public string ProductTypeName { get; set; }
        public string TableTypeTax { get; set; }
        public string TableTypeFee { get; set; }
        public string TableTypeRate { get; set; }
        public string FinancialAdminName { get; set; }
        public string CreditValue { get; set; }
        public string CreditPortionValue { get; set; }
        public string CreditMembershipValue { get; set; }
        public string CreditTotalValue { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateChecked { get; set; }
        public int? UserChecked { get; set; }
        public bool Aproved { get; set; }
        public string Status { get; set; }
        public virtual Client Client { get; set; }
        public virtual User User { get; set; }
    }
}
