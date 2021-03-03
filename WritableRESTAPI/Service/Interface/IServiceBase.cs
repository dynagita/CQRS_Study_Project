using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WritableRESTAPI.Service.Interface
{
    public interface IServiceBase<ViewModel>
    {
        Task<ViewModel> Insert(ViewModel viewModel);
        Task<ViewModel> Update(int id, ViewModel viewModel);
        Task<ViewModel> Delete(int id);

    }
}
