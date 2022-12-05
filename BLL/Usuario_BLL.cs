using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Objetos;
using DAL;
using System.Data;

namespace BLL
{
    public class Usuario_BLL
    {
        public Usuario getlogin(Login login)
        {
            Usuario_DAL dal = new Usuario_DAL();
            return dal.getlogin(login);
        }

        public void RegistrarAcesso(string token)
        {
            Usuario_DAL dal = new Usuario_DAL();
            dal.RegistrarAcesso(token);
        }

        public DataTable monta_menu(int id_usuario)
        {
            return new Usuario_DAL().monta_menu(id_usuario);
        }

        public int usuario_inserir(Usuario user)
        {
            return new Usuario_DAL().usuario_inserir(user);
        }

        public DataTable usuario_consulta(Usuario user)
        {
            return new Usuario_DAL().usuario_consulta(user);
        }
        public DataTable usuario_nivel_consulta(int id_usuario_nivel)
        {
            return new Usuario_DAL().usuario_nivel_consulta(id_usuario_nivel);
        }


    }
}
