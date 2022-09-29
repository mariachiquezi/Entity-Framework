using EntityFramework_codefirst.Models;

namespace EntityFramework_codefirst.Interfaces
{
    public interface ILoginRepository
    {
        //logar
        public string Logar(string email,string senha);
    }
}
