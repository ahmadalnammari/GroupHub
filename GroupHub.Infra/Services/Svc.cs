using GroupHub.Core.Services;
using System;
using System.Threading.Tasks;

namespace GroupHub.Infra.Services
{
    public class Svc : ISvc
    {

        public readonly GroupHubContext context;

        public Svc(GroupHubContext groupHubContext)
        {
            context = groupHubContext;
        }



        //public T Execute<T>(Func<GroupHubContext, T> func, bool transactional = false)
        //{
        //    if (transactional)
        //    {

        //        if (context.Database.CurrentTransaction == null)
        //        {
        //            context.Database.BeginTransaction();
        //        }

        //        using (var dbContextTransaction = context.Database.CurrentTransaction)
        //        {
        //            var result = func(context);
        //            return result;
        //        }
        //    }
        //    else
        //    {
        //        var result = func(context);
        //        return result;

        //    }
        //}






        //public void Execute(Action<IDbContext> func, bool transactional = false)
        //{
        //    if (transactional)
        //    {
        //        With.Transaction(UnitOfWork, context =>
        //        {
        //            func(context);
        //        });
        //    }
        //    else
        //    {

        //        With.Action(UnitOfWork, context =>
        //        {
        //            func(context);
        //        });
        //    }
        //}



        //public async Task ExecuteAsync(Func<IDbContext, Task> func, bool transactional = false)
        //{
        //    if (transactional)
        //    {
        //        await With.TransactionAsync(UnitOfWork, async context =>
        //        {
        //            await func(context);
        //        });
        //    }
        //    else
        //    {
        //        await With.ActionAsync(UnitOfWork, async context =>
        //        {
        //            await func(context);
        //        });
        //    }
        //}




        //public async Task<T> ExecuteAsync<T>(Func<IDbContext, Task<T>> func, bool transactional = false)
        //{
        //    if (transactional)
        //    {
        //        return await With.TransactionAsync(UnitOfWork, async context =>
        //        {
        //            return await func(context);
        //        });
        //    }
        //    else
        //    {
        //        return await With.ActionAsync(UnitOfWork, async context =>
        //        {
        //            return await func(context);
        //        });
        //    }
        //}




    }

}
