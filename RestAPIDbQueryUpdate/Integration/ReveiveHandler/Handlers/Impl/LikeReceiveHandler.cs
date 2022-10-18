using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestAPIDbQueryUpdate.Domain;
using RestAPIDbQueryUpdate.Extensions;
using RestAPIDbQueryUpdate.Integration.Business.Interface;
using RestAPIDbQueryUpdate.Integration.Model;
using RestAPIDbQueryUpdate.Integration.ReveiveHandler.Handlers.Interface;
using RestAPIDbQueryUpdate.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace RestAPIDbQueryUpdate.Integration.ReveiveHandler.Handlers.Impl
{
    public class LikeReceiveHandler : ReceiveHandler<Like>, IReceiveHandler
    {
        ILikeBusiness _business;

        public LikeReceiveHandler(ILikeRepository repository, ILogger<Like> logger, ILikeBusiness business) : base(repository, logger)
        {
            _business = business;
        }

        public override async Task UpdateDBAsync(Message message)
        {
            await _business.UpdateLikeAsync(message);
        }
    }
}
