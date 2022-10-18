using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WritableRESTAPI.Service.Interface
{
    public interface IServiceBase<ViewModel>
    {
        Task<ViewModel> InsertAsync(ViewModel viewModel);
        Task<ViewModel> UpdateAsync(int id, ViewModel viewModel);
        Task<ViewModel> DeleteAsync(int id);

    }
}
