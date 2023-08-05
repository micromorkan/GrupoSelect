namespace GrupoSelect.Domain.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public string Representation { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Profile { get; set; }
        public bool Active { get; set; }
        public bool BranchWithoutAdm { get; set; }
    }
}
