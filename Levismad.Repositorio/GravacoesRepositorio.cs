using System;
using Levismad.Dominios;
using Levismad.Framework.Contratos;
using ServiceStack.OrmLite;

namespace Levismad.Repositorio
{
    public class GravacaoPropostaRepositorio : AbstractRepository
    {
        ResultadoGravacao _result;

        public ResultadoGravacao Gravar()
        {

            _result = new ResultadoGravacao();
            Db = Open();
            using (var transation = Db.OpenTransaction())
            {

                try
                {
                    //stuff

                    transation.Commit();
                    transation.Dispose();
                    Db.Close();

                    return _result;
                }
                catch (Exception)
                {
                    transation.Rollback();
                    Db.Close();
                    transation.Dispose();

                    throw;
                }
            }
        }
    }
}
