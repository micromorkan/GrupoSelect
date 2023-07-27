namespace GrupoSelect.Domain.Entity
{
    public class Credit
    {
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public int TableTypeId { get; set; }
        public int FinancialAdminId { get; set; }
        public int Months { get; set; }
        public string CreditValue { get; set; }
        public string PortionValue { get; set; }
        public string MembershipValue { get; set; }
        public string TotalValue { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }
        public virtual ProductType ProductType { get; set; }
        public virtual TableType TableType { get; set; }
        public virtual FinancialAdmin FinancialAdmin { get; set; }
    }
}
